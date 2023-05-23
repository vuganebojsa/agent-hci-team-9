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
using TravelAgent.services;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for AddPlaceRestaurantPopup.xaml
    /// </summary>
    public partial class AddPlaceRestaurantPopup : Window
    {
        public PlaceRestaurant PlaceRestaurant { get; set; }
        public AddPlaceRestaurantPopup()
        {
            InitializeComponent();
        }

        public AddPlaceRestaurantPopup(PlaceRestaurant placeRestaurant)
        {
            this.PlaceRestaurant = placeRestaurant;
            if(this.PlaceRestaurant != null)
            {
                tbRegistration.Text = "Izmenite smestaj ili restoran";
                FillFields();
            }
            InitializeComponent();
        }

          
        private void FillFields()
        {
            //    ComboBox1.Items.RemoveAt(ComboBox1.Items.IndexOf(ComboBox1.SelectedItem));  

        }

 

        private void btnCancel_ButtonClicked(object sender, EventArgs e)
        {
            YesNoPopup yn = new YesNoPopup("Da li ste sigurni da zelite da otkazete? Ovom akcijom nista nece biti izmenjeno.");
            double width = Window.GetWindow(this).Width;
            double height = Window.GetWindow(this).Height;
            double left = Window.GetWindow(this).Left;
            double top = Window.GetWindow(this).Top;
            yn.Left = left + width / 2 - 100;
            yn.Top = top + height / 2 - 100;

            if (yn.ShowDialog() == true)
            {
                this.Close();
            }
        }

        private void btnSave_ButtonClicked(object sender, EventArgs e)
        {
            Vrsta type = (Vrsta)cbType.SelectedItem;
            String name = tbNaziv.Text.Trim();
            String mesto = tbMesto.Text.Trim();


            PlaceRestaurant pr = new PlaceRestaurant();
            pr.Naziv = name;
            pr.Adresa = mesto;
            pr.vrsta = type;
            
            // ovo je add
            if(this.PlaceRestaurant == null)
            {
                FileService.addPlaceRestaurant(pr);
                this.Close();

            }
            else
            {
                // ovo je edit

                List<PlaceRestaurant> placeRestaurants = FileService.getPlacesAndRestaurants();
                foreach(PlaceRestaurant p in placeRestaurants)
                {
                    if(p.Id == this.PlaceRestaurant.Id)
                    {
                        p.vrsta = type;
                        p.Naziv = name;
                        p.Adresa = mesto;
                        break;
                    }
                }
                // upisi ponovo sve u fajl sa izmenama
                FileService.writePlacesRestaurants(placeRestaurants);

            }


            // da bi mogli znati da li je sacuvano da se refreshuje tabela
            this.DialogResult = true;



        }
    }
}
