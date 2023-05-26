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
    /// Interaction logic for AttractionsManagement.xaml
    /// </summary>
    public partial class AttractionsManagement : UserControl
    {
        public List<TouristAttraction> attractions { get; set; }
        public List<TouristAttraction> attractionsWithFlag { get; set; }
        public AttractionsManagement()
        {
            attractions = FileService.getAtractions();
            filterAttractions();

            InitializeComponent();

            TableDataGrid.AutoGenerateColumns = false;
            TableDataGrid.ItemsSource = attractionsWithFlag;

            // Define columns manually
            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "#";
            nameColumn.Binding = new Binding("Id");
            DataGridTextColumn destinationColumn = new DataGridTextColumn();
            destinationColumn.Header = "Naziv";
            destinationColumn.Binding = new Binding("Naziv");

            DataGridTextColumn priceColumn = new DataGridTextColumn();
            priceColumn.Header = "Mesto";
            priceColumn.Binding = new Binding("Adresa.Naziv");

            // Add the columns to the DataGrid
            TableDataGrid.Columns.Add(nameColumn);
            TableDataGrid.Columns.Add(destinationColumn);
            TableDataGrid.Columns.Add(priceColumn);


        }

        private void filterAttractions()
        {
            var newAttractions = new List<TouristAttraction>();
            foreach (TouristAttraction att in attractions)
            {
                if (att.JeObrisan == "0") newAttractions.Add(att);
            }
            attractionsWithFlag = newAttractions;
        }

        private void btnDelete_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (TouristAttraction)TableDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite red iz tabele kako biste izvrsili promene.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    return;
                }
                return;

            }
            YesNoPopup yn = new YesNoPopup($"Da li ste sigurni da zelite da obrisete {selectedItem.Naziv} objekat?");

            yn.Left = left + width / 2 - 100;
            yn.Top = top + height / 2 - 250;
            if (yn.ShowDialog() == true)
            {
                foreach(TouristAttraction att in attractions)
                {
                    if (att.Id == selectedItem.Id)
                    {
                        att.JeObrisan = "1";
                        break;
                    }
                }

                FileService.deleteAttractions(selectedItem, attractions);


                filterAttractions();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.attractionsWithFlag;
                CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();
                //FileService.writePlacesRestaurants(placesRestaurants);
            }


        }

        private void btnAdd_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;

            AddAttractionPopup ap = new AddAttractionPopup(); //TODO: promeniti ovde isto da bude window AddAttractionPopup
            ap.Left = left + width / 2 - 130;
            ap.Top = top + height / 2 - 250;

            if (ap.ShowDialog() == true)
            {
                tbSearch.Text = "";
                this.attractions = FileService.getAtractions();
                filterAttractions();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.attractionsWithFlag;
                /* TableDataGrid.Items.Refresh();

                 CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();*/
            }
        }

        private void btnEdit_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (TouristAttraction)TableDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite red iz tabele kako biste izvrsili promene.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {

                    return;
                }
                return;

            }
            AddAttractionPopup ap = new AddAttractionPopup(selectedItem); //TODO: uraditi i ovde isto
            ap.Left = left + width / 2 - 130;
            ap.Top = top + height / 2 - 250;

            if (ap.ShowDialog() == true)
            {

                tbSearch.Text = "";
                this.attractions = FileService.getAtractions();
                filterAttractions();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.attractionsWithFlag;
                /* TableDataGrid.Items.Refresh();
                 CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();*/
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            String text = tbSearch.Text.ToLower().Trim();
            if (text == "")
            {
                this.attractions = FileService.getAtractions();
                filterAttractions();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.attractionsWithFlag;
                return;
            }
            var newVals = SearchService.getAttractionsByKeyword(text, this.attractions);
            this.attractions = newVals;
            filterAttractions();
            TableDataGrid.ItemsSource = null;
            TableDataGrid.ItemsSource = this.attractionsWithFlag;


        }
    }
}
