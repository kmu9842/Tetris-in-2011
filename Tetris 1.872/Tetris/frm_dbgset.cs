using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class frm_dbgset : Form
    {
        private ushort NUM;
        bool Auth;

        public frm_dbgset()
        {
            InitializeComponent();
        }

        private void buttonnum_Click(object sender, EventArgs e)
        {
            button10.Text = (ulong.Parse(button10.Text) + ((Button)sender).Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == NUM.ToString()) //인증완료
            {
                Auth = true;
                MessageBox.Show("개발자 인증완료!");
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button10.Text = (ulong.Parse(button10.Text) - 1).ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Auth = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;

            Random r = new Random();
            string tmp = string.Empty;

            for (byte b = 0; b < 2; b++)
            {
                int i = r.Next(1, 9);
                switch (i)
                {
                    case 1:
                        button1.Enabled = false;
                        break;

                    case 2:
                        button2.Enabled = false;
                        break;

                    case 3:
                        button3.Enabled = false;
                        break;

                    case 4:
                        button4.Enabled = false;
                        break;

                    case 5:
                        button5.Enabled = false;
                        break;

                    case 6:
                        button6.Enabled = false;
                        break;

                    case 7:
                        button7.Enabled = false;
                        break;

                    case 8:
                        button8.Enabled = false;
                        break;

                    case 9:
                        button9.Enabled = false;
                        break;
                }
                tmp += i.ToString();
            }
            NUM = ushort.Parse(tmp);
        }

        private void frm_dbgset_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            Auth = false;
            NUM = 9999;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!Auth)
                return;

            try
            {
                블록생성기.인스턴스내놔().노멀블록확률설정(int.Parse(textBox1.Text));
            }
            catch { }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!Auth)
                return;

            Program.Music = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!Auth)
                return;

            Program.Music = !radioButton2.Checked;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Program.Ifrm_main.퓨즈상태 = frm_main.퓨즈Stat.최근상태;
        }
    }
}
