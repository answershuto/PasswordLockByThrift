using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Security.Cryptography;

using System.Threading;




namespace PasswordLockClient
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Thread oThread = new Thread(new ThreadStart(funcThread));
            oThread.Start();
        }

        /*MD5加密算法*/
        private string MD5passwd(string strPasswd)
        {
            byte[] result = Encoding.Default.GetBytes(strPasswd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        private void funcThread()
        {
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(1000);    
            }

            /*30秒保活一次*/
            ThriftClient.instance().keepAlive(IPandPortForm.session); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPandPortForm oIPandPortForm = new IPandPortForm();
            oIPandPortForm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "9";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox_password.Text = textBox_password.Text + "0";
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            string strPasswd = textBox_password.Text;
            int iLen = strPasswd.Length;
            if (iLen > 0)
            {
                textBox_password.Text = strPasswd.Substring(0, iLen - 1);
            }
        }

        private void button_sure_Click(object sender, EventArgs e)
        {
            /*将密码进行MD5加密，防止在传输过程中被黑客拦截*/
            string strPasswdMD5 = MD5passwd(textBox_password.Text);

            int iLockType = 0;
            if (true == radioButton1.Checked)
            {
                iLockType = 1;    
            }
            else if (true == radioButton2.Checked)
            {
                iLockType = 2;
            }
            else if (true == radioButton3.Checked)
            {
                iLockType = 3;
            }
            else if (true == radioButton4.Checked)
            {
                iLockType = 4;
            }
            else if (true == radioButton5.Checked)
            {
                iLockType = 5;
            }
            else if (true == radioButton6.Checked)
            {
                iLockType = 6;
            }
            else if (true == radioButton7.Checked)
            {
                iLockType = 7;
            }
            else if (true == radioButton8.Checked)
            {
                iLockType = 8;
            }

            if (0 == iLockType)
            {
                MessageBox.Show("请选择锁类型");
                return;
            }

            /*输入密码*/
            ThriftClient.instance().inputPasswd(IPandPortForm.session, iLockType, strPasswdMD5);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*退出时调用断开连接的接口*/
            ThriftClient.instance().disconnectServer(IPandPortForm.session); 
        }

        private void button_modifyPasswd_Click(object sender, EventArgs e)
        {
            modifyPasswdForm omodifyPasswdForm = new modifyPasswdForm();
            omodifyPasswdForm.ShowDialog();
        }


      

    }
}
