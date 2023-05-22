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

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            
            InitializeComponent();
        }
        private void Registracija_ButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainWindow = new Registration();

            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void Pocetna_ButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainWindow = new StartWindow();

            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void BivujaButton_ButtonClicked(object sender, EventArgs e)
        {

            String email = tbEmail.Text;
            String password = lozinka.Text;
            // errorControl.ErrorText = "Uneti email ili lozinka nisu ispravni. Molimo Vas pokusajte ponovo.";

            if (email.Trim() == "" || password.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Molimo Vas popunite oba polja kako bi mogli da se prijavite.";
            }
            else if(FileService.getUserByEmailAndPassword(email, password) == null)
            {
                errorControl.Visibility = Visibility.Visible;

                errorControl.ErrorText = "Uneti email ili lozinka nisu ispravni. Molimo Vas pokusajte ponovo.";
            }
            else
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorText = "Success";
                // do rest logic

            }
            

        }


    }
}
