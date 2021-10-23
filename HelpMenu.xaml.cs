using System.Windows;
using System;
using System.Windows.Media.Imaging;

namespace CollisionTesting
{
    /// <summary>
    /// Interaction logic for HelpMenu.xaml
    /// </summary>
    public partial class HelpMenu : Window
    {
        internal string[] answers = new string[] { "First off, thanks for playing!\n" +
            "In this game, you play as a rocket trying to touch the landing pad at the end of the level. \n" +
            "You'll have to dodge the obstacles and enemy missiles to get there!\n" +
            "Use the up arrow key for your thrusters, and use left and right to rotate!\n" +
            "It's simple to learn, but hard to master. Good luck!",
            "Each level has its own timer for a highscore\n" +
            "In order to get a certain rank, you need to beat the level within a certain time frame\n" +
            "Even if you take ages, you can always get a C-Rank - that's for clearing the level\n" +
            "The game keeps track of all your ranks, so you can see how many times you've hit that rank\n" +
            "Try to get an S-Rank on every level!",
            "Throughout the levels, there's enemy missile launchers.\n" +
            "Sometimes there's one, sometimes there's many and sometimes there's none at all.\n" +
            "They'll fire at you when they can see you, so make sure to hide behind terrain!\n" +
            "The missiles are faster and more agile than you, but if you can dodge, they can't fly down!\n" +
            "Missiles die when they hit terrain. When they do, the launchers will start reloading.\n",
            "First off, make sure to start turning your ship before you need to use your thrusters.\n" +
            "This makes sure that you're ready to counter any drift\n" +
            "You can only hold one key at a time. Don't expect to be able to both rotate and thrust!\n" +
            "Enemy missile launchers take four times longer to prepare for their first shot. \n" +
            "If you can get through the level quickly, they might not even be able to fire!"
        };
        public HelpMenu()
        {
            InitializeComponent();
        }

        private void Button_HCat1_Click(object sender, RoutedEventArgs e)
        {
            WriteToLabel(0);
            WriteImage("t1");
        }

        private void Button_HCat2_Click(object sender, RoutedEventArgs e)
        {
            WriteToLabel(1);
            WriteImage("t2");
        }

        private void Button_HCat3_Click(object sender, RoutedEventArgs e)
        {
            WriteToLabel(2);
            WriteImage("t3");
        }

        private void Button_HCat4_Click(object sender, RoutedEventArgs e)
        {
            WriteToLabel(3);
            WriteImage("t4");
        }

        private void WriteToLabel(int x)
        {
            label_Output.Content = answers[x];
        }

        private void MainMenu(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal void WriteImage(string arg)
        {
            Uri source = new Uri("file:///" + Environment.CurrentDirectory + "/bin/" + arg + ".png"); //pulls URI of target file using relative path
            TutorialImage.Source = new BitmapImage(source); //sets image to that URI
        }

        private void EnterTGW(object sender, RoutedEventArgs e)
        {
            LevelConfirm confirm = new LevelConfirm("tgw");
            Hide();
            confirm.ShowDialog();
            if (confirm.GameStarted) { Close(); }
        }
    }
}
