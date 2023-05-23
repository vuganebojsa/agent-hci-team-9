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
using TravelAgent.Model;
using TravelAgent.services;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for BookedTripsOverview.xaml
    /// </summary>
    public partial class BookedTripsOverview : UserControl
    {
        public List<Trip> trips { get; set; }
        public BookedTripsOverview()
        {
            trips = FileService.getBookedTrips();

            InitializeComponent();

            TableDataGrid.AutoGenerateColumns = false;
            TableDataGrid.ItemsSource = trips;

            // Define columns manually
            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "#";
            nameColumn.Binding = new Binding("Id");
            DataGridTextColumn destinationColumn = new DataGridTextColumn();
            destinationColumn.Header = "Naziv";
            destinationColumn.Binding = new Binding("Naziv");

            DataGridTextColumn priceColumn = new DataGridTextColumn();
            priceColumn.Header = "Cena";
            priceColumn.Binding = new Binding("Cena");

            DataGridTextColumn startDateColumn = new DataGridTextColumn();
            startDateColumn.Header = "Datum pocetka";
            startDateColumn.Binding = new Binding("DatumPocetka") { StringFormat = "MM/dd/yyyy" };

            DataGridTextColumn endDateColumn = new DataGridTextColumn();
            endDateColumn.Header = "Datum kraja";
            endDateColumn.Binding = new Binding("DatumKraja") { StringFormat = "MM/dd/yyyy" };

            // Add the columns to the DataGrid
            TableDataGrid.Columns.Add(nameColumn);
            TableDataGrid.Columns.Add(destinationColumn);
            TableDataGrid.Columns.Add(priceColumn);
            TableDataGrid.Columns.Add(startDateColumn);
            TableDataGrid.Columns.Add(endDateColumn);

        }

    }
}
