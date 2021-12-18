using RemoteClientEasySave.Libs;
using RemoteClientEasySave.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace RemoteClientEasySave.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        Thread Thread;
        Client Client;
        List<Backup> _backups;
        Backup _backup;
        ICommand _pausecommand;
        ICommand _startcommand;
        ICommand _stopcommand;



        public ViewModel()
        {
            Client = new Client();
            if (Client.SeConnecter("127.0.0.1", "nazim", 2906))
            {
                GetBackups();
            }
            else
            {
                MessageBox.Show("error");

            }

        }
        private void GetBackups()
        {
            Backups = Client.GetTasks();
        }
        public ICommand PauseCommand
        {
            get
            {
                if (_pausecommand == null) _pausecommand = new RelayCommand(Pause, (object sender) => true);
                return _pausecommand;
            }
        }
        public ICommand StartCommand
        {
            get
            {
                if (_startcommand == null) _startcommand = new RelayCommand(Start, (object sender) => true);
                return _startcommand;
            }
        }
        public ICommand StopCommand
        {
            get
            {
                if (_stopcommand == null) _stopcommand = new RelayCommand(Stop, (object sender) => true);
                return _stopcommand;
            }
        }
        public List<Backup> Backups
        {
            get { return _backups; }
            set { _backups = value; OnPropertyChanged("Backups"); }
        }
        public Backup Backup
        {
            get { return _backup; }
            set { _backup = value; OnPropertyChanged("Backup"); }
        }


        private void Pause(object param)
        {
            if (param as int? == null) return;
            Debug.WriteLine((param as int?).ToString());
        }
        private void Start(object param)
        {
            if (param as int? == null) return;
            Debug.WriteLine((param as int?).ToString());
        }
        private void Stop(object param)
        {
            if (param as int? == null) return;
            Debug.WriteLine((param as int?).ToString());
        }

    }
}
