﻿#pragma checksum "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F49043C8D5D789F7B5FCEAAD8CA50F4E0C5DB9A33E64B858D942466A74465002"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ChanceryStore.Forms.TimetableWindow;
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


namespace ChanceryStore.Forms.TimetableWindow {
    
    
    /// <summary>
    /// AddTimetableWindow
    /// </summary>
    public partial class AddTimetableWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okBtn;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox usersCb;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TimeStartTb;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TimeEndTb;
        
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
            System.Uri resourceLocater = new System.Uri("/ChanceryStore;component/forms/timetablewindow/addtimetablewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
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
            this.okBtn = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
            this.okBtn.Click += new System.Windows.RoutedEventHandler(this.okBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.usersCb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.TimeStartTb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TimeEndTb = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\..\Forms\TimetableWindow\AddTimetableWindow.xaml"
            this.TimeEndTb.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TimeStartTb_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
