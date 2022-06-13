using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Labyrinthe_Complex
{
    class Minautore
    {
        private Timer _minMovement = new Timer(150);
        Random rnd = new Random(); // Variable qui trouve un nombre aléatoire
        public int[,] Lab { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public int PosXMin { get; set; }
        public Minautore(int x, int y, int[,] lab, int left, int top)
        {
            this.MinX = x;
            this.MinY = y;
            this.Lab = lab;
            this.Left = left;
            this.Top = top;
            this.PosXMin = x * 2;


            _minMovement.Elapsed += new ElapsedEventHandler(Mouvement);
            //Débute le timer
            _minMovement.Start();
        }


        public void Mouvement(object source, ElapsedEventArgs e)
        {


            int rdnMin = rnd.Next(0, 4);
            if (rdnMin == 0)
            {
                if (MinY > 2)
                {
                    if (Lab[PosXMin/2, MinY - 1] != 0)
                    {
                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, PosXMin + Left, MinY + Top - 1);
                        MinY -- ;
                    }
                }
            }
            else if (rdnMin == 1)
            {
                if (MinY < Lab.GetLength(1) - 2)
                {
                    if (Lab[PosXMin / 2, MinY + 1] != 0)
                    {
                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, PosXMin + Left, MinY + Top + 1);
                        MinY ++;
                    }

                }
            }
            else if (rdnMin == 2)
            {
                if (MinX > 2)
                {
                    if (Lab[PosXMin / 2 - 1, MinY] != 0)
                    {

                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, (PosXMin + Left) - 2, MinY + Top);
                        PosXMin -= 2;
                    }
                }
            }
            else if (rdnMin == 3)
            {
                if (MinX < Lab.GetLength(0) - 2)
                {
                    if (Lab[PosXMin / 2 + 1, MinY] != 0)
                    {
                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, PosXMin + Left + 2, MinY + Top);
                        PosXMin += 2;
                    }
                }
            }
            


        }
        public void Stop()
        {
            _minMovement.Stop();
        }
       

    }
}
