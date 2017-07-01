using System;
using System.Windows.Threading;
using Dynamo.BL;

namespace Dynamo.Notifier
{
    public class NotificationPoller : IDisposable
    {
        private DispatcherTimer _timer;
        private DateTime _volgendeCheck = DateTime.Today.AddHours(14);
        private DateTime _vorigeCheck = DateTime.Now;
        private BeheerderRepository _beheerderRepository;
        private DateTime _volgendeWebSiteUpdate = DateTime.Now.AddMinutes(15);

        public EventHandler<NotificationPollerEventArgs> OnNotify;
        public bool NieuweBeheerderAangemeld = false;

        public NotificationPoller()
        {
            if (DateTime.Now.Hour < 19)
            {
                _volgendeCheck = DateTime.Today.AddHours(14);
            }
            else
            {
                _volgendeCheck = DateTime.Today.AddHours(19);
            }

            _beheerderRepository = new BeheerderRepository();
            _timer = new DispatcherTimer();
            _timer.Tick +=new EventHandler(_timer_Tick);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
        }

        private bool _bandWaarschuwingBetalingen = true;
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (IsTijdVoorWebSiteUpdate())
            {
                OnNotify(this, new NotificationPollerEventArgs { UpdateWebSite = true });
            }

            if (IsTijdOmBandWaarschuwingTeGeven())
            {
                if(NieuweBeheerderAangemeld)
                {
                    CheckBerichten();
                }
                NieuweBeheerderAangemeld = false;
                if (_bandWaarschuwingBetalingen)
                {
                    _bandWaarschuwingBetalingen = false;
                    CheckBetalingen();
                }
            }
            else 
            {
                _bandWaarschuwingBetalingen = true;
            }

            if (_volgendeCheck < DateTime.Now && DateTime.Now.Hour > 13)
            {
                var huidigDagdeel = DateTime.Now.Hour < 19 ? 2 : 3;

                if (!_beheerderRepository.IsBeheerderIngelogdVoorDagDeel(huidigDagdeel))
                {
                    if (OnNotify != null)
                    {
                        OnNotify(this, new NotificationPollerEventArgs { Message = "Er is nog geen beheerder aangemeld voor dit dagdeel!" });

                        if (_volgendeCheck.AddMinutes(3) > DateTime.Now)
                        {
                            _volgendeCheck = _volgendeCheck.AddMinutes(2);
                        }
                        else
                        {
                            _volgendeCheck = DateTime.Now.AddMinutes(2);
                        }
                    }
                }
            }
        }

        private void CheckBetalingen()
        {
            var dagdeelId = DateTime.Now.Hour <= 18 ? 2 : 3;
            var l = new BandRepository().GetOpenstaandeBedragenVoorDagdeel(dagdeelId);
            if (l.Count > 0)
            {
                var maxBedrag = new InstellingRepository().Load(1);
                foreach (var item in l)
                {
                    if (item.Bedrag > maxBedrag.BedragBandWaarschuwing)
                    {
                        if (OnNotify != null)
                        {
                            OnNotify(this, new NotificationPollerEventArgs { Message = string.Format("Let op! {0} heeft nog een openstaand bedrag van {1:C}", item.BandNaam, item.Bedrag)});
                        }
                    }
                }
            }
        }

        private void CheckBerichten()
        {
            var aantalNieuweberichten = _beheerderRepository.GetAantalNieuweBerichten();
            if (aantalNieuweberichten > 0)
            {
                if (OnNotify != null)
                {
                    if (aantalNieuweberichten == 1)
                    {
                        OnNotify(this, new NotificationPollerEventArgs { Message = string.Format("Je hebt {0} nieuw bericht.", aantalNieuweberichten) });
                    }
                    else
                    {
                        OnNotify(this, new NotificationPollerEventArgs { Message = string.Format("Je hebt {0} nieuwe berichten.", aantalNieuweberichten) });
                    }
                }
            }
        }

        private bool IsTijdVoorWebSiteUpdate()
        {
            if (_volgendeWebSiteUpdate < DateTime.Now)
            {
                _volgendeWebSiteUpdate = DateTime.Now.AddMinutes(15);
                return true;
            }
            return false;
        }

        private bool IsTijdOmBandWaarschuwingTeGeven()
        {
            return NieuweBeheerderAangemeld || ((DateTime.Now.Hour == 17 || DateTime.Now.Hour == 22) && DateTime.Now.Minute > 30);
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
            _beheerderRepository.Dispose();
        }
    }
}
