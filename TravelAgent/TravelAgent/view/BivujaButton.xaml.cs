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

namespace TravelAgent
{
    /// <summary>
    /// Interaction logic for BivujaButton.xaml
    /// </summary>
    public partial class BivujaButton : UserControl
    {
        public BivujaButton()
        {
            InitializeComponent();
        }

        private string buttonContent;

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                tbBivuja.Text = buttonContent;

            }
        }
    }
}
