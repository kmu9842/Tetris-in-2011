using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class frm_newgame : Form
    {
        int 맵세로 = 0, 맵가로 = 0;
        frm_main.난이도 I난이도 = 0;

        public frm_newgame()
        {
            InitializeComponent();
        }

        private void frm_newgame_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            맵가로 = 10;
            맵세로 = 20;
            I난이도 = frm_main.난이도.초보;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (맵가로 < 10 || 맵세로 < 20)
                if (MessageBox.Show("맵이 너무 작습니다.\n계속 진행하면 오류가 발생할 수 있습니다.\n계속하시겠습니까?"
                    , "맵 크기 경고!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;

            // 테트리스 배열 초기화
            맵.초기화(맵가로, 맵세로);

            Program.Ifrm_main.난이도설정(I난이도);
            Program.Ifrm_main.인터페이스자동배열(맵가로, 맵세로);

            this.Hide();
            Program.Ifrm_main.InitGame();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                맵가로 = int.Parse(textBox1.Text);
            }
            catch { }//textBox1.Text = 맵가로.ToString(); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                맵세로 = int.Parse(textBox2.Text);
            }
            catch { }//textBox2.Text = 맵세로.ToString(); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            I난이도 = (frm_main.난이도)comboBox1.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Ifrm_main.자동일시중지끝();
        }
    }
}
