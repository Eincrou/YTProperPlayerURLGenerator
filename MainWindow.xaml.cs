using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ProperPlayerEnforcerURLGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<YouTubeURL> URLs { get; set; }
        SolidColorBrush AnimatedURLsInputBrush = new SolidColorBrush(Colors.White);
        public MainWindow()
        {
            InitializeComponent();

            URLs = new ObservableCollection<YouTubeURL>();
            URLsList.ItemsSource = URLs;
            copyURLButton.IsEnabled = false;

            urlInput.Background = AnimatedURLsInputBrush;

            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName("AnimatedURLsInputBrush", AnimatedURLsInputBrush);
        }

        private void AddYouTubeURLToList(string urlToAdd)
        {
            var duplicateURLs =
                from YouTubeURL url in URLs
                where url.VideoID == YouTubeURL.GetVideoID(urlToAdd)
                select url;

            if (duplicateURLs.Count() == 0)
                URLs.Add(new YouTubeURL(urlInput.Text));
            else
                MessageBox.Show("This URL is valid, but already in the list.", "Duplicate URL Detected");
        }

        private void GetURLButton_Click(object sender, RoutedEventArgs e)
        {            
            if (YouTubeURL.ValidateYTURL(urlInput.Text))
            {
                
                ColorAnimation colorAnim = new ColorAnimation(Colors.White, Colors.LightGreen, TimeSpan.FromMilliseconds(200));
                Storyboard.SetTargetName(colorAnim, "AnimatedURLsInputBrush");
                Storyboard.SetTargetProperty(colorAnim, new PropertyPath(SolidColorBrush.ColorProperty));
                Storyboard storyboard = new Storyboard();
                storyboard.AutoReverse = true;
                storyboard.Children.Add(colorAnim);                
                AddYouTubeURLToList(urlInput.Text);                
                URLsList.SelectedIndex = URLs.Count - 1;
                copyURLButton.IsEnabled = true;
                storyboard.Begin(this);
            }                
            else
                AnimatedURLsInputBrush.Color = Colors.LightPink;
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

        private void ShortURLTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox shortURLTB = sender as TextBox;
            Clipboard.SetText(shortURLTB.Text);
            MessageBox.Show("Youtube URL copied to clipboard.", "Copy to clipboard");
        }
    }
}
