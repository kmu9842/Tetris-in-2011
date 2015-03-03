using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public abstract class ��� : ICloneable
    {
        protected int ����ũ��;
        protected int ����ũ��;
        protected int ������ġ;
        protected int ������ġ;
        protected int ������ǥ;
        protected int ������ǥ;
        protected ��� ������;
        protected Bitmap ���̹���;

        /// <summary>
        /// ����ʱ�ȭ
        /// </summary>
        /// <param name="cx">����ũ��</param>
        /// <param name="cy">����ũ��</param>
        public ���(int cx, int cy)
        {
            ����ũ�� = cx;
            ����ũ�� = cy;
            ������ = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posX">���� ũ��</param>
        /// <param name="posY">���� ũ��</param>
        /// <param name="nextBlock">�������</param>
        public void �ʱ�ȭ(int posX, int posY, ��� nextBlock)
        {
            ������ǥ = posX;
            ������ǥ = posY;
            ������ġ = posX * ����ũ��;
            ������ġ = posY * ����ũ��;
            ������ = nextBlock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g">�׷���</param>
        public void �׷���(Graphics g)
        {         
            if(���̹��� != null)
                g.DrawImage(���̹���, ������ġ, ������ġ, ����ũ�� - 1, ����ũ�� - 1);
            else
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                g.FillRectangle(brush, ������ġ, ������ġ, ����ũ�� - 1, ����ũ�� - 1);
            }
        }

        /// <summary>
        /// �ڽŰ� ����� ��ü�� �����ϰ� �ڽŸ� �̵��Ѵ�.
        /// </summary>
        /// <param name="x">���� ��ǥ</param>
        /// <param name="y">���� ��ǥ</param>
        /// <returns>��������</returns>
        public bool ȥ�ڿ�����(int x, int y)
        {
            if (��.�ʾ���ǥ��(������ǥ + x, ������ǥ + y) && ��.�̰ź����(������ǥ + x, ������ǥ + y))
            {
                ������ǥ += x;
                ������ǥ += y;
                ������ġ = ������ǥ * ����ũ��;
                ������ġ = ������ǥ * ����ũ��;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// �ڽ��� ���� �ڽŰ� ����� ��ü���� �̵��Ѵ�.
        /// </summary>
        /// <param name="x">������ǥ</param>
        /// <param name="y">������ǥ</param>
        /// <returns>����?</returns>
        public bool �ٰ��̿�����(int x, int y)
        {
            if (��.�ʾ���ǥ��(������ǥ + x, ������ǥ + y) && ��.�̰ź����(������ǥ + x, ������ǥ + y))
            {

                if (������ != null)
                {
                    if (������.�ٰ��̿�����(x, y) == false)
                        return false;
                }

                ������ǥ += x;
                ������ǥ += y;
                ������ġ = ������ǥ * ����ũ��;
                ������ġ = ������ǥ * ����ũ��;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// ������ ��ġ�� �����Ѵ�.
        /// </summary>
        /// <param name="x">������ǥ</param>
        /// <param name="y">������ǥ</param>
        public void ��ü����ġ����(int x, int y)
        {
            if (������ != null)
            {
                ������.��ü����ġ����(x, y);
            }

            ������ġ += x;
            ������ġ += y;
        }

        public abstract void �׼�();

        public Object Clone()
        {
            ��� block =  (���)this.MemberwiseClone();

            ��� pBlock = block.������;
            while (pBlock != null)
            {
                pBlock = (���)pBlock.MemberwiseClone();
                pBlock = pBlock.������;
            }

            return block;
        }

        public Point ��ǥ
        {
            get
            {
                return new Point(������ǥ, ������ǥ);
            }
            set
            {
                ������ǥ = value.X;
                ������ǥ = value.Y;
            }
        }

        public Point ��ġ
        {
            get
            {
                return new Point(������ġ, ������ġ);
            }
            set
            {
                ������ġ = value.X;
                ������ġ = value.Y;
            }
        }

        public Point ũ��
        {
            get
            {
                return new Point(����ũ��, ����ũ��);
            }
            set
            {
                ����ũ�� = value.X;
                ����ũ�� = value.Y;
            }
        }

        public ��� ����������()
        {
            return ������;
        }

        public void ���������̰ɷ�(��� block)
        {
            ������ = block;
        }
    }

    public class �븻��� : ���
    {
        public �븻���(int cx, int cy) : base(cx, cy)
        {
            ���̹��� = new Bitmap("NormalBlock.jpg");
        }

        public override void �׼�()
        {
            //��.��_�Ѱ�_�����(������ǥ, ������ǥ);
            if (������ != null)
            {
                ������.�׼�();
            }
        }
    }

    public class ��Ʈ�պ�� : ���
    {
        public ��Ʈ�պ��(int cx, int cy) : base(cx, cy)
        {
          
        }

        public override void �׼�()
        {
            //if (������ != null)
            //{
            //    ������.�׼�();
            //}
        }
    }

    public class ���ε�Ʈ���̾����ۺ� : ��� 
    {
        public ���ε�Ʈ���̾����ۺ�(int cx, int cy) : base(cx, cy)
        {
            ���̹��� = new Bitmap("LineDestroyBlock.jpg");
        }

        public override void �׼�()
        {
            Program.Ifrm_main.PlayS(frm_main.��������.delOne);
              

            //��.��_�Ѱ�_�����(������ǥ, ������ǥ);
            ��.��������(������ǥ);
            if (������ != null)
            {
                ������.�׼�();
            }
        }
    }

    public class �Ѱ��ڻ�����ۺ� : ���
    {
        public �Ѱ��ڻ�����ۺ�(int cx, int cy)
            : base(cx, cy)
        {
            ���̹��� = new Bitmap("OneDestroyBlock.jpg");
        }

        public override void �׼�()
        {
            ��.��_�Ѱ�_�����(������ǥ, ������ǥ);
            ��.��_�Ѱ�_�����(������ǥ, ������ǥ + 1);
            
            if (������ != null)
            {
                ������.�׼�();
            }
        }
    }
}
