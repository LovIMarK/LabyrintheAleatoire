//Author            : Mark Lovink
//Date              : 14.06.2022
//Company           : Etml, Lausanne
//Description       : Classe qui s'occupe du menu du jeu

using System;
using static System.Console;


namespace Labyrinthe_Complex
{

    /// <summary>
    /// Menu avec la hauteur et longeur du labyrinthe et lance l'affichage des labyrinthe
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Méthode qui affiche le menu du jeu, Titre, Jouer, Résolution, Exit
        /// </summary>
        public void StartGameSettings()
        {
            Clear();
            SetWindowSize(90, 30);//Taille de la console
            CursorVisible = false; // Curseur invisible
            int topPos = 16;//Position de départ par rapport au haut de la console
            int startLeftPos = 4;//Position de départ par rapport à la gauche de la console
            int userChoice = 0;//Choix du joueur
            int userLabWidth = 0;//Longeur du labyrinthe
            int userLabHeigth = 0;//Hauteur du labyrinthe
            ConsoleKeyInfo keyInfo;//Info sur la touche utilisée
            do
            {
                Clear();
                ForegroundColor = ConsoleColor.Yellow;
                //Titre du programme
                WriteLine(@"  
    _                _                      _           _     _            
   | |              | |                    (_)         | |   | |           
   | |        __ _  | |__    _   _   _ __   _   _ __   | |_  | |__     ___ 
   | |       / _` | | '_ \  | | | | | '__| | | | '_ \  | __| | '_ \   / _ \
   | |____  | (_| | | |_) | | |_| | | |    | | | | | | | |_  | | | | |  __/
   |______|  \__,_| |_.__/   \__, | |_|    |_| |_| |_|  \__| |_| |_|  \___|
                              __/ |                                        
                             |___/           ");



                ResetColor();
                SetCursorPosition(0, 12);

                //Récupère la longeur et hauteur que le joueur veut
                try
                {
                    Write("Longeur du labyrinthe");
                    ForegroundColor = ConsoleColor.Red;
                    Write(" (chiffre impair plus grand que 4 plus petit que 106) : ");
                    ResetColor();
                    userLabWidth = int.Parse(ReadLine());
                    Write("Hauteur du labyrinthe");
                    ForegroundColor = ConsoleColor.Red;
                    Write(" (chiffre impair plus grand que 8 plus petit que 58) : ");
                    ResetColor();
                    userLabHeigth = int.Parse(ReadLine());
                }
                //Si la valeur donnée retourne une erreur
                catch
                {
                    Clear();
                 
                }
                //Tant que les valeurs ne sont pas les bonnes
            } while (userLabWidth > 106 || userLabHeigth > 58 || userLabWidth % 2 != 1 || userLabHeigth % 2 != 1 || userLabHeigth < 8 || userLabWidth < 4);

            //Affiche les options
            SetCursorPosition(1, topPos);
            ForegroundColor = ConsoleColor.Cyan;
            Write(">> ");
            ResetColor();
            SetCursorPosition(startLeftPos, 16);
            WriteLine("Jouer");
            SetCursorPosition(startLeftPos, 17);
            WriteLine("Résolution");
            SetCursorPosition(startLeftPos, 18);
            WriteLine("Exit");



            int[,] labyrinthIni = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe initial
            int[,] labyrinthClose = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe avec un parcours aléatoirement construit
            bool[,] passed = new bool[userLabWidth, userLabHeigth];// // Tableau à 2 dimensions du labyrinthe avec le parcours du joueur
            int[,] reso = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe avec la solution du parcours le plus court




            //Déplace la flèche dans le menu du jeu
            do
            {
                keyInfo = ReadKey(true);

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {

                    MoveBufferArea(1, topPos, 2, 2, 1, topPos + 1);
                    topPos++;
                    if (topPos == 19)
                    {
                        topPos = 16;
                        MoveBufferArea(1, 19, 2, 2, 1, topPos);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {

                    MoveBufferArea(1, topPos, 2, 2, 1, topPos - 1);
                    topPos--;
                    if (topPos == 15)
                    {
                        topPos = 18;
                        MoveBufferArea(1, 15, 2, 2, 1, topPos);


                    }

                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    userChoice = topPos;
                }


            }
            while (keyInfo.Key != ConsoleKey.Enter);


            //Appel de méthode quand le joueur choisi une option dans le menu
            switch (userChoice)
            {
                //Lance le jeu
                case 16:
                    Clear();
                    Labyrinthe lab = new Labyrinthe(labyrinthIni, labyrinthClose, passed);
                    lab.Show();
                    lab.Play();
                    StartGameSettings();
                    break;
                //Lance la résolution
                case 17:
                    Clear();
                    Labyrinthe labRe = new Labyrinthe(labyrinthIni, labyrinthClose, reso);
                    labRe.Show();
                    labRe.Reso();
                    ReadKey();
                    StartGameSettings();
                    break;
                //Quitte le programme
                case 18:
                    Environment.Exit(0);
                    break;








            }

        }


    }

}
