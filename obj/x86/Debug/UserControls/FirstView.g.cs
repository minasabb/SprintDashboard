﻿#pragma checksum "..\..\..\..\UserControls\FirstView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BBD3F80CD95B7994E0AAD0C6B7255E9C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Themes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TextDashboard.Custom_Control;


namespace TextDashboard.UserControls {
    
    
    /// <summary>
    /// FirstView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class FirstView : TextDashboard.Custom_Control.ResizableContentControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\UserControls\FirstView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl Loading;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\UserControls\FirstView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ScanDeviceButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\UserControls\FirstView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ScanDeviceBorder;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\UserControls\FirstView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ScanDeviceStackPanel;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\UserControls\FirstView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl LoadingResult;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\UserControls\FirstView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanelSearchResult;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/TextDashboard;component/usercontrols/firstview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\FirstView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Loading = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 2:
            this.ScanDeviceButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\UserControls\FirstView.xaml"
            this.ScanDeviceButton.Click += new System.Windows.RoutedEventHandler(this.DeviceScanButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ScanDeviceBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.ScanDeviceStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            
            #line 50 "..\..\..\..\UserControls\FirstView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 57 "..\..\..\..\UserControls\FirstView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 64 "..\..\..\..\UserControls\FirstView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.LoadingResult = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 9:
            this.StackPanelSearchResult = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

