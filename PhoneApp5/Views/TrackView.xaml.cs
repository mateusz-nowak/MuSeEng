using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp5.Utilities;
using Newtonsoft.Json;
using PhoneApp5.Entities;

namespace PhoneApp5.Views
{
    public partial class Track : PhoneApplicationPage
    {
        public Track()
        {
            InitializeComponent();
            title.Text = GlobalVariables.CurrentTrack.Title;
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

        private void Pobierz(object sender, RoutedEventArgs e)
        {
            DownloadBtn.IsEnabled = false;
            DownloadBtn.Content = "Pobieram...";
            
            WebClient webClient = new WebClient();
            
            webClient.OpenReadCompleted += OpenReadCompleted;
            webClient.OpenReadAsync(new Uri("http://mp3filmy.pl" + GlobalVariables.CurrentTrack.Download));
        }

        private void Powrot(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ResultsView.xaml", UriKind.Relative));
        }

        /**
         * Pobieranie i zapis do pliku utworu .mp3
         */
        private void OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists("Mp3s"))
            {
                local.CreateDirectory("Mp3s");
            }

            string location = "Mp3s\\" + GlobalVariables.CurrentTrack.Title + ".mp3";

            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                        location,
                        System.IO.FileMode.OpenOrCreate,
                            local))
            {
                if (isoFileStream.Length != 0)
                {
                    MessageBox.Show("Juz pobrales ten plik!");
                }
                else
                {
                    using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                    {
                        isoFileWriter.WriteLine(e.Result);

                        MessageBox.Show("Zapisano na dysku: " + location);
                    }
                }
            }

            DownloadBtn.IsEnabled = true;
            DownloadBtn.Content = "Pobierz";
        }
    }
}