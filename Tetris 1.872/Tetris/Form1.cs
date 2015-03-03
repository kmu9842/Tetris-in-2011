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
        internal enum 퓨즈Stat : byte
        {
            사용자포즈, 메뉴사용포즈, 저장및로드포즈, 실행중, 실행전, 최근상태
        };

        internal enum 사운드종류 : byte
        {
            게임시작, 드롭다운, 블록무브, 렙업, 게임오버, delOne, delTwo, delThree, delFour, 블록회전
        };

        const int 블록크기 = 20;

        const int 맵가로 = 10;
        const int 맵세로 = 20;

        //미리보기블럭(종류, 아이템)
        //맵상태(블럭위치, 아이템)
        //현재블럭(종류, 위치, 아이템)
        //설정(음악, 확률(아이템여부))
        //점수(속도, 렙, 점수)
        Queue<string> 기록;

        퓨즈Stat 현재퓨즈상태;
        퓨즈Stat 최근퓨즈상태;

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
                        if (게임시작했냐)
                        {
                            BlockDownTimer.Start();
                            FilledLineDelete.Start();
                        }
                        lbl_포즈.Visible = false;
                        break;

                    case 퓨즈Stat.사용자포즈:
                        if (현재퓨즈상태 == 퓨즈Stat.실행중)
                        {
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();
                            lbl_포즈.Visible = true;
                        }
                        break;

                    case 퓨즈Stat.최근상태:
                        현재퓨즈상태 = 최근퓨즈상태;
                        최근퓨즈상태 = 현재퓨즈상태;
                        return;
                        //break;

                    // 퓨즈Stat.저장및로드포즈
                    // 퓨즈Stat.메뉴사용포즈
                    default:
                        if (현재퓨즈상태 == 퓨즈Stat.사용자포즈 || 현재퓨즈상태 == 퓨즈Stat.실행중)
                        {
                            BlockDownTimer.Stop();
                            FilledLineDelete.Stop();
                        }
                        break;
                }

                if (최근퓨즈상태 != 현재퓨즈상태)
                  최근퓨즈상태 = 현재퓨즈상태;
                현재퓨즈상태 = value;
            }
        }

        /// <summary>
        /// 현재 내려오는 블럭
        /// </summary>
        private 블록컨테이너 현재블럭;

        /// <summary>
        /// 미리보기 화면에 나타나는 블럭
        /// </summary>
        private 블록컨테이너 미리보는블럭;

        private 컨테이너생성기 블록컨테이너생산기x;

        SoundPlayer[] SP;

        /// <summary>
        /// 점수기록
        /// </summary>
        private 점수 점수x;

        private bool 게임시작했냐;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 테트리스 배열 초기화
            맵.초기화(맵가로, 맵세로);

            // 폼 크기 초기화
            this.Size = new Size(맵가로 * 블록크기 + StatusPanel.Size.Width + 10 + 6,
                                 맵세로 * 블록크기 + 35 + MainMenu.Size.Height + 6);

            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            
            //메인판넬 초기화
            MainPanel.Size = new Size(맵가로 * 블록크기 + 6,
                                      맵세로 * 블록크기); //+ MainMenu.Size.Height);

            lbl_포즈.Location = new Point((MainPanel.Size.Width - lbl_포즈.Width) / 2, (Size.Height - lbl_포즈.Height) / 2);

            lbl_게임오버.Location = new Point((MainPanel.Size.Width - lbl_게임오버.Width) / 2, (Size.Height - lbl_게임오버.Height) / 2);

            StatusPanel.Location = new Point(MainPanel.Size.Width, StatusPanel.Location.Y);
            PreveiwPanel.Location = new Point(MainPanel.Size.Width, PreveiwPanel.Location.Y);

            //상태판넬 초기화
            StatusPanel.Size = new Size(this.Size.Width - MainPanel.Width, 
                                        맵세로 * 블록크기 + MainMenu.Size.Height);

            //상태판넬 초기화
            PreveiwPanel.Size = new Size(this.Size.Width - MainPanel.Width,
                                        PreveiwPanel.Size.Width);
            // 블럭 종류 초기화
            블록생성기.인스턴스내놔().이런타입의블록추가(new 노말블락(블록크기, 블록크기));
            블록생성기.인스턴스내놔().이런타입의블록추가(new 라인디스트로이아이템블럭(블록크기, 블록크기));
            블록생성기.인스턴스내놔().이런타입의블록추가(new 한개박살아이템블럭(블록크기, 블록크기));
            블록생성기.인스턴스내놔().노멀블록확률설정(90);

            // 블럭 컨테이너 종류 초기화
            블록컨테이너생산기x = new 컨테이너생성기();
            블록컨테이너생산기x.컨데이너타입추가(new ChairA모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new ChairB모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new Ah모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new 정사각형모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new Staff모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new StairsA모양블럭());
            블록컨테이너생산기x.컨데이너타입추가(new StairsB모양블럭());
            기록 = new Queue<string>();
            퓨즈상태 = 퓨즈Stat.실행전;
            최근퓨즈상태 = 현재퓨즈상태;
            SP = new SoundPlayer[4];
            for (int i = 0; i < 4; i++)
            {
                SP[i] = new SoundPlayer();
                SP[i].SoundLocationChanged += new EventHandler(this.SoundPSet);
            }
        }

        // 게임 초기화
        private void InitGame()
        {
            lbl_게임오버.Visible = false; //게임오버메시지 헤제

            맵.클려();
            MainPanel.Invalidate(); //블록안지워지는것 해결!!

            // 랜덤하게 컨테이너 생성
            int n = 0;
            미리보는블럭 = 블록컨테이너생산기x.랜덤컨테이너생성(out n);
            기록.Enqueue("미리보는블럭 = (" + n.ToString() + ");");
            
            //기록.Enqueue("미리보는블럭.생성();");
            미리보는블럭.생성();

            n = 0;
            현재블럭 = 블록컨테이너생산기x.랜덤컨테이너생성(out n);
            기록.Enqueue("현재블럭 = (" + n.ToString() + ");");

            //기록.Enqueue("현재블럭.생성();");
            현재블럭.생성();

            // 위치 초기화
            기록.Enqueue("미리보는블럭.연결고리위치설정(50, 70);");
            미리보는블럭.연결고리위치설정(50, 70);
            
            // 스코어 초기화
            점수x = new 점수();

            BlockDownTimer.Interval = 500;

            // 텍스트 초기화
            ScoreLabel.Text = "0";
            LevelLabel.Text = 점수x.렙구하기().ToString();
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";

            
            BlockDownTimer.Enabled = true;
            FilledLineDelete.Enabled = true;
            게임시작했냐 = true;
            퓨즈상태 = 퓨즈Stat.실행중;
            PlayS(사운드종류.게임시작);
            

            PreveiwPanel.Invalidate();
        }

        // 블럭이 자동으로 떨어지는 타이머 핸들러
        private void BlockDownTimer_Tick(object sender, EventArgs e)
        {
            if (!현재블럭.이동(0, 1))
            {
                PlayS(사운드종류.드롭다운);

                // 보드에 블럭 삽입
                if (!맵.블록추가(현재블럭.첫블록을내놔()))
                {
                    EndGame();
                    return;
                }
                // 블록 액션 수행
                //기록.Enqueue("현재블럭.액션수행();");
                현재블럭.액션수행();
                // 예비블럭가져옴
                기록.Enqueue("현재블럭 = 미리보는블럭;");
                현재블럭 = 미리보는블럭;
                기록.Enqueue("현재블럭.이동(0, 0);");
                현재블럭.이동(0, 0);
                MainPanel.Invalidate();

                //m_CurrentBlock
                // 랜덤하게 컨테이너 생성
                int ni = 0;
                미리보는블럭 = 블록컨테이너생산기x.랜덤컨테이너생성(out ni);
                기록.Enqueue("미리보는블럭 = (" + ni.ToString() + ");");

                //기록.Enqueue("미리보는블럭.생성();");
                미리보는블럭.생성();
                기록.Enqueue("미리보는블럭.연결고리위치설정(50, 70);");
                미리보는블럭.연결고리위치설정(50, 70);
                PreveiwPanel.Invalidate();
            }
            else
                MainPanel.Invalidate(GetRefreshRegion(현재블럭));
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
            if (미리보는블럭 != null)
                미리보는블럭.그려라(g);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // 게임중이 아니면 리턴
            if (!게임시작했냐)
                return;

            if (퓨즈상태 == 퓨즈Stat.실행중) //포즈안됬으면
            {
                switch (e.KeyCode)
                {
                    // 회전
                    case Keys.Up:
                        if (현재블럭.우로돌려())
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록회전);                            
                        }
                        break;
                    case Keys.Down:
                        if (현재블럭.이동(0, 1))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록무브);
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
                        if (현재블럭.이동(-1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록무브);
                        }
                        break;
                    case Keys.Right:
                        if (현재블럭.이동(1, 0))
                        {
                            MainPanel.Invalidate(GetRefreshRegion(현재블럭));
                            PlayS(사운드종류.블록무브);
                        }
                        break;
                    case Keys.Pause:
                            퓨즈상태 = 퓨즈Stat.사용자포즈;
                        break;
                }
            }
            else if(퓨즈상태 == 퓨즈Stat.사용자포즈) //포즈상태이면
            {
                switch (e.KeyCode)
                {
                    case Keys.Pause:
                            퓨즈상태 = 퓨즈Stat.실행중;
                        break;
                }
            }
        }

        // 빠르게 블록이 떨어질때의 타이머 핸들러
        private void FallingDownTimer_Tick(object sender, EventArgs e)
        {
            if (!현재블럭.이동(0, 1))
            {
                PlayS(사운드종류.드롭다운);

                // 보드에 블럭 삽입
                if (!맵.블록추가(현재블럭.첫블록을내놔()))
                {
                    EndGame();
                    return;
                }
                // 블록 액션 수행
                //기록.Enqueue("현재블럭.액션수행();");
                현재블럭.액션수행();
                // 예비블럭가져옴
                기록.Enqueue("현재블럭 = 미리보는블럭;");
                현재블럭 = 미리보는블럭;
                기록.Enqueue("현재블럭.이동(0, 0);");
                현재블럭.이동(0, 0);
                MainPanel.Invalidate();

                // 랜덤하게 컨테이너 생성
                int ni = 0;
                미리보는블럭 = 블록컨테이너생산기x.랜덤컨테이너생성(out ni);
                기록.Enqueue("미리보는블럭 = (" + ni.ToString() + ");");

                //기록.Enqueue("미리보는블럭.생성();");
                미리보는블럭.생성();
                미리보는블럭.연결고리위치설정(50, 70);
                PreveiwPanel.Invalidate();

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
            SpeedLabel.Text = BlockDownTimer.Interval.ToString() + "ms";
            ScoreLabel.Text = 점수x.점수내놔().ToString();
            PlayS(사운드종류.렙업);
        }

        private void EndGame()
        {
            기록.Clear();
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;
            맵.클려();
            현재블럭 = null;
            미리보는블럭 = null;
            PreveiwPanel.Invalidate();
            MainPanel.Invalidate();
            lbl_게임오버.Visible = true;
            게임시작했냐 = false;
            퓨즈상태 = 퓨즈Stat.실행전;
            PlayS(사운드종류.게임오버);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 게임 초기화
            기록.Clear();
            InitGame();
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
            메뉴사용시작();
            String str;

            str = "#조작법\n";
            str += "이동 : 좌우 방향키\n";
            str += "회전 : 위쪽 방향키\n";
            str += "한칸 내리기 : 아래쪽 방향키\n";
            str += "떨어뜨리기 : 스페이스 키\n\n";
            str += "#아이템 설명\n";
            str += "파랑블럭 : 일반 블럭\n";
            str += "녹색블럭 : 해당 블럭의 한칸아래 블럭을 제거한다.\n";
            str += "적색블럭 : 해당 블럭의 라인을 제거한다.\n\n";
            str += "Tetris Ver. 1.487 \n 2009. 9.5 ~ 10.17 \n 제작 : 중부영재교육원 Brain Development 팀";
            MessageBox.Show(str);

            메뉴사용끝();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            메뉴사용시작();
            saveFileDialog1.ShowDialog();
            메뉴사용끝();
        }

        private void loadtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            메뉴사용시작();
            openFileDialog1.ShowDialog();
            메뉴사용끝();
        }        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
                퓨즈상태 = 퓨즈Stat.저장및로드포즈;

            FileStream load = (FileStream)openFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(load, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(gzs);
            큐기록재현(맵.컨버트(DecryptString(sr.ReadToEnd(), SECURITYCODE)));
            sr.Close();
            gzs.Close();
            load.Close();

                퓨즈상태 = 퓨즈Stat.실행중;

                MainPanel.Invalidate();
                PreveiwPanel.Invalidate();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
                퓨즈상태 = 퓨즈Stat.저장및로드포즈;

            FileStream save = (FileStream)saveFileDialog1.OpenFile();
            GZipStream gzs = new GZipStream(save, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gzs);
            
            sw.Write(EncryptString(맵.컨버트(기록), SECURITYCODE));
            sw.Close();
            gzs.Close();
            save.Close();

                퓨즈상태 = 퓨즈Stat.실행중;
        }

        private void endgametoolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlockDownTimer.Enabled = false;
            FallingDownTimer.Enabled = false;
            FilledLineDelete.Enabled = false;
            맵.클려();
            현재블럭 = null;
            미리보는블럭 = null;
            MainPanel.Invalidate();
            PreveiwPanel.Invalidate();
            게임시작했냐 = false;
            퓨즈상태 = 퓨즈Stat.실행전;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void helpToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            메뉴사용시작();
        }

        private void helpToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            메뉴사용끝();
        }

        private void filetoolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            메뉴사용시작();
        }

        private void filetoolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            메뉴사용끝();
        }

        void 메뉴사용시작()
        {
                퓨즈상태 = 퓨즈Stat.메뉴사용포즈;
        }

        void 메뉴사용끝()
        {
                퓨즈상태 = 퓨즈Stat.최근상태;
        }

        void 큐기록재현(Queue<string> 데이터)
        {
            while (데이터.Count != 0)
            {
                
                switch (데이터.Dequeue())
                {
                    default:
                    break;
                }
            }
        }

        private void 설정CToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                {
                    LevelUp();
                }
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
            if (!Program.Muzik)
                return;

            switch (st)
            {
                case 사운드종류.게임시작:
                    SP[0].SoundLocation = "Gamestart.wav";
                    break;

                case 사운드종류.드롭다운:
                    SP[3].SoundLocation = "Dropdown.wav";
                    break;

                case 사운드종류.블록무브:
                    SP[3].SoundLocation = "Rotate.wav";
                    break;

                case 사운드종류.렙업:
                    SP[1].SoundLocation = "Levelup.wav";
                    break;

                case 사운드종류.게임오버:
                    SP[0].SoundLocation = "Gameover.wav";
                    break;

                case 사운드종류.delOne:
                    SP[2].SoundLocation = "One.wav";
                    break;

                case 사운드종류.delTwo:
                    SP[2].SoundLocation = "Two.wav";
                    break;

                case 사운드종류.delThree:
                    SP[2].SoundLocation = "Three.wav";
                    break;

                case 사운드종류.delFour:
                    SP[2].SoundLocation = "Four.wav";
                    break;

                case 사운드종류.블록회전:
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

        private void 설정디버그용ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            퓨즈상태 = 퓨즈Stat.메뉴사용포즈;
            Program.Ifrm_dbgset.Show();
        }
    }
}