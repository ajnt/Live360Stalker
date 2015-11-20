﻿using Windows.UI.Core;
using Windows.UI.Xaml;
using Tweetinvi;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Examplinvi.UniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Auth.SetUserCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");

            if (Auth.Credentials == null ||
                string.IsNullOrEmpty(Auth.Credentials.ConsumerKey) ||
                string.IsNullOrEmpty(Auth.Credentials.ConsumerSecret) || 
                string.IsNullOrEmpty(Auth.Credentials.AccessToken) ||
                string.IsNullOrEmpty(Auth.Credentials.AccessTokenSecret) ||
                Auth.Credentials.AccessToken == "ACCESS_TOKEN")
            {
                Message.Text = "Please enter your credentials in the MainPage.xaml.cs file";
            }
            else
            {
                var user = User.GetLoggedUser();
                Message.Text = string.Format("Hi '{0}'. Welcome on board with Windows 10 Universal App!", user.Name);

                RunSampleStream();
            }
        }

        private void RunSampleStream()
        {
            var uiDispatcher = Dispatcher;
            var s = Stream.CreateSampleStream();

            s.TweetReceived += (o, args) =>
            {
                uiDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Message.Text = args.Tweet.ToString();
                });
            };

            s.StartStreamAsync();
        }

    }
}