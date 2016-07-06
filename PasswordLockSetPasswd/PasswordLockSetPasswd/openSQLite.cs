using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sql;

namespace PasswordLockSetPasswd
{
    public partial class openSQLite : Form
    {
        public openSQLite()
        {
            InitializeComponent();
        }

        private bool bFlagExit = true;

        private string strFileName = "";//文件名

        private void button1_Click(object sender, EventArgs e)
        {
            int iRet = Sql.Sqlite.instance().openSqliteFlie(textBox_sqliteFilePath.Text);
            if (0 != iRet)
            {
                MessageBox.Show("打开数据库失败！");
                return;
            }

            bFlagExit = false;//使得确定窗体时不会直接结束进程

            this.Close();

            return;
        }

        private void button_findPathOfSqlite_Click(object sender, EventArgs e)
        {
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();

            oOpenFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            oOpenFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            oOpenFileDialog.RestoreDirectory = true;
            oOpenFileDialog.Title = "请选择一个本地数据库文件";
            oOpenFileDialog.FilterIndex = 1;
            oOpenFileDialog.CheckPathExists = true;
            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                strFileName = oOpenFileDialog.FileName.ToString();//获取选择文件的路径 
                textBox_sqliteFilePath.Text = strFileName;
            }
        }

        private void openSQLite_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void openSQLite_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bFlagExit)
            {
                Application.Exit();
            }
        }
    }
}
