using System;
using System.Collections.Generic;
using System.Linq;
using RemoteClientEasySave.Libs;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;

namespace RemoteClientEasySave.Models
{
    public enum BackupType
    {
        Complete,
        Differentielle
    }
    public enum BackupState
    {
        Inactif,
        En_Cours,
        En_Attente,
        Finie,
        Erreur
    }
    public class Backup : BaseModel
    {
        private string _name, _source, _destination;
        private BackupType _type;
        private BackupState _state;
        private double _nb_file_remaining, _total_size,_nbfile;
        public int _currentindex = 0;
        private int _progression;
        public string Name
        {
            get { return _name ?? ""; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Source
        {
            get { return _source ?? ""; }
            set
            {
                if (_source == value) return;

                _source = value;
                OnPropertyChanged("Source");
                OnPropertyChanged("Files");
            }
        }
        public string Destination
        {
            get { return _destination ?? ""; }
            set
            {
                if (_destination == value) return;
                _destination = value;
                OnPropertyChanged("Destination");
            }
        } 
        public BackupType Type
        {
            get { return _type; }
            set
            {
                if (Type == value) return;
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        
        public BackupState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        public double NbFileRemaining
        {
            get { return _nb_file_remaining; }
            set
            {
                if (_nb_file_remaining == value) return;
                _nb_file_remaining = value;
                OnPropertyChanged("NbFileRemaining");
                OnPropertyChanged("Progression");
                OnPropertyChanged("IsPrioritaire");
            }
        }
        public double TotalSize
        {
            get { return _total_size; }
            set
            {
                if (_total_size == value) return;
                _total_size = value;
                OnPropertyChanged("TotalSize");
            }
        }
        public int Progression { get
            {
                return _progression;
            }
            set
            {
                _progression = value;
                OnPropertyChanged("Progression");
            }
        }
        public double NbFile
        {
            get { return _nbfile; }
            set { _nbfile = value;OnPropertyChanged("NbFile"); }
        }
    }
}
