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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProperPlayerEnforcerURLGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using System.Collections.ObjectModel;
    public partial class MainWindow : Window
    {
        public ObservableCollection<YoutubeURL> URLs { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            URLs = new ObservableCollection<YoutubeURL>();

            URLsList.ItemsSource = URLs;
            copyURLButton.IsEnabled = false;
        }

        private void GetURLButton_Click(object sender, RoutedEventArgs e)
        {            
            if (YoutubeURL.ValidateYTURL(urlInput.Text))
            {
                urlInput.Background = new SolidColorBrush(Colors.White);
                URLs.Add(new YoutubeURL(urlInput.Text));
                URLsList.SelectedIndex = URLs.Count - 1;
                copyURLButton.IsEnabled = true;
            }                
            else
                urlInput.Background = new SolidColorBrush(Colors.LightPink);
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (URLsList.SelectedIndex >= 0)
            {
                string enforcerURL = URLs[URLsList.SelectedIndex].EnforcerURL;
                Clipboard.SetText(enforcerURL);
                if (URLs.Count > URLsList.SelectedIndex)
                    URLsList.SelectedIndex++;
            }
        }
    }
}
