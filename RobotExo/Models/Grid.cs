using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotExo.Models
{
    public class Grid
    {
        #region propriétés
        public int FinalX { get; private set; }
        public int FinalY { get; private set; }

        public Robot? Robot {  get; set; }

        #endregion

        #region propriétés décomposé
        private int _width;
        private int _height;
        public int Width
        {
            get { return _width; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Width), value, "Largeur inférieur à zéro.");

                _width = value;
            }
        }

        public int Height
        {
            get { return _height;}
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Height), value, "Hauteur inférieur à zéro.");

                _height = value;
            }
        }

        #endregion

        #region constructeurs
        public Grid(int w, int h) 
        {
            Width = w;
            Height = h;
        }
        #endregion

        #region méthodes
        public void InitGame()
        {
            if (Robot == null)
                throw new NullReferenceException("Aucun robot n'est présent sur la grille pour pouvoir initialiser le jeu.");

            var random = new Random();

            FinalX = random.Next(0, Width); 
            FinalY = random.Next(0, Height);

            Robot.Reset();
        }

        public void InitGame(int fX, int fY)
        {
            if (Robot == null)
                throw new NullReferenceException("Aucun robot n'est présent sur la grille pour pouvoir initialiser le jeu.");

            FinalX = fX;
            FinalY = fY;

            Robot.Reset();
        }

        public bool CheckVictory(Robot r)
        {
            if (r.PosX == FinalX && r.PosY == FinalY)
                return true;
            else
                return false;
        }
        #endregion
    }
}
