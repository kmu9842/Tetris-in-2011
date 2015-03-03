using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public static class ��
    {
        /// <summary>
        /// ��ϵ��� 2���� �迭
        /// </summary>
        public static Tetris.���[][] ��ϸ�Ʈ����;
        
        /// <summary>
        /// ���� ����ũ��
        /// </summary>
        public static int ����ũ��;

        /// <summary>
        /// ���� ����ũ�� 
        /// </summary>
        public static int ����ũ��;

        /// <summary>
        /// �� �ʱ�ȭ �ϱ� 
        /// </summary>
        /// <param name="x">�ʱ�ȭ �� ���� ����ũ��</param>
        /// <param name="y">�ʱ�ȭ �� ���� ����ũ��</param>
        public static void �ʱ�ȭ(int x, int y)
        {
            ����ũ�� = x; //����ũ�� ����
            ����ũ�� = y; //����ũ�� ����

            //��ϸ�Ʈ���� ����
            ��ϸ�Ʈ���� = new ���[����ũ��][]; //����ũ�� ���� 2���������迭����
            for(int i = 0; i < x; i++) //for������ ����
            {
                ��ϸ�Ʈ����[i] = new ���[����ũ��]; //�����ϳ��� ���ι迭����
                for(int j = 0; j < y; j++) //for������ ����
                {
                    ��ϸ�Ʈ����[i][j] = null; //null�� �ʱ�ȭ
                }
            }
            //��ϸ�Ʈ���� ������
        }

        /// <summary>
        /// �ʿ� �� �߰�
        /// </summary>
        /// <param name="block">�߰��Һ��</param>
        /// <returns>����߰� ��������</returns>
        public static bool ����߰�(��� block)
        {
            ��� pBlock = block; //����� ���� �� ����

            while (pBlock != null) //����� null�� �ƴϸ�
            {
                Point point = pBlock.��ǥ; //��ǥ ���� �� ����
                if(point.Y < 0) //Y��ǥ�� 0�����϶�
                    return false; //����
                ��ϸ�Ʈ����[point.X][point.Y] = pBlock; //�ƴϸ� ���迭�� ��������ġ�� ������
                
                pBlock = pBlock.����������(); //������ ����� �� ����� ����������� ��ü
            }

            return true; //����
        }

        /// <summary>
        /// ���� �Ѱ� ����� ���� ����
        /// </summary>
        /// <param name="x">���� ��ǥ</param>
        /// <param name="y">���� ��ǥ</param>
        public static void ��_�Ѱ�_�����(int x, int y)
        {
            try
            {
                ��ϸ�Ʈ����[x][y] = null; //�������� �õ�
            }

            catch //������
            {
                return; //����
            }

            // ������ ������� ����
            for (int j = y; j > 0; j--)
            {
                if (��ϸ�Ʈ����[x][j - 1] != null)
                    ��ϸ�Ʈ����[x][j - 1].ȥ�ڿ�����(0, 1);
                ��ϸ�Ʈ����[x][j] = ��ϸ�Ʈ����[x][j - 1];
                ��ϸ�Ʈ����[x][j - 1] = null;
            }

            // ������ �������
            //for (int j = ����ũ��; j > 0; j--) //���Ƕ��μ�ȯ
            //{
            //    if (��ϸ�Ʈ����[x][j - 1] != null) //�Ʒ����� ����ִ���
            //        //if (��ϸ�Ʈ����[x][j + 1] != null) //�Ʒ����� ����ִ���
            //        ��ϸ�Ʈ����[x][j].ȥ�ڿ�����(0, 1); //������

            //    //��ϸ�Ʈ����[x][j] = ��ϸ�Ʈ����[x][j - 1];
            //    //��ϸ�Ʈ����[x][j - 1] = null;
            //}
        }

        /// <summary>
        /// ���η� ��������
        /// </summary>
        /// <param name="y">���� ��ǥ</param>
        public static void ��������(int y)
        {
            // ���� �����
            for(int x = 0; x < ����ũ��; x++)
            {
                ��ϸ�Ʈ����[x][y] = null; // nulló��
                //try
                //{
                //    //��ϸ�Ʈ����[x][y].�׼�();
                //}
                //catch { }
            }

            // ������ �������
            for (int j = y; j > 0; j--)
            {
                for (int i = 0; i < ����ũ��; i++)
                {
                    if (��ϸ�Ʈ����[i][j - 1] != null)
                        ��ϸ�Ʈ����[i][j - 1].ȥ�ڿ�����(0, 1);
                    ��ϸ�Ʈ����[i][j] = ��ϸ�Ʈ����[i][j - 1];
                    ��ϸ�Ʈ����[i][j - 1] = null;
                }
            }
        }

        /// <summary>
        /// ������ ���� ã�Ƽ� �� ����
        /// </summary>
        /// <returns>� ������</returns>
        public static int ��_��_����_������()
        {
            int delCount = 0;
            for(int i = ����ũ�� - 1; i >= 0; i--) //for������ ����
            {
                if (���β�á��(i)) //�������ΰ˻�
                {
                    ��������(i); //��������
                    delCount++; //��������
                    i++; //i����
                }
            }

            return delCount; //��������
        }

        /// <summary>
        /// ���� �� á���� �˻�
        /// </summary>
        /// <param name="y">���� ��ǥ</param>
        /// <returns>���� �� á����</returns>
        private static bool ���β�á��(int y)
        {
            for (int x = 0; x < ����ũ��; x++) //����
            {
                if (��ϸ�Ʈ����[x][y] == null) //����� �ϳ��� ������
                    return false; //����
            }

            return true; //��á���� ��
        }

        /// <summary>
        /// �ش� ��ǥ�� ���� ������� �˻�
        /// </summary>
        /// <param name="x">���� ��ǥ</param>
        /// <param name="y">���� ��ǥ</param>
        /// <returns>�����</returns>
        public static bool �̰ź����(int x, int y)
        {
            if (y < 0)
                return true;

            if (��ϸ�Ʈ����[x][y] == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ��ǥ�� �� �������ΰ�
        /// </summary>
        /// <param name="x">���� ��ǥ</param>
        /// <param name="y">���� ��ǥ</param>
        /// <returns>�ʾ���ǥ��</returns>
        public static bool �ʾ���ǥ��(int x, int y)
        {
            if (x < ����ũ�� && x >= 0 && y < ����ũ��) //�ʾ������˻�
                return true; //����
            else
                return false; //����
        }

        /// <summary>
        /// ������ ��� �� �׸���
        /// </summary>
        /// <param name="g">�׷����� �׷���</param>
        public static void �׷�(Graphics g)
        {
            for (int i = 0; i < ����ũ��; i++)
            {
                for (int j = 0; j < ����ũ��; j++)
                {
                    if (��ϸ�Ʈ����[i][j] != null) //null�� �ƴϸ�
                    {
                        ��ϸ�Ʈ����[i][j].�׷���(g); //�׸�
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Ŭ��()
        {
            for (int i = 0; i < ����ũ��; i++)
            {
                for (int j = 0; j < ����ũ��; j++)
                {
                    ��ϸ�Ʈ����[i][j] = null;
                }
            }
        }

        /// <summary>
        /// �����͸� �ʿ� �ε�
        /// ���ڿ�->�ʵ�����
        /// </summary>
        /// <param name="����Ÿ">�ε��� ������</param>
        public static void ����Ʈ(string ����Ÿ)
        {
            Ŭ��();
            string strtmp = string.Empty;
            char[] tmp = ����Ÿ.ToCharArray();

            for (long i = 0; i < tmp.LongLength; i++) //�����ؼ�����
            {
                if (tmp[i] == ';') //�����ݷ� ����
                {
                    strtmp += tmp[i].ToString(); //�����ݷе� ���� ó��

                    //ó��
                    #region MAPSIZE
                    if (strtmp.Contains("MAPSIZE-")) //��ũ�� �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(8).ToCharArray();
                        bool gotone = false;
                        int x = 0, y = 0;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
                                if (!gotone)
                                   x = int.Parse(stm2);
                                else
                                    y = int.Parse(stm2);
                                gotone = true;

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }

                        �ʱ�ȭ(x, y);
                        Ŭ��();
                    }
                    #endregion
                    #region KEYSETTING
                    else if (strtmp.Contains("KEYSETTING-")) //���� �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(11).ToCharArray();
                        byte gotnum = 0;
                        int pau = 0, toend = 0, rota = 0, left = 0, right = 0, oned = 0;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
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

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_set.����Ű = Program.Ifrm_main.����Ű = (Keys)pau;
                        Program.Ifrm_set.����������Ű = Program.Ifrm_main.����������Ű = (Keys)toend;
                        Program.Ifrm_set.ȸ��Ű = Program.Ifrm_main.ȸ��Ű = (Keys)rota;
                        Program.Ifrm_set.��ĭ����Ű = Program.Ifrm_main.��ĭ����Ű = (Keys)oned;
                        Program.Ifrm_set.��������Ű = Program.Ifrm_main.��������Ű = (Keys)left;
                        Program.Ifrm_set.����������Ű = Program.Ifrm_main.����������Ű = (Keys)right;
                    }
                    #endregion
                    #region MFTm
                    else if (strtmp.Contains("MFTm-")) //���� �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(5).ToCharArray();
                        byte gotnum = 0;
                        ushort bz = 0, ulbz = 0;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
                                if (gotnum == 0)
                                    bz = ushort.Parse(stm2);
                                else if (gotnum == 1)
                                    ulbz = ushort.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }
                    }
                    #endregion
                    #region ETC
                    else if (strtmp.Contains("ETC-")) //���� �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(4).ToCharArray();
                        byte gotnum = 0, b = 0;
                        bool a = false, c = false;
                        int d = 90;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
                                if (gotnum == 0)
                                    a = bool.Parse(stm2);
                                else if (gotnum == 1)
                                    b = byte.Parse(stm2);
                                else if (gotnum == 2)
                                    c = bool.Parse(stm2);
                                else if (gotnum == 3)
                                    d = int.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_main.BGMP = a;
                        Program.Ifrm_main.���̵�����((frm_main.���̵�)b);
                        ��ϻ�����.�ν��Ͻ�����().�Ϲݺ�ϻ���Ȯ�� = d;
                    }
                    #endregion
                    #region LVL
                    else if (strtmp.Contains("LVI-")) //�� �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(4).ToCharArray();
                        byte gotnum = 0;
                        int lv = 0, sc = 0, nlsc = 0, bdt = 0;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
                                if (gotnum == 0)
                                    lv = int.Parse(stm2);
                                else if (gotnum == 1)
                                    sc = int.Parse(stm2);
                                else if (gotnum == 2)
                                    nlsc = int.Parse(stm2);
                                else if (gotnum == 3)
                                    bdt = int.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_main.����x = new ����();
                        Program.Ifrm_main.����x.�� = lv;
                        Program.Ifrm_main.����x.�������� = sc;
                        Program.Ifrm_main.����x.���������������� = nlsc;
                        Program.Ifrm_main.BDTI = bdt;
                    }
                    #endregion
                    #region CURBS
                    else if (strtmp.Contains("CURBS-")) //����� �� �̸����� �� �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(6).ToCharArray();
                        byte gotnum = 0;
                        int cur = 0, n = 0, nn = 0;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
                                if (gotnum == 0)
                                    cur = int.Parse(stm2);
                                else if (gotnum == 1)
                                    n = int.Parse(stm2);
                                else if (gotnum == 2)
                                    nn = int.Parse(stm2);

                                gotnum++;

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }

                        Program.Ifrm_main.�̸����º�2 = Program.Ifrm_main.��������̳ʻ����x.���������̳ʻ���(nn);
                        Program.Ifrm_main.�̸����º�2.����();
                        Program.Ifrm_main.�̸����º�2.�������ġ����(50, 70);

                        Program.Ifrm_main.�̸����º�1 = Program.Ifrm_main.��������̳ʻ����x.���������̳ʻ���(n);
                        Program.Ifrm_main.�̸����º�1.����();
                        Program.Ifrm_main.�̸����º�1.�������ġ����(50, 70);

                        Program.Ifrm_main.����� = Program.Ifrm_main.��������̳ʻ����x.���������̳ʻ���(cur);
                        Program.Ifrm_main.�����.����();
                        ����߰�(Program.Ifrm_main.�����.ù���������());
                        Program.Ifrm_main.�����.�̵�(0, 0);

                    }
                    #endregion
                    #region Filled
                    else if (strtmp.Contains("Filled-")) //ä���� ����ġ �߽߰�
                    {
                        string stm2 = string.Empty;
                        char[] ctm2 = strtmp.Substring(7).ToCharArray();
                        bool gotone = false;
                        int x = 0, y = 0;

                        for (int j = 0; j < ctm2.Length; j++) //�����ؼ�����
                        {
                            if (ctm2[j] == ':') //�ݷ� ����
                            {
                                //stm2 += ctm2[j].ToString(); //�ݷе� ���� ó��

                                //ó��
                                if (!gotone)
                                    x = int.Parse(stm2);
                                else
                                    y = int.Parse(stm2);

                                gotone = true;

                                stm2 = string.Empty; //tmp����
                            }
                            else //�ݷ� ���ö����� �����ؼ�����
                                stm2 += ctm2[j].ToString();
                        }

                        ��ϸ�Ʈ����[x][y] = ��ϻ�����.�ν��Ͻ�����().��ϻ�����(0);
                        ��ϸ�Ʈ����[x][y].�ʱ�ȭ(x, y, null);
                    }
                    #endregion

                    strtmp = string.Empty; //tmp����
                }
                else //�����ݷ� ���ö����� �����ؼ�����
                  strtmp += tmp[i].ToString();
            }
        }

        /// <summary>
        /// ���� �����͸� ���ڿ���
        /// �ʵ�����->���ڿ�
        /// </summary>
        /// <returns>�ʵ�����->���ڿ�</returns>
        public static string ����Ʈ()
        {
            string rettmp = string.Empty;
            
            rettmp += string.Format("MAPSIZE-{0}:{1}:;", ����ũ��, ����ũ��); //��ũ�� ����

            rettmp += string.Format("KEYSETTING-{0}:{1}:{2}:{3}:{4}:{5}:;", (int)Program.Ifrm_main.����Ű
                , (int)Program.Ifrm_main.����������Ű
                , (int)Program.Ifrm_main.ȸ��Ű
                , (int)Program.Ifrm_main.��ĭ����Ű
                , (int)Program.Ifrm_main.��������Ű
                , (int)Program.Ifrm_main.����������Ű); //Ű��������

            rettmp += string.Format("ETC-{0}:{1}:{2}:{3}:;", Program.Ifrm_main.BGMP
                , (byte)Program.Ifrm_main.I���̵�
                , ��ϻ�����.�ν��Ͻ�����().�Ϲݺ�ϻ���Ȯ��); //��Ÿ�������

            rettmp += string.Format("LVI-{0}:{1}:{2}:{3}:;", Program.Ifrm_main.����x.��, 
                Program.Ifrm_main.����x.��������,
                Program.Ifrm_main.����x.����������������, 
                Program.Ifrm_main.BDTI); //������

            rettmp += string.Format("CURBS-{0}:{1}:{2}:;", Program.Ifrm_main.�����Ÿ��,
                Program.Ifrm_main.�̸����º�1Ÿ��, Program.Ifrm_main.�̸����º�2Ÿ��); //������� �̸�����Ÿ��

            for (int x = 0; x < ����ũ��; x++)
            {
                for (int y = 0; y < ����ũ��; y++)
                {
                    if (!�̰ź����(x, y))
                         rettmp += string.Format("Filled-{0}:{1}:;", x, y); //ä���� ��ġ ����
                }
            }

            return rettmp;
        }
    }
}
