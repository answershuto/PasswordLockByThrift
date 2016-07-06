namespace PasswordLockSetPasswd
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_addPasswd = new System.Windows.Forms.Button();
            this.textBox_passwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_addPasswd
            // 
            this.button_addPasswd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_addPasswd.Location = new System.Drawing.Point(83, 66);
            this.button_addPasswd.Name = "button_addPasswd";
            this.button_addPasswd.Size = new System.Drawing.Size(120, 31);
            this.button_addPasswd.TabIndex = 0;
            this.button_addPasswd.Text = "添加密码";
            this.button_addPasswd.UseVisualStyleBackColor = true;
            this.button_addPasswd.Click += new System.EventHandler(this.button_addPasswd_Click);
            // 
            // textBox_passwd
            // 
            this.textBox_passwd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_passwd.Location = new System.Drawing.Point(115, 24);
            this.textBox_passwd.Name = "textBox_passwd";
            this.textBox_passwd.PasswordChar = '*';
            this.textBox_passwd.Size = new System.Drawing.Size(162, 23);
            this.textBox_passwd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "请输入新密码";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_passwd);
            this.Controls.Add(this.button_addPasswd);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_addPasswd;
        private System.Windows.Forms.TextBox textBox_passwd;
        private System.Windows.Forms.Label label1;
    }
}

