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
    public partial class SoldTripAgent : UserControl
    {
        public SoldTrip trip { get; set; }
        public bool IsReservation { get; set; }
        public bool IsUser { get; set; }
        public bool IsLogedIn { get; set; }

        public SoldTripAgent()
        {
            InitializeComponent();
        }


        public SoldTripAgent(SoldTrip trip)
        {
            this.trip = trip;
           
            InitializeComponent();
            FillFields();
            FillDestinationItems();
            

        }

    

        private void FillDestinationItems()
        {
            lbDestinations.ItemsSource = this.trip.Trip.Objekti;

        }

        private void FillFields()
        {
            tbCena.Text = trip.Trip.Cena.ToString() + " RSD";
            tbDatumKraja.Text = trip.Trip.DatumKraja.ToString();
            tbDatumPocetka.Text = trip.Trip.DatumPocetka.ToString();
            tbNaziv.Text = trip.Trip.Naziv.ToString();


            tbImePutnika.Text = trip.User.Name; 
            tbPrezimePutnika.Text = trip.User.Surname;
            tbEmailPutnika.Text = trip.User.Email;
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
                OkPopup ok = new OkPopup("Molimo Vas prvo izaberite stavku iz liste plan putovanja kako biste prikazali njene detalje.");
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
