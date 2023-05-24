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
        }

        private void FillFields()
        {
            tbCena.Text = trip.Cena.ToString() + " RSD";
            tbDatumKraja.Text = trip.DatumKraja.ToString();
            tbDatumPocetka.Text = trip.DatumPocetka.ToString();
            tbNaziv.Text = trip.Naziv.ToString();
            
        }

        private void btnNazad_ButtonClicked(object sender, EventArgs e)
        {

        }

        private void btnRezervisi_ButtonClicked(object sender, EventArgs e)
        {

        }

        private void btnPrikaziDetalje_ButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
