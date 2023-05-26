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
    /// Interaction logic for AddAttractionPopup.xaml
    /// </summary>
    public partial class AddAttractionPopup : Window
    {
        public TouristAttraction Attraction { get; set; }
        public Model.Location SelectedLocation { get; set; }
        public Boolean isMapClicked { get; set; }
        public AddAttractionPopup()
        {
            InitializeComponent();
        }

        public AddAttractionPopup(TouristAttraction attraction)
        {
            this.Attraction = attraction;
            isMapClicked = false;
            InitializeComponent();
            if (this.Attraction != null)
            {
                SelectedLocation = Attraction.Adresa;
                tbRegistration.Text = "Izmenite";
                FillFields();
            }

        }


        private void FillFields()
        {
            //    ComboBox1.Items.RemoveAt(ComboBox1.Items.IndexOf(ComboBox1.SelectedItem));  
            tbNaziv.Text = Attraction.Naziv;
            tbMesto.Text = Attraction.Adresa.Naziv;

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
          
            String name = tbNaziv.Text.Trim();
            String mesto = tbMesto.Text.Trim();

            if (name == "" || mesto == "")
            {

                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";
                return;
            }
            SelectedLocation.Naziv = mesto;
            errorControl.Visibility = Visibility.Hidden;

            TouristAttraction att = new TouristAttraction();
            att.Naziv = name;
            att.Adresa = SelectedLocation;
            att.JeObrisan = "0";
            // ovo je add
            if (this.Attraction == null)
            {
                if (!isMapClicked)
                {
                    errorControl.Visibility = Visibility.Visible;
                    errorControl.ErrorHandler.Text = "Molimo Vas selektujte lokaciju na mapi.";
                    return;
                }
                FileService.addAttraction(att);

                // da bi mogli znati da li je sacuvano da se refreshuje tabela
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup($"Uspesno ste dodali objekat {att.Naziv}.");
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

                List<TouristAttraction> attractions = FileService.getAttractions();
                foreach (TouristAttraction attr in attractions)
                {
                    if (attr.Id == this.Attraction.Id)
                    {
                        attr.Naziv = name;
                        attr.Adresa = SelectedLocation;
                        attr.JeObrisan = "0";
                        this.Attraction = attr;
                        break;
                    }
                }
                // upisi ponovo sve u fajl sa izmenama
                FileService.editLocation(SelectedLocation);
                FileService.writeAttractions(attractions);


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
            if (Attraction != null)
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
