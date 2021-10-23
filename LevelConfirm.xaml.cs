using System;
using System.Windows;
using System.IO;

namespace CollisionTesting
{
    /// <summary>
    /// Interaction logic for LevelConfirm.xaml
    /// </summary>
    public partial class LevelConfirm : Window
    {
        public bool GameStarted = false;
        string levelID;
        public LevelConfirm(string lID)
        {
            levelID = lID;
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            MasterLevelControls mlc = new MasterLevelControls(@"bin\levels\" + levelID);
            string[] scores = mlc.ReadHighScores();
            levelNameBox.Text = "Level" + levelID.ToUpper(); //to upper is only for if the tutorial is on
            rankingBox.Text = "S-Ranks: " + scores[3] + ", A-Ranks: " + scores[4] + ", B-Ranks: " + scores[5] + ", C-Ranks: " + scores[6];
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            GameStarted = true;
            TestingGameWindow mainGame = new TestingGameWindow(levelID);
            Hide();
            mainGame.ShowDialog();
            #region postgame
            double time = mainGame.TimeTaken * 0.032;
            
            MasterLevelControls mlc = new MasterLevelControls(@"bin\levels\" + levelID);
            string[] scores = mlc.ReadHighScores();
            if (mainGame.GameWin)
            {
                if (time < int.Parse(scores[0]))
                {
                    scores[3] = Convert.ToString(int.Parse(scores[3]) + 1); //switches to int, adds one, switches back
                }
                else if (time < int.Parse(scores[1]))
                {
                    scores[4] = Convert.ToString(int.Parse(scores[4]) + 1);
                }
                else if (time < int.Parse(scores[2]))
                {
                    scores[5] = Convert.ToString(int.Parse(scores[5]) + 1);
                }
                else
                {
                    scores[6] = Convert.ToString(int.Parse(scores[6]) + 1);
                }
                LineChanger(string.Join(",", scores), @"bin\levels\" + levelID + ".alpr", 2);
            }
            #endregion
        }
        static void LineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        } //please see credits - this code is not mine

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
