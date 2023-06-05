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

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for ErrorControl.xaml
    /// </summary>
    public partial class ErrorControl : UserControl
    {
        public ErrorControl()
        {
            InitializeComponent();
        }

        private string errorText;

        public string ErrorText
        {
            get { return errorText; }
            set
            {
                errorText = value;
                ErrorHandler.Text = errorText;
            }
        }
    }
}
