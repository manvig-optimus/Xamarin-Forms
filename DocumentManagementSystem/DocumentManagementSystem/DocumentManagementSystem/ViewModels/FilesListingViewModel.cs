using DocumentManagementSystem.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DocumentManagementSystem.ViewModels
{
    public class FilesListingViewModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<string> FileTypes { get; set; }

        private string _selectedFilter;
        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                if (value != _selectedFilter)
                {
                    _selectedFilter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<File> _files;
        public ObservableCollection<File> Files
        {
            get { return _files ?? (_files = new ObservableCollection<File>()); }
            set
            {
                if (_files != value)
                {
                    _files = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CancellationTokenSource TokenSource;
        CancellationToken token;

        public FilesListingViewModel()
        {
            FileTypes = new List<string>() { "Owned Files",
                "Shared With Me",
                "Shared With Me - Edit Mode",
                "Shared With Me - Read Mode",
                "Public on Web",
                "Accessible to Optimus Family" };
            SelectedFilter = FileTypes[0];

            LoadFiles();
        }

        public void LoadFiles()
        {
            Files = new ObservableCollection<File>();
            TokenSource = new CancellationTokenSource();

            token = TokenSource.Token;
            Task.Run(() => GetDriveFiles(), token);
        }

        private async Task GetDriveFiles(string pagetoken = null)
        {
            if (!token.IsCancellationRequested)
            {
                var requestParameters = CreateRequestParameters(pagetoken);

                string result = await RequestHandler.ProcessRequest(Constants.driveFilesUrl, requestParameters);

                if (String.IsNullOrEmpty(result) || result == Constants.IncorrectRequest)
                {
                    MessagingCenter.Send<FilesListingViewModel>(this, "IncorrectRequest");
                }
                else
                {
                    var driveFilesData = JsonConvert.DeserializeObject<DriveFile>(result);
                    if (driveFilesData != null)
                    {
                        foreach (var file in driveFilesData.Files)
                        {
                            Files.Add(file);
                        }

                        if (driveFilesData.NextPageToken != null)
                        {
                            await GetDriveFiles(driveFilesData.NextPageToken);
                            return;
                        }
                    }
                }
            }
            Files = (ObservableCollection<File>)Files.OrderBy(x => x.Name);
        }

        private Dictionary<string, string> CreateRequestParameters(string pagetoken)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("orderBy", "name");
            parameters.Add("pageSize", "1000");
            parameters.Add("fields", "nextPageToken,files(id, name, webViewLink, iconLink, modifiedTime, lastModifyingUser, permissions)");

            if (SelectedFilter == FileTypes[0])
                parameters.Add("q", "trashed=false and 'me' in owners");
            else if (SelectedFilter == FileTypes[1])
                parameters.Add("q", "sharedWithMe and trashed = false and not 'me' in owners and 'me' in readers"); //367
            else if (SelectedFilter == FileTypes[2])
                parameters.Add("q", "sharedWithMe and trashed = false and not 'me' in owners and 'me' in writers"); //319
            else if (SelectedFilter == FileTypes[3])
                parameters.Add("q", "sharedWithMe and trashed = false and not 'me' in owners and not 'me' in writers and 'me' in readers"); //48
            else if (SelectedFilter == FileTypes[4])
                parameters.Add("q", "trashed=false and visibility='anyoneCanFind'"); //19
            else if (SelectedFilter == FileTypes[5])
                parameters.Add("q", "trashed=false and visibility='domainCanFind'"); // 1517

            if (pagetoken != null)
            {
                parameters.Add("pageToken", pagetoken);
            }

            return parameters;
        }
    }
}
