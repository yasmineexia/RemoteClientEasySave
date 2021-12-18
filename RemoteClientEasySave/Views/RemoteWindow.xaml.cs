using RemoteClientEasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemoteClientEasySave.Views
{
    /// <summary>
    /// Logique d'interaction pour RemoteWindow.xaml
    /// </summary>
    public partial class RemoteWindow : Window
    {
        public RemoteWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Modal m = new Modal();
            m.Owner = this;
            m.ShowDialog();
            if(m.IP.Text != null && m.Password.Password != null && (DataContext as ViewModel).Client.SeConnecter(m.IP.Text, m.Password.Password, 2906))
            {
                (DataContext as ViewModel).GetBackups();
            }
            else
            {
                Grid_Loaded(this, e);
            }
        }
    }
}
