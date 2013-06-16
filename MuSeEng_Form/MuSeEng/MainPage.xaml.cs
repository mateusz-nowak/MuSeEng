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
using Microsoft.Phone.Storage;
using Windows.Storage;
using System.IO;
using System.Threading.Tasks;
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

        private async void loadPlaylist() //async wymagany dla await local.createfolderasync
        {
            //moja proba na zapis
            //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var MusicFolder = await local.CreateFolderAsync("MuSeEng", CreationCollisionOption.OpenIfExists);
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
                //WYJATEK TU RZUCA
                //An exception of type 'System.NullReferenceException' occurred in MuSeEng.DLL but was not handled in user code
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
                    holdMenu.Focus();
                    
                };


                mainGrid.Children.Add(tb);

                ++id;
    
            }
        }
        protected async void OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;// MOJA PROBA
            var MusicFolder = await local.CreateFolderAsync("MuSeEng", CreationCollisionOption.OpenIfExists);// MOJA PROBA
            //System.IO.Directory.CreateDirectory("MuSeEng");
            //System.IO.IsolatedStorage.IsolatedStorageFile local =
            //   System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            //if (!System.IO.Directory.Exists("MuSeEng"))
            //{
            //    System.IO.Directory.CreateDirectory("MuSeEng");
            //}

            //string location = "SD card/MuSeEng" + GlobalVariables.CurrentTrack.Title + ".mp3";

            var file = await MusicFolder.CreateFileAsync(GlobalVariables.CurrentTrack.Title + ".mp3", CreationCollisionOption.ReplaceExisting);// MOJA PROBA
            //using (var isoFileStream =
            //        new System.IO.IsolatedStorage.IsolatedStorageFileStream(
            //            location,
            //            System.IO.FileMode.OpenOrCreate, local))
            //{
            //    if (isoFileStream.Length != 0)
            //    {
            //        MessageBox.Show("Juz pobrales ten plik!");
            //    }
            //    else
            //    {
            //        using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
            //        {
            //            isoFileWriter.WriteLine(e.Result);

            //            MessageBox.Show("Zapisano na dysku: " + location);
            //        }
            //    }
            //}

        }
        private void DodajDoPlaylisty(object sender, RoutedEventArgs e)
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists("Mp3s\\Data"))
            {
                local.CreateDirectory("Mp3s\\Data");
            }

            string location = "Mp3s\\Data\\playlist.json";
            Playlist playlist;

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

                foreach (Entities.Track track in playlist.Tracks)
                {
                    if (track.Remote.CompareTo(GlobalVariables.CurrentTrack.Remote) == 0)
                        //WYJATEK TU RZUCA 
                    //An exception of type 'System.NullReferenceException' occurred in MuSeEng.DLL but was not handled in user code
                    {
                        MessageBox.Show("Taki utwor jest juz u Ciebie w playliscie");
                        return;
                    }
                }
            }

            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                        location,
                        System.IO.FileMode.Create,
                            local))
            {
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    playlist.Tracks.Add(GlobalVariables.CurrentTrack);

                    string jsonSerializedData = JsonConvert.SerializeObject(playlist);

                    isoFileWriter.WriteLine(jsonSerializedData);
                    MessageBox.Show("Dodano do playlisty");
                }
            }
        }

        protected void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            switch (menuItem.Header.ToString())
            {
                case "Pobierz":
                    WebClient webClient = new WebClient();

                    webClient.OpenReadCompleted += OpenReadCompleted;
                    webClient.OpenReadAsync(new Uri("http://mp3filmy.pl" + GlobalVariables.CurrentTrack.Download));
                    //WYJATEK TU RZUCA
                    //An exception of type 'System.NullReferenceException' occurred in MuSeEng.DLL but was not handled in user code
                    break;
                case "Dodaj do Playlisty":
                    DodajDoPlaylisty(sender, e);
                    break;
            }
        }
    }
}