using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using EntityControl;

namespace CollisionTesting
{
    /// <summary>
    /// Interaction logic for TestingGameWindow.xaml
    /// </summary>
    public partial class TestingGameWindow : Window
    {
        #region variable decs
        string LevelID = "tgw"; //I'll edit this using the button the player clicks, but it takes on tgw by default
        int lastTerrain;
        int enemyCol = 0;
        public System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        Rocket player = new Rocket(290, 15, 25F, 100F, 9);
        Entity[] terrain = new Entity[] { }; //defining enough terrain for every level at once. unneeded stuff will simply go to a place far beyond the scope of the program
        Launcher[] enemies = new Launcher[] { };
        Rocket_NPC[] rockets = new Rocket_NPC[] { };
        int terrainCollidesTo = 5; //this is changed depending on the level
        public bool GameWin = false;
        public long TimeTaken = 0;
        public Key UserCommand = Key.DbeEnterWordRegisterMode;
        #endregion
        public TestingGameWindow(string lID)
        {
            LevelID = lID;
            InitializeComponent();
        }

        private void KeyRelease(object sender, KeyEventArgs e)
        {
            UserCommand = new MasterLevelControls(UserCommand).KeyRelease(sender, e);
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            UserCommand = new MasterLevelControls(UserCommand).KeyPress(sender, e);
        }

        public void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            MasterLevelControls mlc = new MasterLevelControls(player, terrain, enemies, rockets, terrainCollidesTo, UserCommand);
            TimeTaken += 1;
            if (!mlc.StandardTimerAlgorithm())
            {
                string addToLabel;
                dispatcherTimer.Stop();
                if (mlc.GameWin) { GameWin = true; addToLabel = "You win! "; }  //pushes info of win to a variable that can be sent back
                else { addToLabel = "Game over! "; }
                gameOverLabel.Content = addToLabel + gameOverLabel.Content;
                gameOverLabel.Visibility = Visibility.Visible;
                for (int x = 0; x < terrainCollidesTo; x++)
                {
                    terrain[x].character.Visibility = Visibility.Collapsed;
                } //makes terrain invisible so you can see instructions/time taken
            }
            TimeLabel.Content = (TimeTaken * 0.032).ToString().TrimEnd("0".ToCharArray()[0]); //convert to seconds, and remove trailing zeroes
            //string[] a = mlc.AppendToLabel();
            //TimeLabel.Content = "";
            //for (int x = 0; x < a.Length; x++)
            //{ TimeLabel.Content += a[x]; } //this code allows for output of launcher debug info
        }

        private void OnGameLoad(object sender, EventArgs e)
        {
            new MasterLevelControls(LevelID).InitialiseLevel(ref terrainCollidesTo, ref terrain, ref enemies, ref rockets, ref player, ref lastTerrain, ref enemyCol);
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick); //start the timer's event handler 
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 32); //make the timer
            dispatcherTimer.Start();  //start it
            GameGrid.Children.Add(player.character); //adds player
            for (int x = 0; x < terrainCollidesTo; x++)
            {
                GameGrid.Children.Add(terrain[x].character); //adds terrain (character is just its entity core)
            }
            for (int x = 0; x < enemyCol; x++)
            {
                GameGrid.Children.Add(enemies[x].character); //same for launchers
                GameGrid.Children.Add(rockets[x].character); //and for each one, its child rocket
            }
            terrain[lastTerrain-1].character.Fill = Brushes.LimeGreen;
            terrain[lastTerrain - 1].IsLandingPad = true;
        }//should be ok here - no level-specific data not from files
    }
}