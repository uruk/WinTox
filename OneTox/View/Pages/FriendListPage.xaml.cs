﻿using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneTox.View.Pages
{
    public sealed partial class FriendListPage : Page
    {
        public FriendListPage()
        {
            InitializeComponent();

            DataContext = (Application.Current as App).MainViewModel;
        }

        private void FriendListPageLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += WindowSizeChanged;
        }

        private void FriendListPageUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= WindowSizeChanged;
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width >= 930)
            {
                Frame.Navigate(typeof (MainPage));
            }
        }

        private void FriendListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FriendList.SelectedItem == null)
                return;

            Frame.Navigate(typeof (ChatPage), FriendList.SelectedItem);
        }

        private void AddFriendButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (AddFriendPage));
        }

        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (SettingsPage));
        }
    }
}