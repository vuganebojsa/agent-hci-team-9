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

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for TopNavigation.xaml
    /// </summary>
    public partial class TopNavigation : UserControl
    {
        public TopNavigation()
        {
            InitializeComponent();
        }
        public event EventHandler ButtonClicked;

        private string headerText;

        public string HeaderText
        {
            get { return headerText; }
            set
            {
                headerText = value;
                tbHeaderText.Text = headerText;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentlyloggedInUser.user = null;
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
