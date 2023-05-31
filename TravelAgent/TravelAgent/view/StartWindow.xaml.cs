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
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
        }

        private void Login_ButtonClicked(object sender, EventArgs e)
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
        private void Register_ButtonClicked(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            double left = Left;
            double top = Top;

            registration.Left = left;
            registration.Top = top;
            
            Application.Current.MainWindow = registration;
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void Putovanja_ButtonClicked(object sender, EventArgs e)
        {

            UnregisteredTrips registration = new UnregisteredTrips();
            double left = Left;
            double top = Top;

            registration.Left = left;
            registration.Top = top;

            Application.Current.MainWindow = registration;
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void btnHelp_ButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
