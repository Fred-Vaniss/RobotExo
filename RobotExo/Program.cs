using RobotExo.Models;
using System.Security.Cryptography;

namespace RobotExo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Grid grille = new Grid(10, 10);
            Robot robot = new Robot(grille, RobotUI.AfficherMessage);

            grille.InitGame();

            //Console.WriteLine($"Initialisé la grille, le point final se strouve sur {grille.FinalX},{grille.FinalY}");

            //robot.EnregistrerOrdre(OrdreRobot.Droite);
            //robot.EnregistrerOrdre(OrdreRobot.Droite);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Gauche);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Droite);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Avancer);
            //robot.EnregistrerOrdre(OrdreRobot.Check);

            ////robot.EnregistrerOrdre(OrdreRobot.Avancer);

            //Console.WriteLine("===============================================");

            //robot.Executer();

            RobotUI.MenuRobot(robot);
        }

        static void OnRobotEvent(Robot r, RobotEventArgs args)
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
    }
}