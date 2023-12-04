using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotExo.Models
{
    public delegate void OrdreDelegate();

    public enum DirectionEnum { North, East, South, West }
    public enum OrdreRobot { Avancer, Gauche, Droite }

    public class Robot
    {
        #region propriétés
        private OrdreDelegate? Ordres = null;

        private int _PosX;
        private int _PosY;
        private DirectionEnum _Direction;

        public Grid Grid { get; private set; }
        #endregion

        #region constructeur
        public Robot(Grid g)
        {
            Grid = g;

            g.Robot = this;
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
                    throw new InvalidOperationException("Le robot à atteint la limite de la grille");
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
                    throw new InvalidOperationException("Le robot à atteint la limite de la grille");
                }
                else 
                {
                    _PosY = value;
                }
            }
        }

        public DirectionEnum Direction
        {
            get { return _Direction; }
            set
            { 

                if ( (int)value > 3 )
                {
                    _Direction = DirectionEnum.North;
                }
                else if ( (int)value < 0 )
                {
                    _Direction = DirectionEnum.West;
                }
                else
                {
                    _Direction = value;
                }
            }
        }

        #endregion

        public void Reset()
        {
            PosX = 0;
            PosY = 0;

            Status();
        }

        public void Avancer()
        {

            switch (Direction)
            {
                case DirectionEnum.North:
                    PosY -= 1;
                    break;
                case DirectionEnum.East:
                    PosX += 1;
                    break;
                case DirectionEnum.South:
                    PosY += 1;
                    break;
                case DirectionEnum.West:
                    PosX -= 1;
                    break;
            }

            Status();
        }

        public void TournerGauche()
        {
            Direction -= 1;
            Status();
        }

        public void TournerDroite()
        {
            Direction += 1;
            Status();
        }

        private void Status()
        {
            string direction = string.Empty;

            switch (Direction)
            {
                case DirectionEnum.North:
                    direction = "↑";
                    break;
                case DirectionEnum.East:
                    direction = "→";
                    break;
                case DirectionEnum.South:
                    direction = "↓";
                    break;
                case DirectionEnum.West:
                    direction = "←";
                    break;
            }

            Console.WriteLine($"Le robot se situe à {PosX},{PosY}; direction: {direction}");
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
        }

        public void Executer()
        {
            if (Ordres != null)
                Ordres();
            else
            {
                throw new InvalidOperationException("Le robot n'a aucune ordre enregistré");
            }
        }
    }
}
