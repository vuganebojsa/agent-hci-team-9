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

            UserControl control = new SoldTripsOverview();
            MainContent.Content = null;
            MainContent.Content = control;
        }

        private void SideNavigationAgent_ButtonUpravljanjePutovanjimaClicked(object sender, EventArgs e)
        {
            UserControl control = new TripsManagment();
            MainContent.Content = null;
            MainContent.Content = control;
        }

        private void SideNavigationAgent_ButtonUpravljanjeSmestajemIRestoranima(object sender, EventArgs e)
        {

        }

        private void SideNavigationAgent_ButtonUpravljanjeTuristickimAtrakcijama(object sender, EventArgs e)
        {

        }
    }
}
