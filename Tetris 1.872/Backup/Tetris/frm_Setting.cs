using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class frm_Setting : Form
    {
        public frm_Setting()
        {
            InitializeComponent();

            txt_pause.Text = Program.Ifrm_main.포즈키.ToString();
            txt_drop.Text = Program.Ifrm_main.끝까지내림키.ToString();
            txt_rotate.Text = Program.Ifrm_main.회전키.ToString();
            txt_movedown.Text = Program.Ifrm_main.한칸내림키.ToString();
            txt_moveleft.Text = Program.Ifrm_main.왼쪽으로키.ToString();
            txt_right.Text = Program.Ifrm_main.오른쪽으로키.ToString();

            this.포즈키 = Program.Ifrm_main.포즈키;
            this.끝까지내림키 = Program.Ifrm_main.끝까지내림키;
            this.회전키 = Program.Ifrm_main.회전키;
            this.한칸내림키 = Program.Ifrm_main.한칸내림키;
            this.왼쪽으로키 = Program.Ifrm_main.왼쪽으로키;
            this.오른쪽으로키 = Program.Ifrm_main.오른쪽으로키;
        }

        private void frm_Setting_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            txt_pause.Text = Program.Ifrm_main.포즈키.ToString();
            txt_drop.Text = Program.Ifrm_main.끝까지내림키.ToString();
            txt_rotate.Text = Program.Ifrm_main.회전키.ToString();
            txt_movedown.Text = Program.Ifrm_main.한칸내림키.ToString();
            txt_moveleft.Text = Program.Ifrm_main.왼쪽으로키.ToString();
            txt_right.Text = Program.Ifrm_main.오른쪽으로키.ToString();

            this.포즈키 = Program.Ifrm_main.포즈키;
            this.끝까지내림키 = Program.Ifrm_main.끝까지내림키;
            this.회전키 = Program.Ifrm_main.회전키;
            this.한칸내림키 = Program.Ifrm_main.한칸내림키;
            this.왼쪽으로키 = Program.Ifrm_main.왼쪽으로키;
            this.오른쪽으로키 = Program.Ifrm_main.오른쪽으로키;
        }

        internal Keys 포즈키;
        internal Keys 회전키;
        internal Keys 한칸내림키;
        internal Keys 끝까지내림키;
        internal Keys 왼쪽으로키;
        internal Keys 오른쪽으로키;


        private void button1_Click(object sender, EventArgs e)
        {
            Program.Ifrm_main.포즈키 = this.포즈키;
            Program.Ifrm_main.끝까지내림키 = this.끝까지내림키;
            Program.Ifrm_main.회전키 = this.회전키;
            Program.Ifrm_main.한칸내림키 = this.한칸내림키;
            Program.Ifrm_main.왼쪽으로키 = this.왼쪽으로키;
            Program.Ifrm_main.오른쪽으로키 = this.오른쪽으로키;

            Program.Ifrm_main.BGMP = checkBox1.Checked;

            if (checkBox2.CheckState == CheckState.Unchecked)
                블록생성기.인스턴스내놔().일반블록생성확률 = 100;
            else
                블록생성기.인스턴스내놔().일반블록생성확률 = 90;

            Program.Ifrm_main.마우스일시정지 = checkBox3.Checked;

            if (!Program.Ifrm_main.BGMP && Program.Ifrm_main.BGM != null)
                Program.Ifrm_main.BGM.Stop();

            Program.Ifrm_main.자동일시중지끝();
            this.Hide();
        }

        private void txt_pause_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.포즈키 = e.KeyCode;
        }

        private void txt_pause_KeyUp(object sender, KeyEventArgs e)
        {
            txt_pause.Text = this.포즈키.ToString();
        }

        private void txt_drop_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.끝까지내림키 = e.KeyCode;
        }

        private void txt_drop_KeyUp(object sender, KeyEventArgs e)
        {
            txt_drop.Text = this.끝까지내림키.ToString();
        }

        private void txt_rotate_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.회전키 = e.KeyCode;
        }

        private void txt_rotate_KeyUp(object sender, KeyEventArgs e)
        {
            txt_rotate.Text = this.회전키.ToString();
        }

        private void txt_movedown_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.한칸내림키 = e.KeyCode;
        }

        private void txt_movedown_KeyUp(object sender, KeyEventArgs e)
        {
            txt_movedown.Text = this.한칸내림키.ToString();
        }

        private void txt_moveleft_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.왼쪽으로키 = e.KeyCode;
        }

        private void txt_moveleft_KeyUp(object sender, KeyEventArgs e)
        {
            txt_moveleft.Text = this.왼쪽으로키.ToString();
        }

        private void txt_right_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.오른쪽으로키 = e.KeyCode;
        }

        private void txt_right_KeyUp(object sender, KeyEventArgs e)
        {
            txt_right.Text = this.오른쪽으로키.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.Ifrm_main.자동일시중지끝();
            this.Hide();
        }

    }
}
