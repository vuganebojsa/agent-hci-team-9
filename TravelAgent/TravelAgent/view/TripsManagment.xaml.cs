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
    /// Interaction logic for TripsManagment.xaml
    /// </summary>
    public partial class TripsManagment : UserControl
    {
        public List<Trip> trips { get; set; }
        public List<Trip> tripsWithFlag { get; set; }
        public TripsManagment()
        {


            trips = FileService.getAllTrips();
            List<Trip>  activeTrips = FileService.getAllActiveTrips();
            filterTrips();

            InitializeComponent();
            TableDataGrid.AutoGenerateColumns = false;
            TableDataGrid.ItemsSource = activeTrips;

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
            startDateColumn.Binding = new Binding("DatumPocetka") { StringFormat = "dd/MM/yyyy" };

            DataGridTextColumn endDateColumn = new DataGridTextColumn();
            endDateColumn.Header = "Datum kraja";
            endDateColumn.Binding = new Binding("DatumKraja") { StringFormat = "dd/MM/yyyy" };

            // Add the columns to the DataGrid
            TableDataGrid.Columns.Add(nameColumn);
            TableDataGrid.Columns.Add(destinationColumn);
            TableDataGrid.Columns.Add(priceColumn);
            TableDataGrid.Columns.Add(startDateColumn);
            TableDataGrid.Columns.Add(endDateColumn);
        }
        public void Izmenite_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (Trip)TableDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite red iz tabele kako biste izmenili putovanje.");
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
                Trip trip = FileService.getTripById(selectedItem.Id);
                AddTripPopup ap = new AddTripPopup(trip);
                ap.Left = left + width / 2 - 130;
                ap.Top = top + height / 2 - 250;

                if (ap.ShowDialog() == true)
                {
                    tbSearch.Text = "";
                    this.trips = FileService.getAllTrips();
                    filterTrips();
                    TableDataGrid.ItemsSource = null;
                    TableDataGrid.ItemsSource = this.tripsWithFlag;
                    /* TableDataGrid.Items.Refresh();

                     CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();*/
                }
            }

        }

        public void Obrisite_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (Trip)TableDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite red iz tabele kako biste obrisali putovanje.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {

                    return;
                }
                return;

            }
            YesNoPopup yn = new YesNoPopup($"Da li ste sigurni da zelite da obrisete {selectedItem.Naziv} putovanje?");

            yn.Left = left + width / 2 - 100;
            yn.Top = top + height / 2 - 100;
            if (yn.ShowDialog() == true)
            {
                
                

                foreach (Trip pr in trips)
                {
                    if (pr.Id == selectedItem.Id)
                    {
                        pr.JeObrisan = "1";
                        break;
                    }
                }
                FileService.deleteTrip(trips, selectedItem);
                filterTrips();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.tripsWithFlag;
                CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();
            }

        }
        private void filterTrips()
        {
            
            var newTrips = new List<Trip>();
            foreach (Trip tr in trips)
            {
                if (tr.JeObrisan == "0") newTrips.Add(tr);
            }
            tripsWithFlag = newTrips;
        }
        public void Dodajte_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;

            AddTripPopup ap = new AddTripPopup();
            ap.Left = left + width / 2 - 130;
            ap.Top = top + height / 2 - 250;

            if (ap.ShowDialog() == true)
            {
                tbSearch.Text = "";
                this.trips = FileService.getAllTrips();
                filterTrips();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.tripsWithFlag;
                /* TableDataGrid.Items.Refresh();

                 CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();*/
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            String text = tbSearch.Text.ToLower().Trim();
            if (text == "")
            {
                this.trips = FileService.getAllTrips();
                filterTrips();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.trips;
                return;
            }
            var newVals = SearchService.getTripsByKeyword(text, this.trips);
            this.trips = newVals;
            filterTrips();
            TableDataGrid.ItemsSource = null;
            TableDataGrid.ItemsSource = this.tripsWithFlag;


        }
    }
}
