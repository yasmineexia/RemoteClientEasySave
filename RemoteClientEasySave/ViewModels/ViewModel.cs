using RemoteClientEasySave.Libs;
using RemoteClientEasySave.Models;
using System;
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
        public Client Client;
        private static Object _locker = new Object(); 
        List<Backup> _backups;
        Backup _backup;
        ICommand _pausecommand;
        ICommand _startcommand;
        ICommand _stopcommand;



        public ViewModel()
        {
            Client = new Client();
        }
        public void GetBackups()
        {
            lock (_locker)
            {
                Backups = Client.GetTasks();
            }
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

        public void Actualisation()
        {
            GetBackups();
            Thread.Sleep(1000);
        }
        private void Pause(object param)
        {
            lock (_locker)
            {
                Backups = Client.PauseTask((Backup)param);
            }
        }
        private void Start(object param)
        {
            lock (_locker)
            {
                Backups = Client.StartTask((Backup)param);
                GetBackups();
            }

        }
        private void Stop(object param)
        {
            lock (_locker)
            {
                Backups = Client.StopTask((Backup)param);
            }
        }

    }
}
