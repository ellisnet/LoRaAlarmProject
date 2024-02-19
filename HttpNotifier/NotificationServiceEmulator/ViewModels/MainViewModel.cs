using NotificationServiceEmulator.Models;
using System;

#pragma warning disable CS1591
#pragma warning disable CS8601

namespace NotificationServiceEmulator.ViewModels
{
    public class MainViewModel : SimpleViewModel
    {
        #region Bindable properties

        private string _messageLog = string.Empty;
        public string MessageLog
        {
            get => _messageLog;
            set => SetProperty(ref _messageLog, (value ?? string.Empty));
        }

        #endregion

        #region Commands and their implementations

        #endregion

        public MainViewModel()
        {
            //Only process logic if not in design mode
            if (!IsDesignMode(true))
            {
                MessagingSubscribe<NotificationMessage>(this, nameof(NotificationMessage), message =>
                {
                    var messageAdd = (string.IsNullOrWhiteSpace(MessageLog) ? string.Empty : "\n")
                                     + $"{DateTime.Now:s} - {message.Message}";
                    MessageLog += messageAdd;
                });

                MessageLog += $"The API can be explored via 'http://localhost:{App.ApiHostingPort}/swagger/index.html'\n";
            }
        }

        public override void Dispose()
        {
            MessagingUnsubscribe<NotificationMessage>(this, nameof(NotificationMessage));
            base.Dispose();
        }
    }
}
