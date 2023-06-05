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
    /// Interaction logic for YesNoPopup.xaml
    /// </summary>
    public partial class YesNoPopup : Window
    {
        public YesNoPopup()
        {
            InitializeComponent();
            Loaded += YN_Loaded;
            Uri iconUri = new Uri("../../../icons/bivuja.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.Title = "BIVUJA";
        }

        public YesNoPopup(String text)
        {

            InitializeComponent();
            Loaded += YN_Loaded;
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

        public event EventHandler ButtonYesClicked;
        public event EventHandler ButtonNoClicked;

        private void btnNo_ButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = false;
            ButtonNoClicked?.Invoke(this, EventArgs.Empty);

        }

        private void btnYes_ButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = true;
            ButtonYesClicked?.Invoke(this, EventArgs.Empty);

        }

        private void YN_Loaded(object sender, RoutedEventArgs e)
        {
            // Register KeyDown event for the window
            KeyDown += YN_KeyDown;
        }

        private void YN_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.Key == Key.Enter)
            {
                // Manually trigger the button click event
                btnYes_ButtonClicked(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                // Manually trigger the button click event
                btnNo_ButtonClicked(sender, e);
            }
        }
    }
}
