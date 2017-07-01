using System;

namespace Dynamo.Notifier
{
    public class NotificationPollerEventArgs : EventArgs
    {
        #region Member fields

        public string Message = "";
        public bool UpdateWebSite = false;

        #endregion
    }
}