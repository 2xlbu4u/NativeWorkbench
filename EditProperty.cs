using System;
using System.Windows.Forms;

public partial class EditProperty : Form
{
    public string DisplayLabel
    {
        get{return _propDisplayNameTxt.Text;}
        set { _propDisplayNameTxt.Text = value; }
    }

    public string CodeBehind
    {
        get { return _propCodeBehindTxt.Text; }
        set { _propCodeBehindTxt.Text = value; }
    }
    public EditProperty()
    {
        InitializeComponent();
    }

    private void _clearPropData_Click(object sender, EventArgs e)
    {
        DisplayLabel = "";
        CodeBehind = "";
      
    }
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        const int WM_KEYDOWN = 0x100;
        var keyCode = (Keys)(msg.WParam.ToInt32() & Convert.ToInt32(Keys.KeyCode));
        if (msg.Msg == WM_KEYDOWN)
        {
            if (ModifierKeys == Keys.Control)
            {
                if (keyCode == Keys.A)
                {
                    if (_propCodeBehindTxt.Focused) _propCodeBehindTxt.SelectAll();
                    return true;
                }
            }
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

}

