﻿#pragma checksum "..\..\AddProductForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2FD4DBBCCB585612277B12C134236482062B8F82B1155D8B71526C2976EEF072"
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
    /// AddProductForm
    /// </summary>
    public partial class AddProductForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameTb;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameTb;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox loginTb;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passwordTb;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button imageBtn;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label typePromtLbl;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label firstNamePromtLbl;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lastNamePromtLbl;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\AddProductForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ChanceryStore;component/addproductform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddProductForm.xaml"
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
            this.firstNameTb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.lastNameTb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.loginTb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.passwordTb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.imageBtn = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\AddProductForm.xaml"
            this.imageBtn.Click += new System.Windows.RoutedEventHandler(this.imageBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.typePromtLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.firstNamePromtLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lastNamePromtLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.okBtn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\AddProductForm.xaml"
            this.okBtn.Click += new System.Windows.RoutedEventHandler(this.okBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
