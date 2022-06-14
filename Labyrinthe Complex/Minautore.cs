//Author            : Mark Lovink
//Date              : 14.06.2022
//Company           : Etml, Lausanne
//Description       : Classe qui instancie les minotaur

using System;
using System.Timers;
using static System.Console;

namespace Labyrinthe_Complex
{
    class Minotaur
    {
        private Timer _minMovement = new Timer(250);//Timer pour le déplacement des minotaures
        Random rnd = new Random(); // Variable qui trouve un nombre

        /// <summary>
        /// Getter de la taille du labyrinthe
        /// </summary>
        public int[,] Lab { get; }

        /// <summary>
        /// Get setter de l'axe horizontal du minotaure
        /// </summary>
        public int MinX { get; set; }

        /// <summary>
        ///  Get setter de l'axe vertical minotaure
        /// </summary>
        public int MinY { get; set; }

        /// <summary>
        /// Get setter de la position de départ par rapport à la gauche de la console
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Get setter de la position de départ par rapport au haut de la console
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Get setter de l'axe vertical minotaure * 2 pour l'affichage
        /// </summary>
        public int PosXMin { get; set; }


        /// <summary>
        /// Constructeur des minotaures
        /// </summary>
        /// <param name="x">Position de l'axe horizontal</param>
        /// <param name="y">Position de l'axe vertical</param>
        /// <param name="lab">la taille du labyrinthe</param>
        /// <param name="left">la position de départ par rapport à la gauche de la console</param>
        /// <param name="top">la position de départ par rapport au haut de la console</param>
        public Minotaur(int x, int y, int[,] lab, int left, int top)
        {
            this.MinX = x;
            this.MinY = y;
            this.Lab = lab;
            this.Left = left;
            this.Top = top;
            this.PosXMin = x * 2;

            //Associe le timer avec la méthode qui déplace les des minotaures
            _minMovement.Elapsed += new ElapsedEventHandler(Mouvement);
            //Débute le timer
            _minMovement.Start();
        }
        public Minotaur()
        { }

            /// <summary>
            /// Méthode qui déplace aléatoirement les minotaures dans le labyrinthe
            /// </summary>
            /// <param name="source"></param>
            /// <param name="e"></param>
            public void Mouvement(object source, ElapsedEventArgs e)
        {

            //Une valeur entre 0 et 3
            int rdnMin = rnd.Next(0, 4);
            //Si la valeur est 0
            if (rdnMin == 0)
            {
                //Si la valeur ne touche pas les limites du labyrinthe
                if (MinY > 2)
                {
                    //Déplace d'un caractères en bas le minotaure si il n'y a pas de mur
                    if (Lab[PosXMin/2, MinY - 1] != 0)
                    {
                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, PosXMin + Left, MinY + Top - 1);
                        MinY -- ;
                    }
                }
            }
            //Si la valeur est 1
            else if (rdnMin == 1)
            {
                //Si la valeur ne touche pas les limites du labyrinthe
                if (MinY < Lab.GetLength(1) - 2)
                {
                    //Déplace d'un caractères en haut le minotaure si il n'y a pas de mur
                    if (Lab[PosXMin / 2, MinY + 1] != 0)
                    {
                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, PosXMin + Left, MinY + Top + 1);
                        MinY ++;
                    }

                }
            }
            //Si la valeur est 2
            else if (rdnMin == 2)
            {
                //Si la valeur ne touche pas les limites du labyrinthe
                if (MinX > 2)
                {
                    //Déplace de deux caractères à droite le minotaure si il n'y a pas de mur
                    if (Lab[PosXMin / 2 - 1, MinY] != 0)
                    {

                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, (PosXMin + Left) - 2, MinY + Top);
                        PosXMin -= 2;
                    }
                }
            }
            //Si la valeur est 3
            else if (rdnMin == 3)
            {
                //Si la valeur ne touche pas les limites du labyrinthe
                if (MinX < Lab.GetLength(0) - 2)
                {
                    //Déplace de deux caractères à gauche le minotaure si il n'y a pas de mur
                    if (Lab[PosXMin / 2 + 1, MinY] != 0)
                    {
                        MoveBufferArea(PosXMin + Left, MinY + Top, 2, 1, PosXMin + Left + 2, MinY + Top);
                        PosXMin += 2;
                    }
                }
            }
            


        }
        /// <summary>
        /// Méthode qui arrête le timer de déplacement
        /// </summary>
        public void Stop()
        {
            //Arrête le timer
            _minMovement.Stop();
        }
       

    }
}
