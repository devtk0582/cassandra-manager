using CassandraManager.Services.Repository;
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
    /// Interaction logic for DataQueryView.xaml
    /// </summary>
    public partial class DataQueryView : Page
    {
        private readonly DataRepository _dataRepository;

        public DataQueryView()
        {
            InitializeComponent();

            _dataRepository = new DataRepository(AppContext.Instance);
            InitData();
        }

        private void InitData()
        {
            HostLabel.Content = AppContext.Instance?.Host;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            var query = QueryTextBox.Text;
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Query is required", "Invalid Query", MessageBoxButton.OK);
                return;
            }

            try
            {
                var dataTable = _dataRepository.GetData(query);
                ResultDataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing the query: {ex.Message}", "Execution Error", MessageBoxButton.OK);
            }
        }
    }
}
