namespace Project_DB
{
    partial class Provider_SignUp
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
            this.namebox1 = new System.Windows.Forms.TextBox();
            this.namebox = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.passbox1 = new System.Windows.Forms.TextBox();
            this.mailbox1 = new System.Windows.Forms.TextBox();
            this.passbox = new System.Windows.Forms.Label();
            this.mailbox = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // namebox1
            // 
            this.namebox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namebox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.namebox1.Location = new System.Drawing.Point(123, 90);
            this.namebox1.Name = "namebox1";
            this.namebox1.Size = new System.Drawing.Size(221, 30);
            this.namebox1.TabIndex = 30;
            // 
            // namebox
            // 
            this.namebox.AutoSize = true;
            this.namebox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.namebox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namebox.Location = new System.Drawing.Point(12, 95);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(64, 25);
            this.namebox.TabIndex = 29;
            this.namebox.Text = "Name";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button2.Location = new System.Drawing.Point(183, 304);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 50);
            this.button2.TabIndex = 28;
            this.button2.Text = "Log In Instead";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button1.Location = new System.Drawing.Point(17, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 50);
            this.button1.TabIndex = 27;
            this.button1.Text = "Sign Up";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // passbox1
            // 
            this.passbox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passbox1.Location = new System.Drawing.Point(123, 209);
            this.passbox1.Name = "passbox1";
            this.passbox1.PasswordChar = '*';
            this.passbox1.Size = new System.Drawing.Size(221, 30);
            this.passbox1.TabIndex = 26;
            this.passbox1.UseSystemPasswordChar = true;
            // 
            // mailbox1
            // 
            this.mailbox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailbox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.mailbox1.Location = new System.Drawing.Point(123, 146);
            this.mailbox1.Name = "mailbox1";
            this.mailbox1.Size = new System.Drawing.Size(221, 30);
            this.mailbox1.TabIndex = 25;
            // 
            // passbox
            // 
            this.passbox.AutoSize = true;
            this.passbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.passbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passbox.Location = new System.Drawing.Point(12, 211);
            this.passbox.Name = "passbox";
            this.passbox.Size = new System.Drawing.Size(98, 25);
            this.passbox.TabIndex = 24;
            this.passbox.Text = "Password";
            // 
            // mailbox
            // 
            this.mailbox.AutoSize = true;
            this.mailbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.mailbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailbox.Location = new System.Drawing.Point(13, 149);
            this.mailbox.Name = "mailbox";
            this.mailbox.Size = new System.Drawing.Size(60, 25);
            this.mailbox.TabIndex = 23;
            this.mailbox.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 38);
            this.label1.TabIndex = 22;
            this.label1.Text = "Provider Sign Up";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button3.Location = new System.Drawing.Point(644, 391);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 50);
            this.button3.TabIndex = 31;
            this.button3.Text = "Back";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Provider_SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Project_DB.Properties.Resources.Log_In;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.namebox1);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.passbox1);
            this.Controls.Add(this.mailbox1);
            this.Controls.Add(this.passbox);
            this.Controls.Add(this.mailbox);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "Provider_SignUp";
            this.Text = "Provider_Sign_Up_Page";
            this.Load += new System.EventHandler(this.Provider_SignUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox namebox1;
        private System.Windows.Forms.Label namebox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox passbox1;
        private System.Windows.Forms.TextBox mailbox1;
        private System.Windows.Forms.Label passbox;
        private System.Windows.Forms.Label mailbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
    }
}