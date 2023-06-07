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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage()
        {

            InitializeComponent();
            Loaded += Ok_Loaded;
            SelectedText = "Pregled svih putovanja";
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";

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
                SideNavigation_ButtonHelpClicked(sender, e);
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

        private void SideNavigation_ButtonPregledClicked(object sender, EventArgs e)
        {

            SelectedText = "Pregled svih putovanja";
            UserControl control = new AllTripsOverview();
            MainContent.Content = null;

            // Set the newly created user control as the content of the container
            MainContent.Content = control;
        }

        private void SideNavigation_ButtonRezervisaniClicked(object sender, EventArgs e)
        {
            SelectedText = "Pregled rezervisanih putovanja";
            UserControl control = new BookedTripsOverview();

            MainContent.Content = null;
            MainContent.Content = control;
        }

        private void SideNavigation_ButtonHelpClicked(object sender, EventArgs e)
        {
            if (SelectedText.ToLower() == "pregled rezervisanih putovanja")
            {
                displayHtml display = new displayHtml("/../../../html/PregledRezervisanihPutovanjaWindow.htm");
                display.ShowDialog();

            }
            else if (SelectedText.ToLower() == "pregled svih putovanja")
            {
                displayHtml display = new displayHtml("/../../../html/PregledSvihPutovanjaWindow.htm");
                display.ShowDialog();

            }
        }
    }
  

}
