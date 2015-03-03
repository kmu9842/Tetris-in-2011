using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public static class 맵
    {
        /// <summary>
        /// 블록들의 2차원 배열
        /// </summary>
        public static Tetris.블록[][] 블록매트릭스;
        
        /// <summary>
        /// 맵의 가로크기
        /// </summary>
        public static int 가로크기;

        /// <summary>
        /// 맵의 가로크기 
        /// </summary>
        public static int 세로크기;

        /// <summary>
        /// 맵 초기화 하기 
        /// </summary>
        /// <param name="x">초기화 할 맵의 가로크기</param>
        /// <param name="y">초기화 할 맵의 세로크기</param>
        public static void 초기화(int x, int y)
        {
            가로크기 = x; //가로크기 설정
            세로크기 = y; //세로크기 설정

            //블록매트릭스 생성
            블록매트릭스 = new 블록[가로크기][]; //가로크기 기준 2차원가변배열선언
            for(int i = 0; i < x; i++) //for문으로 루프
            {
                블록매트릭스[i] = new 블록[세로크기]; //가로하나당 세로배열생성
                for(int j = 0; j < y; j++) //for문으로 루프
                {
                    블록매트릭스[i][j] = null; //null로 초기화
                }
            }
            //블록매트릭스 생성끝
        }

        /// <summary>
        /// 맵에 블럭 추가
        /// </summary>
        /// <param name="block">추가할블록</param>
        /// <returns>블록추가 성공여부</returns>
        public static bool 블록추가(블록 block)
        {
            블록 pBlock = block; //블록을 선언 및 저장

            while (pBlock != null) //블록이 null이 아니면
            {
                Point point = pBlock.좌표; //좌표 선언 및 저장
                if(point.Y < 0) //Y좌표가 0이하일때
                    return false; //실패
                블록매트릭스[point.X][point.Y] = pBlock; //아니면 블럭배열의 정해진위치에 블럭저장
                
                pBlock = pBlock.다음블럭내놔(); //선언한 블록을 그 블록의 다음블록으로 대체
            }

            return true; //성공
        }

        /// <summary>
        /// 블럭을 한개 지운뒤 윗블럭 내림
        /// </summary>
        /// <param name="x">가로 좌표</param>
        /// <param name="y">세로 좌표</param>
        public static void 블럭_한개_지우기(int x, int y)
        {
            try
            {
                블록매트릭스[x][y] = null; //블럭삭제를 시도
            }

            catch //오류시
            {
                return; //리턴
            }

            // 윗라인 끌어내리기 원본
            for (int j = y; j > 0; j--)
            {
                if (블록매트릭스[x][j - 1] != null)
                    블록매트릭스[x][j - 1].혼자움직여(0, 1);
                블록매트릭스[x][j] = 블록매트릭스[x][j - 1];
                블록매트릭스[x][j - 1] = null;
            }

            // 윗라인 끌어내리기
            //for (int j = 세로크기; j > 0; j--) //위의라인순환
            //{
            //    if (블록매트릭스[x][j - 1] != null) //아래블럭이 비어있는한
            //        //if (블록매트릭스[x][j + 1] != null) //아래블럭이 비어있는한
            //        블록매트릭스[x][j].혼자움직여(0, 1); //내려가

            //    //블록매트릭스[x][j] = 블록매트릭스[x][j - 1];
            //    //블록매트릭스[x][j - 1] = null;
            //}
        }

        /// <summary>
        /// 가로로 한줄지움
        /// </summary>
        /// <param name="y">세로 좌표</param>
        public static void 한줄지움(int y)
        {
            // 라인 지우기
            for(int x = 0; x < 가로크기; x++)
            {
                블록매트릭스[x][y] = null; // null처리
                //try
                //{
                //    //블록매트릭스[x][y].액션();
                //}
                //catch { }
            }

            // 윗라인 끌어내리기
            for (int j = y; j > 0; j--)
            {
                for (int i = 0; i < 가로크기; i++)
                {
                    if (블록매트릭스[i][j - 1] != null)
                        블록매트릭스[i][j - 1].혼자움직여(0, 1);
                    블록매트릭스[i][j] = 블록매트릭스[i][j - 1];
                    블록매트릭스[i][j - 1] = null;
                }
            }
        }

        /// <summary>
        /// 가득찬 라인 찾아서 다 지움
        /// </summary>
        /// <returns>몇개 지웠나</returns>
        public static int 다_찬_라인_다지워()
        {
            int delCount = 0;
            for(int i = 세로크기 - 1; i >= 0; i--) //for문으로 루프
            {
                if (라인꽉찼나(i)) //꽉찬라인검사
                {
                    한줄지움(i); //한줄지움
                    delCount++; //갯수증가
                    i++; //i증가
                }
            }

            return delCount; //갯수증가
        }

        /// <summary>
        /// 라인 다 찼는지 검사
        /// </summary>
        /// <param name="y">세로 좌표</param>
        /// <returns>라인 다 찼는지</returns>
        private static bool 라인꽉찼나(int y)
        {
            for (int x = 0; x < 가로크기; x++) //루프
            {
                if (블록매트릭스[x][y] == null) //빈것이 하나라도 있으면
                    return false; //거짓
            }

            return true; //꽉찼으면 참
        }

        /// <summary>
        /// 해당 좌표가 맵을 벗어났는지 검사
        /// </summary>
        /// <param name="x">가로 좌표</param>
        /// <param name="y">세로 좌표</param>
        /// <returns>비었나</returns>
        public static bool 이거비었냐(int x, int y)
        {
            if (y < 0)
                return true;

            if (블록매트릭스[x][y] == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 좌표가 맵 범위안인가
        /// </summary>
        /// <param name="x">가로 좌표</param>
        /// <param name="y">세로 좌표</param>
        /// <returns>맵안좌표냐</returns>
        public static bool 맵안좌표냐(int x, int y)
        {
            if (x < 가로크기 && x >= 0 && y < 세로크기) //맵안인지검사
                return true; //리턴
            else
                return false; //리턴
        }

        /// <summary>
        /// 보드의 모든 블럭 그리기
        /// </summary>
        /// <param name="g">그려야할 그래픽</param>
        public static void 그려(Graphics g)
        {
            for (int i = 0; i < 가로크기; i++)
            {
                for (int j = 0; j < 세로크기; j++)
                {
                    if (블록매트릭스[i][j] != null) //null이 아니면
                    {
                        블록매트릭스[i][j].그려라(g); //그림
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void 클려()
        {
            for (int i = 0; i < 가로크기; i++)
            {
                for (int j = 0; j < 세로크기; j++)
                {
                    블록매트릭스[i][j] = null;
                }
            }
        }

        /// <summary>
        /// 데이터를 맵에 로드
        /// 문자열->맵데이터
        /// </summary>
        /// <param name="데이타">로드할 데이터</param>
        public static void 컨버트(string 데이타)
        {
            클려();
            string strtmp = string.Empty;
            char[] tmp = 데이타.ToCharArray();

            for (long i = 0; i < tmp.LongLength; i++) //문장해석루프
            {
                if (tmp[i] == ';') //세미콜론 감지
                {
                    strtmp += tmp[i].ToString(); //세미콜론도 마저 처리

                    //처리
                    #region MAPSIZE
                    if (strtmp.Contains("MAPSIZE-")) //맵크기 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(8).ToCharArray();
                        bool gotone = false;
                        int x = 0, y = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (!gotone)
                                   x = int.Parse(stm2);
                                else
                                    y = int.Parse(stm2);
                                gotone = true;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }

                        초기화(x, y);
                        클려();
                    }
                    #endregion
                    #region KEYSETTING
                    else if (strtmp.Contains("KEYSETTING-")) //세팅 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(11).ToCharArray();
                        byte gotnum = 0;
                        int pau = 0, toend = 0, rota = 0, left = 0, right = 0, oned = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (gotnum == 0)
                                    pau = int.Parse(stm2);
                                else if (gotnum == 1)
                                    toend = int.Parse(stm2);
                                else if (gotnum == 2)
                                    rota = int.Parse(stm2);
                                else if (gotnum == 3)
                                    oned = int.Parse(stm2);
                                else if (gotnum == 4)
                                    left = int.Parse(stm2);
                                else if (gotnum == 5)
                                    right = int.Parse(stm2);
                                
                                gotnum++;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_set.포즈키 = Program.Ifrm_main.포즈키 = (Keys)pau;
                        Program.Ifrm_set.끝까지내림키 = Program.Ifrm_main.끝까지내림키 = (Keys)toend;
                        Program.Ifrm_set.회전키 = Program.Ifrm_main.회전키 = (Keys)rota;
                        Program.Ifrm_set.한칸내림키 = Program.Ifrm_main.한칸내림키 = (Keys)oned;
                        Program.Ifrm_set.왼쪽으로키 = Program.Ifrm_main.왼쪽으로키 = (Keys)left;
                        Program.Ifrm_set.오른쪽으로키 = Program.Ifrm_main.오른쪽으로키 = (Keys)right;
                    }
                    #endregion
                    #region MFTm
                    else if (strtmp.Contains("MFTm-")) //세팅 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(5).ToCharArray();
                        byte gotnum = 0;
                        ushort bz = 0, ulbz = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (gotnum == 0)
                                    bz = ushort.Parse(stm2);
                                else if (gotnum == 1)
                                    ulbz = ushort.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }
                    }
                    #endregion
                    #region ETC
                    else if (strtmp.Contains("ETC-")) //세팅 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(4).ToCharArray();
                        byte gotnum = 0, b = 0;
                        bool a = false, c = false;
                        int d = 90;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (gotnum == 0)
                                    a = bool.Parse(stm2);
                                else if (gotnum == 1)
                                    b = byte.Parse(stm2);
                                else if (gotnum == 2)
                                    c = bool.Parse(stm2);
                                else if (gotnum == 3)
                                    d = int.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_main.BGMP = a;
                        Program.Ifrm_main.난이도설정((frm_main.난이도)b);
                        블록생성기.인스턴스내놔().일반블록생성확률 = d;
                    }
                    #endregion
                    #region LVL
                    else if (strtmp.Contains("LVI-")) //렙 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(4).ToCharArray();
                        byte gotnum = 0;
                        int lv = 0, sc = 0, nlsc = 0, bdt = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (gotnum == 0)
                                    lv = int.Parse(stm2);
                                else if (gotnum == 1)
                                    sc = int.Parse(stm2);
                                else if (gotnum == 2)
                                    nlsc = int.Parse(stm2);
                                else if (gotnum == 3)
                                    bdt = int.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_main.점수x = new 점수();
                        Program.Ifrm_main.점수x.렙 = lv;
                        Program.Ifrm_main.점수x.현재점수 = sc;
                        Program.Ifrm_main.점수x.다음렙을위한점수 = nlsc;
                        Program.Ifrm_main.BDTI = bdt;
                    }
                    #endregion
                    #region CURBS
                    else if (strtmp.Contains("CURBS-")) //현재블럭 및 미리보기 블럭 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(6).ToCharArray();
                        byte gotnum = 0;
                        int cur = 0, n = 0, nn = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (gotnum == 0)
                                    cur = int.Parse(stm2);
                                else if (gotnum == 1)
                                    n = int.Parse(stm2);
                                else if (gotnum == 2)
                                    nn = int.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_main.미리보는블럭2 = Program.Ifrm_main.블록컨테이너생산기x.랜덤컨테이너생성(nn);
                        Program.Ifrm_main.미리보는블럭2.생성();
                        Program.Ifrm_main.미리보는블럭2.연결고리위치설정(50, 70);

                        Program.Ifrm_main.미리보는블럭1 = Program.Ifrm_main.블록컨테이너생산기x.랜덤컨테이너생성(n);
                        Program.Ifrm_main.미리보는블럭1.생성();
                        Program.Ifrm_main.미리보는블럭1.연결고리위치설정(50, 70);

                        Program.Ifrm_main.현재블럭 = Program.Ifrm_main.블록컨테이너생산기x.랜덤컨테이너생성(cur);
                        Program.Ifrm_main.현재블럭.생성();
                        블록추가(Program.Ifrm_main.현재블럭.첫블록을내놔());
                        Program.Ifrm_main.현재블럭.이동(0, 0);

                    }
                    #endregion
                    #region Filled
                    else if (strtmp.Contains("Filled-")) //채워진 맵위치 발견시
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(7).ToCharArray();
                        bool gotone = false;
                        int x = 0, y = 0;

                        for (int j = 0; j < ctm2.Length; j++) //문장해석루프
                        {
                            if (ctm2[j] == ':') //콜론 감지
                            {
                                //stm2 += ctm2[j].ToString(); //콜론도 마저 처리

                                //처리
                                if (!gotone)
                                    x = int.Parse(stm2);
                                else
                                    y = int.Parse(stm2);

                                gotone = true;

                                stm2 = string.Empty; //tmp지움
                            }
                            else //콜론 나올때까지 문장해석진행
                                stm2 += ctm2[j].ToString();
                        }

                        블록매트릭스[x][y] = 블록생성기.인스턴스내놔().블록생성해(0);
                        블록매트릭스[x][y].초기화(x, y, null);
                    }
                    #endregion

                    strtmp = string.Empty; //tmp지움
                }
                else //세미콜론 나올때까지 문장해석진행
                  strtmp += tmp[i].ToString();
            }
        }

        /// <summary>
        /// 맵의 데이터를 문자열로
        /// 맵데이터->문자열
        /// </summary>
        /// <returns>맵데이터->문자열</returns>
        public static string 컨버트()
        {
            string rettmp = string.Empty;
            
            rettmp += string.Format("MAPSIZE-{0}:{1}:;", 가로크기, 세로크기); //맵크기 저장

            rettmp += string.Format("KEYSETTING-{0}:{1}:{2}:{3}:{4}:{5}:;", (int)Program.Ifrm_main.포즈키
                , (int)Program.Ifrm_main.끝까지내림키
                , (int)Program.Ifrm_main.회전키
                , (int)Program.Ifrm_main.한칸내림키
                , (int)Program.Ifrm_main.왼쪽으로키
                , (int)Program.Ifrm_main.오른쪽으로키); //키설정저장

            rettmp += string.Format("ETC-{0}:{1}:{2}:{3}:;", Program.Ifrm_main.BGMP
                , (byte)Program.Ifrm_main.I난이도
                , 블록생성기.인스턴스내놔().일반블록생성확률); //기타등등저장

            rettmp += string.Format("LVI-{0}:{1}:{2}:{3}:;", Program.Ifrm_main.점수x.렙, 
                Program.Ifrm_main.점수x.현재점수,
                Program.Ifrm_main.점수x.다음렙을위한점수, 
                Program.Ifrm_main.BDTI); //렙저장

            rettmp += string.Format("CURBS-{0}:{1}:{2}:;", Program.Ifrm_main.현재블럭타입,
                Program.Ifrm_main.미리보는블럭1타입, Program.Ifrm_main.미리보는블럭2타입); //현재블럭과 미리보기타입

            for (int x = 0; x < 가로크기; x++)
            {
                for (int y = 0; y < 세로크기; y++)
                {
                    if (!이거비었냐(x, y))
                         rettmp += string.Format("Filled-{0}:{1}:;", x, y); //채워진 위치 저장
                }
            }

            return rettmp;
        }
    }
}
