﻿

#pragma checksum "C:\Users\App42Admin\Downloads\App42-WinRT-Samples-master\App42-WinRT-Samples-master\App42-WNS-Sample\App42-WNS-Sample\App42-WNS-Sample.Windows\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "38BC3F6E96724B48B2D4A524DA4B4CF1"
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
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 11 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SendToast_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 12 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SendTile_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 13 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SendToastWithParams_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 14 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SendTileWithImage_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 15 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PinStartSecondaryTile_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 16 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SendSecondaryTile_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

