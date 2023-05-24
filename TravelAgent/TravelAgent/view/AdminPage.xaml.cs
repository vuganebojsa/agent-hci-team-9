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
using System.Windows.Shapes;
using TravelAgent.Model;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {

           
            InitializeComponent();
            SelectedText = "Pregled svih putovanja";
        }

        private string selectedText;

        public string SelectedText
        {
            get { return selectedText; }
            set
            {
                selectedText = value;
                topNav.HeaderText = selectedText;
            }
        }

        private void topNav_ButtonClicked(object sender, EventArgs e)
        {

            YesNoPopup yn = new YesNoPopup("Da li ste sigurni da zelite da se izlogujete? Ovom akcijom cete biti prebaceni na ekran za logovanje.");
            yn.Left = Left + this.Width / 2 - 100;
            yn.Top = Top + this.Height / 2 - 100;
            
            if (yn.ShowDialog() == true)
            {
                Login login = new Login();

                double left = Left;
                double top = Top;

                login.Left = left;
                login.Top = top;

                Application.Current.MainWindow = login;
                Application.Current.MainWindow.Show();
                this.Close();
            }
           
        }

        private void SideNavigationAgent_ButtonPregledProdatihPutovanja(object sender, EventArgs e)
        {

            topNav_ButtonClicked(sender, e);
        }

        private void SideNavigationAgent_ButtonUpravljanjePutovanjimaClicked(object sender, EventArgs e)
        {
            List<IBivuja> objekti = new List<IBivuja>
            {
                new PlaceRestaurant(1, "Bibinovo",
                new Location("Novi Sad 1", 19.842622577411916, 45.2497935492016),
                Vrsta.Restoran, "0"),
                 new PlaceRestaurant(1, "Bibinov Smestaj",
                new Location("Novi Sad 2", 19.942622577411916, 45.3497935492016),
                Vrsta.Smestaj, "0"),
                  new TouristAttraction(1, "Bibinova Atrakcija",
                new Location("Novi Sad 3", 19.942622577411916, 45.2497935492016),
                 "0"),
            };
            Trip trip = new Trip(1, "Krstarenje meridianom", 10000, DateTime.Now, DateTime.Now.AddDays(3), objekti, "0");

            SelectedTrip st = new SelectedTrip(trip);
            MainContent.Content = null;
            // Set the newly created user control as the content of the container
            MainContent.Content = st;
        }

        private void SideNavigationAgent_ButtonUpravljanjeSmestajemIRestoranima(object sender, EventArgs e)
        {
            UserControl control = new PlaceRestaurantManagement();
            MainContent.Content = null;
            topNav.HeaderText = "Upravljanje Smestajem i restoranima";
            // Set the newly created user control as the content of the container
            MainContent.Content = control;
        }

        private void SideNavigationAgent_ButtonUpravljanjeTuristickimAtrakcijama(object sender, EventArgs e)
        {

        }
    }
}
