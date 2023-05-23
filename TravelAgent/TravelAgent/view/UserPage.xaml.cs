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
            Login login = new Login();

            double left = Left;
            double top = Top;

            login.Left = left;
            login.Top = top;

            Application.Current.MainWindow = login;
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void SideNavigation_ButtonPregledClicked(object sender, EventArgs e)
        {
            UserControl control = new SkeletonUpravljanje();
            MainContent.Content = null;

            // Set the newly created user control as the content of the container
            MainContent.Content = control;
        }

        private void SideNavigation_ButtonRezervisaniClicked(object sender, EventArgs e)
        {

        }
    }
  

}
