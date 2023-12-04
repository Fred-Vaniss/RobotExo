using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotExo.Models
{
    public static class RobotUI
    {
        public static void AfficherMessage(Robot r, RobotEventArgs args)
        {
            switch (args.MessageType)
            {
                case MessageType.Info:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;

                case MessageType.Erreur:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case MessageType.Victoire:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

            }

            Console.WriteLine($"{args.MessageType}: {args.Message}");

            Console.ResetColor();
        }

        public static void RefreshGrille(Robot r)
        {
            Grid g = r.Grid;

            for(int i = -1; i <= g.Height + 1; i++)
            {
                string line = "";

                for (int j = -1; j <= g.Width + 1; j++)
                {
                    if(i == -1)
                    {
                        if(j == -1)
                        {
                            line += "╔";
                        }
                        else if (j == g.Width + 1)
                        {
                            line += "╗";
                        }
                        else
                        {
                            line += "═";
                        }

                        if ((j != -1 && j < g.Width))
                        {
                            line += "╤";
                        }
                    }
                    else if (i == g.Height + 1)
                    {
                        if (j == -1)
                        {
                            line += "╚";
                        }
                        else if (j == g.Width + 1)
                        {
                            line += "╝";
                        }
                        else
                        {
                            line += "═";
                        }

                        if ((j != -1 && j < g.Width))
                        {
                            line += "╧";
                        }
                    }
                    else
                    {
                        if (j == -1 || j == g.Width + 1)
                        {
                            line += "║";
                        }
                        else if (i == g.FinalX && j ==  g.FinalY)
                        {
                            line += "X";
                        }
                        else if (i == r.PosX && j == r.PosY)
                        {
                            line += (r.DirectionStr);
                        }
                        else
                        {
                            line += " ";
                        }

                        if ((j != -1 && j < g.Width))
                        {
                            line += "│";
                        }
                    }

                    
                    
                }

                Console.WriteLine(line);

                if (i != -1 && i < g.Height)
                {
                    line = "╟";
                    for (int j = 0; j <= g.Width; j++)
                    {
                        line += "─";
                        if (j < g.Width)
                        {
                            line += "┼";
                        }
                    }
                    line += "╢";
                    Console.WriteLine(line);
                }

            }
        }

        public static void ControleRobot(Robot r)
        {
            ConsoleKey key;

            while(true)
            {
                Console.WriteLine("Choisissez une direction où aller. Arrière pour stopper.");
                key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.UpArrow:
                        r.Avancer();
                        break;
                    case ConsoleKey.LeftArrow:
                        r.TournerGauche();
                        break;
                    case ConsoleKey.RightArrow:
                        r.TournerDroite();
                        break;
                    case ConsoleKey.DownArrow:
                        return;
                }

            }
        }

        public static void MenuRobot(Robot r)
        {
            int choice = 0;
            string input = string.Empty;

            do
            {
                Console.WriteLine("Choisissez une action:");
                Console.WriteLine("1: Déplacer le robot");
                Console.WriteLine("2: Rafraichir la grille");
                Console.WriteLine("3: Ré-initialiser la partie");
                Console.WriteLine("0: Quitter le programme");
                input = Console.ReadLine();

                if(!int.TryParse(input, out choice))
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Choix invalide, veuillez ré-éssayer");
                    Console.ResetColor();
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            ControleRobot(r);
                            break;
                        case 2:
                            RefreshGrille(r);
                            break;
                        case 3:
                            r.Grid.InitGame();
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Choix invalide, veuillez ré-éssayer");
                            Console.ResetColor();
                            break;
                    }
                }
            }
            while (true);

        }
    }
}
