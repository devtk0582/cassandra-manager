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
    /// Interaction logic for DataMonitorView.xaml
    /// </summary>
    public partial class DataMonitorView : Page
    {
        private readonly TableRepository _tableRepository;
        private readonly DataRepository _dataRepository;

        public DataMonitorView()
        {
            InitializeComponent();

            _tableRepository = new TableRepository(AppContext.Instance);
            _dataRepository = new DataRepository(AppContext.Instance);

            InitData();
        }

        private void InitData()
        {
            var tables = _tableRepository.GetTables();
            TableListView.ItemsSource = tables;
        }

        private void TableListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0)
                return;

            var selectedTable = e.AddedItems[0] as string;
            TableNameLabel.Content = selectedTable;

            var columns = _dataRepository.GetTableColumnInfo(selectedTable).ToList();
            ColumnListView.ItemsSource = columns;

            var indexes = _dataRepository.GetTableIndexInfo(selectedTable);
            IndexListView.ItemsSource = indexes;

            try
            {
                var dataTable = _dataRepository.GetData(selectedTable, columns);
                DataListGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing the query: {ex.Message}", "Execution Error", MessageBoxButton.OK);
            }
        }

        private void QueryExecutionViewButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DataQueryView());
        }

    }
}
