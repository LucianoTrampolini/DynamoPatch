using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Notifier
{
    public class NotificationPollerEventArgs:EventArgs
    {
        public string Message = "";
        public bool UpdateWebSite = false;
    }
}
