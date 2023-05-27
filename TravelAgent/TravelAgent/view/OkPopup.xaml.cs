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
    /// Interaction logic for OkPopup.xaml
    /// </summary>
    public partial class OkPopup : Window
    {
        public OkPopup()
        {
            InitializeComponent();
            Loaded += Ok_Loaded;
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
        }
        public OkPopup(String text)
        {
            InitializeComponent();
            Loaded += Ok_Loaded;
            OkText = text;

        }
        private string okText;

        public string OkText
        {
            get { return okText; }
            set
            {
                okText = value;
                poruka.Text = okText;
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
                BivujaButton_ButtonClicked(sender, e);
            }
        }

        private void BivujaButton_ButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
