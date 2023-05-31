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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            InitializeComponent();
            Loaded += Login_Loaded;
            
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
            tbEmail.txtInput.Focus();
        }
        private void Registracija_ButtonClicked(object sender, EventArgs e)
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

        private void Pocetna_ButtonClicked(object sender, EventArgs e)
        {
            StartWindow sw = new StartWindow();

            double left = Left;
            double top = Top;

            sw.Left = left;
            sw.Top = top;

            Application.Current.MainWindow = sw;
            Application.Current.MainWindow.Show();
            this.Close();
        }
        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            // Register KeyDown event for the window
            KeyDown += Login_KeyDown;
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.Enter)
            {
                // Manually trigger the button click event
                BivujaButton_ButtonClicked(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                // Manually trigger the button click event
                Pocetna_ButtonClicked(sender, e);
            }
        }

        private void BivujaButton_ButtonClicked(object sender, EventArgs e)
        {

            String email = tbEmail.Text;
            String password = lozinka.Text;
            // errorControl.ErrorText = "Uneti email ili lozinka nisu ispravni. Molimo Vas pokusajte ponovo.";
            User user = FileService.getUserByEmailAndPassword(email, password);
            if(email.Trim() == "")
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                tbEmail.BorderBrush = Brushes.Red;
                tbEmail.BorderThickness = new Thickness(1);

            }
            else if(password.Trim() == "")
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                lozinka.BorderBrush = Brushes.Red;
                lozinka.BorderThickness = new Thickness(1);
            }
            else
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
            }
            if (email.Trim() == "" || password.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas popunite oba polja kako bi mogli da se prijavite.";
            }
            else if(user == null)
            {
                errorControl.Visibility = Visibility.Visible;

                errorControl.ErrorText = "Uneti email ili lozinka nisu ispravni. Molimo Vas pokusajte ponovo.";
            }
            else
            {
                
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Success";

                double left = Left;
                double top = Top;
                
                
                CurrentlyloggedInUser.user = user;
                

                if (user.role == Role.AGENT)
                {
                    AdminPage sw = new AdminPage();

                    sw.Left = left;
                    sw.Top = top;

                    Application.Current.MainWindow = sw;
                    Application.Current.MainWindow.Show();
                }
                else
                {
                    UserPage sw = new UserPage();

                    sw.Left = left;
                    sw.Top = top;

                    Application.Current.MainWindow = sw;
                    Application.Current.MainWindow.Show();
                }
               
                this.Close();

            }
            

        }

        private void btnHelp_ButtonClicked(object sender, EventArgs e)
        {
            displayHtml display = new displayHtml("C:\\Users\\Bogdan\\HCIprojekat\\agent-hci-team-9\\TravelAgent\\TravelAgent\\bin\\Debug\\net6.0-windows\\LoginWindow.htm");
            display.ShowDialog();
        }
    }
}
