using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class ��ϻ�����
    {
        public static ��ϻ����� �ν��Ͻ� = null;
        
        /// <summary>
        /// �Ϲݺ�ϻ���Ȯ���� �������
        /// </summary>
        private int p�Ϲݺ�ϻ���Ȯ��;

        /// <summary>
        /// get : �� ������ ��� ����� ������ Ȯ��
        /// set : �� ������ ��� ����� ������ Ȯ�� ����
        /// ���� : �����(%)
        /// </summary>
        public int �Ϲݺ�ϻ���Ȯ��
        {
            get { return p�Ϲݺ�ϻ���Ȯ��; }
            set { p�Ϲݺ�ϻ���Ȯ�� = value; }
        }

        /// <summary>
        /// ����� �������
        /// </summary>
        private List<���> ��ϸ���Ʈ;
        private Random �����߻���;

        protected ��ϻ�����()
        {
            ��ϸ���Ʈ = new List<���>();
            �����߻��� = new Random(DateTime.Now.Millisecond);
        }

        public static ��ϻ����� �ν��Ͻ�����()
        {
            if (�ν��Ͻ� == null)
                �ν��Ͻ� = new ��ϻ�����();
            return �ν��Ͻ�;
        }
    
        /// <summary>
        ///  ���� ������ �ִ� �� Ÿ���߿��� ������ Ȯ���� ���� �����ϰ� �ϳ� ����
        /// </summary>
        /// <returns>������ ���</returns>
        public ��� ��ϻ�����()
        {
            int n = �����߻���.Next(100);
            if (n <= p�Ϲݺ�ϻ���Ȯ��)
                n = 0;
            else
            {
                n = �����߻���.Next(��ϸ���Ʈ.Count - 1);
                n++;
            }

            return (���)��ϸ���Ʈ[n].Clone();
        }

        /// <summary>
        ///  ���� ������ �ִ� �� Ÿ���߿��� �����ؼ� ����
        /// </summary>
        /// <returns>������ ���</returns>
        public ��� ��ϻ�����(int n)
        {
            return (���)��ϸ���Ʈ[n].Clone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewBlock">���ο� ���?</param>
        public void �̷�Ÿ���Ǻ���߰�(��� NewBlock)
        {
            ��ϸ���Ʈ.Add(NewBlock);
        }
    }
}
