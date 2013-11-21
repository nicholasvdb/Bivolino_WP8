using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;

namespace BivoApp
{
    public partial class LogoutPopup : PhoneApplicationPage
    {
        public LogoutPopup()
        {
            InitializeComponent();
        }

        private void ClosePopup(object sender, RoutedEventArgs e)
        {
            Popup passwordPopup = this.Parent as Popup;
            passwordPopup.IsOpen = false;
        }
    }
}