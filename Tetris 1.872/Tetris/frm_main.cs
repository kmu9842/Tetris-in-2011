using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;
using UniAPI;

namespace Tetris
{
    public partial class frm_main : Form
    {
        internal bool ���콺�Ͻ����� = true;

        private Win32.HookProcessProc MLL_proc, KLL_proc; //���ο� HookProcessProc (HookCallback����) 
        private IntPtr MLL_hookID = IntPtr.Zero, KLL_hookID = IntPtr.Zero; //��ũ ID
        int max_y = 0, max_x = 0, min_y = 0, min_x = 0; //bool mouseautopause = false;

        private void HookInit() { 
            MLL_hookID = Win32.SetHookAuto(MLL_proc, WH.MOUSE_LL); /*���콺��ũID�� MLL_hookID�� ����*/
            KLL_hookID = Win32.SetHookAuto(KLL_proc, WH.KEYBOARD_LL); /*Ű������ũID�� KLL_hookID�� ����*/
        }

        private void HookShutdown() { 
            Win32.UnhookWindowsHookEx(MLL_hookID); /*���콺����*/
            Win32.UnhookWindowsHookEx(KLL_hookID); /*Ű�������*/ 
        }

        private IntPtr MLLHookCallback(int nCode, IntPtr wParam, IntPtr lParam) /*���콺��ũ ���ν���*/ {
            if (���콺�Ͻ�����)
            {
                if (nCode >= 0) //nCode Check?
                {
                    if (WM.MOUSEMOVE == (WM)wParam)
                    {
                        MSLLHOOKSTRUCT hS = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                        if (hS.pt.x < max_x && hS.pt.x > min_x && hS.pt.y < max_y && hS.pt.y > min_y)
                        {
                            if (ǻ����� == ǻ��Stat.���콺����)
                            {
                                if (�����ǻ��)
                                    ǻ����� = ǻ��Stat.���������;
                                else
                                    ǻ����� = ǻ��Stat.������;
                            }
                            else if (ǻ����� == ǻ��Stat.��������)
                                ǻ����� = ǻ��Stat.�ڵ�����;
                        }
                        else
                        {
                            if (ǻ����� == ǻ��Stat.������)
                                ǻ����� = ǻ��Stat.���콺����;
                            else if (ǻ����� == ǻ��Stat.�ڵ�����)
                                ǻ����� = ǻ��Stat.��������;
                        }
                        //return (IntPtr)1; //�޽����Ա�
                    }
                }
            }
            return Win32.CallNextHookEx(MLL_hookID, nCode, wParam, lParam); /*������ũȣ��*/
        }

        List<Keys> gKdownCur = new List<Keys>();

        void HideMethod()
        {
            if (!Hided)
            {
                BlockDownTimer.Stop();
                FilledLineDelete.Stop();
                MFTimer.Stop();

                if (BGM != null)
                    BGM.Pause();

                Process.GetCurrentProcess().StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Hided = true;
            }
        }

        void ShowMethod()
        {
            if (Hided)
            {
                BlockDownTimer.Start();
                FilledLineDelete.Start();
                MFTimer.Start();

                if (BGM != null && BGMP)
                    BGM.Play();

                Process.GetCurrentProcess().StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                Hided = false;
            }
        }

        bool Hided = false;
        bool PressedKeyFalse = true;

        void ghKeylistCheck(Keys kys)
        { //���� ��������
            PressedKeyFalse = false;

            if (!gKdownCur.Contains(kys))
                PressedKeyFalse = true;
        }

        List<Keys> ghKeylist = new List<Keys>();

        private IntPtr KLLHookCallback(int nCode, IntPtr wParam, IntPtr lParam) /*Ű������ũ ���ν���*/ {
            if (nCode >= 0) //nCode Check?
            {
                if (WM.KEYDOWN == (WM)wParam || WM.SYSKEYDOWN == (WM)wParam)
                { //���׿�������
                    //MessageBox.Show(((Keys)Marshal.ReadInt32(lParam)).ToString());

                    if (!gKdownCur.Contains((Keys)Marshal.ReadInt32(lParam)))
                         gKdownCur.Add((Keys)Marshal.ReadInt32(lParam));

                    ghKeylist.ForEach(new Action<Keys>(ghKeylistCheck));

                    if (!PressedKeyFalse)
                    {
                        if (!Hided)
                            HideMethod();
                        else
                            ShowMethod();
                    }
                }
                /*else if (WM.KEYUP == (WM)wParam || WM.SYSKEYUP == (WM)wParam)
                { //���׿�������
                    if (gKdownCur.Contains((Keys)Marshal.ReadInt32(lParam)))
                         gKdownCur.Remove((Keys)Marshal.ReadInt32(lParam));

                    ghKeylist.ForEach(new Action<Keys>(ghKeylistCheck));

                    if (!PressedKeyFalse)
                    {
                        if (!Hided)
                            HideMethod();
                        else
                            ShowMethod();
                    }
                }*/
            }
            return Win32.CallNextHookEx(MLL_hookID, nCode, wParam, lParam); /*������ũȣ��*/
        }

        internal Audio BGM;
        internal bool BGMP = true;
        string BGMPATH;

        internal enum ǻ��Stat : byte
        {
            ���������, �ڵ�����, ���콺����, ��������, ������, ������, �ڵ��Ͻ���������
        };

        internal enum �������� : byte
        {
            ���ӽ���, ��Ӵٿ�, ��Ϲ���, ����, ���ӿ���, delOne, delTwo, delThree, delFour, ���ȸ��
        };

        internal enum ���̵� : byte
        {
            ���ʺ�, �ʺ�, �߼�, ���
        };

        internal const int ���ũ�� = 20;

        const int �ʰ��� = 10;
        const int �ʼ��� = 20;

        internal ���̵� I���̵� = 0;

        internal Keys ����Ű;
        internal Keys ȸ��Ű;
        internal Keys ��ĭ����Ű;
        internal Keys ����������Ű;
        internal Keys ��������Ű;
        internal Keys ����������Ű;

        ǻ��Stat ����ǻ�����;
        bool �����ǻ��;

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
                        �����ǻ�� = false;

                        if(BGM != null && BGMP && BGM.State != StateFlags.Running)
                            BGM.Play();

                        if (���ӽ����߳�)
                        {
                            BlockDownTimer.Start();
                            FilledLineDelete.Start();
                            MFTimer.Start();
                        }
                        lbl_����.Visible = false;
                        break;

                    case ǻ��Stat.���������:
                        BlockDownTimer.Stop();
                        FilledLineDelete.Stop();
                        MFTimer.Stop();
                        if(BGM != null)
                        BGM.Pause();
                        lbl_����.Visible = true;
                        �����ǻ�� = true;
                        ����ǻ����� = ǻ��Stat.������;
                        return;

                    case ǻ��Stat.�ڵ��Ͻ���������:
                        if (ǻ����� == ǻ��Stat.��������)
                        {
                            ǻ����� = ǻ��Stat.���콺����;
                        }
                        else if (����ǻ����� == ǻ��Stat.�ڵ�����)// || ����ǻ����� == ǻ��Stat.���콺����)
                        {
                            if (�����ǻ��)
                                ǻ����� = ǻ��Stat.���������;
                            else
                                ǻ����� = ǻ��Stat.������;
                        }
                        return;

                    case ǻ��Stat.���콺����:
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();

                            if (BGM != null)
                                BGM.Pause();

                            MFTimer.Stop();

                        if (����ǻ����� == ǻ��Stat.�ڵ�����)
                        {
                            ����ǻ����� = ǻ��Stat.��������;
                            return;
                        }
                        break;

                    case ǻ��Stat.�ڵ�����:
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();

                            if (BGM != null)
                                BGM.Pause();

                            MFTimer.Stop();

                        if (����ǻ����� == ǻ��Stat.���콺����)
                        {
                            ����ǻ����� = ǻ��Stat.��������;
                            return;
                        }
                        break;
                }

                ����ǻ����� = value;
            }
        }

        /// <summary>
        /// ���� �������� ��
        /// </summary>
        internal ��������̳� �����;
        internal int �����Ÿ��;

        /// <summary>
        /// �̸����� ȭ�鿡 ��Ÿ���� ��
        /// </summary>
        internal ��������̳� �̸����º�1;
        internal int �̸����º�1Ÿ��;

        /// <summary>
        /// �̸����� ȭ�鿡 ��Ÿ���� ��2
        /// </summary>
        internal ��������̳� �̸����º�2;
        internal int �̸����º�2Ÿ��;

        internal �����̳ʻ����� ��������̳ʻ����x;

        SoundPlayer[] SP;

        /// <summary>
        /// ����//���
        /// </summary>
        internal ���� ����x;
        internal int BDTI;

        private bool ���ӽ����߳�;

        public frm_main()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            ����Ű = Keys.Pause;
            ȸ��Ű = Keys.Up;
            ��ĭ����Ű = Keys.Down;
            ����������Ű = Keys.Space;
            ��������Ű = Keys.Left;
            ����������Ű = Keys.Right;
            BGMPATH = null;
            �����ǻ�� = false;
            MLL_proc = MLLHookCallback;
            KLL_proc = KLLHookCallback;

            ghKeylist.Add(Keys.LShiftKey); //���Ӽ���Ű �̸�����
            ghKeylist.Add(Keys.LControlKey);
            ghKeylist.Add(Keys.LMenu);
        }

        internal void �������̽��ڵ��迭(int �ʰ���, int �ʼ���)
        {
            //��ũ�� ��������
            this.MinimumSize = new Size(0, 0);
            this.MaximumSize = new Size(0, 0);
            
            // �� ũ�� �ʱ�ȭ
            //this.Size = new Size(�ʰ��� * ���ũ�� + StatusPanel.Size.Width + 10 + 6,
            //                     �ʼ��� * ���ũ�� + 35 + MainMenu.Size.Height + 6);
            if (�ʰ��� >= 10 && �ʼ��� >= 20)
                this.Size = new Size(�ʰ��� * ���ũ�� + StatusPanel.Size.Width + 15,
                                     �ʼ��� * ���ũ�� + MainMenu.Size.Height + 40);
            else
                this.Size = new Size(10 * ���ũ�� + StatusPanel.Size.Width + 15,
                                     20 * ���ũ�� + MainMenu.Size.Height + 40);

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            //�����ǳ� �ʱ�ȭ
            MainPanel.Size = new Size(�ʰ��� * ���ũ�� + 6,
                                      �ʼ��� * ���ũ��); //+ MainMenu.Size.Height);

            lbl_����.Location = new Point((MainPanel.Size.Width - lbl_����.Width) / 2, (Size.Height - lbl_����.Height) / 2);

            lbl_���ӿ���.Location = new Point((MainPanel.Size.Width - lbl_���ӿ���.Width) / 2,
                (Size.Height - lbl_���ӿ���.Height) / 2);

            pvp1.Location = new Point(MainPanel.Size.Width, MainMenu.Size.Height);
            pvp2.Location = new Point(MainPanel.Size.Width, MainMenu.Size.Height + pvp1.Size.Height);
            StatusPanel.Location = new Point(MainPanel.Size.Width, pvp1.Location.Y + pvp1.Size.Height + pvp2.Size.Height);

            //�����ǳ� �ʱ�ȭ
            StatusPanel.Size = new Size(StatusPanel.Size.Width,
                                        this.Size.Height - MainMenu.Size.Height - pvp1.Size.Height);

            //�����ǳ� �ʱ�ȭ
            pvp1.Size = new Size(this.Size.Width - MainPanel.Width, pvp1.Size.Height);
            pvp2.Size = new Size(this.Size.Width - MainPanel.Width, pvp2.Size.Height);

            min_x = this.Location.X;
            max_x = this.Location.X + this.Size.Width;

            min_y = this.Location.Y;
            max_y = this.Location.Y + this.Size.Height;
        }

        internal void ���̵�����(���̵� nid)
        {
            pvp2.Visible = true;
            pvp1.Visible = true;

            if(BGM != null)
                BGM.Stop();

            BGMPATH = null;

            switch (nid)
            {
                case ���̵�.���ʺ�:
                    BDTI = BlockDownTimer.Interval = 800;
                    break;

                case ���̵�.�ʺ�:
                    BDTI = BlockDownTimer.Interval = 650;
                    break;

                case ���̵�.�߼�:
                    BDTI = BlockDownTimer.Interval = 500;
                    pvp2.Visible = false;
                    break;

                case ���̵�.���:
                    BDTI = BlockDownTimer.Interval = 400;
                    pvp2.Visible = false;
                    pvp1.Visible = false;
                    break;

               /* case ���̵�.����:
                    BDTI = BlockDownTimer.Interval = 400;
                    pvp2.Visible = false;
                    pvp1.Visible = false;
                    break;*/

            }
            Program.Ifrm_main.I���̵� = nid;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ��Ʈ���� �迭 �ʱ�ȭ
            ��.�ʱ�ȭ(�ʰ���, �ʼ���);

            �������̽��ڵ��迭(�ʰ���, �ʼ���);
            
            // �� ���� �ʱ�ȭ
            ��ϻ�����.�ν��Ͻ�����().�̷�Ÿ���Ǻ���߰�(new �븻���(���ũ��, ���ũ��));
            ��ϻ�����.�ν��Ͻ�����().�̷�Ÿ���Ǻ���߰�(new ���ε�Ʈ���̾����ۺ�(���ũ��, ���ũ��));
            ��ϻ�����.�ν��Ͻ�����().�̷�Ÿ���Ǻ���߰�(new �Ѱ��ڻ�����ۺ�(���ũ��, ���ũ��));
            ��ϻ�����.�ν��Ͻ�����().�Ϲݺ�ϻ���Ȯ�� = 90;

            // �� �����̳� ���� �ʱ�ȭ
            ��������̳ʻ����x = new �����̳ʻ�����();
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new ChairA����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new ChairB����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new Ah����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new ���簢������());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new Staff����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new StairsA����());
            ��������̳ʻ����x.�����̳�Ÿ���߰�(new StairsB����());


            ǻ����� = ǻ��Stat.������;
            �����ǻ�� = false;
            SP = new SoundPlayer[5];
            for (int i = 0; i < 5; i++)
            {
                SP[i] = new SoundPlayer();
                SP[i].SoundLocationChanged += new EventHandler(this.SoundPSet);
            }

            switch (DateTime.Now.Month)
            {
                case 1:
                    if (DateTime.Now.Day == 1)
                        MessageBox.Show("������ ����!\n��� ���� �����.");
                    break;

                case 2:
                    break;

                case 3:
                    if (DateTime.Now.Day == 1)
                        MessageBox.Show("������ ������..\n�±ر⸦ ��ô�.");
                    else if (DateTime.Now.Day == 3)
                        MessageBox.Show("������ �������� ��..\nŻ����������.");
                    break;

                case 4:
                    if (DateTime.Now.Day == 21)
                        MessageBox.Show("������ ������ ��");
                    else if (DateTime.Now.Day == 22)
                        MessageBox.Show("������ ��������� ��");
                    break;

                case 5:
                    if (DateTime.Now.Day == 5)
                        MessageBox.Show("������ ��̳�");
                    else if (DateTime.Now.Day == 19)
                        MessageBox.Show("������ �߸��� ��..\n���� ���̵� ���÷�����.");
                    break;

                case 6:
                    if (DateTime.Now.Day == 6)
                        MessageBox.Show("������ ������");
                    break;

                case 7:
                    break;

                case 8:
                    if (DateTime.Now.Day == 15)
                        MessageBox.Show("������ ������");
                    break;

                case 9:
                    if (DateTime.Now.Day == 3)
                        MessageBox.Show("������ ������ ����..\n���ϻ�����..");
                    break;

                case 10:
                    if (DateTime.Now.Day == 3)
                        MessageBox.Show("������ �߼�!\n�뵷��..");
                    else if (DateTime.Now.Day == 17)
                        MessageBox.Show("������ ���⹰ ��ȸ ����");
                    break;

                case 11:
                    break;

                case 12:
                    if (DateTime.Now.Day == 25)
                        MessageBox.Show("������ ũ��������!\n�뵷��..");
                    break;

                default:
                    break;
            }

            HookInit();
        }

        // ���� �ʱ�ȭ
        internal void InitGame()
        {
            this.����Ű = Program.Ifrm_set.����Ű;
            this.����������Ű = Program.Ifrm_set.����������Ű;
            this.ȸ��Ű = Program.Ifrm_set.ȸ��Ű;
            this.��ĭ����Ű = Program.Ifrm_set.��ĭ����Ű;
            this.��������Ű = Program.Ifrm_set.��������Ű;
            this.����������Ű = Program.Ifrm_set.����������Ű;

            lbl_���ӿ���.Visible = false; //���ӿ����޽��� ����

            ��.Ŭ��();
            MainPanel.Invalidate(); //��Ͼ��������°� �ذ�!!

            // �����ϰ� �����̳� ����
            �̸����º�2 = ��������̳ʻ����x.���������̳ʻ���(out �̸����º�2Ÿ��);
            �̸����º�2.����();
            �̸����º�1 = ��������̳ʻ����x.���������̳ʻ���(out �̸����º�1Ÿ��);
            �̸����º�1.����();

            //������� �����ϰ�
            ����� = ��������̳ʻ����x.���������̳ʻ���(out �����Ÿ��);
            �����.����();

            // ��ġ �ʱ�ȭ
            �̸����º�2.�������ġ����(50, 70);
            �̸����º�1.�������ġ����(50, 70);
            
            // ���ھ� �ʱ�ȭ
            ����x = new ����();

            // �ؽ�Ʈ �ʱ�ȭ
            ScoreLabel.Text = "0";
            LevelLabel.Text = ����x.�����ϱ�().ToString();
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";

            
            BlockDownTimer.Enabled = true;
            FilledLineDelete.Enabled = true;
            ���ӽ����߳� = true;
            ǻ����� = ǻ��Stat.������;
            PlayS(��������.���ӽ���);

            pvp1.Invalidate();
            pvp2.Invalidate();
        }

        // ���� �ڵ����� �������� Ÿ�̸� �ڵ鷯
        private void BlockDownTimer_Tick(object sender, EventArgs e)
        { //�̸��Է��ϰ� ���ʹ����� ����... �ٷξƷ��ٿ���
            if (!�����.�̵�(0, 1)) //������� �ٴڿ� ��������
            {
                PlayS(��������.��Ӵٿ�);

                if (!��.����߰�(�����.ù���������()))
                {
                    EndGame();
                    return;
                }
                �����.�׼Ǽ���(); //�׼�!
                ����� = �̸����º�1;//�̸����� ���� ���������
                �̸����º�1 = �̸����º�2;//�̸����� ��2�� �̸����� ������
                �����.�̵�(0, 0); //���� ��ġ
                MainPanel.Invalidate(); //���ΰ�ħ

                // �����ϰ� �����̳� ����
                �̸����º�2 = ��������̳ʻ����x.���������̳ʻ���(out �̸����º�2Ÿ��);
                �̸����º�2.����();
                �̸����º�2.�������ġ����(50, 70);
                pvp1.Invalidate(); //���ΰ�ħ
                pvp2.Invalidate(); //���ΰ�ħ
            }
            else //�ȶ���������
                MainPanel.Invalidate(GetRefreshRegion(�����)); //���ΰ�ħ
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
            if (�̸����º�1 != null)
                �̸����º�1.�׷���(g);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // �������� �ƴϸ� ����
            if (!���ӽ����߳�)
                return;

            if (ǻ����� == ǻ��Stat.������) //����ȉ�����
            {
                if (!�����ǻ��)
                {
                    if (e.KeyCode == ȸ��Ű)
                    {
                        if (�����.��ε���())
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.���ȸ��);
                        }
                    }
                    else if (e.KeyCode == ��ĭ����Ű)
                    {
                        if (�����.�̵�(0, 1))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.��Ϲ���);
                        }
                    }
                    else if (e.KeyCode == ����������Ű)
                    {
                        if (!FallingDownTimer.Enabled)
                        {
                            BlockDownTimer.Enabled = false;
                            FallingDownTimer.Enabled = true;
                        }
                    }
                    else if (e.KeyCode == ��������Ű)
                    {
                        if (�����.�̵�(-1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.��Ϲ���);
                        }
                    }
                    else if (e.KeyCode == ����������Ű)
                    {
                        if (�����.�̵�(1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(�����));
                            PlayS(��������.��Ϲ���);
                        }
                    }
                }
                if (e.KeyCode == ����Ű) //��������̸�
                {
                    if (�����ǻ��)
                    {
                        if (ǻ����� == ǻ��Stat.������)
                        {
                            ǻ����� = ǻ��Stat.������;
                            �����ǻ�� = false;
                        }
                        else
                            �����ǻ�� = false;
                    }
                    else
                        ǻ����� = ǻ��Stat.���������;
                }
            }
        }

        // ������ ����� ���������� Ÿ�̸� �ڵ鷯
        private void FallingDownTimer_Tick(object sender, EventArgs e)
        {
            if (!�����.�̵�(0, 1)) //���� �ٴڿ� ������
            {
                PlayS(��������.��Ӵٿ�);

                // ���忡 �� ����
                if (!��.����߰�(�����.ù���������())) //���н� ���ӿ���
                {
                    EndGame();
                    return;
                }

                �����.�׼Ǽ���(); //�׼�!
                ����� = �̸����º�1;//�̸����� ���� ���������
                �̸����º�1 = �̸����º�2;//�̸����� ��2�� �̸����� ������
                �����.�̵�(0, 0); //���� ��ġ
                MainPanel.Invalidate(); //���ΰ�ħ

                // �����ϰ� �����̳� ����
                �̸����º�2 = ��������̳ʻ����x.���������̳ʻ���(out �̸����º�2Ÿ��);
                �̸����º�2.����();
                �̸����º�2.�������ġ����(50, 70);
                pvp1.Invalidate(); //���ΰ�ħ
                pvp2.Invalidate(); //���ΰ�ħ

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
            BDTI = BlockDownTimer.Interval;
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";
            ScoreLabel.Text = ����x.��������().ToString();
            PlayS(��������.����);
        }

        private void EndGame()
        {
            this.����Ű = Program.Ifrm_set.����Ű;
            this.����������Ű = Program.Ifrm_set.����������Ű;
            this.ȸ��Ű = Program.Ifrm_set.ȸ��Ű;
            this.��ĭ����Ű = Program.Ifrm_set.��ĭ����Ű;
            this.��������Ű = Program.Ifrm_set.��������Ű;
            this.����������Ű = Program.Ifrm_set.����������Ű;
            if (BGM != null)
            {
                BGM.Stop();
                BGM = null;
            }
            BGMPATH = null;
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;

            //��ŷ
            if (Program.Ifrm_rank.��ŷ���(����x, I���̵�, txt_name.Text))
                MessageBox.Show("�����մϴ�. " + txt_name.Text + "��\n���� ���翡 ��ϵǼ̽��ϴ�.", "��Ʈ���� ��ŷ");

            Program.Ifrm_rank.Show();

            ��.Ŭ��();
            ����� = null;
            �̸����º�1 = null;
            �̸����º�2 = null;
            pvp1.Invalidate();
            pvp2.Invalidate();
            MainPanel.Invalidate();
            lbl_���ӿ���.Visible = true;
            ���ӽ����߳� = false;
            ǻ����� = ǻ��Stat.������;
            PlayS(��������.���ӿ���);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            �ڵ��Ͻ�����();
            Program.Ifrm_newgame.Show();
            
            // ���� �ʱ�ȭ
            //InitGame();
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
            �ڵ��Ͻ�����();
            string str;

            str = "#���۹�\n";
            str += "�̵� : �¿� ����Ű\n";
            str += "ȸ�� : ���� ����Ű\n";
            str += "��ĭ ������ : �Ʒ��� ����Ű\n";
            str += "����߸��� : �����̽� Ű\n";
            str += "�Ͻ����� ���� : �Ͻ����� ���¿��� Pause/Brk Ű\n";
            str += "�Ͻ����� : Pause/Brk Ű\n\n";
            str += "#������ ����\n";
            str += "�Ķ��� : �Ϲ� ��\n";
            str += "����� : �ش� ���� ��ĭ�Ʒ� ���� �����Ѵ�.\n";
            str += "������ : �ش� ���� ������ �����Ѵ�.\n\n";
            str += "#��� ����\n";
            str += "�� �������� �ӵ��� 2�谡 �˴ϴ�.\n";
            str += "���� ��� : ������ �����ߴٰ� ���߿� ����� �� �ֽ��ϴ�.\n";
            str += "���� : �����ۺ��� �Ҹ��� ���� �Ӽ� �ֽ��ϴ�.\n";
            str += "�ڵ� �Ͻ����� ��� : ������ �������϶� �޴��� Ŭ���ϸ� �ڵ����� ��� ����˴ϴ�.\n";
            str += "�ڵ� �Ͻ����� ��� 2 : ���콺 Ŀ���� ���� â ������ �̵���Ű�� �ڵ����� ��� ����˴ϴ�.\n\n";
            str += "Tetris Ver. 1.872\n 2009. 9.5 ~ \n ���� : �ߺο��米���� Brain Development ��";
            
            MessageBox.Show(str);
            �ڵ��Ͻ�������();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            �ڵ��Ͻ�����();
            if (!���ӽ����߳�)
            {
                MessageBox.Show("���ӿ����ÿ��� �����Ҽ� �����ϴ�...\n���� ���ϰ��ϴ� ������ �ȴٸ� ����� õ��");
                return;
            }

            saveFileDialog1.ShowDialog();
            �ڵ��Ͻ�������();
        }

        private void loadtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            �ڵ��Ͻ�����();
            openFileDialog1.ShowDialog();
            �ڵ��Ͻ�������();
        }        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream load = (FileStream)openFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(load, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(gzs);
            ��.����Ʈ(DecryptString(sr.ReadToEnd()));
            sr.Close();
            gzs.Close();
            load.Close();

            �������̽��ڵ��迭(��.����ũ��, ��.����ũ��);

            lbl_���ӿ���.Visible = false; //���ӿ����޽��� ����
            BlockDownTimer.Interval = BDTI;
            ���ӽ����߳� = true;
            int level = ����x.�����ϱ�();
            LevelLabel.Text = level.ToString();
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";
            ScoreLabel.Text = ����x.��������().ToString();
            MainPanel.Invalidate();
            pvp1.Invalidate();

            ǻ����� = ǻ��Stat.���������;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream save = (FileStream)saveFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(save, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gzs);
            
            sw.Write(EncryptString(��.����Ʈ()));
            sw.Close();
            gzs.Close();
            save.Close();
        }

        private void endgametoolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;
            ��.Ŭ��();
            ����� = null;
            �̸����º�1 = null;
            MainPanel.Invalidate();
            pvp1.Invalidate();
            ���ӽ����߳� = false;
            ǻ����� = ǻ��Stat.������;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            HookShutdown();
            Process.GetCurrentProcess().Kill();
        }

        private void helpToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            �ڵ��Ͻ�����();
        }

        private void helpToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            �ڵ��Ͻ�������();
        }

        private void filetoolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            �ڵ��Ͻ�����();
        }

        private void filetoolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            �ڵ��Ͻ�������();
        }

        internal void �ڵ��Ͻ�����()
        {
            ǻ����� = ǻ��Stat.�ڵ�����;
        }

        internal void �ڵ��Ͻ�������()
        {
            ǻ����� = ǻ��Stat.�ڵ��Ͻ���������;
        }

        private void ����CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            �ڵ��Ͻ�����();
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
                    LevelUp();
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
            if (!Program.SoundEffect)
                return;

            string curL = Application.StartupPath + "\\";

            switch (st)
            {
                case ��������.���ӽ���:
                    SP[0].SoundLocation = curL + "Gamestart.wav";
                    break;

                case ��������.��Ӵٿ�:
                    SP[3].SoundLocation = curL + "Dropdown.wav";
                    break;

                case ��������.��Ϲ���:
                    SP[3].SoundLocation = curL + "Rotate.wav";
                    break;

                case ��������.����:
                    SP[1].SoundLocation = curL + "Levelup.wav";
                    break;

                case ��������.���ӿ���:
                    SP[0].SoundLocation = curL + "Gameover.wav";
                    break;

                case ��������.delOne:
                    SP[2].SoundLocation = curL + "One.wav";
                    break;

                case ��������.delTwo:
                    SP[2].SoundLocation = curL + "Two.wav";
                    break;

                case ��������.delThree:
                    SP[2].SoundLocation = curL + "Three.wav";
                    break;

                case ��������.delFour:
                    SP[2].SoundLocation = curL + "Four.wav";
                    break;

                case ��������.���ȸ��:
                    SP[3].SoundLocation = curL + "Rotate.wav";
                    break;

            }
        }

        private void SoundPSet(object sender, EventArgs e)
        {
            Thread playthread = new Thread(new ParameterizedThreadStart(SoundPlay));
            //�����带 ����� ����ϱ⶧���� �Ҹ� ����� ���� �ּ�ȭ�Ҽ�������
            playthread.Start(sender);
        }

        void SoundPlay(object sender)
        {
            if (((SoundPlayer)sender).SoundLocation != "null.wav")
                ((SoundPlayer)sender).PlaySync();

            ((SoundPlayer)sender).SoundLocation = "null.wav";
        }

        private void pvp2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // �غ����� �� �׸���
            if (�̸����º�2 != null)
                �̸����º�2.�׷���(g);
        }

        private void txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_name.ReadOnly = true;
                txt_name.Enabled = false;
            }
        }

        private void frm_main_Move(object sender, EventArgs e)
        {
            min_x = this.Location.X;
            max_x = this.Location.X + this.Size.Width;

            min_y = this.Location.Y;
            max_y = this.Location.Y + this.Size.Height;
        }

        private void frm_main_LocationChanged(object sender, EventArgs e)
        {
            min_x = this.Location.X;
            max_x = this.Location.X + this.Size.Width;

            min_y = this.Location.Y;
            max_y = this.Location.Y + this.Size.Height;
        }
    }
}