using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App42_WNS_Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AppHelper helper = null;
        SecondaryTile tile = null;

        public MainPage()
        {
            this.InitializeComponent();
            DISPATCHER.Initialize();
            helper = new AppHelper();
            ShowLoadingBar();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            base.OnNavigatedTo(e);
            CreateOrUpdateChannel();
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        public void HideLoadingBar()
        {
            LoadingBar.Visibility = Visibility.Collapsed;
        }

        public void ShowLoadingBar()
        {
            LoadingBar.Visibility = Visibility.Visible;
        }


        private void CreateOrUpdateChannel()
        {
            helper.CreateOrUpdateChannelUri();
        }

        private void SendToast_Click(object sender, RoutedEventArgs e)
        {
            ShowLoadingBar();
            helper.SendSimpleToast();
        }

        private void SendToastWithParams_Click(object sender, RoutedEventArgs e)
        {
            ShowLoadingBar();
            helper.SendToastWithParams();
        }

        private void SendTile_Click(object sender, RoutedEventArgs e)
        {
            ShowLoadingBar();
            helper.SendSimpleTile();
        }

        private void UpdateTile()
        {
            // Simulate a long-running task. For illustration purposes only. 
            if (Debugger.IsAttached)
            {
                // Set a larger delay to give you time to select "Suspend" from the "LifetimeEvents" dropdown in Visual Studio in 
                // order to simulate the app being suspended when the new tile is created. 
                Task.Delay(8000).Wait();
            }
            else
            {
                // When the app is not attached to the debugger, the app will be suspended so we can use a 
                // more realistic delay.
                Task.Delay(2000).Wait();
            }
        }
        public async void PinStartSecondaryTile_Click(object sender, RoutedEventArgs e) 
        {
            // Create the original Square150x150 tile. The image to display on the tile has a purple background and the word "Original" in white text.
            tile = new SecondaryTile(AppConstants.tileId, "App42 Push", "/MainPage.xaml?params=value", new Uri("ms-appx:///Assets/Square71x71Logo.scale-240.png"), TileSize.Default);
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            await tile.RequestCreateAsync();
        }

        private void SendTileWithImage_Click(object sender, RoutedEventArgs e)
        {
            ShowLoadingBar();
            helper.SendTileWithImage();
        }

        public void SendSecondaryTile_Click(object sender, RoutedEventArgs e) 
        {

            if (tile == null) 
            {
                new WNSCallback().ShowMessage("ERROR Please Click On Pin Start Secondary Tile Button.", NotifyType.ErrorMessage);
                return;
            }
               
            ShowLoadingBar();
            helper.SendSecondaryTile();
        }

        /// <summary>
        /// Used to display messages to the user
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="type"></param>
        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
                case NotifyType.ErrorMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
            }
            StatusBlock.Text = strMessage;

            // Collapse the StatusBlock if it has no text to conserve real estate.
            if (StatusBlock.Text != String.Empty)
            {
                StatusBorder.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                StatusBorder.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
