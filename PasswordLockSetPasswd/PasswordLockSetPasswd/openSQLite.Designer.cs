namespace PasswordLockSetPasswd
{
    partial class openSQLite
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_findPathOfSqlite = new System.Windows.Forms.Button();
            this.textBox_sqliteFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_findPathOfSqlite
            // 
            this.button_findPathOfSqlite.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_findPathOfSqlite.Location = new System.Drawing.Point(324, 21);
            this.button_findPathOfSqlite.Name = "button_findPathOfSqlite";
            this.button_findPathOfSqlite.Size = new System.Drawing.Size(38, 23);
            this.button_findPathOfSqlite.TabIndex = 7;
            this.button_findPathOfSqlite.Text = "...";
            this.button_findPathOfSqlite.UseVisualStyleBackColor = true;
            this.button_findPathOfSqlite.Click += new System.EventHandler(this.button_findPathOfSqlite_Click);
            // 
            // textBox_sqliteFilePath
            // 
            this.textBox_sqliteFilePath.Location = new System.Drawing.Point(115, 23);
            this.textBox_sqliteFilePath.Name = "textBox_sqliteFilePath";
            this.textBox_sqliteFilePath.Size = new System.Drawing.Size(203, 21);
            this.textBox_sqliteFilePath.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("仿宋", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "数据库路径";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("仿宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(136, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openSQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 126);
            this.Controls.Add(this.button_findPathOfSqlite);
            this.Controls.Add(this.textBox_sqliteFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "openSQLite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打开数据库";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.openSQLite_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.openSQLite_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_findPathOfSqlite;
        private System.Windows.Forms.TextBox textBox_sqliteFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}