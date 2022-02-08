using System;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Attributes
    {
        private string _notificationTokens;
        
        public string NotificationTokens { get => _notificationTokens; set => _notificationTokens = value; }
    }
}
