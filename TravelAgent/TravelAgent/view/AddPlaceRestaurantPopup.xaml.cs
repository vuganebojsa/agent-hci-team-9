﻿using System;
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
            InitializeComponent();
            if (this.PlaceRestaurant != null)
            {
                tbRegistration.Text = "Izmenite smestaj ili restoran";
                FillFields();
            }
            
        }

          
        private void FillFields()
        {
            //    ComboBox1.Items.RemoveAt(ComboBox1.Items.IndexOf(ComboBox1.SelectedItem));  
            cbType.Text = PlaceRestaurant.vrsta.ToString();
            tbNaziv.Text = PlaceRestaurant.Naziv;
            tbMesto.Text = PlaceRestaurant.Adresa;
        }

 

        private void btnCancel_ButtonClicked(object sender, EventArgs e)
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

        private void btnSave_ButtonClicked(object sender, EventArgs e)
        {
            if(cbType.Text.Trim() == "")
            {
                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";
                
                return;
            }
            Vrsta type = (Vrsta)Enum.Parse(typeof(Vrsta), cbType.Text);
            String name = tbNaziv.Text.Trim();
            String mesto = tbMesto.Text.Trim();

            if(name == "" || mesto == "")
            {

                errorControl.Visibility = Visibility.Visible;
                errorControl.ErrorHandler.Text = "Molimo Vas popunite sva polja.";
                return;
            }

            errorControl.Visibility = Visibility.Hidden;

            PlaceRestaurant pr = new PlaceRestaurant();
            pr.Naziv = name;
            pr.Adresa = mesto;
            pr.vrsta = type;
            pr.isDeleted = "0";
            // ovo je add
            if(this.PlaceRestaurant == null)
            {
                FileService.addPlaceRestaurant(pr);

                // da bi mogli znati da li je sacuvano da se refreshuje tabela
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup($"Uspesno ste dodali objekat {pr.Naziv}.");
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
                // ovo je edit

                List<PlaceRestaurant> placeRestaurants = FileService.getPlacesAndRestaurants();
                foreach(PlaceRestaurant p in placeRestaurants)
                {
                    if(p.Id == this.PlaceRestaurant.Id)
                    {
                        p.vrsta = type;
                        p.Naziv = name;
                        p.Adresa = mesto;
                        p.isDeleted = "0"; 
                        PlaceRestaurant = p;
                        break;
                    }
                }
                // upisi ponovo sve u fajl sa izmenama
                FileService.writePlacesRestaurants(placeRestaurants);
                
                
                double width = Window.GetWindow(this).Width;
                double height = Window.GetWindow(this).Height;
                double left = Window.GetWindow(this).Left;
                double top = Window.GetWindow(this).Top;
                OkPopup ok = new OkPopup("Uspesno ste izmenili objekat.");
                ok.Left = left + width / 2 - 100;
                ok.Top = top + height / 2 - 100;
                if (ok.ShowDialog() == true)
                {
                    // da bi mogli znati da li je sacuvano da se refreshuje tabela

                    this.DialogResult = true;
                }
                
            }





        }
    }
}
