using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Xml.Linq;
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
        public Trip Trip { get; set; }

        public AddTripPopup()
        {

            InitializeComponent();
            Loaded += Ok_Loaded;
            Loaded += Esc_Loaded;

            this.DataContext = this;
            lbAtrakcije.IsEnabled = true;
            lbSmestaji.IsEnabled = true;
            lbPrevuciSmestajeRestorane.IsEnabled = true;
            lbPrevuciAtrakcije.IsEnabled = true;

            List<TouristAttraction> atractions = FileService.getAtractions();
            var newAtractions = new List<TouristAttraction>();
            foreach (TouristAttraction pl in atractions)
            {
                if (pl.JeObrisan == "0") newAtractions.Add(pl);
            }

            atrakcije = new ObservableCollection<TouristAttraction>(newAtractions);
            atrakcije2 = new ObservableCollection<TouristAttraction>();

            List<PlaceRestaurant> places = FileService.getPlacesAndRestaurants();
            var newPlaces = new List<PlaceRestaurant>();
            foreach (PlaceRestaurant pl in places)
            {
                if (pl.JeObrisan == "0") newPlaces.Add(pl);
            }

            smestaji = new ObservableCollection<PlaceRestaurant>(newPlaces);
            smestaji2 = new ObservableCollection<PlaceRestaurant>();
        }
        public AddTripPopup(Trip trip)
        {
            this.Trip = trip;
            InitializeComponent();
            Loaded += Ok_Loaded;
            Loaded += Esc_Loaded;
            smestaji2 = new ObservableCollection<PlaceRestaurant>();
            atrakcije2 = new ObservableCollection<TouristAttraction>();


            List<TouristAttraction> atractions = FileService.getAtractions();
            var newAtr = new List<TouristAttraction>();
            foreach (TouristAttraction at in atractions)
            {

                if (at.JeObrisan == "0") newAtr.Add(at);
            }
            var atrrr = new List<TouristAttraction>();
            foreach (TouristAttraction pl in newAtr)
            {
                int error = 0;
                foreach (IBivuja ibj in Trip.Objekti)
                {
                    if (ibj.GetType() == typeof(TouristAttraction))
                    {
                        if (ibj.Id == pl.Id)
                        {

                            error = 1;
                        }
                    }
                }
                if (error == 0)
                {
                    atrrr.Add(pl);
                }
            }

            atrakcije = new ObservableCollection<TouristAttraction>(atrrr);





            List<PlaceRestaurant> places = FileService.getPlacesAndRestaurants();
            var newPlaces = new List<PlaceRestaurant>();
            foreach (PlaceRestaurant pl in places)
            {
                
                if (pl.JeObrisan == "0") newPlaces.Add(pl);
            }
            var plaaace = new List<PlaceRestaurant>();
            foreach (PlaceRestaurant pl in newPlaces)
            {
                int error = 0;
                foreach (IBivuja ibj in Trip.Objekti)
                {
                    if (ibj.GetType() == typeof(PlaceRestaurant))
                    {
                        if (ibj.Id == pl.Id)
                        {

                            error = 1;
                        }
                    }
                }
                if (error == 0)
                {
                    plaaace.Add(pl);
                }
            }

            smestaji = new ObservableCollection<PlaceRestaurant>(plaaace);
            if (this.Trip != null)
            {
                
                tbRegistration.Text = "Izmenite putovanje";
                FillFields();
            }
            this.DataContext = this;
            
            
            

        }
        private void FillFields()
        {
            tbNaziv.Text = Trip.Naziv;
            tbCena.Text =  Trip.Cena.ToString();
            tbDatumPocetka.Text = Trip.DatumPocetka.ToString("dd/MM/yyyy");
            tbDatumKraja.Text = Trip.DatumKraja.ToString("dd/MM/yyyy");
            foreach (IBivuja ibj in Trip.Objekti)
            {
                if (ibj.GetType() == typeof(PlaceRestaurant))
                {
                    smestaji2.Add((PlaceRestaurant)ibj);
                }
                else
                {
                    atrakcije2.Add((TouristAttraction)ibj);

                }

            }
           
        }

        private void Sacuvajte_ButtonClicked(object sender, EventArgs e)
        {
            

            String name = tbNaziv.Text;
            String cena = tbCena.Text;
            String datumPocetka = tbDatumPocetka.Text.Trim();
            String datumKraja = tbDatumKraja.Text.Trim();
            if (name.Trim() == "")
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbNaziv.BorderBrush = Brushes.Red;
                tbNaziv.BorderThickness = new Thickness(1);
            }
            else if (cena.Trim() == "")
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbCena.BorderBrush = Brushes.Red;
                tbCena.BorderThickness = new Thickness(1);
            }
            else if (datumPocetka.Trim() == "")
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbDatumPocetka.BorderBrush = Brushes.Red;
                tbDatumPocetka.BorderThickness = new Thickness(1);
            }
            else if (datumKraja.Trim() == "")
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderBrush = Brushes.Red;
                tbDatumKraja.BorderThickness = new Thickness(1);
            }
            
            else
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);

            }

           
            
            string format = "dd/MM/yyyy";
            DateTime dateTimePocetak;
            DateTime dateTimeKraj;
            if (tbNaziv.Text.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";

                return;
            }
            if (tbCena.Text.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";

                return;
            }

            if (datumPocetka == "" || datumKraja == "")
            {

                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";
                return;
            }
            if (DateTime.TryParseExact(datumPocetka, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimePocetak))
            {
                
            }
            else
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbDatumPocetka.BorderBrush = Brushes.Red;
                tbDatumPocetka.BorderThickness = new Thickness(1);
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas unesite datum formata:dd/MM/yyyy (dd-dan,MM-mesec, yyyy-godina)";
                return;
            }

            if (DateTime.TryParseExact(datumKraja, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeKraj))
            {

            }
            else
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderBrush = Brushes.Red;
                tbDatumKraja.BorderThickness = new Thickness(1);
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas unesite datum formata:dd/MM/yyyy (dd-dan,MM-mesec, yyyy-godina)";
                return;
            }
            if (dateTimePocetak > dateTimeKraj)
            {
                errorControl.Visibility = Visibility.Visible;
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderBrush = Brushes.Red;
                tbDatumKraja.BorderThickness = new Thickness(1);
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Datum pocetka mora biti pre datuma kraja, molimo Vas unesite drugacije datume";
                return;
            }


            errorControl.Visibility = Visibility.Hidden;
            Trip tr = new Trip();
            
            tr.Naziv = tbNaziv.Text;
            long number;
            if (long.TryParse(tbCena.Text, out number))
            {
               
            }
            else
            {
                tbDatumPocetka.BorderThickness = new Thickness(0);
                tbDatumKraja.BorderThickness = new Thickness(0);
                tbCena.BorderThickness = new Thickness(0);
                tbNaziv.BorderThickness = new Thickness(0);
                tbCena.BorderBrush = Brushes.Red;
                tbCena.BorderThickness = new Thickness(1);
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas unesite cenu u brojevima";
                return;
            }
            tr.Cena = number;
            tr.DatumPocetka = dateTimePocetak;
            tr.DatumKraja = dateTimeKraj;
            tr.JeObrisan = "0";
            List<IBivuja> bivujas = new List<IBivuja>();
            foreach (PlaceRestaurant place in smestaji2)
            {
                if (place != null)
                {
                    bivujas.Add(FileService.getRestaurantsById(place.Id));
                }
            }
            foreach (TouristAttraction atr in atrakcije2)
            {
                if (atr != null)
                {
                    bivujas.Add(FileService.getAtractionsById(atr.Id));
                }
            }
            if (bivujas.Count == 0)
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas odaberite neku od atrakcija ili smestaja/restorana";
                return;
            }
            
            tr.Objekti = bivujas;

            // ovo je add
            if (this.Trip == null)
            {
                
                FileService.addTrips(tr);
                 
                // da bi mogli znati da li je sacuvano da se refreshuje tabela
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup($"Uspesno ste dodali putovanje {tr.Naziv}.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    // da bi mogli znati da li je sacuvano da se refreshuje tabela

                    this.DialogResult = true;
                }

            }
            else
            {
                List<Trip> trips = FileService.getAllTrips();
                
                foreach (Trip trip in trips)
                {  
                    if (trip.Id == this.Trip.Id)
                    {
                       
                        trip.Naziv = tbNaziv.Text;
                        trip.Cena = number;
                        trip.DatumPocetka = dateTimePocetak;
                        trip.DatumKraja = dateTimeKraj;
                        trip.JeObrisan = "0";
                        trip.Objekti = bivujas;

                        Trip = trip;
                        break;
                    }
                }
                
                FileService.writeTrips(trips);


                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup("Uspesno ste izmenili putovanje.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    // da bi mogli znati da li je sacuvano da se refreshuje tabela

                    this.DialogResult = true;
                }
            }
        }
        private void Otkazite_ButtonClicked(object sender, EventArgs e)
        {
            YesNoPopup yn = new YesNoPopup("Da li ste sigurni da zelite da otkazete? Ovom akcijom nista nece biti izmenjeno.");
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            yn.Left = left + width / 2 - 100;
            yn.Top = top + height / 2 - 200;

            if (yn.ShowDialog() == true)
            {
                this.DialogResult = false;
            }

        }

        private void Ok_Loaded(object sender, RoutedEventArgs e)
        {
            // Register KeyDown event for the window
            KeyDown += Ok_KeyDown;
        }

        private void Ok_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.Enter)
            {
                // Manually trigger the button click event
                Sacuvajte_ButtonClicked(sender, e);
            }
        }

        private void Esc_Loaded(object sender, RoutedEventArgs e)
        {
            // Register KeyDown event for the window
            KeyDown +=  Esc_KeyDown;
        }

        private void Esc_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.Escape)
            {
                // Manually trigger the button click event
                Otkazite_ButtonClicked(sender, e);
            }
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
        private void ListView_DragEnter_atrakcije(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
            lbAtrakcije.IsEnabled = true;
            lbSmestaji.IsEnabled = false;
            lbPrevuciSmestajeRestorane.IsEnabled = false;
            lbPrevuciAtrakcije.IsEnabled = true;
        }

        private void ListView_DragEnter_smestaji(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
            lbAtrakcije.IsEnabled = false;
            lbSmestaji.IsEnabled = true;
            lbPrevuciSmestajeRestorane.IsEnabled = true;
            lbPrevuciAtrakcije.IsEnabled = false;
        }

        

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                int eror = 0;
                TouristAttraction atraction = e.Data.GetData("myFormat") as TouristAttraction;
                atrakcije.Remove(atraction);
                foreach(TouristAttraction tr in  atrakcije2)
                {
                    if (tr.Id == atraction.Id)
                    {
                        eror = 1;
                    }
                }
                if (eror == 0)
                {
                       atrakcije2.Add(atraction);
                }
                
                
            }
            lbAtrakcije.IsEnabled = true;
            lbSmestaji.IsEnabled = true;
            lbPrevuciSmestajeRestorane.IsEnabled = true;
            lbPrevuciAtrakcije.IsEnabled = true;
        }

        private void ListView_Drop_Back(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                TouristAttraction atraction = e.Data.GetData("myFormat") as TouristAttraction;
                int eror = 0;
                atrakcije2.Remove(atraction);
                
                foreach (TouristAttraction tr in atrakcije)
                {
                    if (tr.Id == atraction.Id)
                    {
                        eror = 1;
                    }
                }
                if (eror == 0)
                {
                    atrakcije.Add(atraction);
                }
            }
            lbAtrakcije.IsEnabled = true;
            lbSmestaji.IsEnabled = true;
            lbPrevuciSmestajeRestorane.IsEnabled = true;
            lbPrevuciAtrakcije.IsEnabled = true;
        }

        private void ListView_Drop2(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                PlaceRestaurant place = e.Data.GetData("myFormat") as PlaceRestaurant;
                int eror = 0;
                smestaji.Remove(place);

                foreach (PlaceRestaurant tr in smestaji2)
                {
                    if (tr.Id == place.Id)
                    {
                        eror = 1;
                    }
                }
                if (eror == 0)
                {
                    smestaji2.Add(place);
                }
            }
            lbAtrakcije.IsEnabled = true;
            lbSmestaji.IsEnabled = true;
            lbPrevuciSmestajeRestorane.IsEnabled = true;
            lbPrevuciAtrakcije.IsEnabled = true;
        }
        private void ListView_Drop2_Back(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                PlaceRestaurant place = e.Data.GetData("myFormat") as PlaceRestaurant;
                int eror = 0;
                smestaji2.Remove(place);

                foreach (PlaceRestaurant tr in smestaji)
                {
                    if (tr.Id == place.Id)
                    {
                        eror = 1;
                    }
                }
                if (eror == 0)
                {
                    smestaji.Add(place);
                }
            }
            lbAtrakcije.IsEnabled = true;
            lbSmestaji.IsEnabled = true;
            lbPrevuciSmestajeRestorane.IsEnabled = true;
            lbPrevuciAtrakcije.IsEnabled = true;
        }

        private void tbDatumPocetka_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
