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
    /// Interaction logic for PlaceRestaurantManagement.xaml
    /// </summary>
    public partial class PlaceRestaurantManagement : UserControl
    {
        public List<PlaceRestaurant> placesRestaurants { get; set; }
        public PlaceRestaurantManagement()
        {
            placesRestaurants = FileService.getPlacesAndRestaurants();

            InitializeComponent();

            TableDataGrid.ItemsSource = placesRestaurants;
        }

        private void btnDelete_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (PlaceRestaurant)TableDataGrid.SelectedItem;
            if(selectedItem == null)
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
           
            yn.Left = left + width/2 - 100;
            yn.Top = top + height/2 - 100;
            if (yn.ShowDialog() == true)
            {
                this.placesRestaurants.Remove(selectedItem);
                CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();
                FileService.writePlacesRestaurants(placesRestaurants);
            }
            
            
        }

        private void btnAdd_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;

            AddPlaceRestaurantPopup ap = new AddPlaceRestaurantPopup();
            ap.Left = left + width / 2 - 100;
            ap.Top = top + height / 2 - 150;

            if(ap.ShowDialog() == true)
            {
                
                this.placesRestaurants = FileService.getPlacesAndRestaurants();
                TableDataGrid.Items.Refresh();

                CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();
            }
        }

        private void btnEdit_ButtonClicked(object sender, EventArgs e)
        {
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            var selectedItem = (PlaceRestaurant)TableDataGrid.SelectedItem;
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
            AddPlaceRestaurantPopup ap = new AddPlaceRestaurantPopup(selectedItem);
            ap.Left = left + width / 2 - 100;
            ap.Top = top + height / 2 - 150;

            if (ap.ShowDialog() == true)
            {

                this.placesRestaurants = FileService.getPlacesAndRestaurants();
                TableDataGrid.Items.Refresh();
                CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();
            }
        }
    }
}
