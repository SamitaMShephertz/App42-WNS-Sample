using com.shephertz.app42.paas.sdk.windows;
using com.shephertz.app42.paas.sdk.windows.push;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Networking.Connectivity;
using Windows.Networking.PushNotifications;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App42_WNS_Sample
{
    public class AppHelper
    {
        // Initializing PushNotification Service.
        PushNotificationService pushNotificationService = null;

        public AppHelper() {
            // Initializing App42.
            App42API.Initialize(AppConstants.apiKey, AppConstants.secretKey);
            //App42API.SetBaseURL("http://", "192.168.1.85", 8082);
            // Initializing PushNotification Service.
            pushNotificationService = App42API.BuildPushNotificationService();
        }
        String _channelUri;

        /// <summary>
        /// Get or set the value of ChannelUri
        /// </summary>
        public string ChannelUri
        {
            get { return _channelUri; }
            set { _channelUri = value; }
        }

        PushNotificationChannel channel = null;

        /// <summary>
        /// Creating Channel Uri.
        /// </summary>
        public async void CreateOrUpdateChannelUri()
        {
            try
            {
                var vProfile = NetworkInformation.GetInternetConnectionProfile();
                if (vProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                {
                    channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                    ChannelUri = channel.Uri;
                    System.Diagnostics.Debug.WriteLine("Channel Uri :: " + ChannelUri);
                    CheckIfNewUri();
                    channel.PushNotificationReceived += OnPushNotification;
                }
            }

            catch (Exception ex)
            {
                // Could not create a channel. 
                // Error codes are explained on msdn https://msdn.microsoft.com/en-us/library/windows/apps/windows.networking.pushnotifications.pushnotificationchannelmanager.createpushnotificationchannelforapplicationasync.aspx
                System.Diagnostics.Debug.WriteLine("Error while creating channel :: " + ex.ToString());
                new WNSCallback().HideLoading();
                string errorMessage = "Error requesting channel uri : " + ex.ToString();
                new WNSCallback().ShowMessage(errorMessage, NotifyType.ErrorMessage);

            }
        }

        void CheckIfNewUri()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(AppConstants.channelUriKey))
            {
                string savedUri = (string)localSettings.Values[AppConstants.channelUriKey];
                if (!savedUri.Equals(ChannelUri))
                {
                    // Sending Channel Uri to app42 cloud service.
                    SendUriToCloudService();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Channel URI is same. No need to send it to cloud service.");
                    new WNSCallback().HideLoading();
                }
            }
            else
            {
                // Sending Channel Uri to app42 cloud service.
                SendUriToCloudService();
            }
        }

        void SendUriToCloudService() {
            pushNotificationService.StoreDeviceToken(AppConstants.userName, ChannelUri, new WNSCallback());
        }

        public void SendSimpleToast()
        {
            pushNotificationService.SendPushToastMessageToUser(AppConstants.userName, AppConstants.toastTitle, AppConstants.toastContent, null, new WNSPushCallback());
        }

        public void SendSecondaryTile() 
        {
            string messageJson = "{'tileId': '" + AppConstants .tileId+ "','alert': 'Hello from App42 Cloud','squareTemplate': 'TileSquarePeekImageAndText04','squareText1': 'squareText1','squareText2': 'squareText2','wideTemplate': 'TileWideImageAndText02','wideText1': 'wideText1','squareImage': 'http://cdn.shephertz.com/repository/files/adbd07f85163529af91bf167fb7f03de42389df2e01a67620a73c60acc109412/25214a4ed10bd2ca7919707fd679305578316f17/egrhgj.jpg','wideImage': 'http://cdn.shephertz.com/repository/files/adbd07f85163529af91bf167fb7f03de42389df2e01a67620a73c60acc109412/25214a4ed10bd2ca7919707fd679305578316f17/egrhgj.jpg','wideText2': 'wideText2','_App42Convert': true,'secondaryTile': 'true'}";
            pushNotificationService.SendPushMessageToUser(AppConstants.userName, messageJson, new WNSPushCallback());
        }

        public void SendToastWithParams()
        {
            Dictionary<string, object> toastParams = new Dictionary<string, object>();
            toastParams.Add("param1", "Value1");
            toastParams.Add("param2", true);
            toastParams.Add("param3", 1000);
            pushNotificationService.SendPushToastMessageToUser(AppConstants.userName, AppConstants.toastTitle, AppConstants.toastContent, toastParams, new WNSPushCallback());
        }

        public void SendSimpleTile()
        {
            Tile tile = new Tile();
            tile.Title = AppConstants.tileTitle;
            tile.Content = AppConstants.tileContent;
            tile.BadgeCount = AppConstants.badgeCount;
            pushNotificationService.SendPushTileMessageToUser(AppConstants.userName, tile, new WNSPushCallback());
        }

        public void SendTileWithImage()
        {
            Tile tile = new Tile();
            tile.Title = AppConstants.tileTitle;
            tile.Content = AppConstants.tileContent;
            tile.BadgeCount = AppConstants.badgeCount;
            tile.BackgroundImage = AppConstants.tileBGImage;
            pushNotificationService.SendPushTileMessageToUser(AppConstants.userName, tile, new WNSPushCallback());
        }


        /// <summary>
        /// Reciever for WNS notifiations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            String notificationContent = String.Empty;
            System.Diagnostics.Debug.WriteLine(":::OnPushNotification:::");
            switch (e.NotificationType)
            {
                case PushNotificationType.Badge:
                    notificationContent = e.BadgeNotification.Content.GetXml();
                    System.Diagnostics.Debug.WriteLine("Badge recieved :: " + notificationContent);
                    BadgeNotification badge = new BadgeNotification(e.BadgeNotification.Content);
                    BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
                    break;

                case PushNotificationType.Tile:
                    Windows.Data.Xml.Dom.IXmlNode tileNode = e.TileNotification.Content.ChildNodes[0].FirstChild;
                    System.Diagnostics.Debug.WriteLine("tileNode :: " + tileNode);
                    string tileType = tileNode.InnerText;
                    System.Diagnostics.Debug.WriteLine("tile type recieved :: " + tileType);
                    if (tileType == null || tileType.Equals(""))
                    {
                         TileNotification tileNotification = new TileNotification(e.TileNotification.Content);
                         TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
                    }
                    else if (tileType !=null && tileType.Equals("tile")) 
                    {
                        //e.TileNotification.Content.RemoveChild(tileNode);
                        TileNotification tileNotification = new TileNotification(e.TileNotification.Content);
                        TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
                    }
                    else if (tileType != null && tileType.Equals("secondaryTile"))
                    {
                        //e.TileNotification.Content.RemoveChild(tileNode);
                        string tileIdNode = "App";
                        if (e.TileNotification.Content.ChildNodes[0].ChildNodes.Count >= 2) 
                        {
                            tileIdNode = e.TileNotification.Content.ChildNodes[0].ChildNodes[1].InnerText;
                        }

                        System.Diagnostics.Debug.WriteLine("tile id: " + tileIdNode + " secondary tile recieved :: " + e.TileNotification.Content.GetXml());
                        TileNotification tileNotification = new TileNotification(e.TileNotification.Content);
                        TileUpdateManager.CreateTileUpdaterForSecondaryTile(tileIdNode).Update(tileNotification);
                    }
                    else 
                    {
                        System.Diagnostics.Debug.WriteLine("Unknown tile type recieved :: " + e.TileNotification.Content.GetXml());
                        //e.TileNotification.Content.RemoveChild(tileNode);
                        TileNotification tileNotification = new TileNotification(e.TileNotification.Content);
                        TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
                    }
                 break;
                case PushNotificationType.Toast:
                    notificationContent = e.ToastNotification.Content.GetXml();
                    System.Diagnostics.Debug.WriteLine("Toast recieved :: " + notificationContent);
                    ToastNotification toast = new ToastNotification(e.ToastNotification.Content);
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                    break;
                case PushNotificationType.Raw:
                    notificationContent = e.RawNotification.Content;
                    System.Diagnostics.Debug.WriteLine("Raw recieved :: " + notificationContent);
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Unknown notification type recieved :: " + e.NotificationType);
                    break;

            }
            e.Cancel = true;
        }
    }
    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
}
