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

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for ClearableTextBox.xaml
    /// </summary>
    public partial class ClearableTextBox : UserControl
    {
        public ClearableTextBox()
        {
            InitializeComponent();
        }
        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set { 
                placeholder = value;
                tbPlaceholder.Text = placeholder;
            }
        }
        public string Text
        {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();

        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtInput.Text.Contains(';'))
            {
                Text = Text.Replace(";","");

            }
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                tbPlaceholder.Visibility = Visibility.Visible;

            }
            else
            {
                tbPlaceholder.Visibility = Visibility.Hidden;
            }
            if(txtInput.Text.Length > 30)
            {
                Text = Text.Substring(0, 30);
            }
        }
    }
}
