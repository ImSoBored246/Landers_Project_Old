using System.Windows;

namespace CollisionTesting
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayGame(object sender, RoutedEventArgs e)
        {
            Hide();
            new LevelSelect().ShowDialog(); //don't need to define the level select, since it won't be used later
            Show();
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("That feature isn't available yet!","Error!"); //throws temporary error
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit?", "Confirm exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes) //creates yes/no prompt and acts on yes
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Button_HelpMenu_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new HelpMenu().ShowDialog();
            Show();
        }
    }
}
