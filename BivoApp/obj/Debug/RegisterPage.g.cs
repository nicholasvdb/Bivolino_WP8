﻿#pragma checksum "C:\Users\Werelds_\Desktop\BivoApp_6_06\BivoApp\BivoApp\RegisterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B3C8D609F107CC925077EACB0405B339"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace BivoApp {
    
    
    public partial class Page1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.DataTemplate LanguageItemTemplate;
        
        internal System.Windows.DataTemplate LanguageFullModeItemTemplate;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.DataTemplate PickerItemTemplate;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtFirstname;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtLastname;
        
        internal Microsoft.Phone.Controls.ListPicker languagePicker;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtEmail;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtPassword;
        
        internal System.Windows.Controls.CheckBox chkNews;
        
        internal System.Windows.Controls.ProgressBar pgrLoader;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/BivoApp;component/RegisterPage.xaml", System.UriKind.Relative));
            this.LanguageItemTemplate = ((System.Windows.DataTemplate)(this.FindName("LanguageItemTemplate")));
            this.LanguageFullModeItemTemplate = ((System.Windows.DataTemplate)(this.FindName("LanguageFullModeItemTemplate")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.PickerItemTemplate = ((System.Windows.DataTemplate)(this.FindName("PickerItemTemplate")));
            this.txtFirstname = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtFirstname")));
            this.txtLastname = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtLastname")));
            this.languagePicker = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("languagePicker")));
            this.txtEmail = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtEmail")));
            this.txtPassword = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtPassword")));
            this.chkNews = ((System.Windows.Controls.CheckBox)(this.FindName("chkNews")));
            this.pgrLoader = ((System.Windows.Controls.ProgressBar)(this.FindName("pgrLoader")));
        }
    }
}

