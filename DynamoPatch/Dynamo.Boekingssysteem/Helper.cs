using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

using Dynamo.Boekingssysteem.Controls.Melding;
using Dynamo.Boekingssysteem.View;
using Dynamo.Model;

using Hardcodet.Wpf.TaskbarNotification;

namespace Dynamo.Boekingssysteem
{
    /// <summary>
    /// Class die bedoelt is om user interface dingetjes te kunnen regelen zodat dit niet allemaal in het viewmodel hoeft...
    /// </summary>
    public static class Helper
    {
        #region Static fields and Constants

        const int RedShift = 0;
        const int GreenShift = 8;
        const int BlueShift = 16;

        private static FancyBalloon _balloon;
        private static bool _balloonShowing;
        private static DispatcherTimer _timer;
        private static readonly Queue<string> _messageQueue = new Queue<string>();

        private static TaskbarIcon _taskbarIcon;

        private static DateTime _nextToolTip = DateTime.MinValue;

        #endregion

        //TODO threadsafe maken...
        public static Beheerder CurrentBeheerder { get; set; }

        public static MeldingHandler MeldingHandler { get; set; }

        public static TaskbarIcon TaskbarIcon
        {
            get { return _taskbarIcon; }
            set { _taskbarIcon = value; }
        }

        private static Queue<string> messageQueue
        {
            get
            {
                lock (_messageQueue)
                {
                    return _messageQueue;
                }
            }
        }

        public static void AddBalloonMessage(string message)
        {
            if (messageQueue.Contains(message)
                || (_balloonShowing && _balloon != null && _balloon.BalloonText.Equals(message)))
            {
                return;
            }

            messageQueue.Enqueue(message);

            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += _timer_Elapsed;
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Start();
            }
            _timer.IsEnabled = true;
        }

        public static Color GetColorFromInt(int oleColor)
        {
            return Color.FromRgb(
                (byte)((oleColor >> RedShift) & 0xFF),
                (byte)((oleColor >> GreenShift) & 0xFF),
                (byte)((oleColor >> BlueShift) & 0xFF)
                );
        }

        public static void PlayBeheerderMuziekje()
        {
            if (Application.Current != null
                && CurrentBeheerder != null)
            {
                switch (CurrentBeheerder.Naam)
                {
                    case "Queenie":
                        Assembly a = Assembly.GetExecutingAssembly();
                        Stream s = a.GetManifestResourceStream("Dynamo.Boekingssysteem.EasterEggs.Queenie.wav");
                        SoundPlayer player = new SoundPlayer(s);
                        player.Play();
                        break;
                    case "John":
                        Assembly a2 = Assembly.GetExecutingAssembly();
                        Stream s2 = a2.GetManifestResourceStream("Dynamo.Boekingssysteem.EasterEggs.John.wav");
                        SoundPlayer player2 = new SoundPlayer(s2);
                        player2.Play();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void SetColors()
        {
            if (CurrentBeheerder != null
                && Application.Current != null)
            {
                Application.Current.Resources["KleurAchtergrond"] =
                    new LinearGradientBrush(
                        GetColorFromInt((CurrentBeheerder.KleurAchtergrond)),
                        GetColorFromInt((CurrentBeheerder.KleurTekst)),
                        new Point(0.5, 0.5),
                        new Point(2, 2));
                Application.Current.Resources["KleurTekst"] =
                    new SolidColorBrush(GetColorFromInt((CurrentBeheerder.KleurTekst)));
                Application.Current.Resources["KleurAchtergrondVelden"] =
                    new LinearGradientBrush(
                        GetColorFromInt((CurrentBeheerder.KleurAchtergrondVelden)),
                        GetColorFromInt((CurrentBeheerder.KleurTekstVelden)),
                        new Point(0.5, 1),
                        new Point(3, 3));
                Application.Current.Resources["KleurTekstVelden"] =
                    new SolidColorBrush(GetColorFromInt((CurrentBeheerder.KleurTekstVelden)));
                Application.Current.Resources["KleurKnoppen"] =
                    new SolidColorBrush(GetColorFromInt((CurrentBeheerder.KleurKnoppen)));
                Application.Current.Resources["KleurSpecialeKnoppen"] =
                    new LinearGradientBrush(
                        GetColorFromInt((CurrentBeheerder.KleurAchtergrondVelden)),
                        GetColorFromInt(CurrentBeheerder.KleurTekstVelden),
                        new Point(0.5, 0),
                        new Point(2, 1));
                Application.Current.Resources["KleurTekstKnoppen"] =
                    new SolidColorBrush(GetColorFromInt((CurrentBeheerder.KleurTekstKnoppen)));
                Application.Current.Resources["KleurSelecteren"] =
                    new SolidColorBrush(GetColorFromInt((CurrentBeheerder.KleurSelecteren)));
                Application.Current.Resources["Brush_HeaderBackground"] =
                    new LinearGradientBrush(
                        GetColorFromInt((CurrentBeheerder.KleurKnoppen)),
                        GetColorFromInt(CurrentBeheerder.KleurTekstKnoppen),
                        new Point(0.5, 0),
                        new Point(0.5, 1));
            }
        }

        public static int ToOle(Color wpfColor)
        {
            return wpfColor.R << RedShift | wpfColor.G << GreenShift | wpfColor.B << BlueShift;
        }

        private static void _timer_Elapsed(object sender, EventArgs e)
        {
            if (TaskbarIcon != null
                && messageQueue.Count > 0
                && _balloonShowing == false)
            {
                //ShowMessage();
                ShowBalloon();
            }
            if (messageQueue.Count == 0)
            {
                _timer.IsEnabled = false;
            }
        }

        private static void ShowBalloon()
        {
            _balloon = new FancyBalloon();

            _balloon.BalloonText = messageQueue.Dequeue();
            _balloon.Closed += (s, e) => { _balloonShowing = false; };
            TaskbarIcon.ShowCustomBalloon(_balloon, PopupAnimation.Slide, null);
            _balloonShowing = true;
        }

        private static void ShowMessage()
        {
            _balloonShowing = true;
            MeldingHandler.ShowMeldingOk(messageQueue.Dequeue());
            _balloonShowing = false;
        }
    }
}