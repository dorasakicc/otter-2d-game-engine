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
using System.Diagnostics;

namespace OTTER
{
   
    public partial class BGL : Form
    {
        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Game execution condition. If <c>START == true</c> the game will execute.
        /// </summary>
        /// <example><c>START</c> is often used for infinite loops. Example method/script:
        /// <code>
        /// private int MyMethod()
        /// {
        ///     while(START)
        ///     {
        ///       //code goes here
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Number of sprites.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// List of all sprites.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        private int SlucajanBroj(int min, int max)
        {
            Random r = new Random();
            int br = r.Next(min, max + 1);
            return br;
        }

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */
        Farmer farmer;
        Animal zivotinja;
       
        Car auto1;
        
        Car auto3;
        Car auto4;

        private delegate void AnimalEvent(int x,int y );
        private event AnimalEvent NewAnimal;


        private void SetupGame()
        {
            //1. setup stage
            SetStageTitle("PMF");
            //setBackgroundColor(Color.WhiteSmoke);            
            setBackgroundPicture("backgrounds\\cestaa" +
                ".jpg");
            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");

            //2. add sprites
            farmer = new Farmer("sprites\\farmerr.png",300 , 350);

            //Game.AddSprite(farmer);
            farmer.SetSize(40);
            farmer.RotationStyle = "all around";
            int i = SlucajanBroj(70, GameOptions.RightEdge - farmer.Width);
            int ii = SlucajanBroj(70, GameOptions.DownEdge - farmer.Heigth);

            zivotinja = new Animal("sprites\\chicken.png", i, ii);
             //Game.AddSprite(zivotinja);
                zivotinja.SetSize(40);
            zivotinja.AddCostumes("sprites\\cow.png", "sprites\\cat.png", "sprites\\pig.png");
             
            auto1 = new Car("sprites\\auto11.png", 0, 160);
            Game.AddSprite(auto1);
            //auto1.RotationStyle = "all around";
            auto1.SetSize(45);
            auto1.AddCostumes("sprites\\auto1.png");
           
            auto3 = new Car("sprites\\auto22.png", 600, 90);//fixed!
            Game.AddSprite(auto3);
            auto3.SetSize(50);
            auto3.AddCostumes("sprites\\auto2.png");
            //auto3.RotationStyle = "all around";
            auto4 = new Car("sprites\\auto44.png", 540,190);//fixed!
            Game.AddSprite(auto4);
            //auto4.RotationStyle = "all around";
            auto4.SetSize(60);
            auto4.AddCostumes("sprites\\auto4.png");
            Game.AddSprite(zivotinja);
            Game.AddSprite(farmer);
            
            //3. scripts that start


            NewAnimal += CreateAnimal;
            farmer.GameOver += FarmerEnd;
            

            Game.StartScript(FarmerKretanje);
            Game.StartScript(AutoKretanje1);
            Game.StartScript(AutoKretanje3);
            Game.StartScript(AutoKretanje4);
           
            
        }

        /* Scripts */
       
        private void FarmerEnd()
        {
            ISPIS = "GAME OVER: " + farmer.Points + " points";
            Wait(0.1);
            START = false;
        }
       
        private void CreateAnimal(int iks,int ips)
        {
            
           
            if (zivotinja.IsActive)
                return;//nece je zvat ako je vec aktivna
            Random r = new Random();
            int i = SlucajanBroj(70, GameOptions.RightEdge - zivotinja.Width);
            int ii = SlucajanBroj(70, GameOptions.DownEdge - zivotinja.Heigth);


            zivotinja.GotoXY(i, ii);
                zivotinja.SetVisible(true);
                zivotinja.IsActive = true;
            
            
        }
       
        private int FarmerKretanje()
        {
            farmer.X = (GameOptions.RightEdge - farmer.Width) / 2;
            farmer.Y = GameOptions.DownEdge - farmer.Heigth;

            while (START)
            {
                if (sensing.KeyPressed(Keys.Right))
                    farmer.X += 40; 
                if (sensing.KeyPressed(Keys.Left))
                    farmer.X -= 40;
                if (sensing.KeyPressed(Keys.Up))
                    farmer.Y -= 40;
                if (sensing.KeyPressed(Keys.Down))
                    farmer.Y += 40;

                if (farmer.TouchingSprite(zivotinja))
                {
                    Wait(0.01); 
                    ISPIS = "Lives: " + farmer.Lives + "/ Points: " + farmer.Points;
                    
                    zivotinja.SetVisible(false);
                    

                    NewAnimal.Invoke(zivotinja.X,zivotinja.Y);
                    zivotinja.NextCostume();
                   
                }
                if (farmer.TouchingSprite(auto1  ))
                {
                    Wait(0.1);
                    farmer.Lives -= 1;
                    ISPIS = "Lives: " + farmer.Lives + "/ Points: " + farmer.Points;
                   
                    farmer.X = (GameOptions.RightEdge - farmer.Width) / 2;
                    farmer.Y = GameOptions.DownEdge - farmer.Heigth;
                }
                Wait(0.02);
                if (farmer.TouchingSprite(auto3))
                {
                    Wait(0.1);
                    farmer.Lives -= 1;
                    ISPIS = "Lives: " + farmer.Lives + "/ Points: " + farmer.Points;

                    farmer.X = (GameOptions.RightEdge - farmer.Width) / 2;
                    farmer.Y = GameOptions.DownEdge - farmer.Heigth;
                }
                Wait(0.02);
                if (farmer.TouchingSprite(auto4))
                {
                    Wait(0.1);
                    farmer.Lives -= 1;
                    ISPIS = "Lives: " + farmer.Lives + "/ Points: " + farmer.Points;

                    farmer.X = (GameOptions.RightEdge - farmer.Width) / 2;
                    farmer.Y = GameOptions.DownEdge - farmer.Heigth;
                }
                Wait(0.02);
                

            }
            return 0;
        }
        private int AutoKretanje1()
        {
            auto1.SetHeading(Sprite.DirectionsType.right);
            while (START)
            {
                auto1.MoveSteps(5);
                if (auto1.Edge == "right")
                {
                    auto1.NextCostume();
                    auto1.SetHeading(Sprite.DirectionsType.left);
                }
                else if (auto1.Edge == "left")
                {
                    auto1.NextCostume();

                    auto1.SetHeading(Sprite.DirectionsType.right);
                }
                Wait(0.02);
            }
            return 0;
            

        }
        private int AutoKretanje3()
        {
            auto3.SetHeading(Sprite.DirectionsType.left);
            
            while (START)
            {
                auto3.MoveSteps(3);
                
                if (auto3.Edge == "right")
                {
                    auto3.SetHeading(Sprite.DirectionsType.left);
                    auto3.NextCostume();
                }
                else if (auto3.Edge == "left")
                {
                    auto3.SetHeading(Sprite.DirectionsType.right);
                    auto3.NextCostume();
                }
                Wait(0.02);
            }
            return 0;
        }
        private int AutoKretanje4()
        {
            auto4.SetHeading(Sprite.DirectionsType.left);

            while (START)
            {
                auto4.MoveSteps(8);

                if (auto4.Edge == "right")
                {
                    auto4.SetHeading(Sprite.DirectionsType.left);
                    auto4.NextCostume();
                }
                else if (auto4.Edge == "left")
                {
                    auto4.SetHeading(Sprite.DirectionsType.right);
                    auto4.NextCostume();
                }
                Wait(0.02);
            }
            return 0;
        }
      
        /* ------------ GAME CODE END ------------ */
    }

 }

