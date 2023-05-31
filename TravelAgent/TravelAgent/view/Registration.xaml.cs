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
using TravelAgent.services;
using System.Text.RegularExpressions;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {

            InitializeComponent();
            Loaded += Registration_Loaded;
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
            ime.txtInput.Focus();

        }
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                
            }
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

        private void Registration_Loaded(object sender, RoutedEventArgs e)
        {
            // Register KeyDown event for the window
            KeyDown += Registration_KeyDown;
        }

        private void Registration_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.Enter)
            {
                // Manually trigger the button click event
                Prihvati_ButtonClicked(sender, e);
            }

            if (e.Key == Key.Escape)
            {
                // Manually trigger the button click event
                Pocetna_ButtonClicked(sender, e);
            }
        }

        private void Prihvati_ButtonClicked(object sender, EventArgs e)
        {
            String name = ime.Text;
            String surname = prezime.Text;
            String email = tbEmail.Text;
            String password = lozinka.Text;
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (name.Trim() == "")
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                ime.BorderBrush = Brushes.Red;
                ime.BorderThickness = new Thickness(1);
            }else if(surname.Trim() == "")
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                prezime.BorderBrush = Brushes.Red;
                prezime.BorderThickness = new Thickness(1);
            }
            else if (email.Trim() == "")
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                tbEmail.BorderBrush = Brushes.Red;
                tbEmail.BorderThickness = new Thickness(1);
            }else if(password.Trim() == "")
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                lozinka.BorderBrush = Brushes.Red;
                lozinka.BorderThickness = new Thickness(1);
            }
            else
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);

            }


            if (name.Trim() == "" || surname.Trim() == "" || email.Trim() == "" || password.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas popunite sva polja. Da bismo Vas uspesno registrovali morate uneti sve podatke.";
            }

            else if (!Regex.IsMatch(email, pattern))
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                tbEmail.BorderBrush = Brushes.Red;
                tbEmail.BorderThickness = new Thickness(1);
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas unesite email adresu u ispravnom formatu. Primer: perica@gmail.com";
            }else if(password.Length < 6)
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                lozinka.BorderBrush = Brushes.Red;
                lozinka.BorderThickness = new Thickness(1);
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas unesite lozinku od minimalno 6 karaktera. Primer: Perica123";
            }
            else
            {
                lozinka.BorderThickness = new Thickness(0);
                tbEmail.BorderThickness = new Thickness(0);
                prezime.BorderThickness = new Thickness(0);
                ime.BorderThickness = new Thickness(0);
                FileService.registerUser(name, surname, email, password);

                double left = Left;
                double top = Top;

                OkPopup ok = new OkPopup("Uspesno ste se registrovali.");
                ok.Left = left + this.Width/2 - 100;
                ok.Top = top + this.Height/2 - 100;
                if(ok.ShowDialog() == true)
                {
                    Login login = new Login();

                   
                    login.Left = left;
                    login.Top = top;

                    Application.Current.MainWindow = login;
                    Application.Current.MainWindow.Show();
                    this.Close();
                }
               

            }

            
        }


        private void btnHelp_ButtonClicked(object sender, EventArgs e)
        {
            displayHtml display = new displayHtml("C:\\Users\\Bogdan\\HCIprojekat\\agent-hci-team-9\\TravelAgent\\TravelAgent\\bin\\Debug\\net6.0-windows\\RegistrationWindow.htm");
            display.ShowDialog();
        }
    }
}
