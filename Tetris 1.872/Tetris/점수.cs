using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class 점수
    {
        internal int 현재점수;
        internal int 렙;
        internal int 다음렙을위한점수;

        public 점수()
        {
            렙 = 1;
            현재점수 = 0;
            //다음렙을위한점수 = 1000;
            다음렙을위한점수 = 1600;
        }

        public 점수(int 렙렙, int 현점수, int 다음렙점수)
        {
            렙 = 렙렙;
            현재점수 = 현점수;
            다음렙을위한점수 = 다음렙점수;
        }
        
        public void 클리어()
        {
            현재점수 = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">레벨</param>
        public void 렙설정(int level)
        {
            렙 = level;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>레벨</returns>
        public int 렙구하기()
        {
            return 렙;
        }

        /// <summary>
        /// 라인을 지웠을때의 점수 추가
        /// </summary>
        /// <param name="num">지운 라인수</param>
        /// <returns>레벨업인가</returns>
        public bool 한줄클리어(int num)
        {
            //현재점수 += ( num * 100 ) + (렙 * 100);
            현재점수 += ((num * 렙) + (맵.가로크기 - 10) - (맵.세로크기 - 20) + (int)Program.Ifrm_main.I난이도) * 100;
            
            return 렙업했나();
        }

        /// <summary>
        ///  라인을 지우지 않았어도 블럭을 떨어뜨렸다면 점수 추가(안함)
        /// </summary>
        /// <returns>랩업했나</returns>
        public bool 블록추락()
        {
            현재점수 += 0; //블록추락시는 점수안줘

            return 렙업했나();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>랩업여부</returns>
        public bool 렙업했나()
        {
            if (현재점수 >= 다음렙을위한점수)
            {
                렙++;
                다음렙을위한점수 = 다음렙을위한점수 * 2;
                //다음렙을위한점수 += 렙 * 500;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>점수</returns>
        public int 점수내놔()
        {
            return 현재점수;
        }
    }
}
