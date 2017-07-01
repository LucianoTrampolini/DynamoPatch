using System;
using System.ComponentModel;

using Dynamo.Web;

namespace Dynamo.Boekingssysteem
{
    public class WebIntegrationHelper
    {
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

        public void PushAllAsync()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += DoAllWork;
            bw.RunWorkerAsync();
        }

        public void PushPlanning(DateTime date)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += DoWork;
            bw.RunWorkerAsync(date);
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