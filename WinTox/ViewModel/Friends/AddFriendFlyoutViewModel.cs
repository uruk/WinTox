﻿using System;
using SharpTox.Core;
using WinTox.Common;
using WinTox.Helpers;
using WinTox.Model;

namespace WinTox.ViewModel.Friends
{
    public class AddFriendFlyoutViewModel : ObservableObject
    {
        private RelayCommand _addFriendCommand;
        private string _friendId;
        private string _friendIdPlaceholder;
        private string _invitationMessage;
        private bool _isFlyoutClosed;

        public bool IsFlyoutClosed
        {
            get { return _isFlyoutClosed; }
            set
            {
                if (value == _isFlyoutClosed)
                    return;
                _isFlyoutClosed = value;
                RaisePropertyChanged();
                if (value)
                    IsFlyoutClosed = false;
            }
        }

        public string FriendId
        {
            get { return _friendId; }
            set
            {
                if (value == _friendId)
                    return;
                _friendId = value.Trim();
                RaisePropertyChanged();
            }
        }

        public string FriendIdPlaceholder
        {
            get { return _friendIdPlaceholder; }
            private set
            {
                if (value == _friendIdPlaceholder)
                    return;
                _friendIdPlaceholder = value;
                RaisePropertyChanged();
            }
        }

        public string InvitationMessage
        {
            get { return _invitationMessage; }
            set
            {
                if (value == _invitationMessage)
                    return;
                _invitationMessage = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddFriendCommand
        {
            get
            {
                return _addFriendCommand
                       ?? (_addFriendCommand = new RelayCommand(
                           parameter =>
                           {
                               if (String.IsNullOrEmpty(FriendId))
                                   return;

                               bool successfulDnsDiscovery;
                               var discoveredToxId = DnsTools.TryDiscoverToxId(FriendId, out successfulDnsDiscovery);

                               if (successfulDnsDiscovery)
                               {
                                   FriendId = discoveredToxId;
                               }
                               else
                               {
                                   if (!ToxId.IsValid(FriendId))
                                   {
                                       FriendId = String.Empty;
                                       FriendIdPlaceholder = "Invalid Tox ID, please enter it more carefully!";
                                       return;
                                   }

                                   var invitationMessage = GetInvitationMessage();

                                   bool successFulAdd;
                                   ToxModel.Instance.AddFriend(new ToxId(FriendId), invitationMessage, out successFulAdd);

                                   if (successFulAdd)
                                   {
                                       IsFlyoutClosed = true;
                                   }
                               }
                           }));
            }
        }

        public void ResetFlyout()
        {
            FriendId = String.Empty;
            FriendIdPlaceholder = String.Empty;
            InvitationMessage = String.Empty;
        }

        private string GetInvitationMessage()
        {
            if (String.IsNullOrEmpty(InvitationMessage))
                return "Hello! I'd like to add you to my friends list.";
            return InvitationMessage;
        }
    }
}