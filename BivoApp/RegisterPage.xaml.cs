using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BivoApp.Models;
using Microsoft.Phone.Net.NetworkInformation;
using WindowsPhonePostClient;
using System.Windows.Media.Imaging;
using Coding4Fun.Toolkit.Controls;
using Newtonsoft.Json;

namespace BivoApp
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();

            System.Windows.Media.ImageBrush myBrush = new System.Windows.Media.ImageBrush();
            Image image = new Image();
            image.ImageFailed += (s, i) => MessageBox.Show("Failed to load: " + i.ErrorException.Message);
            image.Source = new BitmapImage(new Uri("/Images/bg_hdpi.jpg", UriKind.Relative));
            myBrush.ImageSource = image.Source;
            LayoutRoot.Background = myBrush;

            List<Language> languageSource = new List<Language>();
            
            languageSource.Add(new Language() { Taal = "English" });
            languageSource.Add(new Language() { Taal = "French" });
            languageSource.Add(new Language() { Taal = "German" });
            languageSource.Add(new Language() { Taal = "Dutch" });
            languagePicker.ItemsSource = languageSource;
        }

        
        private void Register(object sender, RoutedEventArgs e)
        {


            register();
            
        }


        private void register()
        {

            bool isNetwork = NetworkInterface.GetIsNetworkAvailable();
            if (!isNetwork)
            {
                MessagePrompt prompt = new MessagePrompt();
                prompt.Title = "Error";

                prompt.Message = "Please check your internet connection";
                prompt.Show();

            }
            else
            {

                bool valid = true;
                if (!TextValidator.IsValidField(txtFirstname))
                {
                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Error";

                    prompt.Message = "Please enter your firstname";
                    prompt.Show();
                    valid = false;
                }
                else if (!TextValidator.IsValidField(txtLastname))
                {

                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Error";

                    prompt.Message = "Please enter your surname";
                    prompt.Show();
                    valid = false;


                }
                else if (!TextValidator.IsValidEmail(txtEmail.Text))
                {

                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Error";

                    prompt.Message = "Please enter a valid email address";
                    prompt.Show();
                    
                    valid = false;


                }
                else if (!TextValidator.IsValdidPassword(txtPassword))
                {
                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Error";

                    prompt.Message = "Please enter a password, minimum 5 chars";
                    prompt.Show();
                    
                    valid = false;


                }  

                if (valid)
                {
                    //register 
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("name", txtLastname.Text);
                    parameters.Add("email", txtEmail.Text);
                    parameters.Add("fname", txtLastname.Text);
                    parameters.Add("password", txtPassword.Text);
                    Language lang = (Language)languagePicker.SelectedItem;
                    
                    parameters.Add("language", lang.Taal);

                    if ((bool)chkNews.IsChecked)
                    {
                        parameters.Add("addtonews", 1);
                    }
                    else
                    {
                        parameters.Add("addtonews", 0);
                    }
                    
                    
                    PostClient proxy = new PostClient(parameters);
                    proxy.DownloadStringCompleted += proxy_DownloadStringCompleted;
                    String urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/adduser";
                    proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));
                    pgrLoader.Visibility = Visibility.Visible;

                }
            }

        }

        void proxy_DownloadStringCompleted(object sender, WindowsPhonePostClient.DownloadStringCompletedEventArgs e)
        {
            LoginAntwoord antwoord = JsonConvert.DeserializeObject<LoginAntwoord>(e.Result);
            if (e.Error == null)
            {
                MessagePrompt prompt = new MessagePrompt();
                prompt.Title = "Success";
             
                prompt.Message = antwoord.Message;
                prompt.Show();

                prompt.Completed += prompt_Completed;

                
            }
            else
            {
                pgrLoader.Visibility = Visibility.Collapsed;
                MessagePrompt prompt = new MessagePrompt();
                prompt.Title = "Error";

                prompt.Message = antwoord.Message;
                prompt.Show();

            }
            
        }

        void prompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            NavigationService.Navigate(new Uri("/ResultPage.xaml", UriKind.Relative));
        }


    }
}