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
    /// Interaction logic for SoldTripsOverview.xaml
    /// </summary>
    public partial class SoldTripsOverview : UserControl
    {
        public List<SoldTrip> soldTrips { get; set; }
        public SoldTripsOverview()
        {
            soldTrips = FileService.getAllSoldTrips();

            InitializeComponent();

            TableDataGrid.AutoGenerateColumns = false;
            TableDataGrid.ItemsSource = soldTrips;

            // Define columns manually
            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "#";
            nameColumn.Binding = new Binding("Trip.Id");

            DataGridTextColumn destinationColumn = new DataGridTextColumn();
            destinationColumn.Header = "Naziv";
            destinationColumn.Binding = new Binding("Trip.Naziv");

            DataGridTextColumn priceColumn = new DataGridTextColumn();
            priceColumn.Header = "Cena";
            priceColumn.Binding = new Binding("Trip.Cena");

            DataGridTextColumn startDateColumn = new DataGridTextColumn();
            startDateColumn.Header = "Datum";
            startDateColumn.Binding = new Binding("Trip.DatumPocetka") { StringFormat = "MM/dd/yyyy" };

            

            DataGridTextColumn putnikColumn = new DataGridTextColumn();
            putnikColumn.Header = "Putnik";
            putnikColumn.Binding = new Binding("User.Name");

            // Add the columns to the DataGrid
            TableDataGrid.Columns.Add(nameColumn);
            TableDataGrid.Columns.Add(destinationColumn);
            TableDataGrid.Columns.Add(priceColumn);
            TableDataGrid.Columns.Add(startDateColumn);
            TableDataGrid.Columns.Add(putnikColumn);
        }

        

        private void Detaljnije_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (SoldTrip)TableDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite red iz tabele kako biste videli detalje.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {

                    return;
                }
                return;

            }
            else
            {
                SoldTripAgent st = new SoldTripAgent(selectedItem);

                AdminPage parentWindow = (AdminPage)Application.Current.MainWindow;
                parentWindow.MainContent.Content = null;
                // Set the newly created user control as the content of the container
                parentWindow.MainContent.Content = st;
            }
        }
    }
}
