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
using System.Windows.Shapes;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Login_ButtonClicked(object sender, EventArgs e)
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
        private void Register_ButtonClicked(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            double left = Left;
            double top = Top;

            registration.Left = left;
            registration.Top = top;
            
            Application.Current.MainWindow = registration;
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void Putovanja_ButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainWindow = new Registration();

            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           /* YesNoPopup yn = new YesNoPopup("Izlaz?\nKlikom na dugme Da izlazite iz aplikacije, dok na dugme Ne ostajete u njoj.");
            yn.Left = Left + this.Width / 2 - 100;
            yn.Top = Top + this.Height / 2 - 100;
            if (yn.ShowDialog() != true)
            {
                e.Cancel = true;
            }*/

        }
    }
}
