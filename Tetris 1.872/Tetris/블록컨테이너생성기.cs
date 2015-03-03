using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class 컨테이너생성기
    {
        private List<블록컨테이너> 컨테이너리스트;
        private Random 난수생성기;

        public 컨테이너생성기()
        {
            컨테이너리스트 = new List<블록컨테이너>();
            난수생성기 = new Random(System.DateTime.Now.Millisecond);
        }
    
        /// <summary>
        /// 현재 가지고 있는 컨테이너 타입중에 랜덤하게 하나 생성해서 반환
        /// </summary>
        /// <returns>컨테이너</returns>
        public 블록컨테이너 랜덤컨테이너생성()
        {
            int n = 난수생성기.Next(컨테이너리스트.Count);

            return (블록컨테이너)컨테이너리스트[n].Clone();
        }

        /// <summary>
        /// 현재 가지고 있는 컨테이너 타입중에 랜덤하게 하나 생성해서 반환
        /// </summary>
        /// <returns>컨테이너</returns>
        public 블록컨테이너 랜덤컨테이너생성(out int n)
        {
            n = 난수생성기.Next(컨테이너리스트.Count);

            return (블록컨테이너)컨테이너리스트[n].Clone();
        }

        /// <summary>
        /// 현재 가지고 있는 컨테이너 타입중에 랜덤하게 하나 생성해서 반환
        /// </summary>
        /// <returns>컨테이너</returns>
        public 블록컨테이너 랜덤컨테이너생성(int n)
        {
            return (블록컨테이너)컨테이너리스트[n].Clone();
        }

        /// <summary>
        /// 새로운 컨테이너 타입 추가
        /// </summary>
        /// <param name="container">추가할 컨테이너</param>
        public void 컨데이너타입추가(블록컨테이너 container)
        {
            컨테이너리스트.Add(container);
        }
    }
}
