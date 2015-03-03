using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class 블록생성기
    {
        public static 블록생성기 인스턴스 = null;
        
        /// <summary>
        /// 일반블록생성확률을 백분율로
        /// </summary>
        private int p일반블록생성확률;

        /// <summary>
        /// get : 블럭 생성시 노멀 블록의 생성될 확률
        /// set : 블럭 생성시 노멀 블록의 생성될 확률 설정
        /// 단위 : 백분율(%)
        /// </summary>
        public int 일반블록생성확률
        {
            get { return p일반블록생성확률; }
            set { p일반블록생성확률 = value; }
        }

        /// <summary>
        /// 블록의 종류목록
        /// </summary>
        private List<블록> 블록리스트;
        private Random 난수발생기;

        protected 블록생성기()
        {
            블록리스트 = new List<블록>();
            난수발생기 = new Random(DateTime.Now.Millisecond);
        }

        public static 블록생성기 인스턴스내놔()
        {
            if (인스턴스 == null)
                인스턴스 = new 블록생성기();
            return 인스턴스;
        }
    
        /// <summary>
        ///  현재 가지고 있는 블럭 타입중에서 정해진 확률에 따라 랜덤하게 하나 생성
        /// </summary>
        /// <returns>생성한 블록</returns>
        public 블록 블록생성해()
        {
            int n = 난수발생기.Next(100);
            if (n <= p일반블록생성확률)
                n = 0;
            else
            {
                n = 난수발생기.Next(블록리스트.Count - 1);
                n++;
            }

            return (블록)블록리스트[n].Clone();
        }

        /// <summary>
        ///  현재 가지고 있는 블럭 타입중에서 선택해서 생성
        /// </summary>
        /// <returns>생성한 블록</returns>
        public 블록 블록생성해(int n)
        {
            return (블록)블록리스트[n].Clone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewBlock">새로운 블록?</param>
        public void 이런타입의블록추가(블록 NewBlock)
        {
            블록리스트.Add(NewBlock);
        }
    }
}
