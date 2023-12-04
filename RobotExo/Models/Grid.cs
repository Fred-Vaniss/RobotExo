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
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int FinalX { get; private set; }
        public int FinalY { get; private set; }

        public Robot? Robot {  get; set; }

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
        #endregion
    }
}
