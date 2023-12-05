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

        private static void RefreshGrille(Robot r)
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
                        else if ((i == r.PosY && j == r.PosX) && (i == g.FinalY && j == g.FinalX))
                        {
                            line += "✓";
                        }
                        else if (i == g.FinalY && j ==  g.FinalX)
                        {
                            line += "X";
                        }
                        else if (i == r.PosY && j == r.PosX)
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

        private static void ControleRobot(Robot r)
        {
            ConsoleKey key;

            RefreshGrille(r);

            while (true)
            {
                Console.WriteLine("Choisissez une direction où aller. Arrière pour stopper.");
                key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        r.Avancer();
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        r.TournerGauche();
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        r.TournerDroite();
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        r.CheckVictory();
                        return;
                    default:
                        Console.Clear();
                        AfficherMessage(r, new RobotEventArgs("Commande invalid", MessageType.Erreur));
                        break;
                }
                RefreshGrille(r);
            }
        }

        private static void PlanifierRobot(Robot r)
        {
            ConsoleKey key;

            r.ViderOrdres();

            RefreshGrille(r);

            while (true)
            {
                Console.WriteLine("Choisissez une direction où planifier le mouvement. Arrière pour exécuter la planification");

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        r.EnregistrerOrdre(OrdreRobot.Avancer);
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        r.EnregistrerOrdre(OrdreRobot.Gauche);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        r.EnregistrerOrdre(OrdreRobot.Droite);
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        r.EnregistrerOrdre(OrdreRobot.Check);
                        r.Executer();
                        return;
                    default:
                        Console.Clear();
                        AfficherMessage(r, new RobotEventArgs("Commande invalid", MessageType.Erreur));
                        break;
                }
                RefreshGrille(r);
            }
        }


        public static void MenuRobot(Robot r)
        {
            int choice = 0;

            while(true)
            {
                RefreshGrille(r);

                Console.WriteLine("Choisissez une action:");
                Console.WriteLine("1: Déplacer le robot (facile)");
                Console.WriteLine("2: Planifier le robot (difficile)");
                Console.WriteLine("3: Ré-initialiser la partie");
                Console.WriteLine("0: Quitter le programme");
                string? input = Console.ReadLine();

                if(!int.TryParse(input, out choice))
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Choix invalide, veuillez ré-éssayer");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            ControleRobot(r);
                            break;
                        case 2:
                            PlanifierRobot(r);
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
        }
    }
}
