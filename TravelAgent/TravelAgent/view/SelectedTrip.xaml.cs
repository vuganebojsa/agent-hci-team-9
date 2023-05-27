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
    /// Interaction logic for SelectedTrip.xaml
    /// </summary>
    public partial class SelectedTrip : UserControl
    {
        public Trip trip { get; set; }
        public bool IsReservation { get; set; }
        public bool IsUser { get; set; }
        public bool IsLogedIn { get; set; }

        public SelectedTrip()
        {
            InitializeComponent();
        }


        public SelectedTrip(Trip trip, bool isReservation, bool isUser, bool isLogedIn)
        {
            this.trip = trip;
            IsReservation = isReservation;
            IsUser = isUser;
            IsLogedIn = isLogedIn;
            InitializeComponent();
            FillFields();
            FillDestinationItems();
            SetPins();
            CheckFlags();
            
        }

        private void CheckFlags()
        {

            if (!IsLogedIn)
            {
                btnPocetna.Visibility = Visibility.Visible;
                btnRezervisi.Visibility = Visibility.Hidden;
            }else if (IsUser)
            {
                btnRezervisi.Visibility = Visibility.Visible;

            }
            else
            {
                btnRezervisi.Visibility = Visibility.Hidden;
            }
            if (IsReservation)
            {
                btnRezervisi.Visibility = Visibility.Hidden;

            }
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

        public void btnNazad_ButtonClicked(object sender, EventArgs e)
        {
            UnregisteredTrips sw = new UnregisteredTrips();

            
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;

            sw.Left = left;
            sw.Top = top;

            Application.Current.MainWindow = sw;
            Application.Current.MainWindow.Show();
            Window.GetWindow(this).Close();

        }

        private void btnRezervisi_ButtonClicked(object sender, EventArgs e)
        {

            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            YesNoPopup yn = new YesNoPopup($"Da li ste sigurni da zelite da rezervisete {trip.Naziv} putovanje?");

            yn.Left = left + width / 2 - 100;
            yn.Top = top + height / 2 - 250;
            if (yn.ShowDialog() == true)
            {

                long userId = CurrentlyloggedInUser.user.Id;
                long tripId = trip.Id;
                FileService.writeReservation(userId, tripId);
                OkPopup ok = new OkPopup($"Uspesno ste rezervisali {trip.Naziv} putovanje.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    return;
                }

            }

           
            

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
