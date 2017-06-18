namespace WindowsFormsApplication
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Timer timerTimeCalibration;
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxCom = new System.Windows.Forms.ComboBox();
			this.start = new System.Windows.Forms.Button();
			this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
			this.buttonOddEvenBit = new System.Windows.Forms.Button();
			this.buttonStopBit = new System.Windows.Forms.Button();
			this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
			this.textReceived = new System.Windows.Forms.TextBox();
			this.trackBarMax = new System.Windows.Forms.TrackBar();
			this.freshen = new System.Windows.Forms.Button();
			this.labelTrackBarMax = new System.Windows.Forms.Label();
			this.labelProgressBarValue = new System.Windows.Forms.Label();
			this.COM = new System.IO.Ports.SerialPort(this.components);
			this.trackBarMin = new System.Windows.Forms.TrackBar();
			this.labelTrackBarMin = new System.Windows.Forms.Label();
			this.panelpicture = new System.Windows.Forms.Panel();
			this.Labelsend = new System.Windows.Forms.Label();
			this.save = new System.Windows.Forms.Button();
			this.timerLink = new System.Windows.Forms.Timer(this.components);
			this.progressBar = new WindowsFormsApplication.VerticalProgressBar();
			this.textSend = new WindowsFormsApplication.SmartTextBox();
			timerTimeCalibration = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.trackBarMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarMin)).BeginInit();
			this.SuspendLayout();
			// 
			// timerTimeCalibration
			// 
			timerTimeCalibration.Interval = 7200000;
			timerTimeCalibration.Tick += new System.EventHandler(this.timerTimeCalibration_Tick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(19, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "串口号";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(19, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "波特率";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("宋体", 12F);
			this.label3.Location = new System.Drawing.Point(19, 86);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "数据位";
			// 
			// comboBoxCom
			// 
			this.comboBoxCom.Cursor = System.Windows.Forms.Cursors.Default;
			this.comboBoxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCom.FormattingEnabled = true;
			this.comboBoxCom.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.comboBoxCom.Location = new System.Drawing.Point(81, 13);
			this.comboBoxCom.Name = "comboBoxCom";
			this.comboBoxCom.Size = new System.Drawing.Size(121, 20);
			this.comboBoxCom.TabIndex = 1;
			// 
			// start
			// 
			this.start.Enabled = false;
			this.start.Location = new System.Drawing.Point(69, 150);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(87, 33);
			this.start.TabIndex = 6;
			this.start.Tag = "";
			this.start.Text = "Start";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.start_Click);
			// 
			// comboBoxBaudRate
			// 
			this.comboBoxBaudRate.Cursor = System.Windows.Forms.Cursors.Default;
			this.comboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBaudRate.FormattingEnabled = true;
			this.comboBoxBaudRate.ImeMode = System.Windows.Forms.ImeMode.On;
			this.comboBoxBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
			this.comboBoxBaudRate.Location = new System.Drawing.Point(81, 48);
			this.comboBoxBaudRate.MaxLength = 10;
			this.comboBoxBaudRate.Name = "comboBoxBaudRate";
			this.comboBoxBaudRate.Size = new System.Drawing.Size(121, 20);
			this.comboBoxBaudRate.TabIndex = 2;
			// 
			// buttonOddEvenBit
			// 
			this.buttonOddEvenBit.Location = new System.Drawing.Point(22, 121);
			this.buttonOddEvenBit.Name = "buttonOddEvenBit";
			this.buttonOddEvenBit.Size = new System.Drawing.Size(75, 23);
			this.buttonOddEvenBit.TabIndex = 4;
			this.buttonOddEvenBit.Tag = "false";
			this.buttonOddEvenBit.Text = "奇偶校验位";
			this.buttonOddEvenBit.UseVisualStyleBackColor = true;
			this.buttonOddEvenBit.Click += new System.EventHandler(this.buttonOddEvenBit_Click);
			// 
			// buttonStopBit
			// 
			this.buttonStopBit.BackColor = System.Drawing.Color.Lime;
			this.buttonStopBit.Location = new System.Drawing.Point(127, 121);
			this.buttonStopBit.Name = "buttonStopBit";
			this.buttonStopBit.Size = new System.Drawing.Size(75, 23);
			this.buttonStopBit.TabIndex = 5;
			this.buttonStopBit.Tag = "true";
			this.buttonStopBit.Text = "停止位";
			this.buttonStopBit.UseVisualStyleBackColor = false;
			this.buttonStopBit.Click += new System.EventHandler(this.buttonStopBit_Click);
			// 
			// comboBoxDataBits
			// 
			this.comboBoxDataBits.Cursor = System.Windows.Forms.Cursors.Default;
			this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDataBits.FormattingEnabled = true;
			this.comboBoxDataBits.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.comboBoxDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
			this.comboBoxDataBits.Location = new System.Drawing.Point(81, 82);
			this.comboBoxDataBits.MaxLength = 1;
			this.comboBoxDataBits.Name = "comboBoxDataBits";
			this.comboBoxDataBits.Size = new System.Drawing.Size(121, 20);
			this.comboBoxDataBits.TabIndex = 3;
			// 
			// textReceived
			// 
			this.textReceived.AcceptsReturn = true;
			this.textReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textReceived.Location = new System.Drawing.Point(22, 189);
			this.textReceived.MaxLength = 0;
			this.textReceived.Multiline = true;
			this.textReceived.Name = "textReceived";
			this.textReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textReceived.Size = new System.Drawing.Size(180, 128);
			this.textReceived.TabIndex = 0;
			this.textReceived.TabStop = false;
			this.textReceived.WordWrap = false;
			// 
			// trackBarMax
			// 
			this.trackBarMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarMax.Location = new System.Drawing.Point(371, 38);
			this.trackBarMax.Maximum = 100;
			this.trackBarMax.Name = "trackBarMax";
			this.trackBarMax.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBarMax.Size = new System.Drawing.Size(45, 300);
			this.trackBarMax.TabIndex = 8;
			this.trackBarMax.TickFrequency = 5;
			this.trackBarMax.Value = 100;
			this.trackBarMax.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// freshen
			// 
			this.freshen.Location = new System.Drawing.Point(234, 60);
			this.freshen.Name = "freshen";
			this.freshen.Size = new System.Drawing.Size(104, 26);
			this.freshen.TabIndex = 9;
			this.freshen.Text = "刷新端口";
			this.freshen.UseVisualStyleBackColor = true;
			this.freshen.Click += new System.EventHandler(this.Form1_Load);
			// 
			// labelTrackBarMax
			// 
			this.labelTrackBarMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTrackBarMax.Location = new System.Drawing.Point(371, 19);
			this.labelTrackBarMax.Name = "labelTrackBarMax";
			this.labelTrackBarMax.Size = new System.Drawing.Size(45, 16);
			this.labelTrackBarMax.TabIndex = 11;
			this.labelTrackBarMax.Text = "100";
			this.labelTrackBarMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelProgressBarValue
			// 
			this.labelProgressBarValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelProgressBarValue.Location = new System.Drawing.Point(420, 16);
			this.labelProgressBarValue.Name = "labelProgressBarValue";
			this.labelProgressBarValue.Size = new System.Drawing.Size(100, 16);
			this.labelProgressBarValue.TabIndex = 12;
			this.labelProgressBarValue.Text = "0";
			this.labelProgressBarValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// COM
			// 
			this.COM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.COM_DataReceived);
			// 
			// trackBarMin
			// 
			this.trackBarMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarMin.Location = new System.Drawing.Point(527, 38);
			this.trackBarMin.Maximum = 100;
			this.trackBarMin.Name = "trackBarMin";
			this.trackBarMin.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBarMin.Size = new System.Drawing.Size(45, 300);
			this.trackBarMin.TabIndex = 8;
			this.trackBarMin.TickFrequency = 5;
			this.trackBarMin.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBarMin.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// labelTrackBarMin
			// 
			this.labelTrackBarMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTrackBarMin.Location = new System.Drawing.Point(525, 17);
			this.labelTrackBarMin.Name = "labelTrackBarMin";
			this.labelTrackBarMin.Size = new System.Drawing.Size(45, 16);
			this.labelTrackBarMin.TabIndex = 11;
			this.labelTrackBarMin.Text = "0";
			this.labelTrackBarMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelpicture
			// 
			this.panelpicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelpicture.BackColor = System.Drawing.Color.Transparent;
			this.panelpicture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.panelpicture.Location = new System.Drawing.Point(0, 360);
			this.panelpicture.Margin = new System.Windows.Forms.Padding(0);
			this.panelpicture.Name = "panelpicture";
			this.panelpicture.Size = new System.Drawing.Size(600, 100);
			this.panelpicture.TabIndex = 15;
			// 
			// Labelsend
			// 
			this.Labelsend.AutoSize = true;
			this.Labelsend.BackColor = System.Drawing.Color.White;
			this.Labelsend.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Labelsend.Font = new System.Drawing.Font("宋体", 20F);
			this.Labelsend.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.Labelsend.Location = new System.Drawing.Point(27, 322);
			this.Labelsend.Name = "Labelsend";
			this.Labelsend.Size = new System.Drawing.Size(228, 27);
			this.Labelsend.TabIndex = 0;
			this.Labelsend.Text = "输入并按回车发送";
			this.Labelsend.Click += new System.EventHandler(this.Lablesend_Click);
			// 
			// save
			// 
			this.save.Font = new System.Drawing.Font("宋体", 15F);
			this.save.Location = new System.Drawing.Point(234, 229);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(104, 31);
			this.save.TabIndex = 17;
			this.save.Text = "save";
			this.save.UseVisualStyleBackColor = true;
			this.save.Click += new System.EventHandler(this.save_Click);
			// 
			// timerLink
			// 
			this.timerLink.Enabled = true;
			this.timerLink.Interval = 2000;
			this.timerLink.Tick += new System.EventHandler(this.timerLink_Tick);
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(420, 38);
			this.progressBar.Margin = new System.Windows.Forms.Padding(1);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(100, 300);
			this.progressBar.TabIndex = 0;
			this.progressBar.TabStop = false;
			this.progressBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBar_MouseDown);
			this.progressBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.progressBar_MouseMove);
			// 
			// textSend
			// 
			this.textSend.AcceptsReturn = true;
			this.textSend.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.textSend.Font = new System.Drawing.Font("宋体", 20F);
			this.textSend.Location = new System.Drawing.Point(22, 319);
			this.textSend.MaxByteLength = ((uint)(20u));
			this.textSend.Name = "textSend";
			this.textSend.Size = new System.Drawing.Size(316, 38);
			this.textSend.TabIndex = 16;
			this.textSend.WordWrap = false;
			this.textSend.TextChanged += new System.EventHandler(this.textSend_TextChanged);
			this.textSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSend_KeyDown);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 461);
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
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonStopBit);
			this.Controls.Add(this.buttonOddEvenBit);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboBoxDataBits);
			this.Controls.Add(this.comboBoxBaudRate);
			this.Controls.Add(this.start);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxCom);
			this.Controls.Add(this.Labelsend);
			this.Controls.Add(this.textSend);
			this.DoubleBuffered = true;
			this.HelpButton = true;
			this.Name = "Form1";
			this.Tag = "";
			this.Text = "液位监测系统上位机";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackBarMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarMin)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCom;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Button buttonOddEvenBit;
        private System.Windows.Forms.Button buttonStopBit;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.Button freshen;
        private System.Windows.Forms.Label labelTrackBarMax;
        private System.Windows.Forms.Label labelProgressBarValue;
        private System.IO.Ports.SerialPort COM;
		private System.Windows.Forms.Label labelTrackBarMin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textReceived;
		private SmartTextBox textSend;
		private System.Windows.Forms.Label Labelsend;
		private System.Windows.Forms.Button save;
		private VerticalProgressBar progressBar;
		private System.Windows.Forms.Timer timerLink;
		private System.Windows.Forms.TrackBar trackBarMax;
		private System.Windows.Forms.TrackBar trackBarMin;
		private System.Windows.Forms.Panel panelpicture;
	}
}

