using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhoneApp5.Utilities;

namespace PhoneApp5.Views
{
    public partial class StatView : PhoneApplicationPage
    {
        public StatView()
        {
            InitializeComponent();
            populateStats();
        }

        private void populateStats()
        {
            mainGrid.Children.Clear();
            loader.Visibility = System.Windows.Visibility.Visible;

            string str = "http://museeng.my.phpcloud.com/?id=" + GlobalVariables.type.ToString();

            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    DisplayResults(JArray.Parse(e.Result));
                    loader.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            client.DownloadStringAsync(new Uri(str));
        }

        private void DisplayResults(JArray result)
        {
            int id = 0;
            foreach (string track in result.ToArray())
            {
                int TopAlignment = 50 * id;
                TextBlock tb = new TextBlock();
                tb.Text = track;
                tb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tb.Margin = new Thickness(0, TopAlignment, 0, 0);
                tb.FontSize = 25;
                tb.MouseLeftButtonDown += (sender, e) =>
                {
                    GlobalVariables.Phrase = (sender as TextBlock).Text;
                    NavigationService.Navigate(new Uri("/Views/ResultsView.xaml", UriKind.Relative));
                };

                mainGrid.Children.Add(tb);

                ++id;
            }
        }

        private void ChangeType(object sender, RoutedEventArgs e)
        {
            string type = (sender as Button).Content.ToString();

            switch (type)
            {
                case "Hip-Hop":
                    GlobalVariables.type = 0;
                    break;
                case "Dance":
                    GlobalVariables.type = 1;
                    break;
                case "Techno":
                    GlobalVariables.type = 2;
                    break;
            }

            populateStats();
        }
    }
}