using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public abstract class ��������̳� : ICloneable
    {
        /// <summary>
        /// ����� 1����
        /// </summary>
        protected ��� ù��°��;

        /// <summary>
        /// ����(�߻�)
        /// </summary>
        public abstract void ����();

        /// <summary>
        /// ���������� ȸ��(�߻�)
        /// </summary>
        /// <returns>?</returns>
        public abstract bool ��ε���();

        /// <summary>
        /// ȸ��
        /// </summary>
        /// <param name="block">���</param>
        /// <param name="pos">��ǥ</param>
        /// <returns>����⼺��?</returns>
        protected bool ��ε���(��� block, Point pos)
        {
            int x, y, tx, ty;
            Point p = block.��ǥ;

            x = p.X - pos.X;
            y = p.Y - pos.Y;
            // �ð���� ȸ���� y��ǥ�� x��ǥ��ŭ �̵��ϰ�, x��ǥ�� y��ǥ�� ���̳ʽ� ����ŭ �̵��Ѵ�.
            tx = -y - x;
            ty = x - y;

            if (��.�ʾ���ǥ��(p.X + tx, p.Y + ty) && ��.�̰ź����(p.X + tx, p.Y + ty))
            {
                if (block.����������() != null)
                {
                    if(!��ε���(block.����������(), pos))
                        return false;
                }
      
                block.ȥ�ڿ�����(tx, ty);

                return true;
            }

            else
                return false;
        }

        /// <summary>
        /// �̵�
        /// </summary>
        /// <param name="x">���� ��ǥ</param>
        /// <param name="y">���� ��ǥ</param>
        /// <returns>?</returns>
        public bool �̵�(int x, int y)
        {
            return ù��°��.�ٰ��̿�����(x, y);
        }

        /// <summary>
        /// ������ ������� ���󰡸� ������ ����
        /// </summary>
        /// <param name="x">���� ��ǥ</param>
        /// <param name="y">���� ��ǥ</param>
        public void �������ġ����(int x, int y)
        {
            Point pos = ù��°��.��ġ;
            ù��°��.��ü����ġ����(x - pos.X, y - pos.Y);
        }

        /// <summary>
        /// ������ ������� ���󰡸� �׸��� ����
        /// </summary>
        /// <param name="g">�׷���</param>
        public void �׷���(Graphics g)
        {
            ��� pBlock = ù��°��;

            while (pBlock != null)
            {
                pBlock.�׷���(g);

                pBlock = pBlock.����������();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>����� 1�����</returns>
        public ��� ù���������()
        {
            return ù��°��;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�����Ȱ�</returns>
        public Object Clone()
        {
            ��������̳� container = (��������̳�)this.MemberwiseClone();
            if (ù��°�� != null)
                container.ù��°�� = (���)this.ù��°��.Clone();

            return container;
        }

        /// <summary>
        /// �� ������ �׼� ����
        /// </summary>
        public void �׼Ǽ���()
        {
            ù��°��.�׼�();
        }

        /// <summary>
        ///  ����� �����ϰ� �ִ� �簢�� ���� ��ǥ ���
        /// </summary>
        /// <returns>�簢�� ���� ��ǥ</returns>
        public Rectangle ��Ͽ�������()
        {
            ��� pBlock = ù��°��;
            Rectangle rc = new Rectangle(1000,1000,0,0);
            Point size = pBlock.ũ��;
            Point MaxLoc = new Point(ù��°��.��ġ.X, ù��°��.��ġ.Y);
            MaxLoc.X += ù��°��.ũ��.X;
            MaxLoc.Y += ù��°��.ũ��.Y;

            while (pBlock != null)
            {
                Point Loc = pBlock.��ġ;

                if (rc.Left > Loc.X)
                    rc.X = Loc.X;
                else if (MaxLoc.X < Loc.X + size.X)
                    MaxLoc.X = Loc.X + size.X;

                if (rc.Top > Loc.Y)
                    rc.Y = Loc.Y;
                else if (MaxLoc.Y < Loc.Y + size.Y)
                    MaxLoc.Y = Loc.Y + size.Y;
                
                pBlock = pBlock.����������();
            }

            rc.Width = MaxLoc.X - rc.Left;
            rc.Height = MaxLoc.Y - rc.Top;

            return rc;
        }
    }

    // �����ǵ� �������̳�

    public class ChairA���� : ��������̳�
    {
        // ���� �ڽ��� ��翡 �´� ��ǥ�� �� ����
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2 + 1, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -1, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2, -2, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2, -3, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.����������().����������().��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
        }
    }

    public class ChairB���� : ��������̳�
    {
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -2, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2, -3, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2 - 1, -1, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.����������().��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
        }
    }

    public class ���簢������ : ��������̳�
    {
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2 + 1, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -1, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2, -2, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2 + 1, -2, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // block2 �������� ȸ��
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
            //return false;
        }
    }

    public class Ah���� : ��������̳�
    {
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -2, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2, -3, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2 - 1, -2, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // block2 �������� ȸ��
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.����������().��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
        }
    }

    public class Staff���� : ��������̳�
    {
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -2, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2, -3, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2, -4, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // block2 �������� ȸ��
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.����������().��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
        }
    }

    public class StairsA���� : ��������̳�
    {
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -2, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2 + 1, -2, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2 + 1, -3, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // block2 �������� ȸ��
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.����������().��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
        }
    }

    public class StairsB���� : ��������̳�
    {
        public override void ����()
        {
            ��� block1 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block2 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block3 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();
            ��� block4 = ��ϻ�����.�ν��Ͻ�����().��ϻ�����();

            block1.�ʱ�ȭ(��.����ũ�� / 2, -1, block2);
            block2.�ʱ�ȭ(��.����ũ�� / 2, -2, block3);
            block3.�ʱ�ȭ(��.����ũ�� / 2 - 1, -2, block4);
            block4.�ʱ�ȭ(��.����ũ�� / 2 - 1, -3, null);

            ù��°�� = block1;
        }

        public override bool ��ε���()
        {
            // block2 �������� ȸ��
            // ȸ���ϱ� ���� ���� ��ǥ ������
            Point pos = ù��°��.����������().��ǥ;

            if (��ε���(ù��°��, pos))
                return true;

            // ȸ���� �ȵȴٸ� �������� �ٲ㼭 �õ��� ����
            else
            {
                ��� block = ù��°��;
                while (block != null)
                {
                    if (��ε���(ù��°��, block.��ǥ))
                        return true;

                    block = block.����������();
                }

                return false;
            }
        }
    }
}
