using DAL3;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App4.Viewmodel
{
    public class PageViewmodel : INotifyPropertyChanged
    {
        public ICommand OutputAllData => new Command(async value => { await LoadAllData(); });

        public ICommand OutputData => new Command(async value => { await LoadData(); });

        public ICommand InputData => new Command(async value => { await EnterData(); });

        private ObservableCollection<Worker> _workers;

        public ObservableCollection<Worker> Workers
        {
            get => _workers;
            set
            {
                _workers = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAllData()
        {
            var client = new HttpClient(GetInsecureHandler());
            var response = await client.GetAsync("http://192.168.0.2:5000/home/getWorkers").ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync();
            var workers = JsonConvert.DeserializeObject<ObservableCollection<Worker>>(json);
            Workers = workers;
        }

        public async Task LoadData()
        {
            var client = new HttpClient(GetInsecureHandler());
            var response = await client.GetAsync("http://192.168.0.2:5000/home/getWorker/1").ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync();
            var workers = JsonConvert.DeserializeObject<ObservableCollection<Worker>>(json);
            Workers = workers;
        }
        
        public async Task EnterData()
        {
            var client = new HttpClient(GetInsecureHandler());
            StringContent queryString = new StringContent("Имя работающего");
            var response = await client.PostAsync("http://192.168.0.2:5000/home/getWorkers", queryString).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync();
            var workers = JsonConvert.DeserializeObject<ObservableCollection<Worker>>(json);
            Workers = workers;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

    }
}
