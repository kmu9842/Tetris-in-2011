using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public abstract class 블록컨테이너 : ICloneable
    {
        /// <summary>
        /// 연결된 1번블럭
        /// </summary>
        protected 블록 첫번째블럭;

        /// <summary>
        /// 생성(추상)
        /// </summary>
        public abstract void 생성();

        /// <summary>
        /// 오른쪽으로 회전(추상)
        /// </summary>
        /// <returns>?</returns>
        public abstract bool 우로돌려();

        /// <summary>
        /// 회전
        /// </summary>
        /// <param name="block">블록</param>
        /// <param name="pos">좌표</param>
        /// <returns>돌기기성공?</returns>
        protected bool 우로돌려(블록 block, Point pos)
        {
            int x, y, tx, ty;
            Point p = block.좌표;

            x = p.X - pos.X;
            y = p.Y - pos.Y;
            // 시계방향 회전시 y좌표를 x좌표만큼 이동하고, x좌표는 y좌표의 마이너스 값만큼 이동한다.
            tx = -y - x;
            ty = x - y;

            if (맵.맵안좌표냐(p.X + tx, p.Y + ty) && 맵.이거비었냐(p.X + tx, p.Y + ty))
            {
                if (block.다음블럭내놔() != null)
                {
                    if(!우로돌려(block.다음블럭내놔(), pos))
                        return false;
                }
      
                block.혼자움직여(tx, ty);

                return true;
            }

            else
                return false;
        }

        /// <summary>
        /// 이동
        /// </summary>
        /// <param name="x">가로 좌표</param>
        /// <param name="y">세로 좌표</param>
        /// <returns>?</returns>
        public bool 이동(int x, int y)
        {
            return 첫번째블럭.다같이움직여(x, y);
        }

        /// <summary>
        /// 블럭들의 연결고리를 따라가며 포지션 수정
        /// </summary>
        /// <param name="x">가로 좌표</param>
        /// <param name="y">세로 좌표</param>
        public void 연결고리위치설정(int x, int y)
        {
            Point pos = 첫번째블럭.위치;
            첫번째블럭.블럭체인위치설정(x - pos.X, y - pos.Y);
        }

        /// <summary>
        /// 블럭들의 연결고리를 따라가며 그리기 수행
        /// </summary>
        /// <param name="g">그래픽</param>
        public void 그려라(Graphics g)
        {
            블록 pBlock = 첫번째블럭;

            while (pBlock != null)
            {
                pBlock.그려라(g);

                pBlock = pBlock.다음블럭내놔();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>연결고리 1번블록</returns>
        public 블록 첫블록을내놔()
        {
            return 첫번째블럭;
        }

        /// <summary>
        /// 복제
        /// </summary>
        /// <returns>복제된것</returns>
        public Object Clone()
        {
            블록컨테이너 container = (블록컨테이너)this.MemberwiseClone();
            if (첫번째블럭 != null)
                container.첫번째블럭 = (블록)this.첫번째블럭.Clone();

            return container;
        }

        /// <summary>
        /// 블럭 각자의 액션 수행
        /// </summary>
        public void 액션수행()
        {
            첫번째블럭.액션();
        }

        /// <summary>
        ///  블록이 차지하고 있는 사각형 영역 좌표 얻기
        /// </summary>
        /// <returns>사각형 영역 좌표</returns>
        public Rectangle 블록영역내놔()
        {
            블록 pBlock = 첫번째블럭;
            Rectangle rc = new Rectangle(1000,1000,0,0);
            Point size = pBlock.크기;
            Point MaxLoc = new Point(첫번째블럭.위치.X, 첫번째블럭.위치.Y);
            MaxLoc.X += 첫번째블럭.크기.X;
            MaxLoc.Y += 첫번째블럭.크기.Y;

            while (pBlock != null)
            {
                Point Loc = pBlock.위치;

                if (rc.Left > Loc.X)
                    rc.X = Loc.X;
                else if (MaxLoc.X < Loc.X + size.X)
                    MaxLoc.X = Loc.X + size.X;

                if (rc.Top > Loc.Y)
                    rc.Y = Loc.Y;
                else if (MaxLoc.Y < Loc.Y + size.Y)
                    MaxLoc.Y = Loc.Y + size.Y;
                
                pBlock = pBlock.다음블럭내놔();
            }

            rc.Width = MaxLoc.X - rc.Left;
            rc.Height = MaxLoc.Y - rc.Top;

            return rc;
        }
    }

    // 재정의된 블럭컨테이너

    public class ChairA모양블럭 : 블록컨테이너
    {
        // 각자 자신의 모양에 맞는 좌표와 블럭 생성
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2 + 1, -1, block2);
            block2.초기화(맵.가로크기 / 2, -1, block3);
            block3.초기화(맵.가로크기 / 2, -2, block4);
            block4.초기화(맵.가로크기 / 2, -3, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.다음블럭내놔().다음블럭내놔().좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
        }
    }

    public class ChairB모양블럭 : 블록컨테이너
    {
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2, -1, block2);
            block2.초기화(맵.가로크기 / 2, -2, block3);
            block3.초기화(맵.가로크기 / 2, -3, block4);
            block4.초기화(맵.가로크기 / 2 - 1, -1, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.다음블럭내놔().좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
        }
    }

    public class 정사각형모양블럭 : 블록컨테이너
    {
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2 + 1, -1, block2);
            block2.초기화(맵.가로크기 / 2, -1, block3);
            block3.초기화(맵.가로크기 / 2, -2, block4);
            block4.초기화(맵.가로크기 / 2 + 1, -2, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // block2 기준으로 회전
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
            //return false;
        }
    }

    public class Ah모양블럭 : 블록컨테이너
    {
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2, -1, block2);
            block2.초기화(맵.가로크기 / 2, -2, block3);
            block3.초기화(맵.가로크기 / 2, -3, block4);
            block4.초기화(맵.가로크기 / 2 - 1, -2, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // block2 기준으로 회전
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.다음블럭내놔().좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
        }
    }

    public class Staff모양블럭 : 블록컨테이너
    {
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2, -1, block2);
            block2.초기화(맵.가로크기 / 2, -2, block3);
            block3.초기화(맵.가로크기 / 2, -3, block4);
            block4.초기화(맵.가로크기 / 2, -4, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // block2 기준으로 회전
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.다음블럭내놔().좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
        }
    }

    public class StairsA모양블럭 : 블록컨테이너
    {
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2, -1, block2);
            block2.초기화(맵.가로크기 / 2, -2, block3);
            block3.초기화(맵.가로크기 / 2 + 1, -2, block4);
            block4.초기화(맵.가로크기 / 2 + 1, -3, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // block2 기준으로 회전
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.다음블럭내놔().좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
        }
    }

    public class StairsB모양블럭 : 블록컨테이너
    {
        public override void 생성()
        {
            블록 block1 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block2 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block3 = 블록생성기.인스턴스내놔().블록생성해();
            블록 block4 = 블록생성기.인스턴스내놔().블록생성해();

            block1.초기화(맵.가로크기 / 2, -1, block2);
            block2.초기화(맵.가로크기 / 2, -2, block3);
            block3.초기화(맵.가로크기 / 2 - 1, -2, block4);
            block4.초기화(맵.가로크기 / 2 - 1, -3, null);

            첫번째블럭 = block1;
        }

        public override bool 우로돌려()
        {
            // block2 기준으로 회전
            // 회전하기 위한 기준 좌표 얻어오기
            Point pos = 첫번째블럭.다음블럭내놔().좌표;

            if (우로돌려(첫번째블럭, pos))
                return true;

            // 회전이 안된다면 기준점을 바꿔서 시도해 보기
            else
            {
                블록 block = 첫번째블럭;
                while (block != null)
                {
                    if (우로돌려(첫번째블럭, block.좌표))
                        return true;

                    block = block.다음블럭내놔();
                }

                return false;
            }
        }
    }
}
