using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    /// <summary>
    /// Represents a collectible animal in the game
    /// </summary>
    public class Animal : Sprite
    {
        /// <summary>
        /// Indicates if the animal is currently active/visible in the game
        /// </summary>
        public bool IsActive { get; set; }

        private int pointValue;
        
        /// <summary>
        /// Point value awarded when this animal is collected
        /// </summary>
        public int PointValue
        {
            get { return pointValue; }
            set { pointValue = value; }
        }

        /// <summary>
        /// Creates a new animal sprite
        /// </summary>
        /// <param name="imagePath">Path to the sprite image</param>
        /// <param name="x">Initial X coordinate</param>
        /// <param name="y">Initial Y coordinate</param>
        public Animal(string imagePath, int x, int y)
            : base(imagePath, x, y)
        {
            IsActive = false;
            this.PointValue = 100;
        }
    }
}
