using System.Windows;

namespace CollisionTesting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                if (e.Args[0] == "devModeOn")
                {
                    new TestingGameWindow("tgw").ShowDialog();
                }
                else { new MainMenu().Show(); }
            }
            else { new MainMenu().Show(); }
        }
    }
}
