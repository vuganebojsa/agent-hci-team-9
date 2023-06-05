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
            PreviewKeyDown += YourWindow_PreviewKeyDown;
            Loaded += Ok_Loaded;

            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
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

        private void YourUserControl_ButtonAddClicked(object sender, EventArgs e)
        {
            
            MessageBox.Show("Button A clicked");
        }
        private void YourWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.A)
                {
                    // Get the YourUserControl instance from the ContentControl
                    AttractionsManagement yourUserControl = MainContent.Content as AttractionsManagement;
                    yourUserControl?.btnAdd_ButtonClicked(sender, e);
                    PlaceRestaurantManagement placeCntrl = MainContent.Content as PlaceRestaurantManagement;
                    placeCntrl?.btnAdd_ButtonClicked(sender, e);
                    TripsManagment tripCntrl = MainContent.Content as TripsManagment;
                    tripCntrl?.Dodajte_ButtonClicked(sender, e);
                }
                else if(e.Key == Key.D)
                {
                    AttractionsManagement yourUserControl = MainContent.Content as AttractionsManagement;
                    yourUserControl?.btnDelete_ButtonClicked(sender, e);
                    PlaceRestaurantManagement placeCntrl = MainContent.Content as PlaceRestaurantManagement;
                    placeCntrl?.btnDelete_ButtonClicked(sender, e);
                    TripsManagment tripCntrl = MainContent.Content as TripsManagment;
                    tripCntrl?.Obrisite_ButtonClicked(sender, e);
                }
                else if (e.Key == Key.E)
                {
                    AttractionsManagement yourUserControl = MainContent.Content as AttractionsManagement;
                    yourUserControl?.btnEdit_ButtonClicked(sender, e);
                    PlaceRestaurantManagement placeCntrl = MainContent.Content as PlaceRestaurantManagement;
                    placeCntrl?.btnEdit_ButtonClicked(sender, e);
                    TripsManagment tripCntrl = MainContent.Content as TripsManagment;
                    tripCntrl?.Izmenite_ButtonClicked(sender, e);
                }
                
            }
        }
        private void Ok_Loaded(object sender, RoutedEventArgs e)
        {
            // Register KeyDown event for the window
            KeyDown += Help_KeyDown;
        }

        private void Help_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.F1)
            {
                // Manually trigger the button click event
                SideNavigationAgent_ButtonHelp(sender, e);
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
            SelectedText = "Pregled prodatih putovanja";
            UserControl control = new SoldTripsOverview();
            MainContent.Content = null;
            MainContent.Content = control;
        }

        private void SideNavigationAgent_ButtonUpravljanjePutovanjimaClicked(object sender, EventArgs e)
        {
            /*
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
            */
            SelectedText = "Upravljanje putovanjima";
            UserControl control = new TripsManagment();
            MainContent.Content = null;
            MainContent.Content = control;

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
            UserControl control = new AttractionsManagement();
            MainContent.Content = null;
            topNav.HeaderText = "Upravljanje atrakcijama";
            // Set the newly created user control as the content of the container
            MainContent.Content = control;
        }

        private void SideNavigationAgent_ButtonHelp(object sender, EventArgs e)
        {   if (topNav.HeaderText.ToLower()=="upravljanje atrakcijama")
            {
                displayHtml display = new displayHtml("/html/RegisterWindow.htm");
                display.ShowDialog();

            }
        else if(topNav.HeaderText.ToLower()=="upravljanje smestajem i restoranima")
            {
                displayHtml display = new displayHtml("/html/PregledSmestajaRestorana.htm");
                display.ShowDialog();

            }
        else if(topNav.HeaderText.ToLower()=="upravljanje putovanjima")
            {
                displayHtml display = new displayHtml("/html/UpravljanjePutovanjimaWindow.htm");
                display.ShowDialog();

            }
        else if(topNav.HeaderText.ToLower()=="pregled prodatih putovanja")
            {
                displayHtml display = new displayHtml("/html/PregledProdatihPutovanja.htm");
                display.ShowDialog();

            }
           
        }

        private void SideNavigationAgent_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
