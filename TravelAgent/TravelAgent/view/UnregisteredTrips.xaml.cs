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
            PreviewKeyDown += YourWindow_PreviewKeyDown;
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
            UserControl control = new AllTripsOverview();
            MainContent.Content = null;

            // Set the newly created user control as the content of the container
            MainContent.Content = control;
        }

        private void YourWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
         
            if (e.Key == Key.Escape)
            {
                    // Get the YourUserControl instance from the ContentControl
                AllTripsOverview yourUserControl = MainContent.Content as AllTripsOverview;
                yourUserControl?.btnBack_ButtonClicked(sender, e);
                SelectedTrip selectedTripCntrl = MainContent.Content as SelectedTrip;
                selectedTripCntrl?.btnNazad_ButtonClicked(sender, e);

            }
                
            
        }
    }
}
