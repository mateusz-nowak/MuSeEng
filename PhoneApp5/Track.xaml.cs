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

namespace PhoneApp5
{
    public partial class Track : PhoneApplicationPage
    {
        public Track()
        {
            InitializeComponent();
            title.Text = GlobalVariables.track.title;
        }

        private void DodajDoPlaylisty(object sender, RoutedEventArgs e)
        {

        }

        private void Pobierz(object sender, RoutedEventArgs e)
        {
            DownloadBtn.IsEnabled = false;
            DownloadBtn.Content = "Pobieram...";
            
            WebClient webClient = new WebClient();
            
            webClient.OpenReadCompleted += OpenReadCompleted;
            webClient.OpenReadAsync(new Uri("http://mp3filmy.pl" + GlobalVariables.track.download));
        }

        private void Powrot(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Results.xaml", UriKind.Relative));
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

            string location = "Mp3s\\" + GlobalVariables.track.title + ".mp3";

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