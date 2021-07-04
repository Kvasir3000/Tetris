using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Tetris
{
    class Counter
    {
        public static int[] coordinateY = new int[4];

        public static int[,] mapStat = {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}

        };

        public static void ChangeState(List<Block> fixedBlock)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    mapStat[i, j] = 0;
                }
            }
            
            for (int i = 0; i < Form1.xCoordinate.Count; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (Form1.xCoordinate[i] / 22 == k && Form1.yCoordinates[i] / 22 == j && Form1.yCoordinates[i] != -1)
                        {
                            mapStat[j, k] = 1;
                            if (j == 19 && k == 3)
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }

        public static bool EndGame()
        {
            bool end = false;

            for (int i = 0; i < 10; i++)
            {
                if (mapStat[0, i] == 1)
                {
                    end = true;
                    break;
                }
            }
            return end;
        }

        public static void CheckForFilledLine()
        {
            int lines = 0;
            int index = -1;

            for (int i = 0; i < 20; i++)
            {
                int line = 0;

                for (int j = 0; j < 10; j++)
                {
                    if (mapStat[i, j] == 1)
                    {
                        line++;
                    }
                }

                if (line == 10)
                {
                    lines++;
                    index++;
                    Form1.score += 100;
                    coordinateY[index] = i;

                    for (int k = 0; k < Form1.xCoordinate.Count; k++)
                    {
                        if (Form1.yCoordinates[k] == i)
                        {
                            Form1.yCoordinates.RemoveAt(k);
                            Form1.xCoordinate.RemoveAt(k);
                        }
                    }
                }
            }

            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();
                Form1.DeleteLine(coordinateY[i]);
                coordinateY[i] = -1;
            }
        }
    }
}
