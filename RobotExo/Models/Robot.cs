using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotExo.Models
{
    public delegate void OrdreDelegate();

    public enum Directions { North, East, South, West }
    public enum OrdreRobot { Avancer, Gauche, Droite }

    public class Robot
    {
        #region propriétés
        private OrdreDelegate? Ordres = null;

        private int _PosX;
        private int _PosY;
        private Directions _Direction;
        public Action<Robot, RobotEventArgs> RobotEvent { get; set; }

        public Grid Grid { get; private set; }
        #endregion

        #region constructeur
        public Robot(Grid g, Action<Robot, RobotEventArgs> method)
        {
            Grid = g;

            g.Robot = this;

            RobotEvent = method;
        }
        #endregion

        #region propriété décomposé public

        public int PosX
        {
            get { return _PosX; }
            set
            {

                if (value < 0 || value > Grid.Width)
                {
                    RobotEvent.Invoke(this, new RobotEventArgs($"*Boink*, le robot touche un mur ({DirectionStr} : {PosX},{PosY})", MessageType.Erreur));
                    //throw new ArgumentOutOfRangeException(nameof(PosX), value, $"La position dépasse la valeur authorisée (0, {Grid.Width})");
                }
                else
                {
                    _PosX = value;
                }
            }
        }

        public int PosY
        {
            get { return _PosY; }
            set
            {

                if (value < 0 || value > Grid.Height)
                {
                    RobotEvent.Invoke(this,new RobotEventArgs($"*Boink*, le robot touche un mur ({DirectionStr} : {PosX},{PosY})", MessageType.Erreur));
                    //throw new ArgumentOutOfRangeException(nameof(PosY), value, $"La position dépasse la valeur authorisée (0, {Grid.Height})");
                }
                else
                {
                    _PosY = value;
                }
            }
        }

        public Directions Direction
        {
            get { return _Direction; }
            set
            {

                if ((int)value > 3)
                {
                    _Direction = Directions.North;
                }
                else if ((int)value < 0)
                {
                    _Direction = Directions.West;
                }
                else
                {
                    _Direction = value;
                }
            }
        }

        public string DirectionStr
        {
            get
            {
                switch (_Direction)
                {
                    case Directions.North:
                        return "↑";

                    case Directions.East:
                        return "→";

                    case Directions.South:
                        return "↓";

                    case Directions.West:
                        return "←";

                    default:
                        return "";
                }
            }
        }

        #endregion

        public void Reset()
        {
            PosX = 0;
            PosY = 0;

        }

        public void Avancer()
        {
            RobotEvent.Invoke(this, new RobotEventArgs($"Le se dirige vers {DirectionStr}: {PosX},{PosY}", MessageType.Info));
            switch (Direction)
            {
                case Directions.North:
                    PosY --;
                    break;
                case Directions.East:
                    PosX ++;
                    break;
                case Directions.South:
                    PosY ++;
                    break;
                case Directions.West:
                    PosX --;
                    break;
            }
        }

        public void TournerGauche()
        {
            Direction -= 1;
            RobotEvent.Invoke(this, new RobotEventArgs($"Le robot tourne à gauche: {DirectionStr}", MessageType.Info));
        }

        public void TournerDroite()
        {
            Direction += 1;
            RobotEvent.Invoke(this, new RobotEventArgs($"Le robot tourne à droite: {DirectionStr}", MessageType.Info));
        }

        public void EnregistrerOrdre(OrdreRobot ordreRobot)
        {
            switch(ordreRobot)
            {
                case OrdreRobot.Avancer:
                    Ordres += Avancer;
                    break;
                case OrdreRobot.Droite:
                    Ordres += TournerDroite;
                    break;
                case OrdreRobot.Gauche:
                    Ordres += TournerGauche;
                    break;
            }

            RobotEvent.Invoke(this, new RobotEventArgs($"Nouvelle ordre enregistré: {ordreRobot}", MessageType.Info));
        }

        public void Executer()
        {
            if (Ordres != null)
            {
                RobotEvent.Invoke(this, new RobotEventArgs($"Exécution des ordres du robot", MessageType.Info));
                Ordres();
            }
            else
            {
                throw new InvalidOperationException("Le robot n'a aucune ordre enregistré");
            }

            if (Grid.CheckVictory(this))
                RobotEvent.Invoke(this, new RobotEventArgs($"🎉 Le robot à atteint le point final. 🥳", MessageType.Victoire));
        }
    }
}
