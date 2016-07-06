using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PasswordLockClient
{
    public partial class IPandPortForm : Form
    {
        public IPandPortForm()
        {
            InitializeComponent();
        }

        public static string session = "";

        private bool bFlag = false;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            try
            {
                session = ThriftClient.instance().connectServer(textBox_ip.Text, int.Parse(textBox_port.Text));
            }
            catch
            {
                MessageBox.Show("无法连接服务器！");
                return;
            }
            
            if ("" == session)
            {
                MessageBox.Show("连接服务器异常");
                return;
            }
            else
            {
                bFlag = true;
                Close();
            }
        }

        private void IPandPortForm_Load(object sender, EventArgs e)
        {

        }

        private void IPandPortForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!bFlag)
            {
                Application.Exit();
            }
        }
    }
}
