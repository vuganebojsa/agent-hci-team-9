using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TravelAgent.Model;
using TravelAgent.services;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for AddTripPopup.xaml
    /// </summary>
    /// 
    
    public partial class AddTripPopup : Window
    {
        Point startPoint = new Point();

        public ObservableCollection<TouristAttraction> atrakcije
        {
            get;
            set;
        }
        public ObservableCollection<TouristAttraction> atrakcije2
        {
            get;
            set;
        }

        public ObservableCollection<PlaceRestaurant> smestaji
        {
            get;
            set;
        }
        public ObservableCollection<PlaceRestaurant> smestaji2
        {
            get;
            set;
        }


        public AddTripPopup()
        {
            InitializeComponent();
            this.DataContext = this;
            
            List<TouristAttraction> atractions = FileService.getAtractions();
            atrakcije = new ObservableCollection<TouristAttraction>(atractions);
            atrakcije2 = new ObservableCollection<TouristAttraction>();

            List<PlaceRestaurant> places = FileService.getPlacesAndRestaurants();
            smestaji = new ObservableCollection<PlaceRestaurant>(places);
            smestaji2 = new ObservableCollection<PlaceRestaurant>();

        }

        private void Sacuvajte_ButtonClicked(object sender, EventArgs e)
        {

        }
        private void Otkazite_ButtonClicked(object sender, EventArgs e)
        {

        }
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }
        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                    // Check if listViewItem is null or not the direct visual child of the ListView
                    if (listViewItem == null || !listView.Items.Contains(listViewItem.Content))
                    {
                        // If the click was not on a valid ListViewItem, do nothing
                        return;
                    }

                    TouristAttraction attraction = (TouristAttraction)listViewItem.Content;

                    DataObject dragData = new DataObject("myFormat", attraction);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            
        }

        private void ListView_MouseMove2(object sender, MouseEventArgs e)
        {

            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Check if listViewItem is null or not the direct visual child of the ListView
                if (listViewItem == null || !listView.Items.Contains(listViewItem.Content))
                {
                    // If the click was not on a valid ListViewItem, do nothing
                    return;
                }

                PlaceRestaurant places = (PlaceRestaurant)listViewItem.Content;

                DataObject dragData = new DataObject("myFormat", places);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }

        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                TouristAttraction atraction = e.Data.GetData("myFormat") as TouristAttraction;
                atrakcije.Remove(atraction);
                atrakcije2.Add(atraction);
            }
        }

        private void ListView_Drop_Back(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                TouristAttraction atraction = e.Data.GetData("myFormat") as TouristAttraction;
                atrakcije2.Remove(atraction);
                atrakcije.Add(atraction);
            }
        }

        private void ListView_Drop2(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                PlaceRestaurant place = e.Data.GetData("myFormat") as PlaceRestaurant;
                smestaji.Remove(place);
                smestaji2.Add(place);
            }
        }
        private void ListView_Drop2_Back(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                PlaceRestaurant place = e.Data.GetData("myFormat") as PlaceRestaurant;
                smestaji2.Remove(place);
                smestaji.Add(place);
            }
        }


    }
}
