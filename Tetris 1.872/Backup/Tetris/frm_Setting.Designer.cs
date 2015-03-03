namespace Tetris
{
    partial class frm_Setting
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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_drop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pause = new System.Windows.Forms.TextBox();
            this.txt_movedown = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_rotate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_right = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_moveleft = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(8, 9);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "아이템블럭";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(296, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "적용하고 닫기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "포즈";
            // 
            // txt_drop
            // 
            this.txt_drop.Location = new System.Drawing.Point(204, 31);
            this.txt_drop.Name = "txt_drop";
            this.txt_drop.Size = new System.Drawing.Size(100, 21);
            this.txt_drop.TabIndex = 9;
            this.txt_drop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_drop_KeyDown_1);
            this.txt_drop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_drop_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "추락";
            // 
            // txt_pause
            // 
            this.txt_pause.Location = new System.Drawing.Point(50, 31);
            this.txt_pause.Name = "txt_pause";
            this.txt_pause.Size = new System.Drawing.Size(100, 21);
            this.txt_pause.TabIndex = 7;
            this.txt_pause.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pause_KeyDown_1);
            this.txt_pause.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_pause_KeyUp);
            // 
            // txt_movedown
            // 
            this.txt_movedown.Location = new System.Drawing.Point(204, 58);
            this.txt_movedown.Name = "txt_movedown";
            this.txt_movedown.Size = new System.Drawing.Size(100, 21);
            this.txt_movedown.TabIndex = 13;
            this.txt_movedown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_movedown_KeyDown_1);
            this.txt_movedown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_movedown_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "아래로";
            // 
            // txt_rotate
            // 
            this.txt_rotate.Location = new System.Drawing.Point(50, 58);
            this.txt_rotate.Name = "txt_rotate";
            this.txt_rotate.Size = new System.Drawing.Size(100, 21);
            this.txt_rotate.TabIndex = 11;
            this.txt_rotate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_rotate_KeyDown_1);
            this.txt_rotate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_rotate_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "회전";
            // 
            // txt_right
            // 
            this.txt_right.Location = new System.Drawing.Point(204, 85);
            this.txt_right.Name = "txt_right";
            this.txt_right.Size = new System.Drawing.Size(100, 21);
            this.txt_right.TabIndex = 17;
            this.txt_right.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_right_KeyDown_1);
            this.txt_right.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_right_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "오른쪽";
            // 
            // txt_moveleft
            // 
            this.txt_moveleft.Location = new System.Drawing.Point(50, 85);
            this.txt_moveleft.Name = "txt_moveleft";
            this.txt_moveleft.Size = new System.Drawing.Size(100, 21);
            this.txt_moveleft.TabIndex = 15;
            this.txt_moveleft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_moveleft_KeyDown_1);
            this.txt_moveleft.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_moveleft_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "왼쪽";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(296, 21);
            this.button2.TabIndex = 18;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(98, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "배경음악";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(176, 9);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(88, 16);
            this.checkBox3.TabIndex = 20;
            this.checkBox3.Text = "마우스 감지";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // frm_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 164);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_right);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_moveleft);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_movedown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_rotate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_drop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_pause);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox2);
            this.Name = "frm_Setting";
            this.Text = "설정";
            this.Load += new System.EventHandler(this.frm_Setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_drop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_pause;
        private System.Windows.Forms.TextBox txt_movedown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_rotate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_right;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_moveleft;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}