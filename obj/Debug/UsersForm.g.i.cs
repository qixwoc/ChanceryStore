﻿#pragma checksum "..\..\UsersForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DD4C407D52E7BD09513C743C622778C95986FE269ECBB1474BBA39EC64E44559"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ChanceryStore;
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


namespace ChanceryStore {
    
    
    /// <summary>
    /// UsersForm
    /// </summary>
    public partial class UsersForm : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 1 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ChanceryStore.UsersForm wind;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle BackBlur;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox UsersLb;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddUserBtn;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteUserBtn;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editBtn;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button serBtn_Copy;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registrationPromtBtn_Copy;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\UsersForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registrationPromtBtn_Copy1;
        
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
            System.Uri resourceLocater = new System.Uri("/ChanceryStore;component/usersform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UsersForm.xaml"
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
            this.wind = ((ChanceryStore.UsersForm)(target));
            return;
            case 2:
            this.BackBlur = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 3:
            this.UsersLb = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.AddUserBtn = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\UsersForm.xaml"
            this.AddUserBtn.Click += new System.Windows.RoutedEventHandler(this.AddUserBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DeleteUserBtn = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\UsersForm.xaml"
            this.DeleteUserBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteUserBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.editBtn = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\UsersForm.xaml"
            this.editBtn.Click += new System.Windows.RoutedEventHandler(this.editBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.serBtn_Copy = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\UsersForm.xaml"
            this.serBtn_Copy.Click += new System.Windows.RoutedEventHandler(this.serBtn_Copy_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.registrationPromtBtn_Copy = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.registrationPromtBtn_Copy1 = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 65 "..\..\UsersForm.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectTeam_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

