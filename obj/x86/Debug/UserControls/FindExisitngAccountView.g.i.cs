﻿#pragma checksum "..\..\..\..\UserControls\FindExisitngAccountView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "60674067540A66FCF797817C6947DDD0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// FindExisitngAccountView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class FindExisitngAccountView : TextDashboard.Custom_Control.SelfExpandableControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TextDashboard.UserControls.FindExisitngAccountView vFindExistingAccountView;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl Loading;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FindAccountBorder;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel FindAccountStackPanel;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboAccount;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox field;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl LoadingResult;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanelSearchResult;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PlainDataGridSprintStyle;
        
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
            System.Uri resourceLocater = new System.Uri("/TextDashboard;component/usercontrols/findexisitngaccountview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
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
            this.vFindExistingAccountView = ((TextDashboard.UserControls.FindExisitngAccountView)(target));
            return;
            case 2:
            this.Loading = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 3:
            this.FindAccountBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.FindAccountStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            
            #line 41 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cboAccount = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.field = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 75 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 82 "..\..\..\..\UserControls\FindExisitngAccountView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.LoadingResult = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 11:
            this.StackPanelSearchResult = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 12:
            this.PlainDataGridSprintStyle = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

