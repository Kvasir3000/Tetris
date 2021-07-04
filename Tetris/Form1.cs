using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Graphics g;
        Graphics g2;
        Pen p = new Pen(Color.Black);
        Rectangle rectangle = new Rectangle(110, 0, 22, 22);
        SolidBrush brush = new SolidBrush(Color.Red);
        Block block;
        static List<Block> FixedBlocks;
        public static int score = 0;
        public static List<int> xCoordinate;
        public static List<int> yCoordinates;

        public void SortCoordinates()
        {
            for (int i = 0; i < xCoordinate.Count; i++)
            {
                if (yCoordinates[i] == -1)
                {
                    xCoordinate.RemoveAt(i);
                    yCoordinates.RemoveAt(i);
                }
            }

            for (int i = 0; i < xCoordinate.Count; i++)
            {
                for (int j = 0; j < xCoordinate.Count - 1; j++)
                {
                    if (xCoordinate[j] > xCoordinate[j + 1])
                    {
                        int tempX = xCoordinate[j];
                        xCoordinate[j] = xCoordinate[j + 1];
                        xCoordinate[j + 1] = tempX;

                        int tempY = yCoordinates[j];
                        yCoordinates[j] = yCoordinates[j + 1];
                        yCoordinates[j + 1] = tempY;
                    }
                }
            }
            Console.WriteLine();
        }

        public void FillArray()
        {
            xCoordinate.Clear();
            yCoordinates.Clear();

            for (int i = 0; i < FixedBlocks.Count; i++)
            {
                for (int j = 0; j < FixedBlocks[i].blockArr.Length; j++)
                {
                    if (FixedBlocks[i].blockArr[j].IsEmpty == false)
                    {
                        xCoordinate.Add(FixedBlocks[i].blockArr[j].X);
                        yCoordinates.Add(FixedBlocks[i].blockArr[j].Y);
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            g2 = CreateGraphics();
            FixedBlocks = new List<Block>();
            xCoordinate = new List<int>();
            yCoordinates = new List<int>();
        }


        private void DrawMap()
        {
            Point pt1 = new Point();
            pt1.X = 0;
            pt1.Y = 0;
            Point pt2 = new Point();
            pt2.X = 220;
            pt2.Y = 0;
            Point pt3 = new Point();
            Point pt4 = new Point();

            for (int i = 0; i < 21; i++)
            {
                pt3.X = pt1.X;
                pt4.Y = pt1.Y;
                pt4.Y = 440;
                pt4.X = pt1.X;

                g.DrawLine(p, pt1, pt2);
                pt1.Y += 22;
                pt2.Y += 22;
            }

            for (int j = 0; j < 11; j++)
            {
                g.DrawLine(p, pt3, pt4);
                pt3.X += 22;
                pt4.X += 22;
            }
            DrawBlock();
        }


        private void DrawBlock()
        {
            for (int i = 0; i < block.blockArr.Length; i++)
            {
                g.FillRectangle(brush, block.blockArr[i]);
                g.DrawRectangle(p, block.blockArr[i]);
            }
        }

        int a = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (FixedBlocks != null)
            {
                DrawFixedBlocks();
            }

            if (a == 0)
            {
                block = new Block();
                brush = block.brushB;
                a++;
            }

            label2.Text = "Score: " + score.ToString();
            DrawMap();

            for (int i = 0; i < FixedBlocks.Count; i++)
            {
                for (int j = 0; j < FixedBlocks[i].blockArr.Length; j++)
                {
                    if (FixedBlocks[i].blockArr[j].IsEmpty == true)
                    {
                        FixedBlocks.RemoveAt(i);
                    }
                }
            }
        }

        private void DrawFixedBlocks()
        {
            for (int i = 0; i < FixedBlocks.Count; i++)
            {
                for (int j = 0; j < FixedBlocks[i].blockArr.Length; j++)
                {
                    if (FixedBlocks[i].blockArr[j].IsEmpty == false)
                    {
                        SolidBrush brushTemp = FixedBlocks[i].brushB;
                        g2.FillRectangle(brushTemp, FixedBlocks[i].blockArr[j]);
                        g2.DrawRectangle(p, FixedBlocks[i].blockArr[j]);
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bool check = true;
            check = TouchAnotherBlock(2);
            g.Clear(Color.White);

            foreach (Rectangle block in block.blockArr)
            {
                if (block.Y >= 418)
                {
                    check = false;
                    break;
                }
            }

            for (int i = 0; i < block.blockArr.Length; i++)
            {
                if (check == true)
                {
                    block.blockArr[i].Y = block.blockArr[i].Y + 22;
                }
                else
                {
                    a--;
                    FixedBlocks.Add(block);
                    FillArray();
                    SortCoordinates();
                    Counter.ChangeState(FixedBlocks);
                    Counter.CheckForFilledLine();
                    block = null;
                    break;
                }
            }

            bool end = Counter.EndGame();

            if (end == true)
            {
                label3.Visible = true;
                label2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                timer1.Stop();
                timer2.Stop();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool check = true;
            g.Clear(Color.White);

            if (e.KeyChar == 's')
            {
                check = TouchAnotherBlock(2);
                foreach (Rectangle block in block.blockArr)
                {
                    if (block.Y + 22 > 418)
                    {
                        check = false;
                    }
                }

                if (block.isFixed == true)
                {
                    check = false;
                }

                for (int i = 0; i < block.blockArr.Length; i++)
                {
                    if (check == true)
                    {
                        block.blockArr[i].Y += 22;
                    }
                }
            }

            if (e.KeyChar == 'd')
            {
                check = TouchAnotherBlock(1);

                foreach (Rectangle block in block.blockArr)
                {
                    if (block.X + 22 > 202)
                    {
                        check = false;
                    }
                }

                for (int i = 0; i < block.blockArr.Length; i++)
                {
                    if (check == true)
                    {
                        block.blockArr[i].X += 22;
                    }
                }
            }

            if (e.KeyChar == 'a')
            {
                check = TouchAnotherBlock(3);
                foreach (Rectangle block in block.blockArr)
                {
                    if (block.X - 22 < 0)
                    {
                        check = false;
                    }
                }

                for (int i = 0; i < block.blockArr.Length; i++)
                {
                    if (check == true)
                    {
                        block.blockArr[i].X -= 22;

                    }
                }
            }

            if (e.KeyChar == ' ')
            {
                block.Rotate();
                check = TouchAnotherBlock(2);
                block.Rotate();
                block.Rotate();
                block.Rotate();

                foreach (Rectangle block in block.blockArr)
                {
                    if (block.Y >= 418 || block.X - 22 < 0 || block.X + 22 >= 202 || block.X + 22 >= 418)
                    {
                        check = false;
                    }
                }

                if (check == true)
                {
                    block.Rotate();
                }
            }
        }

        private bool TouchAnotherBlock(int situation)
        {
            Rectangle[] tempBlock = new Rectangle[4];
            tempBlock[0] = block.blockArr[0];
            tempBlock[1] = block.blockArr[1];
            tempBlock[2] = block.blockArr[2];
            tempBlock[3] = block.blockArr[3];
            Block rotateBlock = new Block();
            int a = tempBlock.Length;

            bool check = true;
            switch (situation)
            {
                case 1:
                    tempBlock[0].X += 22;
                    tempBlock[1].X += 22;
                    tempBlock[2].X += 22;
                    tempBlock[3].X += 22;
                    break;

                case 2:
                    tempBlock[0].Y = tempBlock[0].Y + 22;
                    tempBlock[1].Y = tempBlock[1].Y + 22;
                    tempBlock[2].Y = tempBlock[2].Y + 22;
                    tempBlock[3].Y = tempBlock[3].Y + 22;
                    break;

                case 3:
                    tempBlock[0].X -= 22;
                    tempBlock[1].X -= 22;
                    tempBlock[2].X -= 22;
                    tempBlock[3].X -= 22;
                    break;
            }

            for (int k = 0; k < FixedBlocks.Count; k++)
            {
                for (int i = 0; i < tempBlock.Length; i++)
                {
                    for (int j = 0; j < tempBlock.Length; j++)
                    {
                        if (tempBlock[i].X == FixedBlocks[k].blockArr[j].X && tempBlock[i].Y == FixedBlocks[k].blockArr[j].Y)
                        {
                            check = false;
                            block.isFixed = true;
                            i = tempBlock.Length;
                            k = FixedBlocks.Count;
                            break;
                        }
                    }
                }
            }
            return check;
        }


        public static void DeleteLine(int yCoordinate)
        {
            for (int i = 0; i < FixedBlocks.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (FixedBlocks[i].blockArr[j].Y / 22 == yCoordinate && FixedBlocks[i].blockArr[j].Y != -1)
                    {
                        FixedBlocks[i].blockArr[j] = Rectangle.Empty;
                        FixedBlocks[i].blockArr[j].Y = -1;
                    }
                    if (FixedBlocks[i].blockArr[j].Y / 22 < yCoordinate && FixedBlocks[i].blockArr[j].Y != -1)
                    {
                        Console.WriteLine();
                        FixedBlocks[i].blockArr[j].Y += 22;
                    }
                }
            }
        }
    }
}
