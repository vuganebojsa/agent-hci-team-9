﻿using System;
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
        public List<PlaceRestaurant> placesRestaurantsWithFlag { get; set; }
        public PlaceRestaurantManagement()
        {
            placesRestaurants = FileService.getPlacesAndRestaurants();
            filterPlacesRestaurants();

            InitializeComponent();

            TableDataGrid.ItemsSource = placesRestaurantsWithFlag;
        }

        private void filterPlacesRestaurants()
        {
            var newPlaces = new List<PlaceRestaurant>();
            foreach(PlaceRestaurant pl in placesRestaurants)
            {
                if (pl.isDeleted == "0") newPlaces.Add(pl);
            }
            placesRestaurantsWithFlag = newPlaces;
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
                FileService.deletePlacesRestaurants(selectedItem, placesRestaurants);

                foreach(PlaceRestaurant pr in placesRestaurants)
                {
                    if(pr.Id == selectedItem.Id)
                    {
                        pr.isDeleted = "1";
                        break;
                    }
                }
                filterPlacesRestaurants();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.placesRestaurantsWithFlag;
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

            AddPlaceRestaurantPopup ap = new AddPlaceRestaurantPopup();
            ap.Left = left + width / 2 - 100;
            ap.Top = top + height / 2 - 150;

            if(ap.ShowDialog() == true)
            {
                tbSearch.Text = "";
                this.placesRestaurants = FileService.getPlacesAndRestaurants();
                filterPlacesRestaurants();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.placesRestaurantsWithFlag;
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

                tbSearch.Text = "";
                this.placesRestaurants = FileService.getPlacesAndRestaurants();
                filterPlacesRestaurants();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.placesRestaurantsWithFlag;
                /* TableDataGrid.Items.Refresh();
                 CollectionViewSource.GetDefaultView(TableDataGrid.ItemsSource).Refresh();*/
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            String text = tbSearch.Text.ToLower().Trim();
            if (text == "")
            {
                this.placesRestaurants = FileService.getPlacesAndRestaurants();
                filterPlacesRestaurants();
                TableDataGrid.ItemsSource = null;
                TableDataGrid.ItemsSource = this.placesRestaurantsWithFlag;
                return;
            }
            var newVals = SearchService.getPlaceRestaurantsByKeyword(text, this.placesRestaurants);
            this.placesRestaurants = newVals;
            filterPlacesRestaurants();
            TableDataGrid.ItemsSource = null;
            TableDataGrid.ItemsSource = this.placesRestaurantsWithFlag;


        }
    }
}
