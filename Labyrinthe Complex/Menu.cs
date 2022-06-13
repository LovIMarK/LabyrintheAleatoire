using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Labyrinthe_Complex
{
   
    public class Menu
    {
        private bool _difficutly;//Variable qui stoque l'état de l'option difficulté   
        public void StartGameSettings()
        {
            Clear();
           
            CursorVisible = false;
            int topPos = 16;
            int startLeftPos = 4;
            int userChoice = 0;
            int userLabWidth = 0;
            int userLabHeigth = 0;
            ConsoleKeyInfo keyInfo;


            SetCursorPosition(startLeftPos, topPos - 3);
            WriteLine("Bienvenue dans le Spicy Invador");

            SetCursorPosition(1, topPos);
            ForegroundColor = ConsoleColor.Cyan;
            Write(">> ");
            ResetColor();
            SetCursorPosition(startLeftPos, 16);
            WriteLine("Jouer");
            SetCursorPosition(startLeftPos, 17);
            WriteLine("Options");
            SetCursorPosition(startLeftPos, 18);
            WriteLine("Résolution");
            SetCursorPosition(startLeftPos, 19);
            WriteLine("About");
            SetCursorPosition(startLeftPos, 20);
            WriteLine("Exit");

            //Déplace la flèche dans le menu du jeu
            do
            {
                keyInfo = ReadKey(true);

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {

                    MoveBufferArea(1, topPos, 2, 2, 1, topPos + 1);
                    topPos++;
                    if (topPos == 21)
                    {
                        topPos = 16;
                        MoveBufferArea(1, 21, 2, 2, 1, topPos);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {

                    MoveBufferArea(1, topPos, 2, 2, 1, topPos - 1);
                    topPos--;
                    if (topPos == 15)
                    {
                        topPos = 20;
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
                    do
                    {
                        try
                        {
                            Write("Longeur du labyrinthe (chiffre impare plus petit que 80) : ");
                            userLabWidth = int.Parse(ReadLine());
                            Write("Hauteur du labyrinthe (chiffre impare plus petit que 60) : ");
                            userLabHeigth = int.Parse(ReadLine());
                        }
                        catch
                        {
                            WriteLine("Merci donner un valeure correcte ! ");
                        }
                    } while (userLabWidth > 80 && userLabHeigth > 60);
                    int[,] labyrinthIni = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe initial
                    int[,] labyrinthClose = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe avec un parcours aléatoirement construit
                    int[,] reso = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe avec la solution du parcours le plus court
                    bool[,] passed = new bool[userLabWidth, userLabHeigth];
                    Labyrinthe lab = new Labyrinthe(labyrinthIni, labyrinthClose, reso, passed);
                    lab.Show();
                    lab.Play();

                    break;
                //Ouvre les options
                case 17:
                    Options();
                    break;
                //Ouvre le score
                case 18:
                    Clear();
                    do
                    {
                        try
                        {
                            Write("Longeur du labyrinthe (chiffre impare plus petit que 80) : ");
                            userLabWidth = int.Parse(ReadLine());
                            Write("Hauteur du labyrinthe (chiffre impare plus petit que 60) : ");
                            userLabHeigth = int.Parse(ReadLine());
                        }
                        catch
                        {
                            WriteLine("Merci donner un valeure correcte ! ");
                        }
                    } while (userLabWidth > 80 && userLabHeigth > 60);
                    labyrinthIni = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe initial
                    labyrinthClose = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe avec un parcours aléatoirement construit
                    reso = new int[userLabWidth, userLabHeigth]; // Tableau à 2 dimensions du labyrinthe avec la solution du parcours le plus court
                   passed = new bool[userLabWidth, userLabHeigth];
                    Labyrinthe labRe = new Labyrinthe(labyrinthIni, labyrinthClose, reso, passed);
                    labRe.Show();
                    labRe.Reso();
                    ReadKey();
                    break;
                //Ouvre le à propos
                case 19:
                
                    break;
                //Quitte le programme
                case 20:
                    Environment.Exit(0);
                    break;



            }

        }
        public void Options()
        {
            ConsoleKeyInfo keyInfo;
          
            string difficulty = "";
           

            if (!_difficutly)
            {
                difficulty = "    Facile";
            }
            else if (_difficutly)
            {
                difficulty = " Difficile";
            }

            int topPos = 4;
            int userChoice = 0;


            Clear();

            WriteLine("Option");
            WriteLine("--------------------");
            SetCursorPosition(1, topPos);
            ForegroundColor = ConsoleColor.Cyan;
            Write(">> ");
            ResetColor();
            SetCursorPosition(4, topPos );
            WriteLine("Difficulté " + difficulty);
            SetCursorPosition(4, topPos + 2);
            WriteLine("Retour ");

            do
            {  //Déplace la flèche dans le menu des options
                do
                {
                    keyInfo = ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.DownArrow )
                    {

                        MoveBufferArea(1, topPos, 2, 2, 1, topPos + 2);
                        topPos += 2;
                        if(topPos >6)
                        {
                            MoveBufferArea(1, topPos, 2, 2, 1, topPos -4);
                            topPos -= 4;
                        }

                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow )
                    {
                        MoveBufferArea(1, topPos, 2, 2, 1, topPos - 2);
                        topPos -= 2;
                        if(topPos<4)
                        {
                            MoveBufferArea(1, topPos, 2, 2, 1, topPos + 4);
                            topPos += 4;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        userChoice = topPos - 4;
                    }

                }
                while (keyInfo.Key != ConsoleKey.Enter);

                //Change les options de son et de difficulté 
                switch (userChoice)
                {

                    case 0:
                        _difficutly = !_difficutly;
                        if (!_difficutly)
                        {
                            difficulty = "    Facile";
                        }
                        else if (_difficutly)
                        {
                            difficulty = " Difficile";
                        }
                        SetCursorPosition(4, topPos);
                        WriteLine("Difficulté " + difficulty);
                        break;
                    case 2:
                        StartGameSettings();
                        break;




                }
            } while (userChoice != 2);

        }

    }
        
}
