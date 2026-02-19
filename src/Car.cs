using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    /// <summary>
    /// Represents a car obstacle in the game
    /// </summary>
    public class Car : Sprite
    {
        private string edge;
        
        /// <summary>
        /// Indicates which edge the car has reached ("right", "left", or empty)
        /// </summary>
        public string Edge
        {
            get { return edge; }
            set { edge = value; }
        }
        
        public delegate void EventHandler();
        
        /// <summary>
        /// Event fired when the game should end
        /// </summary>
        public event EventHandler GameOver;
       
        /// <summary>
        /// Event fired when the player loses a life
        /// </summary>
        public event EventHandler LoseLife;

        /// <summary>
        /// X coordinate with boundary checking
        /// </summary>
        public override int X
        {
            get { return base.X; }
            set
            {
                if (value + this.Width > GameOptions.RightEdge)
                {
                    base.x = GameOptions.RightEdge - this.Width;
                    this.Edge = "right";
                }
                else if (value < 0)
                {
                    base.x = 0;
                    this.Edge = "left";
                }
                else
                {
                    base.x = value;
                    this.Edge = "";
                }
            }
        }
        
        /// <summary>
        /// Indicates if the car is currently active in the game
        /// </summary>
        public bool IsActive { get; set; }

        private int speed;
        
        /// <summary>
        /// Movement speed of the car
        /// </summary>
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        
        /// <summary>
        /// Moves the car by its speed value
        /// </summary>
        public void MoveSteps()
        {
            this.X += this.Speed;
        }

        /// <summary>
        /// Creates a new car obstacle
        /// </summary>
        /// <param name="imagePath">Path to the car sprite image</param>
        /// <param name="x">Initial X coordinate</param>
        /// <param name="y">Initial Y coordinate</param>
        public Car(string imagePath, int x, int y)
            : base(imagePath, x, y)
        {
            Edge = "";
            IsActive = false;
            Speed = 10;
        }
    }
}
