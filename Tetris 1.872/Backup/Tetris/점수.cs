using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class ����
    {
        internal int ��������;
        internal int ��;
        internal int ����������������;

        public ����()
        {
            �� = 1;
            �������� = 0;
            //���������������� = 1000;
            ���������������� = 1600;
        }

        public ����(int ����, int ������, int ����������)
        {
            �� = ����;
            �������� = ������;
            ���������������� = ����������;
        }
        
        public void Ŭ����()
        {
            �������� = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">����</param>
        public void ������(int level)
        {
            �� = level;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>����</returns>
        public int �����ϱ�()
        {
            return ��;
        }

        /// <summary>
        /// ������ ���������� ���� �߰�
        /// </summary>
        /// <param name="num">���� ���μ�</param>
        /// <returns>�������ΰ�</returns>
        public bool ����Ŭ����(int num)
        {
            //�������� += ( num * 100 ) + (�� * 100);
            �������� += ((num * ��) + (��.����ũ�� - 10) - (��.����ũ�� - 20) + (int)Program.Ifrm_main.I���̵�) * 100;
            
            return �����߳�();
        }

        /// <summary>
        ///  ������ ������ �ʾҾ ���� ����߷ȴٸ� ���� �߰�(����)
        /// </summary>
        /// <returns>�����߳�</returns>
        public bool ����߶�()
        {
            �������� += 0; //����߶��ô� ��������

            return �����߳�();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>��������</returns>
        public bool �����߳�()
        {
            if (�������� >= ����������������)
            {
                ��++;
                ���������������� = ���������������� * 2;
                //���������������� += �� * 500;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>����</returns>
        public int ��������()
        {
            return ��������;
        }
    }
}
