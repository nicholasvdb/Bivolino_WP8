using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BivoApp.Resources;
using BivoApp.Models;
using System.Windows.Controls.Primitives;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;
using WindowsPhonePostClient;
using System.Windows.Media.Imaging;
using Coding4Fun.Toolkit.Controls;

namespace BivoApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        private bool cm = true;
        IsolatedStorageSettings settings;

        // Constructor
        public MainPage()
        {
            
            
            InitializeComponent();

            btnLogout = new ApplicationBarIconButton();

            System.Windows.Media.ImageBrush myBrush = new System.Windows.Media.ImageBrush();
            Image image = new Image();
            
            image.Source = new BitmapImage(new Uri("/Images/bg_hdpi.jpg", UriKind.Relative));
            myBrush.ImageSource = image.Source;
            LayoutRoot.Background = myBrush;

            

            loadSettings();

            SetHeight();
            SetWeight();
            SetAge();
            SetCollar();

            SetArmLength();
            SetBustSize();
            SetHipSize();
            SetWaistSize();
            SetCupSize();

           
        }


        private void SetHeight()
        {
            List<Male> heightSourceMale = new List<Male>();
            List<Female> heightSourceFemale = new List<Female>();



            if (cm)
            {

                inchColumn.Width = GridLength.Auto;
                inchColumnFemale.Width = GridLength.Auto;

                height2Male.Visibility = Visibility.Collapsed;
                height2Female.Visibility = Visibility.Collapsed;


                for (int i = 150; i <= 215; i++)
                {
                    heightSourceMale.Add(new Male() { Height = Convert.ToString(i) + " cm" });
                }
                height1Male.ItemsSource = heightSourceMale;


                for (int i = 150; i <= 185; i++)
                {
                    heightSourceFemale.Add(new Female() { Height = Convert.ToString(i) + " cm" });
                }
                height1Female.ItemsSource = heightSourceFemale;





            }
            else
            {


                List<Male> heightFeetSource = new List<Male>();
                for (int i = 4; i <= 7; i++)
                {
                    heightFeetSource.Add(new Male() { Height = Convert.ToString(i) + " feet" });
                }
                height1Male.ItemsSource = heightFeetSource;


                List<Male> heightInchSource = new List<Male>();
                for (double i = 0.0; i <= 11.5; i += 0.5)
                {
                    heightInchSource.Add(new Male() { Height = Convert.ToString(i) + " inches" });
                }
                height2Male.ItemsSource = heightInchSource;



                List<Female> heightFeetSourceFemale = new List<Female>();
                for (int i = 4; i <= 6; i++)
                {
                    heightFeetSourceFemale.Add(new Female() { Height = Convert.ToString(i) + " feet" });
                }
                height1Female.ItemsSource = heightFeetSourceFemale;



                List<Female> heightInchSourceFemale = new List<Female>();
                for (double i = 0.0; i <= 11.5; i += 0.5)
                {
                    heightInchSourceFemale.Add(new Female() { Height = Convert.ToString(i) + " inches" });
                }

                height2Female.ItemsSource = heightInchSourceFemale;


            }



        }


        private void SetWeight()
        {
            List<Male> weightSource = new List<Male>();
            List<Female> weightSourceFemale = new List<Female>();

            if (cm)
            {
                for (int i = 45; i <= 165; i++)
                {
                    weightSource.Add(new Male() { Weight = Convert.ToString(i) + " kg" });
                }
                weightMale.ItemsSource = weightSource;

                for (int i = 43; i <= 110; i++)
                {
                    weightSourceFemale.Add(new Female() { Weight = Convert.ToString(i) + " kg" });
                }
                weightFemale.ItemsSource = weightSourceFemale;

            }
            else
            {

                List<Male> weightInchSource = new List<Male>();
                double stone = 7.0;
                int index = 1;
                for (int i = 99; i <= 365; i++)
                {
                    if (index == 13)
                    {
                        weightInchSource.Add(new Male() { Weight = i.ToString() + "/" + stone.ToString() + "." + index.ToString() + " lbs/stone" });
                        index = 0;
                        stone += 1;
                    }
                    else
                    {
                        weightInchSource.Add(new Male() { Weight = i.ToString() + "/" + stone.ToString() + "." + index.ToString() + " lbs/stone" });
                        index += 1;
                    }
                }

                weightMale.ItemsSource = weightInchSource;



                List<Female> weightInchSourceFemale = new List<Female>();
                stone = 6.0;
                index = 10;
                for (int i = 94; i <= 241; i++)
                {
                    if (index == 13)
                    {
                        weightInchSourceFemale.Add(new Female() { Weight = i.ToString() + "/" + stone.ToString() + "." + index.ToString() + " lbs/stone" });
                        index = 0;
                        stone += 1;
                    }
                    else
                    {
                        weightInchSourceFemale.Add(new Female() { Weight = i.ToString() + "/" + stone.ToString() + "." + index.ToString() + " lbs/stone" });
                        index += 1;
                    }
                }

                weightFemale.ItemsSource = weightInchSourceFemale;


            }

        }


        private void SetCollar()
        {
            if (cm)
            {
                List<Male> collarSource = new List<Male>();
                string[] append = { "/XS", "/S", "/M", "/L", "/XL", "/2XL", "/3XL", "/4XL", "/5XL", "/6XL", "/7XL", "/8XL", "/9XL" };
                int j = 0;
                for (int i = 36; i <= 60; i++)
                {
                    if (i % 2 == 0)
                    {
                        collarSource.Add(new Male() { CollarSize = Convert.ToString(i) + append[j] });
                        j++;
                    }
                    else
                    {
                        collarSource.Add(new Male() { CollarSize = Convert.ToString(i) });
                    }

                }
                collarSizeMale.ItemsSource = collarSource;

            }
            else
            {
                List<Male> collarInchSource = new List<Male>();
                for (double i = 14.00; i <= 23.50; i += 0.50)
                {
                    collarInchSource.Add(new Male() { CollarSize = i.ToString() + " inches" });
                }

                collarSizeMale.ItemsSource = collarInchSource;
            }


        }


        private void SetAge()
        {

            List<Male> ageSource = new List<Male>();

            for (int i = 16; i <= 99; i++)
            {
                ageSource.Add(new Male() { Age = Convert.ToString(i) + " years" });
            }
            ageMale.ItemsSource = ageSource;
            ageFemale.ItemsSource = ageSource;


        }

        List<Female> cupSizeCmSource = new List<Female>();
        List<Female> cupSizeUkSource = new List<Female>();
        private void SetCupSize()
        {

   
                
                int eu = 60;
                int fr = 80;
                for (int i = 0; i < 10; i++)
                {
                    cupSizeCmSource.Add(new Female() { CupSize = eu.ToString() + " (eu)" });
                    eu += 5;
                }
                for (int i = 0; i < 10; i++)
                {
                    cupSizeCmSource.Add(new Female() { CupSize = fr.ToString() + " (fr)" });
                    fr += 5;
                }

                
               

         

                for (int i = 30; i <= 48; i += 2)
                {
                    cupSizeUkSource.Add(new Female() { CupSize = i.ToString() + " (uk)" });
                }

                if (cm)
                {
                    bustFemale.ItemsSource = cupSizeCmSource;
                }
                else
                {
                    bustFemale.ItemsSource = cupSizeUkSource;
                }
                

            



        }

        List<Female> cupLetterSource = new List<Female>();
        private void SetBustSize()
        {



            cupLetterSource.Add(new Female() { CupSize = "AA" });    
            cupLetterSource.Add(new Female() { CupSize = "A" });
                cupLetterSource.Add(new Female() { CupSize = "B" });
                cupLetterSource.Add(new Female() { CupSize = "C" });
                cupLetterSource.Add(new Female() { CupSize = "D" });
                cupLetterSource.Add(new Female() { CupSize = "DD" });
                cupLetterSource.Add(new Female() { CupSize = "E" });
                cupLetterSource.Add(new Female() { CupSize = "F" });
                cupLetterSource.Add(new Female() { CupSize = "FF" });
                cupLetterSource.Add(new Female() { CupSize = "G" });

                cupFemale.ItemsSource = cupLetterSource;

        }



        private void SetArmLength()
        {

            List<Female> armList = new List<Female>();
            armList.Add(new Female() { ArmLength = "short" });
            armList.Add(new Female() { ArmLength = "normal" });
            armList.Add(new Female() { ArmLength = "long" });

            armFemale.ItemsSource = armList;

        }


        private void SetWaistSize()
        {
            List<Female> waistList = new List<Female>();
            waistList.Add(new Female() { WaistSize = "xsmall" });
            waistList.Add(new Female() { WaistSize = "small" });
            waistList.Add(new Female() { WaistSize = "average" });
            waistList.Add(new Female() { WaistSize = "large" });
            waistList.Add(new Female() { WaistSize = "xlarge" });

            waistFemale.ItemsSource = waistList;
        }

        private void SetHipSize()
        {

            List<Female> hipList = new List<Female>();
            hipList.Add(new Female() { HipSize = "xslender" });
            hipList.Add(new Female() { HipSize = "slender" });
            hipList.Add(new Female() { HipSize = "average" });
            hipList.Add(new Female() { HipSize = "fuller" });
            hipList.Add(new Female() { HipSize = "xfuller" });

            hipFemale.ItemsSource = hipList;

        }

        private void loadSettings()
        {

            settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("cm"))
            {

                cm = true;
                settings.Add("cm", "1");
                settings.Save();

            }
            else
            {


                string cmSetting = IsolatedStorageSettings.ApplicationSettings["cm"] as string;
              
                if (cmSetting.Contains("1"))
                {
                    cm = true;
                }
                else
                {
                    cm = false;

                }

            }

            if (settings.Contains("login"))
            {
                btnLogout.IsEnabled = true;
            }

        }

        private List<String> populate(int start, int end)
        {
            List<String> lijst = new List<String>();

            for (int i = start; i <= end; i++)
            {
                lijst.Add(i.ToString());

            }


            return lijst;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("login"))
            {
                InitAppBar(true);
            }
            else
            {
                InitAppBar(false);
            }
            

        }

        private void ShowSettings(object sender, EventArgs e)
        {
            AboutPrompt p = new AboutPrompt();
            p.Title = "Settings";
            p.VersionNumber = "";
            p.Body = new SettingsPopup();
            p.Completed += settings_Closed;

            p.Show();
        }

        void settings_Closed(object sender, EventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("cm"))
            {
                String cmSetting = IsolatedStorageSettings.ApplicationSettings["cm"] as string;
                bool cmOld = cm;

               
                if (cmSetting.Contains("1"))
                {
                    cm = true;
                    
                }
                else
                {
                    cm = false;
                   
                }

                if (cmOld != cm)
                {
                    //Windows Phone seems to ignore the Refresh or even the Cache settings.
                    //I think this is due to save battery. But it is easy to circumvent.
                    //Instead of appending "?Refresh=true" just append a number like this:
                   
                    NavigationService.Navigate(new Uri("/MainPage.xaml?" + DateTime.Now.Ticks, UriKind.Relative));
                   
                }



            }

        }






        private void calculate_Click_1(object sender, RoutedEventArgs e)
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
                if (PivotInput.SelectedIndex == 0)
                {
                    Male wrapper = new Male();
                    String cmSetting = IsolatedStorageSettings.ApplicationSettings["cm"] as string;

                    if (cmSetting.Contains("2"))
                    {
                        Male height = (Male)height1Male.SelectedItem;
                        Male height2 = (Male)height2Male.SelectedItem;
                        wrapper.Height = height.Height.Replace(" feet", "'") + height2.Height.Replace(" inches", "");
                        

                        Male weight = (Male)weightMale.SelectedItem;
                        int index = weight.Weight.IndexOf('/');
                        wrapper.Weight = weight.Weight.Remove(index);

                      

                        Male collar = (Male)collarSizeMale.SelectedItem;
                        wrapper.CollarSize = collar.CollarSize.Replace(" inches", "");

                        wrapper.Cm = "2";
                    }
                    else
                    {
                        Male height = (Male)height1Male.SelectedItem;
                        wrapper.Height = height.Height;

                        Male weight = (Male)weightMale.SelectedItem;
                        wrapper.Weight = weight.Weight.Replace(" kg", "");

                        Male collar = (Male)collarSizeMale.SelectedItem;
                        wrapper.CollarSize = collar.CollarSize;

                        wrapper.Cm = "1";
                    }

                    
                    Male age = (Male)ageMale.SelectedItem;
                    

                    wrapper.Gender = "M";
                    
                    wrapper.Age = age.Age;
                    PhoneApplicationService.Current.State["Data"] = wrapper;
                    NavigationService.Navigate(new Uri("/ResultPage.xaml", UriKind.Relative));
                }
                else
                {
                    Female wrapper = new Female();
                    String cmSetting = IsolatedStorageSettings.ApplicationSettings["cm"] as string;

                    if (cmSetting.Contains("2"))
                    {
                        Female height = (Female)height1Female.SelectedItem;
                        Female height2 = (Female)height2Female.SelectedItem;
                        wrapper.Height = height.Height.Replace(" feet", "'") + height2.Height.Replace(" inches", "");
                      

                        Female weight = (Female)weightFemale.SelectedItem;
                        int index = weight.Weight.IndexOf('/');
                        wrapper.Weight = weight.Weight.Remove(index);

                        Female cup = (Female)cupFemale.SelectedItem;
                    Female bust = (Female)bustFemale.SelectedItem;

                    int indexCup = cupFemale.SelectedIndex;
                    int indexBust = bustFemale.SelectedIndex;

                    string cupEu = cupSizeCmSource[indexCup].CupSize;
                    string bustEu = cupLetterSource[indexBust].CupSize;


                    wrapper.CupSize = bust.CupSize.Replace(" ", "") + "-" + cup.CupSize + "/" + cupEu.Replace(" ", "") + "-" + bustEu;
                 
                    wrapper.Cm = "2";
                 
                    }
                    else
                    {
                        Female height = (Female)height1Female.SelectedItem;
                        wrapper.Height = height.Height;

                        Female weight = (Female)weightFemale.SelectedItem;
                        wrapper.Weight = weight.Weight.Replace(" kg", "");

                        Female cup = (Female)cupFemale.SelectedItem;
                        Female bust = (Female)bustFemale.SelectedItem;

                        int indexCup = cupFemale.SelectedIndex;
                        int indexBust = bustFemale.SelectedIndex;

                        string size = "";

                        if (bust.CupSize.Contains("eu"))
                        {
                            size = cupSizeCmSource[indexBust].CupSize;
                        }else if (bust.CupSize.Contains("fr"))
                        {
                            size = cupSizeCmSource[indexBust - 10].CupSize;
                        }
                        wrapper.CupSize = bust.CupSize.Replace(" ", "") + "-" + cup.CupSize + "/" + size.Replace(" ", "") + "-" + cup.CupSize;
                        
            

                        wrapper.Cm = "1";
                    }

                    wrapper.Gender = "F";
                    Measure age = (Measure)ageFemale.SelectedItem;
                    wrapper.Age = age.Age.Replace(" years", "");

                    Female arms = (Female)armFemale.SelectedItem;
                    wrapper.ArmLength = arms.ArmLength;

                    Female waist = (Female)waistFemale.SelectedItem;
                    wrapper.WaistSize = waist.WaistSize;

                    Female hips = (Female)hipFemale.SelectedItem;
                    wrapper.HipSize = hips.HipSize;
                  
                    PhoneApplicationService.Current.State["Data"] = wrapper;
                    NavigationService.Navigate(new Uri("/ResultPage.xaml", UriKind.Relative));
                }
               

           
            }

        }

        private void ShowAbout(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        ApplicationBarIconButton btnLogout;
        private void InitAppBar(bool loggedIn)
        {
            ApplicationBar appBar = new ApplicationBar();
            appBar.Mode = ApplicationBarMode.Minimized;

            ApplicationBarIconButton settings = new ApplicationBarIconButton(new Uri("/Images/feature.settings.png", UriKind.Relative));
            settings.Click += new EventHandler(ShowSettings);
            settings.Text = "Settings";
            appBar.Buttons.Add(settings);

            ApplicationBarIconButton about = new ApplicationBarIconButton(new Uri("/Images/questionmark.png", UriKind.Relative));
            about.Click += new EventHandler(ShowAbout);
            about.Text = "about";
            appBar.Buttons.Add(about);

            
            btnLogout = new ApplicationBarIconButton(new Uri("/Images/close.png", UriKind.Relative));
            btnLogout.Click += new EventHandler(LogoutAccount);
            btnLogout.Text = "logout";
            if (loggedIn)
            {
                btnLogout.IsEnabled = true;
            }
            else 
            {
                btnLogout.IsEnabled = false;
            }
            
            appBar.Buttons.Add(btnLogout);

            ApplicationBar = appBar;
        }

        private void LogoutAccount(object sender, EventArgs e)
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
                btnLogout.IsEnabled = false;
            }


        }
     
    }
}