using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for AddPlaceRestaurantPopup.xaml
    /// </summary>
    public partial class AddPlaceRestaurantPopup : Window
    {
        public PlaceRestaurant PlaceRestaurant { get; set; }
        public Model.Location SelectedLocation { get; set; }
        public Boolean isMapClicked { get; set; }
        public AddPlaceRestaurantPopup()
        {
            InitializeComponent();
        }

        public AddPlaceRestaurantPopup(PlaceRestaurant placeRestaurant)
        {
            this.PlaceRestaurant = placeRestaurant;
            isMapClicked = false;
            InitializeComponent();
            if (this.PlaceRestaurant != null)
            {
                SelectedLocation = PlaceRestaurant.Adresa;
                tbRegistration.Text = "Izmenite smestaj ili restoran";
                FillFields();
            }
            
        }

          
        private void FillFields()
        {
            //    ComboBox1.Items.RemoveAt(ComboBox1.Items.IndexOf(ComboBox1.SelectedItem));  
            cbType.Text = PlaceRestaurant.vrsta.ToString();
            tbNaziv.Text = PlaceRestaurant.Naziv;
            tbMesto.Text = PlaceRestaurant.Adresa.Naziv;

            int zoomLevel = 12; // Adjust the zoom level as desired

            bingMap.Center = new Microsoft.Maps.MapControl.WPF.Location(SelectedLocation.Latitude, SelectedLocation.Longitude);
            Pushpin pin = new Pushpin();
            pin.Location = new Microsoft.Maps.MapControl.WPF.Location(SelectedLocation.Latitude, SelectedLocation.Longitude);
            bingMap.Children.Add(pin);
            bingMap.ZoomLevel = zoomLevel;
        }

 

        private void btnCancel_ButtonClicked(object sender, EventArgs e)
        {
            YesNoPopup yn = new YesNoPopup("Da li ste sigurni da zelite da otkazete? Ovom akcijom nista nece biti izmenjeno.");
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            yn.Left = left + width / 2 - 100;
            yn.Top = top + height / 2 - 200;

            if (yn.ShowDialog() == true)
            {
                this.DialogResult = false;
            }
        }

        private void btnSave_ButtonClicked(object sender, EventArgs e)
        {
            if(cbType.Text.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";
                
                return;
            }
            Vrsta type = (Vrsta)Enum.Parse(typeof(Vrsta), cbType.Text);
            String name = tbNaziv.Text.Trim();
            String mesto = tbMesto.Text.Trim();

            if(name == "" || mesto == "")
            {

                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";
                return;
            }
            SelectedLocation.Naziv = mesto;
            errorControl.Visibility = Visibility.Hidden;

            PlaceRestaurant pr = new PlaceRestaurant();
            pr.Naziv = name;
            pr.Adresa = SelectedLocation;
            pr.vrsta = type;
            pr.JeObrisan = "0";
            // ovo je add
            if(this.PlaceRestaurant == null)
            {
                if (!isMapClicked)
                {
                    errorControl.Visibility = Visibility.Visible;
                    errorControl.ErrorHandler.Text = "Molimo Vas selektujte lokaciju na mapi.";
                    return;
                }
                FileService.addPlaceRestaurant(pr);

                // da bi mogli znati da li je sacuvano da se refreshuje tabela
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup($"Uspesno ste dodali objekat {pr.Naziv}.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    // da bi mogli znati da li je sacuvano da se refreshuje tabela

                    this.DialogResult = true;
                }

            }
            else
            {
                // ovo je edit

                List<PlaceRestaurant> placeRestaurants = FileService.getPlacesAndRestaurants();
                foreach(PlaceRestaurant p in placeRestaurants)
                {
                    if(p.Id == this.PlaceRestaurant.Id)
                    {
                        p.vrsta = type;
                        p.Naziv = name;
                        p.Adresa = SelectedLocation;
                        p.JeObrisan = "0"; 
                        PlaceRestaurant = p;
                        break;
                    }
                }
                // upisi ponovo sve u fajl sa izmenama
                FileService.editLocation(SelectedLocation);
                FileService.writePlacesRestaurants(placeRestaurants);
                
                
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup("Uspesno ste izmenili objekat.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    // da bi mogli znati da li je sacuvano da se refreshuje tabela

                    this.DialogResult = true;
                }
                
            }





        }

        private async void bubgNao_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bingMap.Children.Clear();
            e.Handled = true;

            Point mousePosition = e.GetPosition(bingMap);
            Microsoft.Maps.MapControl.WPF.Location clickedLocation = bingMap.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = clickedLocation;
            double latitude = clickedLocation.Latitude;
            double longitude = clickedLocation.Longitude;
            bingMap.Children.Add(pin);
            string locationName = "";
            // Do something with the latitude and longitude values
            try
            {
                locationName = await MapService.ReverseGeocodeAsync(latitude, longitude);
            }
            catch (Exception ex)
            {

            }
            if(PlaceRestaurant != null)
            {
                SelectedLocation.Naziv = locationName;
                SelectedLocation.Longitude = longitude;
                SelectedLocation.Latitude = latitude;

            }
            else 
                SelectedLocation = new Model.Location(locationName, longitude, latitude);
            tbMesto.Text = locationName;
            isMapClicked = true;

        }
    }
}
