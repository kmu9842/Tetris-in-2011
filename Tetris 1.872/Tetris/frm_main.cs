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
        internal bool 마우스일시정지 = true;

        private Win32.HookProcessProc MLL_proc, KLL_proc; //새로운 HookProcessProc (HookCallback포함) 
        private IntPtr MLL_hookID = IntPtr.Zero, KLL_hookID = IntPtr.Zero; //후크 ID
        int max_y = 0, max_x = 0, min_y = 0, min_x = 0; //bool mouseautopause = false;

        private void HookInit() { 
            MLL_hookID = Win32.SetHookAuto(MLL_proc, WH.MOUSE_LL); /*마우스후크ID를 MLL_hookID에 저장*/
            KLL_hookID = Win32.SetHookAuto(KLL_proc, WH.KEYBOARD_LL); /*키보드후크ID를 KLL_hookID에 저장*/
        }

        private void HookShutdown() { 
            Win32.UnhookWindowsHookEx(MLL_hookID); /*마우스언훅*/
            Win32.UnhookWindowsHookEx(KLL_hookID); /*키보드언훅*/ 
        }

        private IntPtr MLLHookCallback(int nCode, IntPtr wParam, IntPtr lParam) /*마우스후크 프로시저*/ {
            if (마우스일시정지)
            {
                if (nCode >= 0) //nCode Check?
                {
                    if (WM.MOUSEMOVE == (WM)wParam)
                    {
                        MSLLHOOKSTRUCT hS = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                        if (hS.pt.x < max_x && hS.pt.x > min_x && hS.pt.y < max_y && hS.pt.y > min_y)
                        {
                            if (퓨즈상태 == 퓨즈Stat.마우스포즈)
                            {
                                if (사용자퓨즈)
                                    퓨즈상태 = 퓨즈Stat.사용자포즈;
                                else
                                    퓨즈상태 = 퓨즈Stat.실행중;
                            }
                            else if (퓨즈상태 == 퓨즈Stat.더블포즈)
                                퓨즈상태 = 퓨즈Stat.자동포즈;
                        }
                        else
                        {
                            if (퓨즈상태 == 퓨즈Stat.실행중)
                                퓨즈상태 = 퓨즈Stat.마우스포즈;
                            else if (퓨즈상태 == 퓨즈Stat.자동포즈)
                                퓨즈상태 = 퓨즈Stat.더블포즈;
                        }
                        //return (IntPtr)1; //메시지먹기
                    }
                }
            }
            return Win32.CallNextHookEx(MLL_hookID, nCode, wParam, lParam); /*다음후크호출*/
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
        { //버그 예상지점
            PressedKeyFalse = false;

            if (!gKdownCur.Contains(kys))
                PressedKeyFalse = true;
        }

        List<Keys> ghKeylist = new List<Keys>();

        private IntPtr KLLHookCallback(int nCode, IntPtr wParam, IntPtr lParam) /*키보드후크 프로시저*/ {
            if (nCode >= 0) //nCode Check?
            {
                if (WM.KEYDOWN == (WM)wParam || WM.SYSKEYDOWN == (WM)wParam)
                { //버그예상지점
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
                { //버그예상지점
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
            return Win32.CallNextHookEx(MLL_hookID, nCode, wParam, lParam); /*다음후크호출*/
        }

        internal Audio BGM;
        internal bool BGMP = true;
        string BGMPATH;

        internal enum 퓨즈Stat : byte
        {
            사용자포즈, 자동포즈, 마우스포즈, 더블포즈, 실행중, 실행전, 자동일시정지해제
        };

        internal enum 사운드종류 : byte
        {
            게임시작, 드롭다운, 블록무브, 렙업, 게임오버, delOne, delTwo, delThree, delFour, 블록회전
        };

        internal enum 난이도 : byte
        {
            왕초보, 초보, 중수, 고수
        };

        internal const int 블록크기 = 20;

        const int 맵가로 = 10;
        const int 맵세로 = 20;

        internal 난이도 I난이도 = 0;

        internal Keys 포즈키;
        internal Keys 회전키;
        internal Keys 한칸내림키;
        internal Keys 끝까지내림키;
        internal Keys 왼쪽으로키;
        internal Keys 오른쪽으로키;

        퓨즈Stat 현재퓨즈상태;
        bool 사용자퓨즈;

        internal 퓨즈Stat 퓨즈상태
        {
            get
            {
                return 현재퓨즈상태;
            }
            set
            {
                switch (value)
                {
                    case 퓨즈Stat.실행전:
                        break;

                    case 퓨즈Stat.실행중:
                        사용자퓨즈 = false;

                        if(BGM != null && BGMP && BGM.State != StateFlags.Running)
                            BGM.Play();

                        if (게임시작했냐)
                        {
                            BlockDownTimer.Start();
                            FilledLineDelete.Start();
                            MFTimer.Start();
                        }
                        lbl_포즈.Visible = false;
                        break;

                    case 퓨즈Stat.사용자포즈:
                        BlockDownTimer.Stop();
                        FilledLineDelete.Stop();
                        MFTimer.Stop();
                        if(BGM != null)
                        BGM.Pause();
                        lbl_포즈.Visible = true;
                        사용자퓨즈 = true;
                        현재퓨즈상태 = 퓨즈Stat.실행중;
                        return;

                    case 퓨즈Stat.자동일시정지해제:
                        if (퓨즈상태 == 퓨즈Stat.더블포즈)
                        {
                            퓨즈상태 = 퓨즈Stat.마우스포즈;
                        }
                        else if (현재퓨즈상태 == 퓨즈Stat.자동포즈)// || 현재퓨즈상태 == 퓨즈Stat.마우스포즈)
                        {
                            if (사용자퓨즈)
                                퓨즈상태 = 퓨즈Stat.사용자포즈;
                            else
                                퓨즈상태 = 퓨즈Stat.실행중;
                        }
                        return;

                    case 퓨즈Stat.마우스포즈:
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();

                            if (BGM != null)
                                BGM.Pause();

                            MFTimer.Stop();

                        if (현재퓨즈상태 == 퓨즈Stat.자동포즈)
                        {
                            현재퓨즈상태 = 퓨즈Stat.더블포즈;
                            return;
                        }
                        break;

                    case 퓨즈Stat.자동포즈:
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();

                            if (BGM != null)
                                BGM.Pause();

                            MFTimer.Stop();

                        if (현재퓨즈상태 == 퓨즈Stat.마우스포즈)
                        {
                            현재퓨즈상태 = 퓨즈Stat.더블포즈;
                            return;
                        }
                        break;
                }

                현재퓨즈상태 = value;
            }
        }

        /// <summary>
        /// 현재 내려오는 블럭
        /// </summary>
        internal 블록컨테이너 현재블럭;
        internal int 현재블럭타입;

        /// <summary>
        /// 미리보기 화면에 나타나는 블럭
        /// </summary>
        internal 블록컨테이너 미리보는블럭1;
        internal int 미리보는블럭1타입;

        /// <summary>
        /// 미리보기 화면에 나타나는 블럭2
        /// </summary>
        internal 블록컨테이너 미리보는블럭2;
        internal int 미리보는블럭2타입;

        internal 컨테이너생성기 블록컨테이너생산기x;

        SoundPlayer[] SP;

        /// <summary>
        /// 점수//기록
        /// </summary>
        internal 점수 점수x;
        internal int BDTI;

        private bool 게임시작했냐;

        public frm_main()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            포즈키 = Keys.Pause;
            회전키 = Keys.Up;
            한칸내림키 = Keys.Down;
            끝까지내림키 = Keys.Space;
            왼쪽으로키 = Keys.Left;
            오른쪽으로키 = Keys.Right;
            BGMPATH = null;
            사용자퓨즈 = false;
            MLL_proc = MLLHookCallback;
            KLL_proc = KLLHookCallback;

            ghKeylist.Add(Keys.LShiftKey); //게임숨김키 미리정의
            ghKeylist.Add(Keys.LControlKey);
            ghKeylist.Add(Keys.LMenu);
        }

        internal void 인터페이스자동배열(int 맵가로, int 맵세로)
        {
            //폼크기 제한해제
            this.MinimumSize = new Size(0, 0);
            this.MaximumSize = new Size(0, 0);
            
            // 폼 크기 초기화
            //this.Size = new Size(맵가로 * 블록크기 + StatusPanel.Size.Width + 10 + 6,
            //                     맵세로 * 블록크기 + 35 + MainMenu.Size.Height + 6);
            if (맵가로 >= 10 && 맵세로 >= 20)
                this.Size = new Size(맵가로 * 블록크기 + StatusPanel.Size.Width + 15,
                                     맵세로 * 블록크기 + MainMenu.Size.Height + 40);
            else
                this.Size = new Size(10 * 블록크기 + StatusPanel.Size.Width + 15,
                                     20 * 블록크기 + MainMenu.Size.Height + 40);

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            //메인판넬 초기화
            MainPanel.Size = new Size(맵가로 * 블록크기 + 6,
                                      맵세로 * 블록크기); //+ MainMenu.Size.Height);

            lbl_포즈.Location = new Point((MainPanel.Size.Width - lbl_포즈.Width) / 2, (Size.Height - lbl_포즈.Height) / 2);

            lbl_게임오버.Location = new Point((MainPanel.Size.Width - lbl_게임오버.Width) / 2,
                (Size.Height - lbl_게임오버.Height) / 2);

            pvp1.Location = new Point(MainPanel.Size.Width, MainMenu.Size.Height);
            pvp2.Location = new Point(MainPanel.Size.Width, MainMenu.Size.Height + pvp1.Size.Height);
            StatusPanel.Location = new Point(MainPanel.Size.Width, pvp1.Location.Y + pvp1.Size.Height + pvp2.Size.Height);

            //상태판넬 초기화
            StatusPanel.Size = new Size(StatusPanel.Size.Width,
                                        this.Size.Height - MainMenu.Size.Height - pvp1.Size.Height);

            //상태판넬 초기화
            pvp1.Size = new Size(this.Size.Width - MainPanel.Width, pvp1.Size.Height);
            pvp2.Size = new Size(this.Size.Width - MainPanel.Width, pvp2.Size.Height);

            min_x = this.Location.X;
            max_x = this.Location.X + this.Size.Width;

            min_y = this.Location.Y;
            max_y = this.Location.Y + this.Size.Height;
        }

        internal void 난이도설정(난이도 nid)
        {
            pvp2.Visible = true;
            pvp1.Visible = true;

            if(BGM != null)
                BGM.Stop();

            BGMPATH = null;

            switch (nid)
            {
                case 난이도.왕초보:
                    BDTI = BlockDownTimer.Interval = 800;
                    break;

                case 난이도.초보:
                    BDTI = BlockDownTimer.Interval = 650;
                    break;

                case 난이도.중수:
                    BDTI = BlockDownTimer.Interval = 500;
                    pvp2.Visible = false;
                    break;

                case 난이도.고수:
                    BDTI = BlockDownTimer.Interval = 400;
                    pvp2.Visible = false;
                    pvp1.Visible = false;
                    break;

               /* case 난이도.본좌:
                    BDTI = BlockDownTimer.Interval = 400;
                    pvp2.Visible = false;
                    pvp1.Visible = false;
                    break;*/

            }
            Program.Ifrm_main.I난이도 = nid;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 테트리스 배열 초기화
            맵.초기화(맵가로, 맵세로);

            인터페이스자동배열(맵가로, 맵세로);
            
            // 블럭 종류 초기화
            블록생성기.인스턴스내놔().이런타입의블록추가(new 노말블락(블록크기, 블록크기));
            블록생성기.인스턴스내놔().이런타입의블록추가(new 라인디스트로이아이템블럭(블록크기, 블록크기));
            블록생성기.인스턴스내놔().이런타입의블록추가(new 한개박살아이템블럭(블록크기, 블록크기));
            블록생성기.인스턴스내놔().일반블록생성확률 = 90;

            // 블럭 컨테이너 종류 초기화
            블록컨테이너생산기x = new 컨테이너생성기();
            블록컨테이너생산기x.컨데이너타입추가(new ChairA모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new ChairB모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new Ah모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new 정사각형모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new Staff모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new StairsA모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new StairsB모양블럭());


            퓨즈상태 = 퓨즈Stat.실행전;
            사용자퓨즈 = false;
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
                        MessageBox.Show("오늘은 설날!\n새뱃돈 많이 벌어라.");
                    break;

                case 2:
                    break;

                case 3:
                    if (DateTime.Now.Day == 1)
                        MessageBox.Show("오늘은 삼일절..\n태극기를 답시다.");
                    else if (DateTime.Now.Day == 3)
                        MessageBox.Show("오늘은 납세자의 날..\n탈세하지말자.");
                    break;

                case 4:
                    if (DateTime.Now.Day == 21)
                        MessageBox.Show("오늘은 과학의 날");
                    else if (DateTime.Now.Day == 22)
                        MessageBox.Show("오늘은 정보통신의 날");
                    break;

                case 5:
                    if (DateTime.Now.Day == 5)
                        MessageBox.Show("오늘은 어린이날");
                    else if (DateTime.Now.Day == 19)
                        MessageBox.Show("오늘은 발명의 날..\n좋은 아이디어를 떠올려보자.");
                    break;

                case 6:
                    if (DateTime.Now.Day == 6)
                        MessageBox.Show("오늘은 현충일");
                    break;

                case 7:
                    break;

                case 8:
                    if (DateTime.Now.Day == 15)
                        MessageBox.Show("오늘은 광복절");
                    break;

                case 9:
                    if (DateTime.Now.Day == 3)
                        MessageBox.Show("오늘은 임정원 생일..\n생일빵사절..");
                    break;

                case 10:
                    if (DateTime.Now.Day == 3)
                        MessageBox.Show("오늘은 추석!\n용돈좀..");
                    else if (DateTime.Now.Day == 17)
                        MessageBox.Show("오늘은 산출물 대회 예선");
                    break;

                case 11:
                    break;

                case 12:
                    if (DateTime.Now.Day == 25)
                        MessageBox.Show("오늘은 크리스마스!\n용돈좀..");
                    break;

                default:
                    break;
            }

            HookInit();
        }

        // 게임 초기화
        internal void InitGame()
        {
            this.포즈키 = Program.Ifrm_set.포즈키;
            this.끝까지내림키 = Program.Ifrm_set.끝까지내림키;
            this.회전키 = Program.Ifrm_set.회전키;
            this.한칸내림키 = Program.Ifrm_set.한칸내림키;
            this.왼쪽으로키 = Program.Ifrm_set.왼쪽으로키;
            this.오른쪽으로키 = Program.Ifrm_set.오른쪽으로키;

            lbl_게임오버.Visible = false; //게임오버메시지 헤제

            맵.클려();
            MainPanel.Invalidate(); //블록안지워지는것 해결!!

            // 랜덤하게 컨테이너 생성
            미리보는블럭2 = 블록컨테이너생산기x.랜덤컨테이너생성(out 미리보는블럭2타입);
            미리보는블럭2.생성();
            미리보는블럭1 = 블록컨테이너생산기x.랜덤컨테이너생성(out 미리보는블럭1타입);
            미리보는블럭1.생성();

            //현재블럭도 랜덤하게
            현재블럭 = 블록컨테이너생산기x.랜덤컨테이너생성(out 현재블럭타입);
            현재블럭.생성();

            // 위치 초기화
            미리보는블럭2.연결고리위치설정(50, 70);
            미리보는블럭1.연결고리위치설정(50, 70);
            
            // 스코어 초기화
            점수x = new 점수();

            // 텍스트 초기화
            ScoreLabel.Text = "0";
            LevelLabel.Text = 점수x.렙구하기().ToString();
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";

            
            BlockDownTimer.Enabled = true;
            FilledLineDelete.Enabled = true;
            게임시작했냐 = true;
            퓨즈상태 = 퓨즈Stat.실행중;
            PlayS(사운드종류.게임시작);

            pvp1.Invalidate();
            pvp2.Invalidate();
        }

        // 블럭이 자동으로 떨어지는 타이머 핸들러
        private void BlockDownTimer_Tick(object sender, EventArgs e)
        { //이름입력하고 엔터누르면 오류... 바로아래줄에서
            if (!현재블럭.이동(0, 1)) //현재블럭이 바닥에 떨어지면
            {
                PlayS(사운드종류.드롭다운);

                if (!맵.블록추가(현재블럭.첫블록을내놔()))
                {
                    EndGame();
                    return;
                }
                현재블럭.액션수행(); //액션!
                현재블럭 = 미리보는블럭1;//미리보는 블럭이 현재블럭으로
                미리보는블럭1 = 미리보는블럭2;//미리보는 블럭2가 미리보는 블럭으로
                현재블럭.이동(0, 0); //새블럭 배치
                MainPanel.Invalidate(); //새로고침

                // 랜덤하게 컨테이너 생성
                미리보는블럭2 = 블록컨테이너생산기x.랜덤컨테이너생성(out 미리보는블럭2타입);
                미리보는블럭2.생성();
                미리보는블럭2.연결고리위치설정(50, 70);
                pvp1.Invalidate(); //새로고침
                pvp2.Invalidate(); //새로고침
            }
            else //안떨어졌으면
                MainPanel.Invalidate(GetRefreshRegion(현재블럭)); //새로고침
        }

        // 메인 화면 그리기
        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // 현재 내려오는 블럭 그리기
            if(현재블럭 != null)
                현재블럭.그려라(g);

            // 보드 그리기
            맵.그려(g);
        }

        // 블럭 미리보기 패널 그리기
        private void PreveiwPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 준비중인 블럭 그리기
            if (미리보는블럭1 != null)
                미리보는블럭1.그려라(g);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // 게임중이 아니면 리턴
            if (!게임시작했냐)
                return;

            if (퓨즈상태 == 퓨즈Stat.실행중) //포즈안됬으면
            {
                if (!사용자퓨즈)
                {
                    if (e.KeyCode == 회전키)
                    {
                        if (현재블럭.우로돌려())
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록회전);
                        }
                    }
                    else if (e.KeyCode == 한칸내림키)
                    {
                        if (현재블럭.이동(0, 1))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록무브);
                        }
                    }
                    else if (e.KeyCode == 끝까지내림키)
                    {
                        if (!FallingDownTimer.Enabled)
                        {
                            BlockDownTimer.Enabled = false;
                            FallingDownTimer.Enabled = true;
                        }
                    }
                    else if (e.KeyCode == 왼쪽으로키)
                    {
                        if (현재블럭.이동(-1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록무브);
                        }
                    }
                    else if (e.KeyCode == 오른쪽으로키)
                    {
                        if (현재블럭.이동(1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록무브);
                        }
                    }
                }
                if (e.KeyCode == 포즈키) //포즈상태이면
                {
                    if (사용자퓨즈)
                    {
                        if (퓨즈상태 == 퓨즈Stat.실행중)
                        {
                            퓨즈상태 = 퓨즈Stat.실행중;
                            사용자퓨즈 = false;
                        }
                        else
                            사용자퓨즈 = false;
                    }
                    else
                        퓨즈상태 = 퓨즈Stat.사용자포즈;
                }
            }
        }

        // 빠르게 블록이 떨어질때의 타이머 핸들러
        private void FallingDownTimer_Tick(object sender, EventArgs e)
        {
            if (!현재블럭.이동(0, 1)) //블럭이 바닥에 닿으면
            {
                PlayS(사운드종류.드롭다운);

                // 보드에 블럭 삽입
                if (!맵.블록추가(현재블럭.첫블록을내놔())) //실패시 게임오버
                {
                    EndGame();
                    return;
                }

                현재블럭.액션수행(); //액션!
                현재블럭 = 미리보는블럭1;//미리보는 블럭이 현재블럭으로
                미리보는블럭1 = 미리보는블럭2;//미리보는 블럭2가 미리보는 블럭으로
                현재블럭.이동(0, 0); //새블럭 배치
                MainPanel.Invalidate(); //새로고침

                // 랜덤하게 컨테이너 생성
                미리보는블럭2 = 블록컨테이너생산기x.랜덤컨테이너생성(out 미리보는블럭2타입);
                미리보는블럭2.생성();
                미리보는블럭2.연결고리위치설정(50, 70);
                pvp1.Invalidate(); //새로고침
                pvp2.Invalidate(); //새로고침

                FallingDownTimer.Enabled = false;
                BlockDownTimer.Enabled = true;
            }
            else
                MainPanel.Invalidate(GetRefreshRegion(현재블럭));
        }

        // 레벨업시 상태창 업데이트
        private void LevelUp()
        {
            int level = 점수x.렙구하기();
            LevelLabel.Text = level.ToString();
            BlockDownTimer.Interval -= (BlockDownTimer.Interval / (level + 2));
            BDTI = BlockDownTimer.Interval;
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";
            ScoreLabel.Text = 점수x.점수내놔().ToString();
            PlayS(사운드종류.렙업);
        }

        private void EndGame()
        {
            this.포즈키 = Program.Ifrm_set.포즈키;
            this.끝까지내림키 = Program.Ifrm_set.끝까지내림키;
            this.회전키 = Program.Ifrm_set.회전키;
            this.한칸내림키 = Program.Ifrm_set.한칸내림키;
            this.왼쪽으로키 = Program.Ifrm_set.왼쪽으로키;
            this.오른쪽으로키 = Program.Ifrm_set.오른쪽으로키;
            if (BGM != null)
            {
                BGM.Stop();
                BGM = null;
            }
            BGMPATH = null;
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;

            //랭킹
            if (Program.Ifrm_rank.랭킹등록(점수x, I난이도, txt_name.Text))
                MessageBox.Show("축하합니다. " + txt_name.Text + "님\n명예의 전당에 등록되셨습니다.", "테트리스 랭킹");

            Program.Ifrm_rank.Show();

            맵.클려();
            현재블럭 = null;
            미리보는블럭1 = null;
            미리보는블럭2 = null;
            pvp1.Invalidate();
            pvp2.Invalidate();
            MainPanel.Invalidate();
            lbl_게임오버.Visible = true;
            게임시작했냐 = false;
            퓨즈상태 = 퓨즈Stat.실행전;
            PlayS(사운드종류.게임오버);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            자동일시중지();
            Program.Ifrm_newgame.Show();
            
            // 게임 초기화
            //InitGame();
        }

        // 블럭의 리프레쉬 영역을 구한다 
        private Rectangle GetRefreshRegion(블록컨테이너 cont)
        {
            Rectangle rc = cont.블록영역내놔();
            rc.Width = 블록크기 * 8;
            rc.Height = 블록크기 * 8;
            rc.X -= 블록크기 * 2;
            rc.Y -= 블록크기 * 2;

            return rc;
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            자동일시중지();
            string str;

            str = "#조작법\n";
            str += "이동 : 좌우 방향키\n";
            str += "회전 : 위쪽 방향키\n";
            str += "한칸 내리기 : 아래쪽 방향키\n";
            str += "떨어뜨리기 : 스페이스 키\n";
            str += "일시정지 해제 : 일시정지 상태에서 Pause/Brk 키\n";
            str += "일시정지 : Pause/Brk 키\n\n";
            str += "#아이템 설명\n";
            str += "파랑블럭 : 일반 블럭\n";
            str += "녹색블럭 : 해당 블럭의 한칸아래 블럭을 제거한다.\n";
            str += "적색블럭 : 해당 블럭의 라인을 제거한다.\n\n";
            str += "#기능 설명\n";
            str += "블럭 떨어지는 속도도 2배가 됩니다.\n";
            str += "저장 기능 : 게임을 저장했다가 나중에 계속할 수 있습니다.\n";
            str += "설정 : 아이템블럭과 소리를 끄고 켤수 있습니다.\n";
            str += "자동 일시정지 기능 : 게임이 진행중일때 메뉴를 클릭하면 자동으로 포즈가 적용됩니다.\n";
            str += "자동 일시정지 기능 2 : 마우스 커서를 게임 창 밖으로 이동시키면 자동으로 포즈가 적용됩니다.\n\n";
            str += "Tetris Ver. 1.872\n 2009. 9.5 ~ \n 제작 : 중부영재교육원 Brain Development 팀";
            
            MessageBox.Show(str);
            자동일시중지끝();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            자동일시중지();
            if (!게임시작했냐)
            {
                MessageBox.Show("게임오버시에는 저장할수 없습니다...\n저장 못하게하는 이유를 안다면 당신은 천재");
                return;
            }

            saveFileDialog1.ShowDialog();
            자동일시중지끝();
        }

        private void loadtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            자동일시중지();
            openFileDialog1.ShowDialog();
            자동일시중지끝();
        }        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream load = (FileStream)openFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(load, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(gzs);
            맵.컨버트(DecryptString(sr.ReadToEnd()));
            sr.Close();
            gzs.Close();
            load.Close();

            인터페이스자동배열(맵.가로크기, 맵.세로크기);

            lbl_게임오버.Visible = false; //게임오버메시지 헤제
            BlockDownTimer.Interval = BDTI;
            게임시작했냐 = true;
            int level = 점수x.렙구하기();
            LevelLabel.Text = level.ToString();
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";
            ScoreLabel.Text = 점수x.점수내놔().ToString();
            MainPanel.Invalidate();
            pvp1.Invalidate();

            퓨즈상태 = 퓨즈Stat.사용자포즈;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream save = (FileStream)saveFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(save, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gzs);
            
            sw.Write(EncryptString(맵.컨버트()));
            sw.Close();
            gzs.Close();
            save.Close();
        }

        private void endgametoolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;
            맵.클려();
            현재블럭 = null;
            미리보는블럭1 = null;
            MainPanel.Invalidate();
            pvp1.Invalidate();
            게임시작했냐 = false;
            퓨즈상태 = 퓨즈Stat.실행전;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            HookShutdown();
            Process.GetCurrentProcess().Kill();
        }

        private void helpToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            자동일시중지();
        }

        private void helpToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            자동일시중지끝();
        }

        private void filetoolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            자동일시중지();
        }

        private void filetoolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            자동일시중지끝();
        }

        internal void 자동일시중지()
        {
            퓨즈상태 = 퓨즈Stat.자동포즈;
        }

        internal void 자동일시중지끝()
        {
            퓨즈상태 = 퓨즈Stat.자동일시정지해제;
        }

        private void 설정CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            자동일시중지();
            Program.Ifrm_set.Show();
        }

        internal void FilledLineDelete_Tick(object sender, EventArgs e)
        {
            // 지울 라인이 있는지 검사
            int n = 맵.다_찬_라인_다지워();

            // 점수 계산
            if (n != 0)
            {
                if (점수x.한줄클리어(n))
                    LevelUp();
                ScoreLabel.Text = 점수x.점수내놔().ToString();

                switch (n)
                {
                    case 1:
                        PlayS(사운드종류.delOne);
                        break;

                    case 2:
                        PlayS(사운드종류.delTwo);
                        break;

                    case 3:
                        PlayS(사운드종류.delThree);
                        break;

                    default:
                        PlayS(사운드종류.delFour);
                        break;
                }

                MainPanel.Invalidate();
            }
            //else
            //{
            //    if (점수x.블록추락()) //사용하지 않음
            //        LevelUp();
            //}            
        }

        internal void PlayS(사운드종류 st)
        {
            if (!Program.SoundEffect)
                return;

            string curL = Application.StartupPath + "\\";

            switch (st)
            {
                case 사운드종류.게임시작:
                    SP[0].SoundLocation = curL + "Gamestart.wav";
                    break;

                case 사운드종류.드롭다운:
                    SP[3].SoundLocation = curL + "Dropdown.wav";
                    break;

                case 사운드종류.블록무브:
                    SP[3].SoundLocation = curL + "Rotate.wav";
                    break;

                case 사운드종류.렙업:
                    SP[1].SoundLocation = curL + "Levelup.wav";
                    break;

                case 사운드종류.게임오버:
                    SP[0].SoundLocation = curL + "Gameover.wav";
                    break;

                case 사운드종류.delOne:
                    SP[2].SoundLocation = curL + "One.wav";
                    break;

                case 사운드종류.delTwo:
                    SP[2].SoundLocation = curL + "Two.wav";
                    break;

                case 사운드종류.delThree:
                    SP[2].SoundLocation = curL + "Three.wav";
                    break;

                case 사운드종류.delFour:
                    SP[2].SoundLocation = curL + "Four.wav";
                    break;

                case 사운드종류.블록회전:
                    SP[3].SoundLocation = curL + "Rotate.wav";
                    break;

            }
        }

        private void SoundPSet(object sender, EventArgs e)
        {
            Thread playthread = new Thread(new ParameterizedThreadStart(SoundPlay));
            //스레드를 사용해 재생하기때문에 소리 끊김과 렉을 최소화할수이있음
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

            // 준비중인 블럭 그리기
            if (미리보는블럭2 != null)
                미리보는블럭2.그려라(g);
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