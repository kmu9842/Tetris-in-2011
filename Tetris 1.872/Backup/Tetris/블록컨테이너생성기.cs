using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class �����̳ʻ�����
    {
        private List<��������̳�> �����̳ʸ���Ʈ;
        private Random ����������;

        public �����̳ʻ�����()
        {
            �����̳ʸ���Ʈ = new List<��������̳�>();
            ���������� = new Random(System.DateTime.Now.Millisecond);
        }
    
        /// <summary>
        /// ���� ������ �ִ� �����̳� Ÿ���߿� �����ϰ� �ϳ� �����ؼ� ��ȯ
        /// </summary>
        /// <returns>�����̳�</returns>
        public ��������̳� ���������̳ʻ���()
        {
            int n = ����������.Next(�����̳ʸ���Ʈ.Count);

            return (��������̳�)�����̳ʸ���Ʈ[n].Clone();
        }

        /// <summary>
        /// ���� ������ �ִ� �����̳� Ÿ���߿� �����ϰ� �ϳ� �����ؼ� ��ȯ
        /// </summary>
        /// <returns>�����̳�</returns>
        public ��������̳� ���������̳ʻ���(out int n)
        {
            n = ����������.Next(�����̳ʸ���Ʈ.Count);

            return (��������̳�)�����̳ʸ���Ʈ[n].Clone();
        }

        /// <summary>
        /// ���� ������ �ִ� �����̳� Ÿ���߿� �����ϰ� �ϳ� �����ؼ� ��ȯ
        /// </summary>
        /// <returns>�����̳�</returns>
        public ��������̳� ���������̳ʻ���(int n)
        {
            return (��������̳�)�����̳ʸ���Ʈ[n].Clone();
        }

        /// <summary>
        /// ���ο� �����̳� Ÿ�� �߰�
        /// </summary>
        /// <param name="container">�߰��� �����̳�</param>
        public void �����̳�Ÿ���߰�(��������̳� container)
        {
            �����̳ʸ���Ʈ.Add(container);
        }
    }
}
