﻿

#pragma checksum "C:\Users\App42Admin\Downloads\App42-WinRT-Samples-master\App42-WinRT-Samples-master\App42-WNS-Sample\App42-WNS-Sample\App42-WNS-Sample.Windows\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "48A98DBF7882EEB9A8B7949B9DD7B16B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App42_WNS_Sample
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button SendToast; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button SendTile; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button SendToastWithParams; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button SendTileWithImage; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ProgressBar LoadingBar; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Border StatusBorder; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock StatusBlock; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///MainPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            SendToast = (global::Windows.UI.Xaml.Controls.Button)this.FindName("SendToast");
            SendTile = (global::Windows.UI.Xaml.Controls.Button)this.FindName("SendTile");
            SendToastWithParams = (global::Windows.UI.Xaml.Controls.Button)this.FindName("SendToastWithParams");
            SendTileWithImage = (global::Windows.UI.Xaml.Controls.Button)this.FindName("SendTileWithImage");
            LoadingBar = (global::Windows.UI.Xaml.Controls.ProgressBar)this.FindName("LoadingBar");
            StatusBorder = (global::Windows.UI.Xaml.Controls.Border)this.FindName("StatusBorder");
            StatusBlock = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("StatusBlock");
        }
    }
}



