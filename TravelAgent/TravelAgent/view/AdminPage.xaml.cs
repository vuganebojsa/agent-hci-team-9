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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
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
    }
}
