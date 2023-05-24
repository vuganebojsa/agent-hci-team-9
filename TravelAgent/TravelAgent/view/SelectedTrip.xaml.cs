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

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for SelectedTrip.xaml
    /// </summary>
    public partial class SelectedTrip : UserControl
    {
        public Trip trip { get; set; }
        public bool IsReservation { get; set; }
        // dodati jos flegova

        public SelectedTrip()
        {
            InitializeComponent();
        }


        public SelectedTrip(Trip trip)
        {
            this.trip = trip;
            InitializeComponent();
            FillFields();
            FillDestinationItems();
            SetPins();
        }

        private void SetPins()
        {
            List<Microsoft.Maps.MapControl.WPF.Location> locations = new List<Microsoft.Maps.MapControl.WPF.Location>();

            foreach (IBivuja bivuja in trip.Objekti){
                Pushpin pin = new Pushpin();
                pin.Location = new Microsoft.Maps.MapControl.WPF.Location(bivuja.Adresa.Latitude, bivuja.Adresa.Longitude);
                locations.Add(pin.Location);
                bingMap.Children.Add(pin);
            }
            if (trip.Objekti.Count > 0)
            {
                var w = new Pushpin().Width;
                var h = new Pushpin().Height;
                var margin = new Thickness(w / 2, h, w / 2, 0);

                //Set view
                bingMap.Loaded += (s, e) =>
                {
                    bingMap.SetView(locations, new Thickness(30), 0);

                };
               /* double averageLatitude = trip.Objekti.Average(val => val.Adresa.Latitude);
                double averageLongitude = trip.Objekti.Average(val => val.Adresa.Longitude);*/

                /*bingMap.Center = new Microsoft.Maps.MapControl.WPF.Location(averageLatitude, averageLongitude);*/
            }

        }

        private void FillDestinationItems()
        {
            lbDestinations.ItemsSource = this.trip.Objekti;
           
        }

        private void FillFields()
        {
            tbCena.Text = trip.Cena.ToString() + " RSD";
            tbDatumKraja.Text = trip.DatumKraja.ToString();
            tbDatumPocetka.Text = trip.DatumPocetka.ToString();
            tbNaziv.Text = trip.Naziv.ToString();
            

            // proveri flegove i stavi visible invisible na polja
        }

        private void btnNazad_ButtonClicked(object sender, EventArgs e)
        {
            // na osnovu flegova odredi gde se vraca, tj na koji/ciji ekran

        }

        private void btnRezervisi_ButtonClicked(object sender, EventArgs e)
        {
            
        }

        private void btnPrikaziDetalje_ButtonClicked(object sender, EventArgs e)
        {
            //if item from list selected set visibiliti to visible and set items
            IBivuja selectedItem = (IBivuja)lbDestinations.SelectedItem;
            if (selectedItem == null)
            {
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite stavku iz liste kako biste prikazali njene detalje.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    return;
                }
                return;
            }

            tbDetaljiMesto.Text = selectedItem.Adresa.Naziv;
            tbDetaljiNaziv.Text = selectedItem.Naziv;

            SetTbDetalji(selectedItem);
            gridDetalji.Visibility = Visibility.Visible;
        }

        private void SetTbDetalji(IBivuja selectedItem)
        {
            if (selectedItem.GetType() == typeof(PlaceRestaurant))
            {
                tbDetaljiTip.Text = (selectedItem as PlaceRestaurant).vrsta.ToString();

            }
            else
            {
                tbDetaljiTip.Text = "Turisticka Atrakcija";
            }
        }
    }
}
