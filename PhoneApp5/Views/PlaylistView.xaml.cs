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
using PhoneApp5.Entities;
using PhoneApp5.Utilities;

namespace PhoneApp5.Views
{
    public partial class PlaylistView : PhoneApplicationPage
    {
        private Playlist playlist;

        public PlaylistView()
        {
            InitializeComponent();
            loadPlaylist();
            displayPlaylist();
        }

        private void displayPlaylist()
        {
            int id = 0;
            foreach (Entities.Track track in playlist.Tracks)
            {
                int TopAlignment = 50 * id;

                TextBlock tb = new TextBlock();
                tb.Text = track.Title;
                tb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tb.Margin = new Thickness(0, TopAlignment, 0, 0);
                tb.FontSize = 25;
                tb.Tag = track.Id;
                tb.MouseLeftButtonDown += (sender, e) =>
                {
                    foreach (Entities.Track tr in playlist.Tracks)
                    {
                        if (tr.Id.ToString().CompareTo((sender as TextBlock).Tag.ToString()) == 0)
                        {
                            GlobalVariables.CurrentTrack = tr;
                            NavigationService.Navigate(new Uri("/Views/TrackView.xaml", UriKind.Relative));
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

        private void loadPlaylist()
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists("Mp3s\\Data"))
            {
                local.CreateDirectory("Mp3s\\Data");
            }

            string location = "Mp3s\\Data\\playlist.json";

            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                        location,
                        System.IO.FileMode.OpenOrCreate,
                            local))
            {
                if (isoFileStream.Length == 0)
                {
                    playlist = new Playlist("Nowa playlista");
                }
                else
                {
                    using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                    {
                        playlist = JsonConvert.DeserializeObject<Playlist>(isoFileReader.ReadToEnd());
                    }
                }
            }
        }
    }
}