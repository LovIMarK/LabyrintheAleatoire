//Author            : Mark Lovink
//Date              : 14.06.2022
//Company           : Etml, Lausanne
//Description       : Classe qui instancie les labyrinthes

using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;

namespace Labyrinthe_Complex
{
    public class Labyrinthe
    {
        //Liste de tout les minotaures
        private List<Minotaur> _minotaur = new List<Minotaur>();

        /// <summary>
        /// Getter de la taille du labyrinthe
        /// </summary>
        public int[,] labyrinthIni { get; set; }

        /// <summary>
        /// Getter de la taille du labyrinthe
        /// </summary>
        public int[,] labyrinthClose { get; set; }

        /// <summary>
        /// Getter de la taille du labyrinthe
        /// </summary>
        public int[,] reso { get; set; }

        /// <summary>
        /// Getter de la taille du labyrinthe
        /// </summary>
        public bool[,] passed { get; set; }

        /// <summary>
        /// Labyrinthe pour le jeu avec les paramètres pour le jeu
        /// </summary>
        /// <param name="labyrinthInit">tableau du labyrinthe avec juste des murs pour fermer le labyrinthe</param>
        /// <param name="labyrinthClosed">tableau du labyrinthe avec les murs former </param>
        /// <param name="passed">tableau du labyrinthe avec le passage du joueur</param>
        public Labyrinthe(int[,] labyrinthInit ,int[,] labyrinthClosed, bool[,] passed)
        {
            this.labyrinthIni = labyrinthInit;
            this.labyrinthClose = labyrinthClosed;
            this.passed = passed;


        }

        /// <summary>
        /// Labyrinthe pour la résolution avec les paramètres pour la résolution
        /// </summary>
        /// <param name="labyrinthInit">tableau du labyrinthe avec juste des murs pour fermer le labyrinthe</param>
        /// <param name="labyrinthClosed">tableau du labyrinthe avec les murs former</param>
        /// <param name="resol">tableau du labyrinthe avec le chemin le plus optimal pour résoudre le labyrinthe</param>
        public Labyrinthe(int[,] labyrinthInit, int[,] labyrinthClosed, int[,] resol)
        {
            this.labyrinthIni = labyrinthInit;
            this.labyrinthClose = labyrinthClosed;
            this.reso = resol;


        }

        ConsoleKeyInfo ckyKeyPress; // Valeur pour savoir sur quelle touche le joueur appuie.

        const int leftPos = 2;//Variable avec la position du départ gauche du labyrinthe
        const int topPos = 2; //Variable avec la position du départ haut du labyrinthe

        int rndNumber = 4; //Variable qui donne une valeur aux cases noirs
        int distance = 2;//Variable avec la valeur qui sépare la case de départ de celle de l'arrivée
        int wallOpened = 0;//Variable qui stocke le nombre de murs à changer
        bool dist = false; // Variable qui enregistre le changement de valeur de distance
        bool rndClosed = true;

        bool passedOn = true;//Si le joueur est passé sur une case
        bool win = false;//Si le joueur à gagné ou perdu
        int playerX = 1;//Position du joueur 
        int playerY = 1;//Position du joueur 
        Random rnd = new Random(); // Variable qui trouve un nombre aléatoire

        public void Play()
        {

            SetCursorPosition(WindowWidth / 2 - 9, 0);
            WriteLine("                                        ");
            SetCursorPosition(playerX + leftPos - 1, playerY + topPos);
            ForegroundColor = ConsoleColor.Green;
            WriteLine("██");
            SetCursorPosition(playerX + leftPos + 1, playerY + topPos);
            ForegroundColor = ConsoleColor.Green;
            WriteLine("██");
            ResetColor();
            int minX = 0;//Position de l'axe horizontal du minotaure
            int minY = 0;//Position de l'axe vertical du minotaure
            int minNumber = 1;
            Minotaur simple = new Minotaur(); //Instancie un minotaur 

            //Dépendant de la grandeur du labyrinthe il y a plus de minotaures
            if (labyrinthClose.Length > 350)
            {
                minNumber = 4;
            }
            if (labyrinthClose.Length > 850)
            {
                minNumber = 6;
            }
            if (labyrinthClose.Length > 1250)
            {
                minNumber = 8;
            }

            //Affiche le nombre de minotaure
            for (int i = 0; i < minNumber; i++)
            {
                do
                {
                    minX = rnd.Next(0, labyrinthClose.GetLength(0));//Position aléatoire de l'axe horizontal du minotaure
                    minY = rnd.Next(0, labyrinthClose.GetLength(1));//Position aléatoire de l'axe vertical du minotaure
                    //Tant que la position n'est pas dans le labyrinthe et n'est pas sur les 3 première ou 3 dernière ligne du labyrinthe
                } while (labyrinthClose[minX, minY] == 0 || minY == 1 || minY == 2 || minY == 3 || minY == labyrinthClose.GetLength(1)-1 || minY == labyrinthClose.GetLength(1)-2 || minY == labyrinthClose.GetLength(1)-3);

                SetCursorPosition(minX * 2 + leftPos, topPos + minY);
                WriteLine("██");
                simple = new Minotaur(minX, minY, labyrinthClose, leftPos, topPos);
                //Ajout du minotaure dans une liste
                _minotaur.Add(simple);

            }

            do
            {
                //Si une touche est utilisée
                if (KeyAvailable)
                {

                    ckyKeyPress = ReadKey(); // Connaître la réponse du joueur.

                    //Si la touche est gauche 
                    if (ckyKeyPress.Key == ConsoleKey.LeftArrow && labyrinthClose[playerX - 1, playerY] > 0)
                    {
                        //déplace le joueur à gauche
                        passed[playerX, playerY] = passedOn;
                        playerX--;

                    }
                    //Si la touche est droite 
                    else if (ckyKeyPress.Key == ConsoleKey.RightArrow && labyrinthClose[playerX + 1, playerY] > 0)
                    {
                        //déplace le joueur à droite
                        passed[playerX, playerY] = passedOn;
                        playerX++;

                    }
                    //Si la touche est en haut 
                    else if (ckyKeyPress.Key == ConsoleKey.UpArrow && labyrinthClose[playerX, playerY - 1] > 0)
                    {
                        //déplace le joueur en haut
                        passed[playerX, playerY] = passedOn;
                        playerY--;

                    }
                    //Si la touche est en bas 
                    else if (ckyKeyPress.Key == ConsoleKey.DownArrow && labyrinthClose[playerX, playerY + 1] > 0)
                    {
                        //déplace le joueur en bas 
                        passed[playerX, playerY] = passedOn;
                        playerY++;

                    }
                    //Si le joueur n'est pas encore passé sur la case, la case devient vert
                    if (!passed[playerX, playerY])
                    {
                        passedOn = true;
                        SetCursorPosition(playerX * 2 + leftPos, playerY + topPos);
                        ForegroundColor = ConsoleColor.Green;
                        WriteLine("██");
                        ResetColor();

                    }
                    else
                    //Sinon la case change de couleur
                    {
                        SetCursorPosition(playerX * 2 + leftPos, playerY + topPos);
                        ForegroundColor = ConsoleColor.Magenta;
                        WriteLine("██");
                        ResetColor();
                        passedOn = false;
                        passed[playerX, playerY] = passedOn;
                    }
                }
                //Si le joueur est à la fin du labyrinthe
                if (playerX == labyrinthClose.GetLength(0) - 2 && playerY == labyrinthClose.GetLength(1) - 2)
                {
                    SetCursorPosition(playerX * 2 + leftPos, playerY + topPos);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("██");
                    ResetColor();
                    win = true;
                    //Arrêt du déplacement du minotaure
                    foreach (Minotaur x in _minotaur)
                    {
                        x.Stop();
                    }
                    Clear();
                    SetCursorPosition(WindowWidth / 2 - 9, WindowHeight / 2);
                    WriteLine("Bien joué");
                    SetCursorPosition(WindowWidth / 2-9 , WindowHeight / 2 + 1);
                    WriteLine("Appuyez sur une touche pour continuer");

                }
                for (int a = 0; a < _minotaur.Count; a++)
                {

                    //Si un minotaures touche le joueur
                    if (simple.PosXMin / 2 == playerX && simple.MinY == playerY  || (_minotaur[a].PosXMin / 2 == playerX && _minotaur[a].MinY == playerY))
                    {
                        //Arrêt du déplacement du minotaure
                        foreach (Minotaur x in _minotaur)
                        {
                            x.Stop();
                        }

                        //Vide la liste des minotaures
                        _minotaur.Clear();
                        Clear();
                        //Valeur entre 0 et 9
                        int damaged = rnd.Next(0, 10);

                        SetWindowSize(150, 30);
                        WriteLine(@"  
                          / \                                                               .      .
                          | |                                                               |\____/|
                          |.|                                                              (\|----|/)
                          |.|                                                               \ 0  0 /
                          |:|                                                                |    |
                        ,_|:|_,   /  )                                                    ___/\../\____
                          (Oo    / _I_                                                   /     --       \
                           +\ \  || __|                                                 /  \         /   \
                              \ \||___|                                                |    \___/___/(   |
                                \ /.:.\-\                                              \   /|  }{   | \  )
                                 |.:. /-----\                                           \  ||__}{__|  |  |
                                 |___|::oOo::|                                           \  |;;;;;;;\  \ / \_______
                                 /   |:<_T_>:|                                            \ /;;;;;;;;| [,,[|======'
                                |_____\ ::: /                                               |;;;;;;/ |     /
                                | |  \ \:/                                                  ||;;|\   |
                                | |   | |                                                   ||;;/|   /
                                \ /   | \___                                                \_|:||__|
                                / |   \_____\                                                \ ;||  /
                                `-'                                                          |= || =|
                                                                                             |= /\ =|
                                                                                             /_/  \_\");


                        SetCursorPosition(3, 28);
                        WriteLine("Le combat commence attendez");
                        //Si la valeur aléatoire est plus petite que 2 le joueur gagne le combat
                        if (damaged <= 2)
                        {
                            Thread.Sleep(3000);
                            SetCursorPosition(3, 29);
                            WriteLine("Vous avez gagné");
                            Thread.Sleep(3000);
                            Clear();


                            //Affiche le labyrinthe de nouveau et affiche le déplacement du joueur
                            for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                            {

                                for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                {
                                    //Affiche le labyrinthe de nouveau
                                    if (labyrinthClose[i, j] >= 1)
                                    {
                                        SetCursorPosition(i * 2 + leftPos, j + topPos);
                                        ForegroundColor = ConsoleColor.Black;
                                        WriteLine("██");
                                        ResetColor();

                                    }
                                    else
                                    {
                                        SetCursorPosition(i * 2 + leftPos, j + topPos);
                                        ForegroundColor = ConsoleColor.DarkGray;
                                        WriteLine("██");
                                        ResetColor();
                                    }
                                    //Affiche le déplacement du joueur
                                    if (passed[i, j] == passedOn)
                                    {
                                        SetCursorPosition(i * 2 + leftPos, j + topPos);
                                        ForegroundColor = ConsoleColor.Green;
                                        WriteLine("██");
                                        ResetColor();
                                    }
                                }
                            }
                            SetCursorPosition(leftPos, topPos + 1);
                            ForegroundColor = ConsoleColor.Black;
                            WriteLine("██");
                            SetCursorPosition((labyrinthIni.GetLength(0)) * 2, labyrinthIni.GetLength(1));
                            WriteLine("██");
                            ResetColor();
                           

                        }
                        //Sinon le joueur perd 
                        else
                        {
                            //Arrêt du déplacement du minotaure
                            foreach (Minotaur x in _minotaur)
                            {
                                x.Stop();
                            }
                            //Vide la liste des minotaures
                            _minotaur.Clear();
                            win = true;
                            Thread.Sleep(3000);
                            SetCursorPosition(3, 29);
                            WriteLine("Vous avez perdu contre le minotaure");
                            Thread.Sleep(3000);
                            Clear();
                            SetCursorPosition(WindowWidth / 2-9, WindowHeight / 2);
                            WriteLine("Game Over");
                            SetCursorPosition(WindowWidth / 2 - 23, WindowHeight / 2+1);
                            WriteLine("Appuyez sur une touche pour continuer");
                           
                        }
                    }
                }
                //Tant que le joueur n'a pas gagné ou perdu
            } while (!win);
           

            ReadKey();


        }

        public void Reso()
        {
            ///Résolution (J'appique une valeur 1 à la sortie du labyrinthe le programme va ensuite faire tout les chemin inverse du labyrinthe et va augmenter la valeur du chemin par rapport à la distance de la sortie)
            ///Première case à la sortie 1, une case plus tard la vlauer sera de 2 etc...

            reso[reso.GetLength(0) - 1, reso.GetLength(1) - 2] = 1; //Attribut à la sortie du labyrinthe la valeur 1 (à une case de la sortie) 
            do
            {


                //Parcours tout le tableau en commencent par la fin
                for (int i = labyrinthClose.GetLength(0) - 1; i > 0; i--)
                {

                    for (int j = labyrinthClose.GetLength(1) - 1; j > 0; j--)
                    {
                        //Que des if pour vérifier les 4 possibilitées
                        //Si la valeur du tableau est plus grande que 0 (Si la recherche du tableau vaut un chemin ou pas)
                        if (labyrinthClose[i, j] > 0)
                        {
                            //Si la valeur en dessous de la recherche du tableau n'est pas égale à 0 (Si la valeur du chemin en dessous de la recherche à déjà été effectué ou non)
                            if (reso[i, j - 1] != 0)
                            {
                                //Si la valeur sur laquelle la recherche s'effectue n'a pas encore été assigné
                                if (reso[i, j] == 0)
                                {
                                    //On donne la valeur de la case prcécédente à une variable +1
                                    distance = reso[i, j - 1] + 1;
                                    //On donne à cette case une distance de plus que celle précedent
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
                                    //On donne la valeur de la case prcécédente à une variable +1
                                    distance = reso[i, j + 1] + 1;
                                    //On donne à cette case une distance de plus que celle précedent
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
                                    //On donne la valeur de la case prcécédente à une variable +1
                                    distance = reso[i - 1, j] + 1;
                                    //On donne à cette case une distance de plus que celle précedent
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
                                    //On donne la valeur de la case prcécédente à une variable +1
                                    distance = reso[i + 1, j] + 1;
                                    //On donne à cette case une distance de plus que celle précedent
                                    reso[i, j] = distance;
                                    //Valeur bool est vrai
                                    dist = true;
                                }
                            }
                        }
                       
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

            ////////////Montre le chemin de reso (valeur +1) 
            //for (sbyte i = 0; i < reso.GetLength(0); i++)
            //{
            //    for (sbyte j = 0; j < reso.GetLength(1); j++)
            //    {
            //        //Changer les 0 en une valeur quelqconque pour parer les problème dans la suite 
            //        if (reso[i, j] > 0)
            //        {
            //            SetCursorPosition(i * 2 + leftPos, j + topPos);
            //            WriteLine(reso[i, j]);
            //        }

            //    }
            //}
            //////////////


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
           
            ReadKey();
            ForegroundColor = ConsoleColor.Green;
              SetCursorPosition(WindowWidth / 2 - 9, 0);
            WriteLine("Appuyez sur une touche pour continuer");
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
                Thread.Sleep(30);
                //Dessine un caractère vert pour montrer le chemin su la position donné
                SetCursorPosition(x * 2 + leftPos, y + topPos);
                ForegroundColor = ConsoleColor.Green;
                WriteLine("██");
                ResetColor();


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
                if (x == (labyrinthClose.GetLength(0) - 2) && y == labyrinthClose.GetLength(1) - 2)
                {
                    //remplir la dernière case
                    SetCursorPosition((x * 2) + 2 + leftPos, y + topPos);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("██");
                    ResetColor();

                }


            }
        }





        public void Show()
        {
            SetWindowSize((labyrinthIni.GetLength(0) +15 ) * 2, labyrinthIni.GetLength(1) + 5); // Grandeur de la console
            Clear();
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

                        SetCursorPosition(i * 2 + leftPos, j + topPos);
                        ForegroundColor = ConsoleColor.DarkGray;
                        WriteLine("██");
                        ResetColor();
                    }
                    if (labyrinthIni[i, j] == 2)//Si la valeur donné est 2 on affiche les trous du chemin du labyrinthe coincé par les murs
                    {
                        SetCursorPosition(i * 2 + leftPos, j + topPos);
                        WriteLine("  ");

                        //Si la valeur donné est 2 on donne une valeur à cette position dans le tableau du labyrinthe avec le chemin
                        labyrinthClose[i, j] = rndNumber;
                        //On change le nombre en lui donnant une augmentation
                        rndNumber++;
                    }

                }
            }

            SetCursorPosition(WindowWidth / 2 - 9,0);
            WriteLine("Chargement en cours");


            ///////Choisir une cordonné au hasard dans le tableau et assemblé 2 murs entre eux


            do
            {
                wallOpened = labyrinthClose.Length;//On lui donne la valeur de ta taille du tableau
                int rndI = rnd.Next(0, labyrinthClose.GetLength(0)); //Choix d'une cordonné aléatoire de la première valeur du tableau
                int rndJ = rnd.Next(0, labyrinthClose.GetLength(1));//Choix d'une cordonné aléatoire de la seconde valeur du tableau


                if (labyrinthClose[rndI, rndJ] > 4 && labyrinthIni[rndI, rndJ] == 2) //Si la valeur du tableau labyrinthClose et plus grande que 4 par rapportau rndNumber et si c'est un carctère de chemin dans le tableau labyrinthIni 
                {


                    do
                    {
                        int rndWallAss = rnd.Next(0, 4); //Variable qui stocke une valeur entre 1 et 4

                        //Si la valeur est 0 on enlève le mur Nord 
                        if (rndWallAss == 0)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rndJ > 2)
                            {
                                //Si la case noire en dessous de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rndI, rndJ - 2] != labyrinthClose[rndI, rndJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rndI, rndJ - 1] = labyrinthClose[rndI, rndJ];

                                    int cell = labyrinthClose[rndI, rndJ - 2];
                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {

                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 
                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rndI, rndJ];
                                            }
                                        }
                                    }

                                }
                            }
                        }  //Si la valeur est 1 on enlève le mur Sud 
                        else if (rndWallAss == 1)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rndJ < labyrinthClose.GetLength(1) - 2)
                            {
                                //Si la case noire en dessus de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rndI, rndJ + 2] != labyrinthClose[rndI, rndJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rndI, rndJ + 1] = labyrinthClose[rndI, rndJ];
                                    int cell = labyrinthClose[rndI, rndJ + 2];

                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {
                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 

                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rndI, rndJ];
                                            }
                                        }
                                    }

                                }
                            }
                        } //Si la valeur est 2 on enlève le mur Ouest 
                        else if (rndWallAss == 2)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rndI > 2)
                            {
                                //Si la case noire à gauche de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rndI - 2, rndJ] != labyrinthClose[rndI, rndJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rndI - 1, rndJ] = labyrinthClose[rndI, rndJ];
                                    int cell = labyrinthClose[rndI - 2, rndJ];
                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {

                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 

                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rndI, rndJ];
                                            }
                                        }
                                    }


                                }
                            }
                        }//Si la valeur est 3 on enlève le mur Est 
                        else if (rndWallAss == 3)
                        {
                            //Si la valeur ne touche pas les limites du labyrinthe
                            if (rndI < labyrinthClose.GetLength(0) - 2)
                            {
                                //Si la case noire à droite de celle choisi au hasard n'a pas la même valeur 
                                if (labyrinthClose[rndI + 2, rndJ] != labyrinthClose[rndI, rndJ])
                                {
                                    //On casse le mur entre les deux cases noires en lui donnant la même valeur que celle choisi aléatoirement
                                    labyrinthClose[rndI + 1, rndJ] = labyrinthClose[rndI, rndJ];
                                    int cell = labyrinthClose[rndI + 2, rndJ];
                                    //Parcours tout le tableau
                                    for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                                    {

                                        for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                                        {
                                            //Toutes les cases qui on la même valeurs que la 2ème case changent pour au final n'avoir plus qu'un valeur unie 

                                            if (labyrinthClose[i, j] == cell)
                                            {
                                                labyrinthClose[i, j] = labyrinthClose[rndI, rndJ];
                                            }
                                        }
                                    }

                                }

                            }

                        }

                        //Sinon (si aucun mur n'a été changé) valeur bool = faux
                        else
                        {
                            rndClosed = false;
                        }

                    } while (!rndClosed);//Tant qu'aucun mur n'a été changé recommence la boucle

                }


                //Parcours tout le tableau
                for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
                {

                    for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                    {
                        //Si la valeur de la recherche n'est pas égale à la valeur aléatoire ou égale à la valeur de base (0) 
                        if (labyrinthClose[i, j] != labyrinthClose[rndI, rndJ] && labyrinthClose[i, j] != 0)
                        {
                            //On enlève unve valeur à wallOpened
                            wallOpened--;
                        }
                    }
                }






            } while (wallOpened != labyrinthClose.Length); //Tant que tout le tableau n'est pas égale à 0 ou une seule valeur de mur recommence
            SetCursorPosition(WindowWidth / 2 - 9, 0);
            WriteLine("Appuyez sur une touche pour continuer");
            ReadKey();

           
            //Parcours tout le tableau
            for (sbyte i = 0; i < labyrinthClose.GetLength(0); i++)
            {

                for (sbyte j = 0; j < labyrinthClose.GetLength(1); j++)
                {
                    //Affiche le chemin créer aléatoirement dans le labyrinthe si la valeur est de 1
                    if (labyrinthClose[i, j] >= 1)
                    {
                        SetCursorPosition(i * 2 + leftPos, j + topPos);
                        ForegroundColor = ConsoleColor.Black;
                        WriteLine("██");
                        ResetColor();

                    }
                }
            }
            ForegroundColor = ConsoleColor.Cyan;
              SetCursorPosition(WindowWidth / 2 - 9, 0);
            WriteLine("Appuyez sur une touche pour continuer");


        }

    }
}
