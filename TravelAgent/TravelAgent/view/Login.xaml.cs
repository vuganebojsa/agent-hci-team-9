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

            private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow = new Registration();

            Application.Current.MainWindow.Show();
            this.Close();


        }
    }
}
