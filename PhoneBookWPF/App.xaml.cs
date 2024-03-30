using PhoneBookWPF.View;
using System.Windows;

namespace PhoneBookWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PhoneBookWindow PhoneBookWindow = new PhoneBookWindow();

        public static RegistrationWindow RegistrationWindow = new RegistrationWindow();

        public static RecordsView RecordsView = new RecordsView();

        public static RolesView RolesView = new RolesView();

        public static UsersView UsersView = new UsersView();

        public static ActionsWithRecordView ActionsWithRecordView = new ActionsWithRecordView();

        public static ActionsWithRoleView ActionsWithRoleView = new ActionsWithRoleView();

        public static ActionAddUserView ActionAddUserView = new ActionAddUserView();

        public static ActionDeleteUserView ActionDeleteUserView = new ActionDeleteUserView();

        public static ActionsRoleUserView ActionsRoleUserView = new ActionsRoleUserView();

        public static new MainWindow MainWindow = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
            App.PhoneBookWindow.Show();

            base.OnStartup(e);
        }
    }
}
