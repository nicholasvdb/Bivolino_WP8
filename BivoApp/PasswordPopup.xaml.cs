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
using WindowsPhonePostClient;
using BivoApp.Models;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;

namespace BivoApp
{
    public partial class PasswordPopup : PhoneApplicationPage
    {
        public PasswordPopup()
        {
            InitializeComponent();
        }

        private void SendMail(object sender, RoutedEventArgs e)
        {
            if (TextValidator.IsValidEmail(txtEmail.Text))
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("email", txtEmail.Text);

                PostClient proxy = new PostClient(parameters);

                proxy.DownloadStringCompleted += proxy_DownloadStringCompleted_SendMail;

                String urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/forgotpassword";
                proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));
            }
            else 
            {
                MessageBox.Show("Please fill in a valid email address", "Error", MessageBoxButton.OK);
            }
            
        }

        void proxy_DownloadStringCompleted_SendMail(object sender, WindowsPhonePostClient.DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //Process the result... 
                string data = e.Result;
                LoginAntwoord antwoord = JsonConvert.DeserializeObject<LoginAntwoord>(data);
                if (antwoord.Success == "1")
                {
                    MessageBox.Show(antwoord.Message, "Success", MessageBoxButton.OK);
                    Popup passwordPopup = this.Parent as Popup;
                    passwordPopup.IsOpen = false;
                }
                else
                {
                    MessageBox.Show(antwoord.Message, "Error", MessageBoxButton.OK);
                }
                
            }
            else
            {
                MessageBox.Show(e.Error.ToString());
            }
            //throw new NotImplementedException();
        }

        private void ClosePopup(object sender, RoutedEventArgs e)
        {
            Popup passwordPopup = this.Parent as Popup;
            passwordPopup.IsOpen = false;
        }
    }
}