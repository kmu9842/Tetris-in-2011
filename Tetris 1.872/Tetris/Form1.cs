using System;
using System.Media;
using System.IO.Compression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    { //jko55@chol.com
        internal enum ǻ��Stat : byte
        {
            ���������, �޴��������, ����׷ε�����, ������, ������, �ֱٻ���
        };

        internal enum �������� : byte
        {
            ���ӽ���, ��Ӵٿ�, ��Ϲ���, ����, ���ӿ���, delOne, delTwo, delThree, delFour, ���ȸ��
        };

        const int ���ũ�� = 20;

        const int �ʰ��� = 10;
        const int �ʼ��� = 20;

        //�̸������(����, ������)
        //�ʻ���(����ġ, ������)
        //�����(����, ��ġ, ������)
        //����(����, Ȯ��(�����ۿ���))
        //����(�ӵ�, ��, ����)
        Queue<string> ���;

        ǻ��Stat ����ǻ�����;
        ǻ��Stat �ֱ�ǻ�����;

        internal ǻ��Stat ǻ�����
        {
            get
            {
                return ����ǻ�����;
            }
            set
            {
                switch (value)
                {
                    case ǻ��Stat.������:
                        break;

                    case ǻ��Stat.������:
                        if (���ӽ����߳�)
                        {
                            BlockDownTimer.Start();
                            FilledLineDelete.Start();
                        }
                        lbl_����.Visible = false;
                        break;

                    case ǻ��Stat.���������:
                        if (����ǻ����� == ǻ��Stat.������)
                        {
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();
                            lbl_����.Visible = true;
                        }
                        break;

                    case ǻ��Stat.�ֱٻ���:
                        ����ǻ����� = �ֱ�ǻ�����;
                        �ֱ�ǻ����� = ����ǻ�����;
                        return;
                        //break;

                    // ǻ��Stat.����׷ε�����
                    // ǻ��Stat.�޴��������
                    default:
                        if (����ǻ����� == ǻ��Stat.��������� || ����ǻ����� == ǻ��Stat.������)
                        {
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();
                        }
                        break;
                }

                if (�ֱ�ǻ����� != ����ǻ�����)
                  �ֱ�ǻ����� = ����ǻ�����;
                ����ǻ����� = value;
            }
        }

        /// <summary>
        /// ���� �������� ��
        /// </summary>
        private ��������̳� �����;

        /// <summary>
        /// �̸����� ȭ�鿡 ��Ÿ���� ��
        /// </summary>
        private ��������̳� �̸����º�;

        private �����̳ʻ����� ��������̳ʻ����x;

        SoundPlayer[] SP;

        /// <summary>
        /// �������
        /// </summary>
        private ���� ����x;

        private bool ���ӽ����߳�;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ��Ʈ���� �迭 �ʱ�ȭ
            ��.�ʱ�ȭ(�ʰ���, �ʼ���);

            // �� ũ�� �ʱ�ȭ
            this.Size = new Size(�ʰ��� * ���ũ�� + StatusPanel.Size.Width + 10 + 6,
                                 �ʼ��� * ���ũ�� + 35 + MainMenu.Size.Height + 6);

            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            
            //�����ǳ� �ʱ�ȭ
            MainPanel.Size = new Size(�ʰ��� * ���ũ�� + 6,
                                      �ʼ��� * ���ũ��); //+ MainMenu.Size.Height);

            lbl_����.Location = new Point((MainPanel.Size.Width - lbl_����.Width) / 2, (Size.Height - lbl_����.Height) / 2);

            lbl_���ӿ���.Location = new Point((MainPanel.Size.Width - lbl_���ӿ���.Width) / 2, (Size.Height - lbl_���ӿ���.Height) / 2);

            StatusPanel.Location = new Point(MainPanel.Size.Width, StatusPanel.Location.Y);
            PreveiwPanel.Location = new Point(MainPanel.Size.Width, PreveiwPanel.Location.Y);

            //�����ǳ� �ʱ�ȭ
            StatusPanel.Size = new Size(this.Size.Width - MainPanel.Width, 
                                        �ʼ��� * ���ũ�� + MainMenu.Size.Height);

            //�����ǳ� �ʱ�ȭ
            PreveiwPanel.Size = new Size(this.Size.Width - MainPanel.Width,
                                        PreveiwPanel.Size.Width);
            // �� ���� �ʱ�ȭ
            ��ϻ�����.�ν��Ͻ�����().�̷�Ÿ���Ǻ���߰�(new �븻���(���ũ��, ���ũ��));
            ��ϻ�����.�ν��Ͻ�����().�̷�Ÿ���Ǻ���߰�(new ���ε�Ʈ���̾����ۺ�(���ũ��, ���ũ��));
            ��ϻ�����.�ν��Ͻ�����().�̷�Ÿ���Ǻ���߰�(new �Ѱ��ڻ�����ۺ�(���ũ��, ���ũ��));
            ��ϻ�����.�ν��Ͻ�����().��ֺ��Ȯ������(90);

            // �� �����̳� ���� �ʱ�ȭ
            ��������̳ʻ����x = new �����̳ʻ�����();
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new ChairA����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new ChairB����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new Ah����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new ���簢������());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new Staff����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new StairsA����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new StairsB����());
            ��� = new Queue<string>();
            ǻ����� = ǻ��Stat.������;
            �ֱ�ǻ����� = ����ǻ�����;
            SP = new SoundPlayer[4];
            for (int i = 0; i < 4; i++)
            {
                SP[i] = new SoundPlayer();
                SP[i].SoundLocationChanged += new EventHandler(this.SoundPSet);
            }
        }

        // ���� �ʱ�ȭ
        private void InitGame()
        {
            lbl_���ӿ���.Visible = false; //���ӿ����޽��� ����

            ��.Ŭ��();
            MainPanel.Invalidate(); //��Ͼ��������°� �ذ�!!

            // �����ϰ� �����̳� ����
            int n = 0;
            �̸����º� = ��������̳ʻ����x.���������̳ʻ���(out n);
            ���.Enqueue("�̸����º� = (" + n.ToString() + ");");
            
            //���.Enqueue("�̸����º�.����();");
            �̸����º�.����();

            n = 0;
            ����� = ��������̳ʻ����x.���������̳ʻ���(out n);
            ���.Enqueue("����� = (" + n.ToString() + ");");

            //���.Enqueue("�����.����();");
            �����.����();

            // ��ġ �ʱ�ȭ
            ���.Enqueue("�̸����º�.�������ġ����(50, 70);");
            �̸����º�.�������ġ����(50, 70);
            
            // ���ھ� �ʱ�ȭ
            ����x = new ����();

            BlockDownTimer.Interval = 500;

            // �ؽ�Ʈ �ʱ�ȭ
            ScoreLabel.Text = "0";
            LevelLabel.Text = ����x.�����ϱ�().ToString();
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";

            
            BlockDownTimer.Enabled = true;
            FilledLineDelete.Enabled = true;
            ���ӽ����߳� = true;
            ǻ����� = ǻ��Stat.������;
            PlayS(��������.���ӽ���);
            

            PreveiwPanel.Invalidate();
        }

        // ���� �ڵ����� �������� Ÿ�̸� �ڵ鷯
        private void BlockDownTimer_Tick(object sender, EventArgs e)
        {
            if (!�����.�̵�(0, 1))
            {
                PlayS(��������.��Ӵٿ�);

                // ���忡 �� ����
                if (!��.����߰�(�����.ù���������()))
                {
                    EndGame();
                    return;
                }
                // ��� �׼� ����
                //���.Enqueue("�����.�׼Ǽ���();");
                �����.�׼Ǽ���();
                // �����������
                ���.Enqueue("����� = �̸����º�;");
                ����� = �̸����º�;
                ���.Enqueue("�����.�̵�(0, 0);");
                �����.�̵�(0, 0);
                MainPanel.Invalidate();

                //m_CurrentBlock
                // �����ϰ� �����̳� ����
                int ni = 0;
                �̸����º� = ��������̳ʻ����x.���������̳ʻ���(out ni);
                ���.Enqueue("�̸����º� = (" + ni.ToString() + ");");

                //���.Enqueue("�̸����º�.����();");
                �̸����º�.����();
                ���.Enqueue("�̸����º�.�������ġ����(50, 70);");
                �̸����º�.�������ġ����(50, 70);
                PreveiwPanel.Invalidate();
            }
            else
                MainPanel.Invalidate(GetRefreshRegion(�����));
        }

        // ���� ȭ�� �׸���
        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // ���� �������� �� �׸���
            if(����� != null)
                �����.�׷���(g);

            // ���� �׸���
            ��.�׷�(g);
        }

        // �� �̸����� �г� �׸���
        private void PreveiwPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // �غ����� �� �׸���
            if (�̸����º� != null)
                �̸����º�.�׷���(g);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // �������� �ƴϸ� ����
            if (!���ӽ����߳�)
                return;

            if (ǻ����� == ǻ��Stat.������) //����ȉ�����
            {
                switch (e.KeyCode)
                {
                    // ȸ��
                    case Keys.Up:
                        if (�����.��ε���())
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.���ȸ��);                            
                        }
                        break;
                    case Keys.Down:
                        if (�����.�̵�(0, 1))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.��Ϲ���);
                        }
                        break;
                    case Keys.Space:
                        if (!FallingDownTimer.Enabled)
                        {
                            BlockDownTimer.Enabled = false;
                            FallingDownTimer.Enabled = true;
                        }
                        break;
                    case Keys.Left:
                        if (�����.�̵�(-1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.��Ϲ���);
                        }
                        break;
                    case Keys.Right:
                        if (�����.�̵�(1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.��Ϲ���);
                        }
                        break;
                    case Keys.Pause:
                            ǻ����� = ǻ��Stat.���������;
                        break;
                }
            }
            else if(ǻ����� == ǻ��Stat.���������) //��������̸�
            {
                switch (e.KeyCode)
                {
                    case Keys.Pause:
                            ǻ����� = ǻ��Stat.������;
                        break;
                }
            }
        }

        // ������ ����� ���������� Ÿ�̸� �ڵ鷯
        private void FallingDownTimer_Tick(object sender, EventArgs e)
        {
            if (!�����.�̵�(0, 1))
            {
                PlayS(��������.��Ӵٿ�);

                // ���忡 �� ����
                if (!��.����߰�(�����.ù���������()))
                {
                    EndGame();
                    return;
                }
                // ��� �׼� ����
                //���.Enqueue("�����.�׼Ǽ���();");
                �����.�׼Ǽ���();
                // �����������
                ���.Enqueue("����� = �̸����º�;");
                ����� = �̸����º�;
                ���.Enqueue("�����.�̵�(0, 0);");
                �����.�̵�(0, 0);
                MainPanel.Invalidate();

                // �����ϰ� �����̳� ����
                int ni = 0;
                �̸����º� = ��������̳ʻ����x.���������̳ʻ���(out ni);
                ���.Enqueue("�̸����º� = (" + ni.ToString() + ");");

                //���.Enqueue("�̸����º�.����();");
                �̸����º�.����();
                �̸����º�.�������ġ����(50, 70);
                PreveiwPanel.Invalidate();

                FallingDownTimer.Enabled = false;
                BlockDownTimer.Enabled = true;
            }
            else
                MainPanel.Invalidate(GetRefreshRegion(�����));
        }

        // �������� ����â ������Ʈ
        private void LevelUp()
        {
            int level = ����x.�����ϱ�();
            LevelLabel.Text = level.ToString();
            BlockDownTimer.Interval -= (BlockDownTimer.Interval / (level + 2));
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";
            ScoreLabel.Text = ����x.��������().ToString();
            PlayS(��������.����);
        }

        private void EndGame()
        {
            ���.Clear();
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;
            ��.Ŭ��();
            ����� = null;
            �̸����º� = null;
            PreveiwPanel.Invalidate();
            MainPanel.Invalidate();
            lbl_���ӿ���.Visible = true;
            ���ӽ����߳� = false;
            ǻ����� = ǻ��Stat.������;
            PlayS(��������.���ӿ���);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ���� �ʱ�ȭ
            ���.Clear();
            InitGame();
        }

        // ���� �������� ������ ���Ѵ� 
        private Rectangle GetRefreshRegion(��������̳� cont)
        {
            Rectangle rc = cont.��Ͽ�������();
            rc.Width = ���ũ�� * 8;
            rc.Height = ���ũ�� * 8;
            rc.X -= ���ũ�� * 2;
            rc.Y -= ���ũ�� * 2;

            return rc;
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            �޴�������();
            String str;

            str = "#���۹�\n";
            str += "�̵� : �¿� ����Ű\n";
            str += "ȸ�� : ���� ����Ű\n";
            str += "��ĭ ������ : �Ʒ��� ����Ű\n";
            str += "����߸��� : �����̽� Ű\n\n";
            str += "#������ ����\n";
            str += "�Ķ��� : �Ϲ� ��\n";
            str += "����� : �ش� ���� ��ĭ�Ʒ� ���� �����Ѵ�.\n";
            str += "������ : �ش� ���� ������ �����Ѵ�.\n\n";
            str += "Tetris Ver. 1.487 \n 2009. 9.5 ~ 10.17 \n ���� : �ߺο��米���� Brain Development ��";
            MessageBox.Show(str);

            �޴���볡();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            �޴�������();
            saveFileDialog1.ShowDialog();
            �޴���볡();
        }

        private void loadtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            �޴�������();
            openFileDialog1.ShowDialog();
            �޴���볡();
        }        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
                ǻ����� = ǻ��Stat.����׷ε�����;

            FileStream load = (FileStream)openFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(load, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(gzs);
            ť�������(��.����Ʈ(DecryptString(sr.ReadToEnd(), SECURITYCODE)));
            sr.Close();
            gzs.Close();
            load.Close();

                ǻ����� = ǻ��Stat.������;

                MainPanel.Invalidate();
                PreveiwPanel.Invalidate();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
                ǻ����� = ǻ��Stat.����׷ε�����;

            FileStream save = (FileStream)saveFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(save, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gzs);
            
            sw.Write(EncryptString(��.����Ʈ(���), SECURITYCODE));
            sw.Close();
            gzs.Close();
            save.Close();

                ǻ����� = ǻ��Stat.������;
        }

        private void endgametoolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;
            ��.Ŭ��();
            ����� = null;
            �̸����º� = null;
            MainPanel.Invalidate();
            PreveiwPanel.Invalidate();
            ���ӽ����߳� = false;
            ǻ����� = ǻ��Stat.������;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void helpToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            �޴�������();
        }

        private void helpToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            �޴���볡();
        }

        private void filetoolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            �޴�������();
        }

        private void filetoolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            �޴���볡();
        }

        void �޴�������()
        {
                ǻ����� = ǻ��Stat.�޴��������;
        }

        void �޴���볡()
        {
                ǻ����� = ǻ��Stat.�ֱٻ���;
        }

        void ť�������(Queue<string> ������)
        {
            while (������.Count != 0)
            {
                
                switch (������.Dequeue())
                {
                    default:
                    break;
                }
            }
        }

        private void ����CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Ifrm_set.Show();
        }

        internal void FilledLineDelete_Tick(object sender, EventArgs e)
        {
            // ���� ������ �ִ��� �˻�
            int n = ��.��_��_����_������();

            // ���� ���
            if (n != 0)
            {
                if (����x.����Ŭ����(n))
                {
                    LevelUp();
                }
                ScoreLabel.Text = ����x.��������().ToString();

                switch (n)
                {
                    case 1:
                        PlayS(��������.delOne);
                        break;

                    case 2:
                        PlayS(��������.delTwo);
                        break;

                    case 3:
                        PlayS(��������.delThree);
                        break;

                    default:
                        PlayS(��������.delFour);
                        break;
                }

                MainPanel.Invalidate();
            }
            //else
            //{
            //    if (����x.����߶�()) //������� ����
            //        LevelUp();
            //}            
        }

        internal void PlayS(�������� st)
        {
            if (!Program.Muzik)
                return;

            switch (st)
            {
                case ��������.���ӽ���:
                    SP[0].SoundLocation = "Gamestart.wav";
                    break;

                case ��������.��Ӵٿ�:
                    SP[3].SoundLocation = "Dropdown.wav";
                    break;

                case ��������.��Ϲ���:
                    SP[3].SoundLocation = "Rotate.wav";
                    break;

                case ��������.����:
                    SP[1].SoundLocation = "Levelup.wav";
                    break;

                case ��������.���ӿ���:
                    SP[0].SoundLocation = "Gameover.wav";
                    break;

                case ��������.delOne:
                    SP[2].SoundLocation = "One.wav";
                    break;

                case ��������.delTwo:
                    SP[2].SoundLocation = "Two.wav";
                    break;

                case ��������.delThree:
                    SP[2].SoundLocation = "Three.wav";
                    break;

                case ��������.delFour:
                    SP[2].SoundLocation = "Four.wav";
                    break;

                case ��������.���ȸ��:
                    SP[3].SoundLocation = "Rotate.wav";
                    break;
            }
        }

        private void SoundPSet(object sender, EventArgs e)
        {
            if (((SoundPlayer)sender).SoundLocation != "null.wav")
                ((SoundPlayer)sender).Play();

            ((SoundPlayer)sender).SoundLocation = "null.wav";
        }

        private void ��������׿�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ǻ����� = ǻ��Stat.�޴��������;
            Program.Ifrm_dbgset.Show();
        }
    }
}