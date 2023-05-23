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
        private void Prihvati_ButtonClicked(object sender, EventArgs e)
        {
            String name = ime.Text;
            String surname = prezime.Text;
            String email = tbEmail.Text;
            String password = lozinka.Text;
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            

            if (name.Trim() == "" || surname.Trim() == "" || email.Trim() == "" || password.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas popunite sva polja. Da bismo Vas uspesno registrovali morate uneti sve podatke.";
            }

            else if (!Regex.IsMatch(email, pattern))
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas unesite email adresu u ispravnom formatu. Primer: perica@gmail.com";
            }else if(password.Length < 6)
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas unesite lozinku od minimalno 6 karaktera. Primer: Perica123";
            }
            else
            {
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

        
    }
}
