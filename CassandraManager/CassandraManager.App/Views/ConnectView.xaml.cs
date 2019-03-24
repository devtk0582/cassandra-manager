using CassandraManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CassandraManager.App.Views
{
    /// <summary>
    /// Interaction logic for ConnectView.xaml
    /// </summary>
    public partial class ConnectView : Page
    {
        public ConnectView()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(HostTextBox.Text)
                || string.IsNullOrEmpty(PortTextBox.Text)
                || string.IsNullOrEmpty(KeyspaceTextBox.Text))
            {
                return;
            }

            var connectionString =
                $"Contact Points={HostTextBox.Text};Port={PortTextBox.Text};Default Keyspace={KeyspaceTextBox.Text}";

            if (!string.IsNullOrEmpty(UsernameTextBox.Text)
                && !string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                connectionString += $";Username={UsernameTextBox.Text};Password={PasswordTextBox.Text}";
            }

            AppContext.Instance = new CassandraDbContext(connectionString);

            this.NavigationService.Navigate(new DataMonitorView());
        }
    }
}
