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
using System.IO.IsolatedStorage;

namespace BivoApp
{
    public partial class SettingsPopup : PhoneApplicationPage
    {
        public SettingsPopup()
        {
            InitializeComponent();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("cm"))
            {
                string cm = IsolatedStorageSettings.ApplicationSettings["cm"] as string;
                if (cm.Contains("1"))
                {
                    chkMetric.IsChecked = true;
                }
                else
                {
                    chkImperial.IsChecked = true;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveSetting();
        }

        private void SaveSetting()
        {

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("cm"))
            {
                if ((bool)chkMetric.IsChecked)
                {
                    settings.Add("cm", "1");
                }
                else
                {
                    settings.Add("cm", "2");
                }
                
            }
            else
            {
                if ((bool)chkMetric.IsChecked)
                {
                    settings["cm"] = "1";
                }
                else
                {
                    settings["cm"] = "2";
                }
            }
            settings.Save();
        }
    }
}