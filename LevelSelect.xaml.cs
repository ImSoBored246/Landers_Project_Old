using System.Windows;
using System.IO;

namespace CollisionTesting
{
    /// <summary>
    /// Interaction logic for LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {
        public LevelSelect()
        {
            InitializeComponent();
        }

        private void SelectLevel(int levelID)
        {
            if (int.Parse(File.ReadAllText(@"bin\workingLevels.txt")) < levelID) //file contains a number, corresponding to the latest developed LevelID
            {
                MessageBox.Show("That level doesn't exist yet!");
            }
            else
            {
                if (levelID == 0)
                {
                    LevelConfirm confirm = new LevelConfirm("tgw");
                    Hide();
                    confirm.ShowDialog();
                    if (confirm.GameStarted) { Close(); }
                }
                else
                {
                    LevelConfirm confirm = new LevelConfirm(levelID.ToString());
                    Hide();
                    confirm.ShowDialog();
                    if (confirm.GameStarted) { Close(); }
                }
            }
        }

        #region LevelSelectButtons
        private void Button_L01_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(1);
        }

        private void Button_L02_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(2);
        }

        private void Button_L03_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(3);
        }

        private void Button_L04_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(4);
        }

        private void Button_L05_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(5);
        }

        private void Button_L06_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(6);
        }

        private void Button_L07_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(7);
        }

        private void Button_L08_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(8);
        }

        private void Button_L09_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(9);
        }

        private void Button_L10_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(10);
        }

        private void Button_L11_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(11);
        }

        private void Button_L12_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(12);
        }

        private void Button_L13_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(13);
        }

        private void Button_L14_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(14);
        }

        private void Button_L15_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(15);
        }

        private void Button_L16_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(16);
        } //there’s probably a way to automate the creation of these buttons due to consistency (nothing unique)
        #endregion

        private void Button_Tutorial_Click(object sender, RoutedEventArgs e)
        {
            SelectLevel(0); //ID 0 will be the tutorial level
            //since the value stored in workingLevels.txt will be >=0, 
            //I'll use TestingGameWindow as a placeholder
        }

        private void Button_ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}