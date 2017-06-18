using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;
using static System.Windows.Forms.DataFormats;

namespace WindowsFormsApplication
{

	public partial class Form1 : Form
	{
		/// <summary>
		/// 初始化
		/// </summary>
		public Form1()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 窗口载入
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
			string[] comlist = SerialPort.GetPortNames();
			comboBoxCom.Items.Clear();
			if (comlist.Length == 0)
			{
				MessageBox.Show("无串口设备");
				//freshen.Visible = true;
				if (start.Text == "Start")
				{
					start.Text = "No Link";
					start.Enabled = false;
					COM.Close();
				}
			}
			else
			{
				//freshen.Visible = false;
				foreach (var s in comlist)
				{
					comboBoxCom.Items.Add(s);
				}
				//comboBoxCom选定条目
				comboBoxCom.SelectedIndex = 0;
				//comboBoxBaudRate  1.9600 3.38400 4.115200
				comboBoxBaudRate.SelectedIndex = 4;
				//comboBoxWordLength
				comboBoxDataBits.SelectedIndex = 3;
				//开始按钮使能
				start.Enabled = true;
				if (start.Text == "No Link")
					start.Text = "Start";
				//恢复上次的阈值
				trackBarMax.Value = Properties.Settings.Default.MaxValue;
				trackBarMin.Value = Properties.Settings.Default.MinValue;
				labelTrackBarMax.Text = trackBarMax.Value.ToString();
				labelTrackBarMin.Text = trackBarMin.Value.ToString();
			}
		}
		/// <summary>
		/// 奇偶控制位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOddEvenBit_Click(object sender, EventArgs e)
		{
			if (buttonOddEvenBit.Tag.ToString() == "true")
			{
				buttonOddEvenBit.BackColor = SystemColors.Control;
				COM.Parity = Parity.None;
				buttonOddEvenBit.Tag = "false";
			}
			else
			{
				buttonOddEvenBit.BackColor = Color.Lime;
				COM.Parity = Parity.Odd;
				buttonOddEvenBit.Tag = "true";
			}
		}
		/// <summary>
		/// 停止位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStopBit_Click(object sender, EventArgs e)
		{
			if (buttonStopBit.Tag.ToString() == "true")
			{
				buttonStopBit.BackColor = SystemColors.Control;
				COM.StopBits = StopBits.None;
				buttonStopBit.Tag = "false";
			}
			else
			{
				buttonStopBit.BackColor = Color.Lime;
				COM.StopBits = StopBits.One;
				buttonStopBit.Tag = "true";
			}
		}
		/// <summary>
		/// 开始按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void start_Click(object sender, EventArgs e)
		{
			if (start.Text == "Start")
			{
				try
				{
					COM.DataBits = Convert.ToInt32(comboBoxDataBits.SelectedItem.ToString());
					COM.BaudRate = Convert.ToInt32(comboBoxBaudRate.SelectedItem);
					COM.PortName = comboBoxCom.SelectedItem.ToString();
					COM.Open();
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show(COM.PortName + "打开失败，请检查连接。");
					freshen.Visible = true;
					return;
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("访问被拒绝，是否还有其他端口监听软件？");
					return;
				}
				start.Text = "Stop";
				sendTime();
			}
			else
			{
				COM.Close();
				start.Text = "Start";
				progressBar.Value = 0;
				Warring = false;
				labelProgressBarValue.Text = "0";
			}
		}
		/// <summary>
		/// 协议处理
		/// </summary>
		char[] trimChar = { 'H', ':' };
		/// <summary>
		/// 趋势图字段
		/// </summary>
		Point pointStart, pointValue, pointEnd;
		/// <summary>
		/// 串口接收
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void COM_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			Control.CheckForIllegalCrossThreadCalls = false;//跨线程操作控件
			if (COM.IsOpen)
			{
				string[] str = new string[5];
				try
				{
					str[0] = COM.ReadLine();
				}
				catch (System.IO.IOException)
				{
					textReceived.AppendText("IOerror!\r\n");
				}
				catch (Exception)
				{
					MessageBox.Show(e.ToString());
				}
				if (str[0] == null || str[0].Length == 0)
				{
					return;
				}
				if (str[0] == "Boot")
				{
					COM.WriteLine("Link");
					sendTime();
				}
				if (str[0] == "Time")
				{
					timerLink.Interval = 2000;
					sendTime();
				}
				if (str[0] == "Alarm")
				{
					timerLink.Interval = 2000;
					COM.WriteLine("");
					COM.WriteLine("Aon");
				}
				if (str[0] == "Lift alarm")
				{
					timerLink.Interval = 2000;
				}
				if (str[0][0] == 'H')
				{
					textReceived.AppendText(str[0] + '\r' + '\n');
					str = str[0].Split(',');
					str[0] = str[0].TrimStart('H');
					try
					{
						progressBar.Value = int.Parse(str[0]);
						labelProgressBarValue.Text = progressBar.Value.ToString();
					}
					catch (System.FormatException)
					{
						textReceived.AppendText("Format Error!\r\n");
					}
					catch (Exception)
					{
						MessageBox.Show(e.ToString());
					}
					if (progressBar.Value > trackBarMax.Value || progressBar.Value < trackBarMin.Value)
					{
						Warring = true;
					}
					else
					{
						Warring = false;
					}
					//以下为panel绘图部分
					Pen pen = new Pen(Color.Red, 2);
					Pen clear = new Pen(Color.Blue, 3);
					Graphics g = panelpicture.CreateGraphics();
					pointValue.Y = 100 - progressBar.Value;
					pointStart.Y = 100;
					g.DrawLine(Pens.White, pointStart, pointEnd);
					g.DrawLine(clear, pointStart, pointValue);
					g.DrawLine(pen, pointStart, pointValue);
					if (pointStart.X < panelpicture.Width)
					{
						pointStart.X++;
						pointValue.X++;
						pointEnd.X++;
					}
					else
					{
						pointStart.X = 0;
						pointValue.X = 0;
						pointEnd.X = 0;
					}

				}
			}
		}
		/// <summary>
		/// 窗口关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Controls.Clear();
			if (start.Text == "Start")
			{
				COM.Close();
			}
		}
		/// <summary>
		/// 调节阈值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trackBar_Scroll(object sender, EventArgs e)
		{
			TrackBar T = (TrackBar)sender;
			if (T.Value < trackBarMin.Value)
			{
				trackBarMin.Value = T.Value;
			}
			if (trackBarMax.Value < T.Value)
			{
				trackBarMax.Value = T.Value;
			}
			labelTrackBarMax.Text = trackBarMax.Value.ToString();
			labelTrackBarMin.Text = trackBarMin.Value.ToString();
			if (start.Text == "Stop")
			{
				if (progressBar.Value > trackBarMax.Value || progressBar.Value < trackBarMin.Value)
				{
					Warring = true;
				}
				else
				{
					Warring = false;
				}
			}

			Properties.Settings.Default.MaxValue = trackBarMax.Value;
			Properties.Settings.Default.MinValue = trackBarMin.Value;
			Properties.Settings.Default.Save();
		}
		/// <summary>
		/// 发送文字到下位机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textSend_KeyDown(object sender, KeyEventArgs e)
		{
			if (textSend.Text == null)
			{
				return;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				if (!COM.IsOpen)
				{
					MessageBox.Show("未连接串口");
					return;
				}
				Bitmap bmp = new Bitmap(320, 24);
				Graphics g = Graphics.FromImage(bmp);
				g.Clear(Color.White);
				g.DrawString(textSend.Text, textSend.Font, Brushes.Black, 0, 0);
				COM.WriteLine("\r\n");
				COM.WriteLine("Pclear");
				COM.WriteLine("\r\n");
				for (int y = 0; y < 24; y++)
				{
					COM.WriteLine("Py" + ((char)y).ToString());
					for (int x = 0; x < 320; x++)
					{
						if (bmp.GetPixel(x, y).GetBrightness() < 0.8f)
							COM.WriteLine("P1");
						else
							COM.WriteLine("P0");
					}
				}
				COM.WriteLine("Pover");
			}
		}

		private bool warring = false;
		/// <summary>
		/// Warring属性
		/// </summary>
		public bool Warring
		{
			get
			{
				return warring;
			}
			set
			{
				if (COM.IsOpen)
				{
					if (value != warring)
					{
						if (value == true)
						{
							COM.WriteLine("Aon");
						}
						else
						{
							COM.WriteLine("Aoff");
						}
						warring = value;
						warringChange();
					}
					if (warring == true)
					{
						COM.WriteLine("Aon");
					}
				}
			}
		}
		/// <summary>
		/// 报警函数 被属性所调用
		/// </summary>
		private void warringChange()
		{
			SoundPlayer player = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("warring"));
			if (Warring == true)
			{
				player.PlayLooping();
			}
			else
			{
				player.Stop();
			}
		}
		/// <summary>
		/// 悬浮窗控制字段
		/// </summary>
		private Point mouseOffset;
		private Point lastCilck;
		/// <summary>
		/// 悬浮窗控制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void progressBar_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.Name == "Form2")
			{
				mouseOffset = new Point(-e.X, -e.Y);
			}
			if (e.Location == lastCilck)
			{
				if (this.Name == "Form2")
				{
					showForm1();
				}
				else
				{
					hideForm1();
				}
			}
			else
			{
				lastCilck = e.Location;
			}
		}
		/// <summary>
		/// 悬浮窗控制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void progressBar_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.Name == "Form2" && e.Button == MouseButtons.Left)
			{
				if (MousePosition != mouseOffset)
				{
					Point mousePos = Control.MousePosition;
					mousePos.Offset(mouseOffset.X, mouseOffset.Y);
					this.Location = mousePos;
					this.PerformLayout();
				}
			}
		}
		/// <summary>
		/// 文本框水印
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textSend_TextChanged(object sender, EventArgs e)
		{
			if (textSend.TextLength == 0)
			{
				Labelsend.Visible = true;
			}
			else
			{
				Labelsend.Visible = false;
			}
		}
		/// <summary>
		/// 文本框水印
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Lablesend_Click(object sender, EventArgs e)
		{
			textSend.Focus();
		}
		/// <summary>
		/// 保存接收框内的数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void save_Click(object sender, EventArgs e)
		{
			Control.CheckForIllegalCrossThreadCalls = false;//跨线程操作控件
			FileStream f;
			f = File.OpenWrite("rec.txt");
			String s = '[' + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ']';
			s += "\r\n\r\n";
			s += textReceived.Text;
			textReceived.Clear();
			f.Position = f.Length;
			f.Write(Encoding.Default.GetBytes(s), 0, s.Length);
			f.Close();
		}
		/// <summary>
		/// 发送当前时间
		/// </summary>
		private void sendTime()
		{
			//空行清除中断计数变量，防止数据出现异常。
			COM.WriteLine("");
			DateTime time = DateTime.Now;
			time = time.AddSeconds(2);//MCU接收加转换的时间，随芯片与算法而定。
			COM.WriteLine('T' + time.ToString("yyyyMMdd HHmmss") + '\n');
		}
		/// <summary>
		/// Link信号发生器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerLink_Tick(object sender, EventArgs e)
		{
			if (COM.IsOpen)
			{
				COM.WriteLine("Link");
			}
		}
		/// <summary>
		/// 时钟校准定时器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timerTimeCalibration_Tick(object sender, EventArgs e)
		{
			if (COM.IsOpen)
			{
				sendTime();
			}
		}

		/// <summary>
		/// 显示悬浮窗
		/// </summary>
		private void hideForm1()
		{
			//隐藏所有控件
			this.Controls.Clear();
			//但是显示progressBar
			this.Controls.Add(this.progressBar);
			// Form
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(100, 300);
			this.Controls.Add(this.progressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form2";
			this.ResumeLayout(false);
			this.TopMost = true;
			//progressBar属性更改
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
| System.Windows.Forms.AnchorStyles.Left)
| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(0, 0);
			this.progressBar.Margin = new System.Windows.Forms.Padding(0);
			//this.progressBar.Size = new System.Drawing.Size(100, 300);
		}
		/// <summary>
		/// 恢复窗口
		/// </summary>
		private void showForm1()
		{
			this.Controls.Clear();
			//progressBar恢复
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(420, 38);
			this.progressBar.Margin = new System.Windows.Forms.Padding(1);
			this.progressBar.Size = new System.Drawing.Size(100, 300);
			//Form恢复
			this.Name = "Form1";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 461);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.TopMost = false;
			//显示其他组件
			this.Controls.Add(this.save);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.panelpicture);
			this.Controls.Add(this.labelProgressBarValue);
			this.Controls.Add(this.labelTrackBarMin);
			this.Controls.Add(this.labelTrackBarMax);
			this.Controls.Add(this.freshen);
			this.Controls.Add(this.trackBarMin);
			this.Controls.Add(this.trackBarMax);
			this.Controls.Add(this.textReceived);
			this.Controls.Add(label3);
			this.Controls.Add(this.buttonStopBit);
			this.Controls.Add(this.buttonOddEvenBit);
			this.Controls.Add(label2);
			this.Controls.Add(this.comboBoxDataBits);
			this.Controls.Add(this.comboBoxBaudRate);
			this.Controls.Add(this.start);
			this.Controls.Add(label1);
			this.Controls.Add(this.comboBoxCom);
			this.Controls.Add(this.Labelsend);
			this.Controls.Add(this.textSend);
			this.PerformLayout();
			//panel图从头开始绘画
			pointStart.X = 0;
			pointValue.X = 0;
			pointEnd.X = 0;
		}
	}
}
