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
    /// Interaction logic for SkeletonUpravljanje.xaml
    /// </summary>
    public partial class SkeletonUpravljanje : UserControl
    {
        public SkeletonUpravljanje()
        {
            InitializeComponent();

            List<TouristAttraction> atractions = new List<TouristAttraction>
            {
                new TouristAttraction(1, "Beograd", "Masarikova 12, Beograd"),
                new TouristAttraction(2, "Novi Sad", "Masarikova 12, Beograd"),
                new TouristAttraction(3, "Radsdasda", "Masarikova 12, Beograd"),
                new TouristAttraction(3, "Radsdasda", "Masarikova 12, Beograd"),
                new TouristAttraction(3, "Radsdasda", "Masarikova 12, Beograd"),
                new TouristAttraction(3, "Radsdasda", "Masarikova 12, Beograd"),
                new TouristAttraction(3, "Radsdasda", "Masarikova 12, Beograd"),
                new TouristAttraction(3, "Radsdasda", "Masarikova 12, Beograd")
            };

            TableDataGrid.ItemsSource = atractions;
        }
    }
}
