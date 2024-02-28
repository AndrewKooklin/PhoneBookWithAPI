using System.Windows;

namespace PhoneBookWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbUserName.Text = PhoneBookWPF.Properties.Settings.Default.UserName;
            tbPassword.Password = PhoneBookWPF.Properties.Settings.Default.Password;
        }
    }
}
