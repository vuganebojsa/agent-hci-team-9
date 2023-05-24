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
    /// Interaction logic for UnregisteredTrips.xaml
    /// </summary>
    public partial class UnregisteredTrips : Window
    {
        public UnregisteredTrips()
        {
            InitializeComponent();
            UserControl control = new AllTripsOverview();
            MainContent.Content = null;

            // Set the newly created user control as the content of the container
            MainContent.Content = control;
        }
    }
}
