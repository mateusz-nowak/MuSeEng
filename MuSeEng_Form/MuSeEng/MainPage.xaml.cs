using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MuSeEng.Entities;
using Newtonsoft.Json;
using MuSeEng.Utilities;
using Newtonsoft.Json.Linq;

namespace MuSeEng
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MediaElement media = new MediaElement();
        private Playlist playlist;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            LayoutRoot.Children.Add(media);

            // Load Playlist
            loadPlaylist();
            
            // Load Statistics
            loadStatistics();
        }

        protected void loadStatistics()
        {
            populateStats("itemHipHop", 0);
            populateStats("itemDance", 1);
            populateStats("itemTechno", 2);
        }

        private void populateStats(string name, int type)
        {
            mainGrid.Children.Clear();

            string str = "http://museeng.my.phpcloud.com/?id=" + type.ToString();

            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    DisplayResultsStats(name, JArray.Parse(e.Result));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            client.DownloadStringAsync(new Uri(str));
        }

        private void DisplayResultsStats(string name, JArray result)
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
                    Search(track, 1);
                    mainPanorama.Focus();
                };

                Grid element;

                switch (name)
                {
                    case "itemHipHop":
                        element = gridHipHop;
                        break;
                    case "itemDance":
                        element = gridDance;
                        break;
                    case "itemTechno":
                        element = gridTechno;
                         break;
                    default:
                         throw new Exception("Unknown type!");
                }

                element.Children.Add(tb);

                ++id;
            }
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
                            Search(tr.Title, 1);
                            
                            break;
                        }
                    }
                };

                playlistMainGrid.Children.Add(tb);

                ++id;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        protected void SearchClick(object sender, RoutedEventArgs e)
        {
            String searchFieldContent = searchField.Text;

            if (searchFieldContent.Length < 3)
            {
                MessageBox.Show("Wyszukiwany utwor musi miec conajmniej 2 znaki");
                return;
            }

            Search(searchFieldContent, 1);
        }

        protected void Search(string phrase, int page)
        {
            mainGrid.Children.Clear();

            string str = "http://mp3filmy.pl/track/search.json?q=" + HttpUtility.UrlEncode(phrase) + "&page=" + page.ToString();

            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    DisplayResults(JsonConvert.DeserializeObject<Result>(e.Result));
                    // @TODO: Paginacja.
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            };
            client.DownloadStringAsync(new Uri(str));
        }

        protected void DisplayResults(Result result)
        {
            int id = 0;
            foreach (Entities.Track track in result.Tracks)
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
                    foreach (Entities.Track tr in result.Tracks)
                    {
                        if (tr.Id.ToString().CompareTo((sender as TextBlock).Tag.ToString()) == 0)
                        {
                            try
                            {
                                media.Stop();
                                // MessageBox.Show("KLIK");
                                // @TODO: Klikniecie.
                                media.Source = new Uri("http://mp3filmy.pl" + tr.Download, UriKind.RelativeOrAbsolute);
                                media.AutoPlay = true;
                                media.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            break;
                        }
                    }
                };
                tb.Hold += (sender, e) =>
                {
                    MessageBox.Show("TAPPED");
                };

                mainGrid.Children.Add(tb);

                ++id;
            }
        }
    }
}