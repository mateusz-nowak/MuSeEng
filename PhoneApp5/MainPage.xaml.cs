using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PhoneApp5.Utilities;

namespace PhoneApp5
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        /**
         * Wyszukiwanie utworow Mp3
         */
        private void Szukaj(object sender, RoutedEventArgs e)
        {
            String searchFieldContent = searchField.Text;

            if (searchFieldContent.Length < 3)
            {
                MessageBox.Show("Wyszukiwany utwor musi miec conajmniej 2 znaki");

                return;
            }

            GlobalVariables.phrase = searchFieldContent;
            NavigationService.Navigate(new Uri("/Results.xaml", UriKind.Relative));
        }
    }
}