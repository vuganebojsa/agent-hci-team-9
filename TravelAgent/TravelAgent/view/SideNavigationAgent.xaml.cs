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
    /// Interaction logic for SideNavigationAgent.xaml
    /// </summary>
    public partial class SideNavigationAgent : UserControl
    {
        public SideNavigationAgent()
        {
            InitializeComponent();
            question.btnSideNav.HorizontalContentAlignment = HorizontalAlignment.Center;

        }
    }
}
