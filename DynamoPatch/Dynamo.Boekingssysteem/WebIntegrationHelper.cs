using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Web;
using System.ComponentModel;

namespace Dynamo.Boekingssysteem
{
    public class WebIntegrationHelper
    {
        public void PushPlanning(DateTime date)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += DoWork;
            bw.RunWorkerAsync(date);
        }

        public void PushAllAsync()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += DoAllWork;
            bw.RunWorkerAsync();
        }

        public void PushAll()
        {
            DateTime date = DateTime.Today;

            while (date < DateTime.Today.AddDays(120))
            {
                new Integration().PushWeekDataToServer(date);
                date = date.AddDays(7);
            }
            new Integration().PushLastUpdateFile();
        }

        private void DoAllWork(object sender, DoWorkEventArgs e)
        {
            PushAll();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var date = (DateTime)e.Argument;
            new Integration().PushWeekDataToServer(date);
            new Integration().PushLastUpdateFile();
        }
    }
}
