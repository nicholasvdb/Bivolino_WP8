using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhonePostClient;
using Newtonsoft.Json;
using BivoApp.Models;
using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Coding4Fun.Toolkit.Controls;

namespace BivoApp
{
    public partial class ResultPage : PhoneApplicationPage
    {
        Measure m;
        string email = "";
        string password = "";
        public ResultPage()
        {
           
            InitializeComponent();

            System.Windows.Media.ImageBrush myBrush = new System.Windows.Media.ImageBrush();
            Image image = new Image();
            image.ImageFailed += (s, i) => MessageBox.Show("Failed to load: " + i.ErrorException.Message);
            image.Source = new BitmapImage(new Uri("/Images/bg_hdpi.jpg", UriKind.Relative));
            myBrush.ImageSource = image.Source;
            LayoutRoot.Background = myBrush;


            m = (Measure)PhoneApplicationService.Current.State["Data"];
            pgrLoader.Visibility = Visibility.Visible;
            RequestServer();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("login"))
            {
                email = IsolatedStorageSettings.ApplicationSettings["login"] as string;
                password = IsolatedStorageSettings.ApplicationSettings["password"] as string;
                LoggedIn();
            }
          

        }

        private void RequestServer()
        {
            
            
            if (m.Gender.Equals("M"))
            {
                Male male = (Male)m;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("gender", "M");
                parameters.Add("cm", male.Cm);
                parameters.Add("age", male.Age);
                parameters.Add("weight", male.Weight);
                parameters.Add("height", male.Height);
                parameters.Add("collarsize", male.CollarSize);

                PostClient proxy = new PostClient(parameters);
                proxy.DownloadStringCompleted += proxy_DownloadStringCompleted;
                String urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/calcmale";
                proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));
            }
            else if (m.Gender.Equals("F"))
            {
                txtBelly.Visibility = Visibility.Collapsed;
                txtCollar.Visibility = Visibility.Collapsed;
                txtChest.Visibility = Visibility.Collapsed;
                lblBelly.Visibility = Visibility.Collapsed;
                lblCollar.Visibility = Visibility.Collapsed;
                lblChest.Visibility = Visibility.Collapsed;

                btnChest.Visibility = Visibility.Collapsed;
                btnCollar.Visibility = Visibility.Collapsed;
                btnBelly.Visibility = Visibility.Collapsed;

                btnUpperbust.Visibility = Visibility.Visible;
                btnUnderbust.Visibility = Visibility.Visible;
                btnHips.Visibility = Visibility.Visible;

                txtUpperbust.Visibility = Visibility.Visible;
                txtUnderbust.Visibility = Visibility.Visible;
                txtHips.Visibility = Visibility.Visible;
                lblUpperbust.Visibility = Visibility.Visible;
                lblUnderbust.Visibility = Visibility.Visible;
                lblHips.Visibility = Visibility.Visible;

                Female female = (Female)m;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("gender", "F");
                parameters.Add("cm", female.Cm);
                parameters.Add("age", female.Age);
                parameters.Add("weight", female.Weight);
                parameters.Add("height", female.Height);
                parameters.Add("cup", female.CupSize);
                parameters.Add("arm", female.ArmLength);
                parameters.Add("waist", female.WaistSize);
                parameters.Add("hip", female.HipSize);

                PostClient proxy = new PostClient(parameters);
                proxy.DownloadStringCompleted += proxy_DownloadStringCompleted;
                String urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/calcfemale";
                proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));

            }
        }

        void proxy_DownloadStringCompleted(object sender, WindowsPhonePostClient.DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string data = e.Result;
                string size = "";

                if (m.Cm.Equals("1"))
                {
                    size = " cm";
                }
                else 
                {
                    size = " inches";
                }

                if (m.Gender.Equals("M"))
                {
                    JToken root = JObject.Parse(data);
                    JToken result = root["CalculateSizeResult"];
                    MaleResult deserializedResult = JsonConvert.DeserializeObject<MaleResult>(result.ToString());
                    txtArm.Text = deserializedResult.Arm + size;
                    txtBack.Text = deserializedResult.Back + size;
                    txtBelly.Text = deserializedResult.Belly + size;
                    txtBmi.Text = deserializedResult.Bmi;
                    txtChest.Text = deserializedResult.Chest + size;
                    txtCollar.Text = deserializedResult.Collarsize + size;
                    txtPrediction.Text = deserializedResult.SizePredicition;
                    txtShoulder.Text = deserializedResult.Shoulder + size;
                    txtSleeve.Text = deserializedResult.Sleeve + size;
                    txtWaist.Text = deserializedResult.Waist + size;
                    txtWrist.Text = deserializedResult.Wrist + size;
                }
                else if (m.Gender.Equals("F"))
                {

                    JToken root = JObject.Parse(data);
                    JToken result = root["CalculateSizeResult"];
                    FemaleResult deserializedResult = JsonConvert.DeserializeObject<FemaleResult>(result.ToString());
                    txtUnderbust.Text = deserializedResult.UnderBust + size;
                    txtUpperbust.Text = deserializedResult.UpperBust + size;

                    txtArm.Text = deserializedResult.Arm + size;
                    txtBack.Text = deserializedResult.Back + size;


                    txtBmi.Text = deserializedResult.Bmi;
                    
                    
                    txtHips.Text = deserializedResult.Hips + size;
                    txtPrediction.Text = deserializedResult.SizePredicition;
                    txtShoulder.Text = deserializedResult.Shoulder + size;
                    txtSleeve.Text = deserializedResult.Sleeve + size;
                    txtWaist.Text = deserializedResult.Waist + size;
                    txtWrist.Text = deserializedResult.Wrist + size;
                    

                }

                SetBmiTextColor(txtBmi.Text);
                
            }
            else
            {
                MessagePrompt prompt = new MessagePrompt();
                prompt.Title = "Error";
                prompt.Message = e.Error.ToString();
                prompt.Show();

            }
            pgrLoader.Visibility = Visibility.Collapsed;
          
        }


        private void SetBmiTextColor(String value)
        {
            String[] bmi = value.Split(' ');


            if (bmi[1].Equals("normal"))
            {
                txtBmi.Foreground = new SolidColorBrush(Colors.Green);

            } else if (bmi[1].Equals("underweight") || bmi[1].Equals("overweight")){
                 txtBmi.Foreground = new SolidColorBrush(Colors.Orange);

            }else if (bmi[1].Equals("obese")){

                txtBmi.Foreground = new SolidColorBrush(Colors.Red);
            }


            
        }

        private void NavigateToRegister(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new Uri("/RegisterPage.xaml", UriKind.Relative));
        }

        private void LoginOrMail(object sender, RoutedEventArgs e)
        {
            bool isNetwork = NetworkInterface.GetIsNetworkAvailable();
              if (!isNetwork)
            {
                MessagePrompt prompt = new MessagePrompt();
                prompt.Title = "Error";
                  prompt.Message = "Please check your internet connection";
                  prompt.Show();
            }
            else{
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains("login"))
                {
                    SendMail();
                }
                else 
                {
                    bool valid = true;
                    
                    if(!TextValidator.IsValidEmail(txtEmail.Text)){
                        MessagePrompt prompt = new MessagePrompt();
                        prompt.Title = "Error";
                        prompt.Message = "Please enter a valid email address";
                        prompt.Show();
                        valid = false;
                    }  else if (!TextValidator.IsValdidPassword(txtPassword))
                    {

                        MessagePrompt prompt = new MessagePrompt();
                        prompt.Title = "Error";
                        prompt.Message = "Please enter a valid password";
                        prompt.Show();
                        valid = false;


                    }
                    
                    if (valid)
                    {
                        login();
                        
                    }
                }
              }
        }

        private void SendMail() 
        {

            pgrLoader.Visibility = Visibility.Visible;

            string size = "";
            if (m.Cm.Equals("1"))
                {
                    size = "cm";
                }
                else
                {
                    size = "inches";
                }
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            String urlAddress = "";
            if (txtUpperbust.Visibility == Visibility.Visible) 
            {
                FemaleResult result = new FemaleResult();
                parameters.Add("email",txtEmail.Text.Replace(" " + size, ""));
                parameters.Add("underbust", txtUnderbust.Text.Replace(" " + size, ""));
                parameters.Add("upperbust", txtUpperbust.Text.Replace(" " + size, ""));
                parameters.Add("hip", txtHips.Text.Replace(" " + size, ""));
                parameters.Add("sleeve", txtSleeve.Text.Replace(" " + size, ""));
                parameters.Add("shoulder", txtShoulder.Text.Replace(" " + size, ""));
                parameters.Add("arm", txtArm.Text.Replace(" " + size, ""));
                parameters.Add("wrist", txtWrist.Text.Replace(" " + size, ""));
                parameters.Add("waist", txtWaist.Text.Replace(" " + size, ""));
                parameters.Add("back", txtBack.Text.Replace(" " + size, ""));
                parameters.Add("predicted", txtPrediction.Text);
                
               
                urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/female";
            }
            else
            {
                MaleResult result = new MaleResult();
                parameters.Add("email", txtEmail.Text.Replace(" " + size, ""));
                parameters.Add("belly", txtBelly.Text.Replace(" " + size, ""));
                parameters.Add("chest", txtChest.Text.Replace(" " + size, ""));
                parameters.Add("collar", txtCollar.Text.Replace(" " + size, ""));
                parameters.Add("sleeve", txtSleeve.Text.Replace(" " + size, ""));
                parameters.Add("shoulder", txtShoulder.Text.Replace(" " + size, ""));
                parameters.Add("arm", txtArm.Text.Replace(" " + size, ""));
                parameters.Add("wrist", txtWrist.Text.Replace(" " + size, ""));
                parameters.Add("waist", txtWaist.Text.Replace(" " + size, ""));
                parameters.Add("back", txtBack.Text.Replace(" " + size, ""));
                parameters.Add("predicted", txtPrediction.Text);
              
                urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/male";

      
            }

            if (m.Cm.Equals("1"))
            {
                parameters.Add("size", "cm");
            }
            else
            {
                parameters.Add("size", "inch");
            }

            PostClient proxy = new PostClient(parameters);
            proxy.DownloadStringCompleted += proxy_DownloadStringCompleted_SendMail;
            proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));
        }

        void proxy_DownloadStringCompleted_SendMail(object sender, WindowsPhonePostClient.DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //Process the result... 
                string data = e.Result;

                LoginAntwoord antwoord = JsonConvert.DeserializeObject<LoginAntwoord>(data);

                if (antwoord.Success.Equals("1"))
                {
                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Success";
                    prompt.Message = antwoord.Message;
                    prompt.Show();
                }
                else
                {
                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Error";
                    prompt.Message = antwoord.Message;
                    prompt.Show();
                }
                
               

            }
            else
            {
                MessagePrompt prompt = new MessagePrompt();
                prompt.Title = "Error";
                prompt.Message = e.Error.ToString();
                prompt.Show();
            }
            pgrLoader.Visibility = Visibility.Collapsed;
            //throw new NotImplementedException();
        }

        private void login()
        {
            pgrLoader.Visibility = Visibility.Visible;
            
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("email", txtEmail.Text);
            parameters.Add("password", txtPassword.Text);

            PostClient proxy = new PostClient(parameters);

            proxy.DownloadStringCompleted += proxy_DownloadStringCompleted_Login;

            String urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/login";
            proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));
          
     

        }

        void proxy_DownloadStringCompleted_Login(object sender, WindowsPhonePostClient.DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //Process the result... 
                string data = e.Result;

                LoginAntwoord antwoord = JsonConvert.DeserializeObject<LoginAntwoord>(data);

                if (antwoord.Success == "1")
                {
                    IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                    if (!settings.Contains("login"))
                    {

                        settings.Add("login", txtEmail.Text);
                        settings.Add("password", txtPassword.Text);
                    }
                    else
                    {
                        settings["login"] = txtEmail.Text;
                        settings["password"] = txtPassword.Text;
                    }
                    settings.Save();
                    
                    MessagePrompt p = new MessagePrompt();
                    p.Title = "Success";
                    p.Message = antwoord.Message;
                    p.Show();
                    LoggedIn();
                }
                else
                {
                    MessagePrompt p = new MessagePrompt();
                    p.Title = "Error";
                    p.Message = antwoord.Message;
                    p.Show();
                }
                 
            }
            else
            {
                MessagePrompt p = new MessagePrompt();
                p.Title = "Error";
                p.Message = e.Error.ToString();
                p.Show();
            }
            pgrLoader.Visibility = Visibility.Collapsed;
            //throw new NotImplementedException();
        }


        private void LoggedIn() {
            txtEmail.Text = IsolatedStorageSettings.ApplicationSettings["login"] as string;
            string pass = IsolatedStorageSettings.ApplicationSettings["password"] as string;
            txtPassword.Text = passwordLenght(pass);
            txtEmail.IsEnabled = false;
            txtPassword.IsEnabled = false;
            btnLogin.Content = "Send Mail";
            btnRegister.Visibility = Visibility.Collapsed;
            btnForgot.Visibility = Visibility.Collapsed;
            btnLogout.Visibility = Visibility.Visible;
        
        }


        private String passwordLenght(String text)
        {
            String ster = "";

            for (int i = 0; i <= text.Length; i++)
            {
                ster = ster + "*";

            }

            return ster;

        }


        private void Logout(object sender, RoutedEventArgs e)
        {
            MessagePrompt logoutPrompt = new MessagePrompt();
            logoutPrompt.Title = "Logout";
            logoutPrompt.Message = "Do you really want to logout?";
            logoutPrompt.IsCancelVisible = true;
            logoutPrompt.Completed += logoutPrompt_Completed;

            logoutPrompt.Show();



            
        }

        void logoutPrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.Result != null)
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                settings.Remove("login");
                settings.Remove("password");
                settings.Save();

                txtEmail.Text = "";
                txtPassword.Text= "";
                txtEmail.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnLogin.Content = "Login";
                btnForgot.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Collapsed;
            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AboutPrompt p = new AboutPrompt();
            p.Title = "";
            p.VersionNumber = "";


            Button b = (Button)sender;

            if (txtBelly.Visibility == Visibility.Visible)
            {
                    switch (b.Name)
                {
                    case "btnCollar": p.Body = new ImagePopup("collar_male.png");
                        break;
                    case "btnChest": p.Body = new ImagePopup("chest_male.png");
                        break;
                    case "btnSleeve": p.Body = new ImagePopup("sleeve_male.png");
                        break;
                    case "btnShoulder": p.Body = new ImagePopup("shoulder_male.png");
                        break;
                    case "btnArm": p.Body = new ImagePopup("arm_male.png");
                        break;
                    case "btnWrist": p.Body = new ImagePopup("wrist_male.png");
                        break;
                    case "btnBelly": p.Body = new ImagePopup("belly_male.png");
                        break;
                    case "btnWaist": p.Body = new ImagePopup("waist_male.png");
                        break;
                    case "btnBack": p.Body = new ImagePopup("back_male.png");
                        break;
                    case "btnBmi": p.Body = new ImagePopup("bmi.png");
                        break;
                }
            }else
            {
                switch (b.Name)
                    {
                        case "btnCollar": p.Body = new ImagePopup("col_female.png");
                            break;
                        case "btnUpperbust": p.Body = new ImagePopup("upperbust.png");
                            break;
                        case "btnUnderbust": p.Body = new ImagePopup("underbust.png");
                            break;
                        case "btnSleeve": p.Body = new ImagePopup("sleeve_female.png");
                            break;
                        case "btnShoulder": p.Body = new ImagePopup("shoulder_female.png");
                            break;
                        case "btnArm": p.Body = new ImagePopup("arm_female.png");
                            break;
                        case "btnWrist": p.Body = new ImagePopup("wrist_female.png");
                            break;
                        case "btnHips": p.Body = new ImagePopup("hip_female.png");
                            break;
                        case "btnWaist": p.Body = new ImagePopup("waist_female.png");
                            break;
                        case "btnBack": p.Body = new ImagePopup("back_female.png");
                            break;
                        case "btnBmi": p.Body = new ImagePopup("bmi.png");
                            break;
                    }
            }
            
            p.Show();
            
           
        }

        private void ForgotPassword(object sender, RoutedEventArgs e)
        {
            InputPrompt input = new InputPrompt();
            input.Completed += input_Completed;
            input.Title = "Password Recovery";
            input.Message = "Please fill in your email to recover your password";
            
     
            input.Show();
            
        }

        void input_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.Result != null)
            {
                InputPrompt p = (InputPrompt)sender;

                if (TextValidator.IsValidEmail(p.Value))
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("email", p.Value);

                    PostClient proxy = new PostClient(parameters);

                    proxy.DownloadStringCompleted += proxy_DownloadStringCompleted_SendMail;

                    String urlAddress = "http://bivoapp.eu01.aws.af.cm/rest/forgotpassword";
                    proxy.DownloadStringAsync(new Uri(urlAddress, UriKind.Absolute));
                }
                else
                {
              
                    MessagePrompt prompt = new MessagePrompt();
                    prompt.Title = "Error";
                    prompt.Message = "Please fill in a valid email address";
                    prompt.Show();
                }
            }
        }
    }
}