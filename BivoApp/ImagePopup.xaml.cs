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
using System.Windows.Controls.Primitives;

namespace BivoApp
{
    public partial class ImagePopup : PhoneApplicationPage
    {
        public ImagePopup(string image)
        {
            InitializeComponent();

            imgPopup.Source = new BitmapImage(new Uri("/Images/Popups/" + image, UriKind.Relative));
        }

       
    }
}