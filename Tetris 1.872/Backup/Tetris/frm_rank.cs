using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class frm_rank : Form
    {
        class Rank
        {
            private string pName;
            //private byte pRank;
            private int pScore;
            private byte pLvl;
            private frm_main.난이도 pMode;
            private long pTotalScore;

            public string 이름
            {
                get
                {
                    return pName;
                }

                set
                {
                    pName = value;
                }
            }

            public int 점수
            {
                get
                {
                    return pScore;
                }

                set
                {
                    pScore = value;
                    pTotalScore = pScore * ((int)pMode + 1);
                }
            }

            public byte 렙
            {
                get
                {
                    return pLvl;
                }

                set
                {
                    pLvl = value;
                }
            }

            public byte 모드
            {
                get
                {
                    return (byte)pMode;
                }

                set
                {
                    pMode = (frm_main.난이도)value;
                    pTotalScore = pScore * ((int)pMode + 1);
                }
            }

            public long 최종가치
            {
                get
                {
                    return pTotalScore;
                }
            }

            public Rank()
            {
                pName = "";
                pScore = 0;
                pLvl = 0;
                pMode = 0;
                pTotalScore = 0;
            }

            public Rank(string 이름, int 점수, byte 렙, frm_main.난이도 모드)
            {
                pName = 이름;
                pScore = 점수;
                pLvl = 렙;
                pMode = 모드;
                pTotalScore = pScore * ((int)pMode + 1);
            }
        }

        Rank[] r5arr = new Rank[5];

        public frm_rank()
        {
            InitializeComponent();
        }

        internal void frm_rank_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            for (int i = 0; i < 5; i++)
            {
                r5arr[i] = new Rank();
            }

            로드();
        }

        internal void 로드()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ranking.dat", FileMode.OpenOrCreate, FileAccess.Read);
            GZipStream gzs = new GZipStream(fs, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(gzs);

            string 데이타 = string.Empty, strtmp = string.Empty, rank = string.Empty, name = string.Empty,
                score = string.Empty, lv = string.Empty, mode = string.Empty;

            try
            {
                데이타 = frm_main.DecryptString(sr.ReadToEnd());
            }
            catch 
            {
                for (int i = 0; i < 5; i++)
                {
                    r5arr[i] = new Rank();
                }
                return;
            }

            sr.Close();
            gzs.Close();
            fs.Close();

            char[] tmp = 데이타.ToCharArray();

            for (long i = 0; i < tmp.LongLength; i++) //문장해석루프
            {
                if (tmp[i] == ';') //세미콜론 감지
                {
                    strtmp += tmp[i].ToString(); //세미콜론도 마저 처리

                    //처리
                    if (strtmp.Contains("RANKDT-")) //맵크기 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(7).ToCharArray();
                        byte gotnum = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (gotnum == 0)
                                    name = stm2;
                                else if (gotnum == 1)
                                    score = stm2;
                                else if (gotnum == 2)
                                    lv = stm2;
                                else if (gotnum == 3)
                                    mode = stm2;
                                else if (gotnum == 4)
                                    rank = stm2;

                                gotnum++;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }
                    }

                    switch (rank)
                    {
                        case "1":
                            r5arr[0] = new Rank(name, int.Parse(score), byte.Parse(lv), ((frm_main.난이도)byte.Parse(mode)));
                            break;

                        case "2":
                            r5arr[1] = new Rank(name, int.Parse(score), byte.Parse(lv), ((frm_main.난이도)byte.Parse(mode)));
                            break;

                        case "3":
                            r5arr[2] = new Rank(name, int.Parse(score), byte.Parse(lv), ((frm_main.난이도)byte.Parse(mode)));
                            break;

                        case "4":
                            r5arr[3] = new Rank(name, int.Parse(score), byte.Parse(lv), ((frm_main.난이도)byte.Parse(mode)));
                            break;

                        case "5":
                            r5arr[4] = new Rank(name, int.Parse(score), byte.Parse(lv), ((frm_main.난이도)byte.Parse(mode)));
                            break;
                    }

                    strtmp = string.Empty; //tmp지움
                }
                else //세미콜론 나올때까지 문장해석진행
                    strtmp += tmp[i].ToString();
            }
            서열과표시대입();
        }

        void 서열과표시대입()
        {
            for (int i = 0; i < 5; i++)
            {
                if (r5arr[i] == null)
                    r5arr[i] = new Rank();
            }

            r1l.Text = r5arr[0].렙.ToString();
            r1m.Text = ((frm_main.난이도)r5arr[0].모드).ToString();
            r1n.Text = r5arr[0].이름.ToString();
            r1s.Text = r5arr[0].점수.ToString();

            r2l.Text = r5arr[1].렙.ToString();
            r2m.Text = ((frm_main.난이도)r5arr[1].모드).ToString();
            r2n.Text = r5arr[1].이름.ToString();
            r2s.Text = r5arr[1].점수.ToString();

            r3l.Text = r5arr[2].렙.ToString();
            r3m.Text = ((frm_main.난이도)r5arr[2].모드).ToString();
            r3n.Text = r5arr[2].이름.ToString();
            r3s.Text = r5arr[2].점수.ToString();

            r4l.Text = r5arr[3].렙.ToString();
            r4m.Text = ((frm_main.난이도)r5arr[3].모드).ToString();
            r4n.Text = r5arr[3].이름.ToString();
            r4s.Text = r5arr[3].점수.ToString();

            r5l.Text = r5arr[4].렙.ToString();
            r5m.Text = ((frm_main.난이도)r5arr[4].모드).ToString();
            r5n.Text = r5arr[4].이름.ToString();
            r5s.Text = r5arr[4].점수.ToString();
        }

        internal bool 랭킹등록(점수 s, frm_main.난이도 n, string name)
        {
            로드();

            for (byte i = 0; i < r5arr.Length; i++)
            {
                if (r5arr[i] == null)
                    r5arr[i] = new Rank();

                if (s.현재점수 * ((byte)n + 1) > r5arr[i].최종가치)
                {
                    switch (i)
                    {
                        case 0:
                            r5arr[4] = r5arr[3];
                            r5arr[3] = r5arr[2];
                            r5arr[2] = r5arr[1];
                            r5arr[1] = r5arr[0];
                            r5arr[0] = new Rank(name, s.현재점수, (byte)s.렙, n);
                            break;

                        case 1:
                            r5arr[4] = r5arr[3];
                            r5arr[3] = r5arr[2];
                            r5arr[2] = r5arr[1];
                            r5arr[1] = new Rank(name, s.현재점수, (byte)s.렙, n);
                            break;

                        case 2:
                            r5arr[4] = r5arr[3];
                            r5arr[3] = r5arr[2];
                            r5arr[2] = new Rank(name, s.현재점수, (byte)s.렙, n);
                            break;

                        case 3:
                            r5arr[4] = r5arr[3];
                            r5arr[3] = new Rank(name, s.현재점수, (byte)s.렙, n);
                            break;

                        case 4:
                            r5arr[4] = new Rank(name, s.현재점수, (byte)s.렙, n);
                            break;
                    }

                    서열과표시대입();
                    저장();

                    return true;
                }
            }

                return false;
        }

        void 저장()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ranking.dat",
                FileMode.OpenOrCreate, FileAccess.ReadWrite);
            GZipStream gzs = new GZipStream(fs, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gzs);

            string rettmp = string.Empty;

            for (byte f = 0; f < r5arr.Length; f++)
            {
                if (r5arr[f] == null)
                    r5arr[f] = new Rank();

                rettmp += string.Format("RANKDT-{0}:{1}:{2}:{3}:{4}:;",
                    r5arr[f].이름, r5arr[f].점수, r5arr[f].렙
                    , r5arr[f].모드, f + 1);
            }

            sw.Write(frm_main.EncryptString(rettmp));
            sw.Close();
            gzs.Close();
            fs.Close();
        }

        frm_main.난이도 텍스트를난이도로변환(string txt)
        {
            return ((frm_main.난이도)byte.Parse(txt));
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            저장();
            Program.Ifrm_main.자동일시중지끝();
            this.Hide();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                r5arr[i] = new Rank();
            }

            서열과표시대입();
            저장();
        }
    }
}
