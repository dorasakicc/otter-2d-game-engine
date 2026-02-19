using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OTTER
{
    public class SpriteList<T> : List<T>
    {        
        public new void Add(T item)
        {
            base.Add(item);
            Change = true;
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            Change = true;
        }

        public SpriteList()
        {
            Change = false;
        }

        private bool change;

        public bool Change
        {
            get { return change; }
            set { change = value; }
        }
    }

    /// <summary>
    /// Class for general game-related activities
    /// </summary>
    public class Game
    {

        /// <summary>
        /// Pauses execution of the current method
        /// </summary>
        /// <param name="ms">Milliseconds to wait</param>
        public static void WaitMS(int ms)
        {
            Thread.Sleep(ms);
        }

        /// <summary>
        /// Adds a new sprite to the game
        /// </summary>
        /// <param name="s">The sprite to add</param>
        public static void AddSprite(Sprite s)
        {            
            s.SpriteIndex = BGL.spriteCount;
            BGL.spriteCount++;
            BGL.allSprites.Add(s);
        }

        /// <summary>
        /// Starts a script and waits for it to complete before continuing
        /// </summary>
        /// <remarks>Behaves the same as <see cref="Game.StartScript"/> except it waits for completion before proceeding to the next command.</remarks>
        /// <param name="scriptName">The script function to execute</param>
        public static void StartScriptAndWait(Func<int> scriptName)
        {
            Task t = Task.Factory.StartNew(scriptName);
            t.Wait();
        }

        /// <summary>
        /// <summary>
        /// Starts a script in parallel execution
        /// </summary>
        /// <param name="scriptName">The script function to execute</param>
        /// <example>
        /// <para>The method (script or procedure) started by <c>Game.StartScript</c> must be written in a specific way or it won't be accepted. 
        /// The method must have a return value of type <c>int</c> which can be used to return information about whether an error occurred (e.g., 0 means no error). 
        /// All methods/scripts called via Game.StartScript() execute simultaneously.
        /// </para>
        /// Example:
        /// <code>
        /// private int SpriteMethod()
        /// {
        ///     //code
        ///     return 0;
        /// }
        /// </code>
        /// <para>Usually for sprite activities that take a longer time (or during gameplay) a while loop is used. 
        /// The loop condition uses the START variable (see: <see cref="BGL.START"/>).
        /// <code>
        /// while (START)
        /// {
        ///     //do something
        /// }
        /// </code>
        /// </para>
        /// </example>
        public static void StartScript(Func<int> scriptName)
        {
            Task t;
            //t = Task.Factory.StartNew(scriptName, TaskCreationOptions.LongRunning);
            t = Task.Factory.StartNew(scriptName);
        }                        
       
    } //game
}
