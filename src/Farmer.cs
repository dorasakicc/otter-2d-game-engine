using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    /// <summary>
    /// Represents the player character (farmer)
    /// </summary>
    public class Farmer : Sprite
    {
        public delegate void EventHandler();
        
        /// <summary>
        /// Event fired when the game ends (win or lose)
        /// </summary>
        public event EventHandler GameOver;
        
        private int points;
        
        /// <summary>
        /// Current score. Game is won when points reach 800.
        /// </summary>
        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                if (points >= 800)
                    GameOver?.Invoke();
            }
        }
        
        private int lives;
        
        /// <summary>
        /// Remaining lives. Game is lost when lives reach 0.
        /// </summary>
        public int Lives
        {
            get { return lives; }
            set
            {
                lives = value;
                if (lives <= 0)
                    GameOver?.Invoke();
            }
        }
        
        /// <summary>
        /// Creates a new Farmer player character
        /// </summary>
        /// <param name="imagePath">Path to the farmer sprite image</param>
        /// <param name="x">Initial X coordinate</param>
        /// <param name="y">Initial Y coordinate</param>
        public Farmer(string imagePath, int x, int y)
            : base(imagePath, x, y)
        {
            Points = 0;
            Lives = 3;
        }
        
        /// <summary>
        /// Checks collision with an animal and awards points
        /// </summary>
        /// <param name="animal">The animal to check collision with</param>
        /// <returns>True if collision detected</returns>
        public bool TouchingSprite(Animal animal)
        {
            Sprite s = animal;
            if (this.TouchingSprite(s))
            {
                animal.IsActive = false;
                this.Points += animal.PointValue;
                animal.SetVisible(false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks collision with a car (causes life loss)
        /// </summary>
        /// <param name="car">The car to check collision with</param>
        /// <returns>True if collision detected</returns>
        public bool TouchingSprite(Car car)
        {
            Sprite s = car;
            if (this.TouchingSprite(s))
            {
                car.IsActive = false;
                return true;
            }
            return false;
        }
    }
}
