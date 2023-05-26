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
    /// Interaction logic for AllTripsOverview.xaml
    /// </summary>
    public partial class AllTripsOverview : UserControl
    {
        public List<Trip> trips { get; set; }
        public AllTripsOverview()
        {
            trips = FileService.getAllActiveTrips();

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
        private void Prikazi_ButtonClicked(object sender, EventArgs e)
        {

            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (Trip)TableDataGrid.SelectedItem;
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
                User curUser = CurrentlyloggedInUser.user;
                bool isUser = false;
                bool isLogedIn = true;
                if (curUser != null)
                {
                    isUser = curUser.role == Role.USER;

                }
                else
                {
                    isLogedIn = false;
                }

                SelectedTrip st = new SelectedTrip(selectedItem, false, isUser, isLogedIn);
                if (isUser)
                {
                    UserPage parentWindow = (UserPage)Application.Current.MainWindow;
                    parentWindow.MainContent.Content = null;
                    // Set the newly created user control as the content of the container
                    parentWindow.MainContent.Content = st;
                }else if (isLogedIn)
                {
                    AdminPage parentWindow = (AdminPage)Application.Current.MainWindow;
                    parentWindow.MainContent.Content = null;
                    // Set the newly created user control as the content of the container
                    parentWindow.MainContent.Content = st;
                }
                else
                {
                    UnregisteredTrips parentWindow = (UnregisteredTrips)Application.Current.MainWindow;
                    parentWindow.MainContent.Content = null;
                    // Set the newly created user control as the content of the container
                    parentWindow.MainContent.Content = st;
                }

               
            }

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Trip> trips = FileService.getAllActiveTrips();
            String text = tbSearch.Text.ToLower().Trim();
            if (text == "")
            {
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = trips;
                return;
            }
            var newVals = SearchService.getTripsByKeyword(text, trips);
            trips = newVals;
            TableDataGrid.ItemsSource = null;
            TableDataGrid.ItemsSource = trips;


        }


    }
}
