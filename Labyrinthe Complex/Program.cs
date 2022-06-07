using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;

namespace Labyrinthe_Complex
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rdn = new Random(); // Variable qui trouve un nombre aléatoire

            ConsoleKeyInfo ckyKeyPress; // Valeur pour savoir sur quelle touche le joueur appuie.

            const int leftPos = 2;//Variable avec la position du départ gauche du labyrinthe
            const int topPos = 2; //Variable avec la position du départ haut du labyrinthe

            const int labI = 47;//Longeur du labyrinthe (impaire)
            const int LabJ = 21;//Largeur du labyrinthe (impaire)

            SetWindowSize((labI + 5)*2, LabJ + 5); // Grandeur de la console
            CursorVisible = false;//Curseur non visible

            int[,] labyrinthIni = new int[labI, LabJ]; // Tableau à 2 dimensions du labyrinthe initial
            int[,] labyrinthClose = new int[labI, LabJ]; // Tableau à 2 dimensions du labyrinthe avec un parcours aléatoirement construit
            int[,] reso = new int[labI, LabJ]; // Tableau à 2 dimensions du labyrinthe avec la solution du parcours le plus court
            bool[,] passed = new bool[labI, LabJ];




            int rdnNumber = 4; //Variable qui donne une valeur aux cases noirs
            int distance = 2;//Variable avec la valeur qui sépare la case de départ de celle de l'arrivée
            int wallOpened = 0;//Variable qui stocke le nombre de murs à changer
            bool dist = false; // Variable qui enregistre le changement de valeur de distance
            bool rdnClosed = true;

            bool passedOn = true;
            bool win = false;
            int playerX = 1;
            int playerY = 1;








            //Parcours le tableau à 2 dimensions contenant le labyrinthe de base
            for (sbyte i = 0; i < labyrinthIni.GetLength(0); i++)
            {
                for (sbyte j = 0; j < labyrinthIni.GetLength(1); j++)
                {

                    labyrinthIni[i, j] = 1;//1 colonne sur 2 on met un mur fixe
                    if (j % 2 == 1) //1 colonne sur 2 on met un efface un mur
                    {
                        if (i % 2 == 1)//1 ligne sur 2 on met un efface un mur
                        {
                            labyrinthIni[i, j] = 2;
                        }
                    }


                    //Efface l'entré du labyrinthe
                    labyrinthIni[0, 1] = 0;

                    //Efface la sortiedu labyrinthe
                    labyrinthIni[labyrinthIni.GetLength(0) - 1, labyrinthIni.GetLength(1) - 2] = 0;

                    if (labyrinthIni[i, j] == 1) //Si la valeur donné est 1 on affiche les murs blancs
                    {

                        SetCursorPosition(i*2 + leftPos, j + topPos);
                        ForegroundColor = ConsoleColor.DarkGray;
                        WriteLine("██");
                        ResetColor();
                    }
                    if (labyrinthIni[i, j] == 2)//Si la valeur donné est 2 on affiche les trous du chemin du labyrinthe coincé par les murs
                    {
                        SetCursorPosition(i*2 + leftPos, j + topPos);
                        WriteLine("  ");

                        //Si la valeur donné est 2 on donne une valeur à cette position dans le tableau du labyrinthe avec le chemin
                        labyrinthClose[i, j] = rdnNumber;
                        //On change le nombre en lui donnant une augmentation
                        rdnNumber++;
                    }

                }
            }


          

            ///////Choisir une cordonné au hasard dans le tableau et assemblé 2 murs entre eux

            do
            {
                wallOpened = labyrinthClose.Length;//On lui donne la valeur de ta taille du tableau
                int rdnI = rdn.Next(0, labyrinthClose.GetLength(0)); //Choix d'une cordonné aléatoire de la première valeur du tableau
                int rdnJ = rdn.Next(0, labyrinthClose.GetLength(1));//Choix d'une cordonné aléatoire de la seconde valeur du tableau


                if (labyrinthClose[rdnI, rdnJ] > 4 && labyrinthIni[rdnI, rdnJ] == 2) //Si la valeur du tableau labyrinthClose et plus grande que 4 par rapportau rdnNumber et si c'est un carctère de chemin dans le tableau labyrinthIni 
                {


                    do
                    {
                        int rdnWallAss = rdn.Next(0, 4); //Variable qui stocke une valeur entre 1 et 4

                        //Si la valeur est 0 on enlève le mur Nord 
                        if (rdnWallAss == 0)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rdnJ > 2)
                            {
                                //Si la case noire en dessous de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rdnI, rdnJ - 2] != labyrinthClose[rdnI, rdnJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rdnI, rdnJ - 1] = labyrinthClose[rdnI, rdnJ];

                                    int cell = labyrinthClose[rdnI, rdnJ - 2];
                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {

                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 
                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rdnI, rdnJ];
                                            }
                                        }
                                    }

                                }
                            }
                        }  //Si la valeur est 1 on enlève le mur Sud 
                        else if (rdnWallAss == 1)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rdnJ < labyrinthClose.GetLength(1) - 2)
                            {
                                //Si la case noire en dessus de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rdnI, rdnJ + 2] != labyrinthClose[rdnI, rdnJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rdnI, rdnJ + 1] = labyrinthClose[rdnI, rdnJ];
                                    int cell = labyrinthClose[rdnI, rdnJ + 2];

                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {
                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 

                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rdnI, rdnJ];
                                            }
                                        }
                                    }

                                }
                            }
                        } //Si la valeur est 2 on enlève le mur Ouest 
                        else if (rdnWallAss == 2)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rdnI > 2)
                            {
                                //Si la case noire à gauche de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rdnI - 2, rdnJ] != labyrinthClose[rdnI, rdnJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rdnI - 1, rdnJ] = labyrinthClose[rdnI, rdnJ];
                                    int cell = labyrinthClose[rdnI - 2, rdnJ];
                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {

                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 

                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rdnI, rdnJ];
                                            }
                                        }
                                    }


                                }
                            }
                        }//Si la valeur est 3 on enlève le mur Est 
                        else if (rdnWallAss == 3)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rdnI < labyrinthClose.GetLength(0) - 2)
                            {
                                //Si la case noire à droite de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rdnI + 2, rdnJ] != labyrinthClose[rdnI, rdnJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rdnI + 1, rdnJ] = labyrinthClose[rdnI, rdnJ];
                                    int cell = labyrinthClose[rdnI + 2, rdnJ];
                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {

                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 

                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rdnI, rdnJ];
                                            }
                                        }
                                    }

                                }

                            }

                        }

                        //Sinon (si aucun mur n'a été changé) valeur bool = faux
                        else
                        {
                            rdnClosed = false;
                        }

                    } while (!rdnClosed);//Tant qu'aucun mur n'a été changé recommence la boucle

                }


                //Parcours tout le tableau
                for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                {

                    for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                    {
                        //Si la valeur de la recherche n'est pas égale à la valeur aléatoire ou égale à la valeur de base (0) 
                        if (labyrinthClose[i, j] != labyrinthClose[rdnI, rdnJ] && labyrinthClose[i, j] != 0)
                        {
                            //On enlève unve valeur à wallOpened
                            wallOpened--;
                        }
                    }
                }





            } while (wallOpened != labyrinthClose.Length); //Tant que tout le tableau n'est pas égale à 0 ou une seule valeur de mur recommence

            ReadKey();
            //Parcours tout le tableau
            for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
            {

                for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                {
                    //Affiche le chemin créer aléatoirement dans le labyrinthe si la valeur est de 1
                    if (labyrinthClose[i, j] >= 1)
                    {
                        SetCursorPosition(i*2  + leftPos, j + topPos);
                        ForegroundColor = ConsoleColor.Black;
                        WriteLine("██");
                        ResetColor();

                    }
                }
            }

            //SetCursorPosition(playerX + leftPos, playerY + topPos);
            //ForegroundColor = ConsoleColor.Green;
            //WriteLine("█");
            //SetCursorPosition(playerX + leftPos - 1, playerY + topPos);
            //ForegroundColor = ConsoleColor.Green;
            //WriteLine("█");
            //ResetColor();
            //do
            //{

            //    ckyKeyPress = ReadKey(); // Connaître la réponse du joueur.

            //    if (ckyKeyPress.Key == ConsoleKey.LeftArrow && labyrinthClose[playerX - 1, playerY] > 0)
            //    {

            //        passed[playerX, playerY] = passedOn;
            //        playerX--;

            //    }
            //    else if (ckyKeyPress.Key == ConsoleKey.RightArrow && labyrinthClose[playerX + 1, playerY] > 0)
            //    {

            //        passed[playerX, playerY] = passedOn;
            //        playerX++;

            //    }
            //    else if (ckyKeyPress.Key == ConsoleKey.UpArrow && labyrinthClose[playerX, playerY - 1] > 0)
            //    {

            //        passed[playerX, playerY] = passedOn;
            //        playerY--;

            //    }
            //    else if (ckyKeyPress.Key == ConsoleKey.DownArrow && labyrinthClose[playerX, playerY + 1] > 0)
            //    {

            //        passed[playerX, playerY] = passedOn;
            //        playerY++;

            //    }

            //    if (!passed[playerX, playerY])
            //    {
            //        passedOn = true;
            //        SetCursorPosition(playerX + leftPos, playerY + topPos);
            //        ForegroundColor = ConsoleColor.Green;
            //        WriteLine("█");
            //        ResetColor();

            //    }
            //    else
            //    {
            //        SetCursorPosition(playerX + leftPos, playerY + topPos);
            //        ForegroundColor = ConsoleColor.Magenta;
            //        WriteLine("█");
            //        ResetColor();
            //        passedOn = false;
            //        passed[playerX, playerY] = passedOn;
            //    }

            //    if (playerX == labyrinthClose.GetLength(0) - 2 && playerY == labyrinthClose.GetLength(1) - 2)
            //    {
            //        SetCursorPosition(labyrinthClose.GetLength(0) - 1 + leftPos, labyrinthClose.GetLength(1) - 2 + topPos);
            //        ForegroundColor = ConsoleColor.Green;
            //        WriteLine("█");
            //        ResetColor();
            //        win = true;
            //    }
            //} while (!win);

            ReadKey();


            /////Résolution (J'appique une valeur 1 à la sortie du labyrinthe le programme va ensuite faire tout les chemin inverse du labyrinthe et va augmenter la valeur du chemin par rapport à la distance de la sortie)
            /////Première case à la sortie 1, une case plus tard la vlauer sera de 2 etc...

            reso[reso.GetLength(0) - 1, reso.GetLength(1) - 2] = 1; //Attribut à la sortie du labyrinthe la valeur 1 (à une case de la sortie) 
            do
            {


                //Parcours tout le tableau en commencent par la fin
                for (int i = labyrinthClose.GetLength(0) - 1; i > 0; i--)
                {

                    for (int j = labyrinthClose.GetLength(1) - 1; j > 0; j--)
                    {
                        //Que des if pour véréfier les 4 possibilitées
                        //Si la valeur du tableau est plus grande que 0 (Si la recherche du tableau vaut un chemin ou pas)
                        if (labyrinthClose[i, j] > 0)
                        {
                            //Si la valeur en dessous de la recherche du tableau n'est pas égale à 0 (Si la valeur du chemin en dessous de la recherche à déjà été effectué ou non)
                            if (reso[i, j - 1] != 0)
                            {
                                //Si la valeur sur laquelle la recherche s'effectue n'a pas encore été assigné
                                if (reso[i, j] == 0)
                                {
                                    //On donne à cette case une distance de plus que celle précedent
                                    distance = reso[i, j - 1] + 1;
                                    reso[i, j] = distance;
                                    //Valeur bool est vrai
                                    dist = true;
                                }
                            }
                            //Si la valeur en dessus de la recherche du tableau n'est pas égale à 0 (Si la valeur du chemin en dessous de la recherche à déjà été effectué ou non)
                            if (reso[i, j + 1] != 0)
                            {
                                //Si la valeur sur laquelle la recherche s'effectue n'a pas encore été assigné
                                if (reso[i, j] == 0)
                                {
                                    //On donne à cette case une distance de plus que celle précedent
                                    distance = reso[i, j + 1] + 1;
                                    reso[i, j] = distance;
                                    //Valeur bool est vrai
                                    dist = true;
                                }
                            }
                            //Si la valeur à gauche de la recherche du tableau n'est pas égale à 0 (Si la valeur du chemin en dessous de la recherche à déjà été effectué ou non)

                            if (reso[i - 1, j] != 0)
                            {
                                //Si la valeur sur laquelle la recherche s'effectue n'a pas encore été assigné
                                if (reso[i, j] == 0)
                                {
                                    //On donne à cette case une distance de plus que celle précedent
                                    distance = reso[i - 1, j] + 1;
                                    reso[i, j] = distance;
                                    //Valeur bool est vrai
                                    dist = true;
                                }
                            }
                            //Si la valeur à droite de la recherche du tableau n'est pas égale à 0 (Si la valeur du chemin en dessous de la recherche à déjà été effectué ou non)
                            if (reso[i + 1, j] != 0)
                            {
                                //Si la valeur sur laquelle la recherche s'effectue n'a pas encore été assigné
                                if (reso[i, j] == 0)
                                {
                                    //On donne à cette case une distance de plus que celle précedent
                                    distance = reso[i + 1, j] + 1;
                                    reso[i, j] = distance;
                                    //Valeur bool est vrai
                                    dist = true;
                                }
                            }
                        }
                        //for (int a = 0; a < distance; a++)
                        //{


                        //    for (sbyte b = 0; b < reso.GetLength(0); b++)
                        //    {

                        //        for (sbyte c = 0; c < reso.GetLength(1); c++)
                        //        {
                        //            if (reso[b, c] == a)
                        //            {
                        //                ForegroundColor = ConsoleColor.Green;
                        //                SetCursorPosition(b * 3 + leftPos + 20, c + topPos);
                        //                WriteLine(reso[b, c]);


                        //            }
                        //        }
                        //    }
                        //}

                        //    if (j > 1)
                        //{
                        //    if (labyrinthClose[i, j - 1] > 0 && reso[i, j - 1]==0)
                        //    {
                        //        reso[i, j - 1] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (j < reso.GetLength(1) - 1)
                        //{
                        //    if (labyrinthClose[i, j + 1] > 0 && reso[i, j + 1] == 0)
                        //    {
                        //        reso[i, j + 1] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (i > 1)
                        //{
                        //    if (labyrinthClose[i - 1, j] > 0 && reso[i - 1, j] ==0)
                        //    {
                        //        reso[i - 1, j] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (i < reso.GetLength(0) - 1 && reso[i + 1, j] == 0)
                        //{
                        //    if (labyrinthClose[i + 1, j] > 0 /*&& reso[i + 1, j] == distance - 1*/)
                        //    {
                        //        reso[i + 1, j] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (j > 1)
                        //{
                        //    if (labyrinthClose[i, j - 1] > 0 && reso[i, j - 1] == distance - 1)
                        //    {
                        //        reso[i, j - 1] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (j < reso.GetLength(1) - 1)
                        //{
                        //    if (labyrinthClose[i, j + 1] > 0 && reso[i, j + 1] == distance - 1)
                        //    {
                        //        reso[i, j + 1] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (i > 1)
                        //{
                        //    if (labyrinthClose[i - 1, j] > 0 && reso[i - 1, j] == distance - 1)
                        //    {
                        //        reso[i - 1, j] = distance;
                        //        dist = true;
                        //    }
                        //}
                        //if (i < reso.GetLength(0) - 1)
                        //{
                        //    if (labyrinthClose[i + 1, j] > 0 && reso[i + 1, j] == distance - 1)
                        //    {
                        //        reso[i + 1, j] = distance;
                        //        dist = true;
                        //    }
                        //}


                        //Si dist est vrai (Si le programme est rentré dans un if précédent)
                        if (dist)
                        {
                            //Augmente le chiffre des distance
                            distance++;
                        }
                        //Reset du bool
                        dist = false;

                    }
                }
            } while (reso[1, 1] != distance - 1); //Tant que la valeur du début du labyrinthe n'est pas égale à la distance recommence




            //Parcours tout le tableau
            for (sbyte i = 0; i < reso.GetLength(0); i++)
            {
                for (sbyte j = 0; j < reso.GetLength(1); j++)
                {
                    //Changer les 0 en une valeur quelqconque pour parer les problème dans la suite 
                    if (reso[i, j] == 0)
                    {
                        reso[i, j] = reso.Length;
                    }

                }
            }


            int x = 1;
            int y = 1;
            //Dessine le premier caractère du labyrinthe en vert
            SetCursorPosition(x - 1 + leftPos, y + topPos);
            ForegroundColor = ConsoleColor.Green;
            WriteLine("██");
            ResetColor();
            

            //Tant que la dernière case du labyrinthe n'est pas atteinte
            while (x != reso.GetLength(0) - 1 && y != reso.GetLength(1) - 1)
            {
                //Ralenti la boucle
                Thread.Sleep(20);
                //Dessine un caractère vert pour montrer le chemin su la position donné
                SetCursorPosition(x*2 + leftPos, y + topPos);
                ForegroundColor = ConsoleColor.Green;
                WriteLine("██");
                ResetColor();

                ///Problème (ne revenait pas en arrière) donc affihcage de valeur sur le problème
                //SetCursorPosition(0, 0);
                //WriteLine(reso[x, y]);

                //Si la position Nord est plus petit que le Sud,Ouest,Est, descendre le caractère de réponse
                if (reso[x, y - 1] <= reso[x, y + 1] && reso[x, y - 1] <= reso[x - 1, y] && reso[x, y - 1] <= reso[x + 1, y])
                {
                    y--;
                }
                //Si la position Sud est plus petit que le Nord,Ouest,Est, monter le caractère de réponse
                else if (reso[x, y + 1] <= reso[x, y - 1] && reso[x, y + 1] <= reso[x - 1, y] && reso[x, y + 1] <= reso[x + 1, y])
                {
                    y++;
                }
                //Si la position Ouest est plus petit que le Nord,Sud,Est, déplace le caractère de réponse à gauche
                else if (reso[x - 1, y] <= reso[x, y - 1] && reso[x - 1, y] <= reso[x, y + 1] && reso[x - 1, y] <= reso[x + 1, y])
                {
                    x--;
                }
                //Si la position Est est plus petit que le Nord,Sud,Ouest, déplace le caractère de réponse à droite
                else if (reso[x + 1, y] <= reso[x, y - 1] && reso[x + 1, y] <= reso[x, y + 1] && reso[x + 1, y] <= reso[x - 1, y])
                {
                    x++;
                }

                //Si l'algoritme arrive à l'avant dernière case 
                if (x == (labyrinthClose.GetLength(0) - 2)  && y == labyrinthClose.GetLength(1) - 2)
                {
                    //remplir la dernière case
                    SetCursorPosition((x *2)+2 + leftPos, y + topPos);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("██");
                    ResetColor();

                }


            }
            ReadLine();
        }
    }
}

