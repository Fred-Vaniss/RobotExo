using RobotExo.Models;

namespace exo_bonus_samuel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            Grid grille = new Grid(15, 15);
            Robot robot = new Robot(grille, RobotMessage);


            grille.InitGame(3, 9);

            Console.WriteLine($"Initialisé la grille, le point final se strouve sur {grille.FinalX},{grille.FinalY}");

            robot.EnregistrerOrdre(OrdreRobot.Droite);
            robot.EnregistrerOrdre(OrdreRobot.Droite);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Gauche);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Droite);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);
            robot.EnregistrerOrdre(OrdreRobot.Avancer);

            //robot.EnregistrerOrdre(OrdreRobot.Avancer);

            Console.WriteLine("===============================================");

            robot.Executer();
        }

        static void RobotMessage(Robot r, RobotEventArgs args)
        {
            Console.WriteLine($"{args.MessageType}: {args.Message}");
        }
    }
}