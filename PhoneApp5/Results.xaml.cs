﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp5.Utilities;
using PhoneApp5.Entities;
using Newtonsoft.Json;
using System.Windows.Shapes;

namespace PhoneApp5
{
    public partial class Results : PhoneApplicationPage
    {
        private int page = 1;
        private Result result;

        public Results()
        {
            InitializeComponent();

            Search(GlobalVariables.phrase, page);
        }

        private void Search(string phrase, int page)
        {
            mainGrid.Children.Clear();
            loader.Visibility = System.Windows.Visibility.Visible;

            string str = "http://pastebin.com/raw.php?i=uEHPdxcm";

            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                result = JsonConvert.DeserializeObject<Result>(e.Result);

                loader.Visibility = System.Windows.Visibility.Collapsed;
                nextPage.Visibility = result.next == 1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                prevPage.Visibility = page == 1 ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                DisplayResults(result);
            };
            client.DownloadStringAsync(new Uri(str));
        }

        private void DisplayResults(Result result)
        {
            int id = 0;
            foreach (Entities.Track track in result.tracks)
            {
                int TopAlignment = 50 * id;

                TextBlock tb = new TextBlock();
                tb.Text = track.title;
                tb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tb.Margin = new Thickness(0, TopAlignment, 0, 0);
                tb.FontSize = 25;
                tb.Tag = track.id;
                tb.MouseLeftButtonDown += (sender, e) =>
                {
                    foreach (Entities.Track tr in result.tracks)
                    {
                        if (tr.id.ToString().CompareTo((sender as TextBlock).Tag.ToString()) == 0)
                        {
                            GlobalVariables.track = tr;
                            NavigationService.Navigate(new Uri("/Track.xaml", UriKind.Relative));
                            break;
                        }
                    }
                };

                mainGrid.Children.Add(tb);

                ++id;
            }
        }

        private void Powrot(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void NastepnaStrona(object sender, RoutedEventArgs e)
        {
            Search(GlobalVariables.phrase, ++page);
        }

        private void PoprzedniaStrona(object sender, RoutedEventArgs e)
        {
            Search(GlobalVariables.phrase, --page);
        }
    }
}