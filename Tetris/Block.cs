using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class Block
    {
        public Rectangle[] blockArr = new Rectangle[4];

        static Random rnd = new Random();
        public SolidBrush brushB = new SolidBrush(Color.White);

        public int[] posX = new int[4];
        public int[] posY = new int[4];

        public bool isFixed = false;
        public Block()
        {
            switch (rnd.Next(1, 8))
            {
                case 1:
                    IBlock();
                    break;

                case 2:
                    LBlock();
                    break;

                case 3:
                    LRightBlock();
                    break;

                case 4:
                    ReverseT();
                    break;

                case 5:
                    OBlock();
                    break;

                case 6:
                    ZBlock();
                    break;

                case 7:
                    ZBlockLeft();
                    break;
            }
        }

        public void IBlock()
        {
            brushB = new SolidBrush(Color.BlueViolet);
            Rectangle rectangle1 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle3 = new Rectangle(110, 44, 22, 22);
            Rectangle rectangle4 = new Rectangle(110, 66, 22, 22);

            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void LBlock()
        {
            brushB = new SolidBrush(Color.Green);
            Rectangle rectangle1 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle3 = new Rectangle(110, 44, 22, 22);
            Rectangle rectangle4 = new Rectangle(88, 44, 22, 22);

            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void OBlock()
        {
            brushB = new SolidBrush(Color.Red);
            Rectangle rectangle1 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(88, 0, 22, 22);
            Rectangle rectangle3 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle4 = new Rectangle(88, 22, 22, 22);

            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void LRightBlock()
        {
            brushB = new SolidBrush(Color.Chocolate);
            Rectangle rectangle1 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle3 = new Rectangle(110, 44, 22, 22);
            Rectangle rectangle4 = new Rectangle(132, 44, 22, 22);

            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void ZBlock()
        {
            brushB = new SolidBrush(Color.DeepPink);
            Rectangle rectangle1 = new Rectangle(88, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle3 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle4 = new Rectangle(132, 22, 22, 22);
            ;
            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void ZBlockLeft()
        {
            brushB = new SolidBrush(Color.Blue);
            Rectangle rectangle1 = new Rectangle(132, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle3 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle4 = new Rectangle(88, 22, 22, 22);

            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void ReverseT()
        {
            brushB = new SolidBrush(Color.Orange);
            Rectangle rectangle1 = new Rectangle(110, 0, 22, 22);
            Rectangle rectangle2 = new Rectangle(110, 22, 22, 22);
            Rectangle rectangle3 = new Rectangle(132, 22, 22, 22);
            Rectangle rectangle4 = new Rectangle(88, 22, 22, 22);

            blockArr[0] = rectangle1;
            blockArr[1] = rectangle2;
            blockArr[2] = rectangle3;
            blockArr[3] = rectangle4;
        }

        public void Rotate()
        {
            for (int i = 0; i < blockArr.Length; i++)
            {
                if (i != 1)
                {
                    int tempX = blockArr[i].X - blockArr[1].X;
                    int tempY = blockArr[i].Y - blockArr[1].Y;

                    double x = tempX * 0 - tempY * 1;
                    double y = tempX * 1 + tempY * 0;
                    x = blockArr[1].X + x;
                    y = blockArr[1].Y + y;

                    blockArr[i].X = (int)x;
                    blockArr[i].Y = (int)y;
                }
            }
        }
    }
}
