﻿#pragma checksum "C:\Users\guziaster\Desktop\MuSeEng\MuSeEng_Form\MuSeEng\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C556AE856DF3D953D2452DE53DBD05DE"
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


namespace MuSeEng {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama MuSeEng;
        
        internal Microsoft.Phone.Controls.PanoramaItem mainPanorama;
        
        internal System.Windows.Controls.TextBox searchField;
        
        internal System.Windows.Controls.Grid mainGrid;
        
        internal Microsoft.Phone.Controls.ContextMenu holdMenu;
        
        internal System.Windows.Controls.Grid playlistMainGrid;
        
        internal Microsoft.Phone.Controls.PanoramaItem itemDance;
        
        internal System.Windows.Controls.Grid gridDance;
        
        internal Microsoft.Phone.Controls.PanoramaItem itemTechno;
        
        internal System.Windows.Controls.Grid gridTechno;
        
        internal Microsoft.Phone.Controls.PanoramaItem itemHipHop;
        
        internal System.Windows.Controls.Grid gridHipHop;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MuSeEng;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MuSeEng = ((Microsoft.Phone.Controls.Panorama)(this.FindName("MuSeEng")));
            this.mainPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("mainPanorama")));
            this.searchField = ((System.Windows.Controls.TextBox)(this.FindName("searchField")));
            this.mainGrid = ((System.Windows.Controls.Grid)(this.FindName("mainGrid")));
            this.holdMenu = ((Microsoft.Phone.Controls.ContextMenu)(this.FindName("holdMenu")));
            this.playlistMainGrid = ((System.Windows.Controls.Grid)(this.FindName("playlistMainGrid")));
            this.itemDance = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("itemDance")));
            this.gridDance = ((System.Windows.Controls.Grid)(this.FindName("gridDance")));
            this.itemTechno = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("itemTechno")));
            this.gridTechno = ((System.Windows.Controls.Grid)(this.FindName("gridTechno")));
            this.itemHipHop = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("itemHipHop")));
            this.gridHipHop = ((System.Windows.Controls.Grid)(this.FindName("gridHipHop")));
        }
    }
}

