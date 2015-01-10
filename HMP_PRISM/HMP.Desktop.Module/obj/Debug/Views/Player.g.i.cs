﻿#pragma checksum "..\..\..\Views\Player.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1451315A11C6B0B3B9E5E67E80768F00"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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
using System.Windows.Interactivity;
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


namespace HMP.Desktop.Module.Views {
    
    
    /// <summary>
    /// Player
    /// </summary>
    public partial class Player : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Views\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement player;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPlay;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPause;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider volume;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider posMedia;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Views\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonOpen;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HMP.Desktop.Module;component/views/player.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\Player.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.player = ((System.Windows.Controls.MediaElement)(target));
            
            #line 13 "..\..\..\Views\Player.xaml"
            this.player.MediaOpened += new System.Windows.RoutedEventHandler(this.Element_MediaOpened);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonPlay = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.buttonPause = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.volume = ((System.Windows.Controls.Slider)(target));
            return;
            case 5:
            this.posMedia = ((System.Windows.Controls.Slider)(target));
            
            #line 34 "..\..\..\Views\Player.xaml"
            this.posMedia.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.posMedia_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buttonOpen = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

