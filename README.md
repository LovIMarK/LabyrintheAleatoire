# Labyrinthe Complex

## Description

**Labyrinthe Complex** est un jeu en **C#** (console) qui génère un labyrinthe et propose au joueur de s’y aventurer. Le programme crée dynamiquement un parcours, place des **minotaures** aléatoirement et offre un **menu** permettant de :
1. Jouer et explorer le labyrinthe
2. Afficher la résolution du labyrinthe (chemin optimal)
3. Quitter le jeu

Le projet met en œuvre des mécanismes de **génération** et de **résolution** de labyrinthes, la **gestion de monstres** (Minotaures) qui se déplacent dans la console, ainsi qu’une **intéraction clavier** pour diriger le personnage.

---

## Fonctionnalités principales

1. **Génération du labyrinthe**
   - Construction aléatoire d’un labyrinthe en console (murs, chemins).
   - Paramètres personnalisables (largeur, hauteur du labyrinthe).

2. **Déplacement du joueur**
   - Contrôles via les flèches du clavier (Haut/Bas/Gauche/Droite).
   - Suivi de la trace déjà parcourue (couleur différente dans la console).

3. **Minotaures intelligents** (threads Timer)
   - Les minotaures se déplacent de façon autonome.
   - Le joueur peut se faire surprendre et perdre s’il est touché par un minotaure.

4. **Résolution automatique** du labyrinthe
   - Algorithme de pathfinding (marquage des distances) pour trouver le chemin optimal.
   - Visualisation pas à pas de la progression dans le labyrinthe.

5. **Menu** intuitif
   - Choix de la taille du labyrinthe (dimensions impaires).
   - Options : Jouer, Résolution, Quitter.

---

## Structure du projet

```bash
Labyrinthe_Complex/
├── Program.cs           # Point d'entrée du programme (Main)
├── Menu.cs              # Menu de configuration (taille, choix de mode)
├── Labyrinthe.cs        # Génération, affichage et gestion de la logique du labyrinthe
├── Minotaure.cs         # Classe de gestion d'un minotaure (déplacement, collisions)
├── Player.cs            # Classe représentant le joueur (positions, etc.)
└── (autres fichiers)    # Code et ressources supplémentaires
