using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public abstract class 블록 : ICloneable
    {
        protected int 가로크기;
        protected int 세로크기;
        protected int 가로위치;
        protected int 세로위치;
        protected int 가로좌표;
        protected int 세로좌표;
        protected 블록 다음블럭;
        protected Bitmap 블럭이미지;

        /// <summary>
        /// 블록초기화
        /// </summary>
        /// <param name="cx">가로크기</param>
        /// <param name="cy">세로크기</param>
        public 블록(int cx, int cy)
        {
            가로크기 = cx;
            세로크기 = cy;
            다음블럭 = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posX">가로 크기</param>
        /// <param name="posY">세로 크기</param>
        /// <param name="nextBlock">다음블록</param>
        public void 초기화(int posX, int posY, 블록 nextBlock)
        {
            가로좌표 = posX;
            세로좌표 = posY;
            가로위치 = posX * 가로크기;
            세로위치 = posY * 세로크기;
            다음블럭 = nextBlock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g">그래픽</param>
        public void 그려라(Graphics g)
        {         
            if(블럭이미지 != null)
                g.DrawImage(블럭이미지, 가로위치, 세로위치, 가로크기 - 1, 세로크기 - 1);
            else
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                g.FillRectangle(brush, 가로위치, 세로위치, 가로크기 - 1, 세로크기 - 1);
            }
        }

        /// <summary>
        /// 자신과 연결된 객체는 제외하고 자신만 이동한다.
        /// </summary>
        /// <param name="x">가로 좌표</param>
        /// <param name="y">세로 좌표</param>
        /// <returns>성공여부</returns>
        public bool 혼자움직여(int x, int y)
        {
            if (맵.맵안좌표냐(가로좌표 + x, 세로좌표 + y) && 맵.이거비었냐(가로좌표 + x, 세로좌표 + y))
            {
                가로좌표 += x;
                세로좌표 += y;
                가로위치 = 가로좌표 * 가로크기;
                세로위치 = 세로좌표 * 세로크기;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 자신은 물론 자신과 연결된 객체까지 이동한다.
        /// </summary>
        /// <param name="x">가로좌표</param>
        /// <param name="y">세로좌표</param>
        /// <returns>성공?</returns>
        public bool 다같이움직여(int x, int y)
        {
            if (맵.맵안좌표냐(가로좌표 + x, 세로좌표 + y) && 맵.이거비었냐(가로좌표 + x, 세로좌표 + y))
            {

                if (다음블럭 != null)
                {
                    if (다음블럭.다같이움직여(x, y) == false)
                        return false;
                }

                가로좌표 += x;
                세로좌표 += y;
                가로위치 = 가로좌표 * 가로크기;
                세로위치 = 세로좌표 * 세로크기;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 블럭들의 위치를 설정한다.
        /// </summary>
        /// <param name="x">가로좌표</param>
        /// <param name="y">세로좌표</param>
        public void 블럭체인위치설정(int x, int y)
        {
            if (다음블럭 != null)
            {
                다음블럭.블럭체인위치설정(x, y);
            }

            가로위치 += x;
            세로위치 += y;
        }

        public abstract void 액션();

        public Object Clone()
        {
            블록 block =  (블록)this.MemberwiseClone();

            블록 pBlock = block.다음블럭;
            while (pBlock != null)
            {
                pBlock = (블록)pBlock.MemberwiseClone();
                pBlock = pBlock.다음블럭;
            }

            return block;
        }

        public Point 좌표
        {
            get
            {
                return new Point(가로좌표, 세로좌표);
            }
            set
            {
                가로좌표 = value.X;
                세로좌표 = value.Y;
            }
        }

        public Point 위치
        {
            get
            {
                return new Point(가로위치, 세로위치);
            }
            set
            {
                가로위치 = value.X;
                세로위치 = value.Y;
            }
        }

        public Point 크기
        {
            get
            {
                return new Point(가로크기, 세로크기);
            }
            set
            {
                가로크기 = value.X;
                세로크기 = value.Y;
            }
        }

        public 블록 다음블럭내놔()
        {
            return 다음블럭;
        }

        public void 다음블럭은이걸로(블록 block)
        {
            다음블럭 = block;
        }
    }

    public class 노말블락 : 블록
    {
        public 노말블락(int cx, int cy) : base(cx, cy)
        {
            블럭이미지 = new Bitmap("NormalBlock.jpg");
        }

        public override void 액션()
        {
            //맵.블럭_한개_지우기(가로좌표, 세로좌표);
            if (다음블럭 != null)
            {
                다음블럭.액션();
            }
        }
    }

    public class 스트롱블락 : 블록
    {
        public 스트롱블락(int cx, int cy) : base(cx, cy)
        {
          
        }

        public override void 액션()
        {
            //if (다음블럭 != null)
            //{
            //    다음블럭.액션();
            //}
        }
    }

    public class 라인디스트로이아이템블럭 : 블록 
    {
        public 라인디스트로이아이템블럭(int cx, int cy) : base(cx, cy)
        {
            블럭이미지 = new Bitmap("LineDestroyBlock.jpg");
        }

        public override void 액션()
        {
            Program.Ifrm_main.PlayS(frm_main.사운드종류.delOne);
              

            //맵.블럭_한개_지우기(가로좌표, 세로좌표);
            맵.한줄지움(세로좌표);
            if (다음블럭 != null)
            {
                다음블럭.액션();
            }
        }
    }

    public class 한개박살아이템블럭 : 블록
    {
        public 한개박살아이템블럭(int cx, int cy)
            : base(cx, cy)
        {
            블럭이미지 = new Bitmap("OneDestroyBlock.jpg");
        }

        public override void 액션()
        {
            맵.블럭_한개_지우기(가로좌표, 세로좌표);
            맵.블럭_한개_지우기(가로좌표, 세로좌표 + 1);
            
            if (다음블럭 != null)
            {
                다음블럭.액션();
            }
        }
    }
}
