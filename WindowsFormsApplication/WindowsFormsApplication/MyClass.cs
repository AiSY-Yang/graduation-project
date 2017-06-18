using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
public partial class VerticalProgressBar : ProgressBar
{
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style |= 0x04;
            return cp;
        }
    }
}
public partial class SmartTextBox : TextBox
{
	uint _maxByteLength = 0;
	public uint MaxByteLength 
	{
		get { return _maxByteLength; }
		set { _maxByteLength = value; }
	}
	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		if (ReadOnly) return;
		if (_maxByteLength == 0) return;
		if (char.IsControl(e.KeyChar)) return; 
		int textByteLength = Encoding.GetEncoding(950).GetByteCount(Text + e.KeyChar.ToString()); //取得原本字符串和新字符串相加后的Byte长度
		int selectTextByteLength = Encoding.GetEncoding(950).GetByteCount(SelectedText); //取得选取字符串的Byte长度, 选取字符串将会被取代
		if (textByteLength - selectTextByteLength > _maxByteLength) e.Handled = true; //相减后长度若大于设定值, 则不送出该字符
	}
}
}
