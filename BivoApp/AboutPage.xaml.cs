using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace BivoApp
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();

            System.Windows.Media.ImageBrush myBrush = new System.Windows.Media.ImageBrush();
            Image image = new Image();
            image.ImageFailed += (s, i) => MessageBox.Show("Failed to load: " + i.ErrorException.Message);
            image.Source = new BitmapImage(new Uri("/Images/bg_hdpi.jpg", UriKind.Relative));
            myBrush.ImageSource = image.Source;
            LayoutRoot.Background = myBrush;

            imgPatch.Source = new BitmapImage(new Uri("/Images/patch.png", UriKind.Relative));


            
        }
    }
}