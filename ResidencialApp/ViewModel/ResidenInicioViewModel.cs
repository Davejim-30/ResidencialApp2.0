using System;
using System.Collections.Generic;
using System.Text;
using ResidencialApp.Model;
using System.Timers;
using Xamarin.Forms;

namespace ResidencialApp.ViewModel
{
    public class ResidenInicioViewModel
    {
        public ResidenModel ResidenData { get; set; }
        public Command ComandoHora { get; set; }
        public Command ComandoFecha {  get; set; }
        private Timer timer;

        public ResidenInicioViewModel()
        {
            ResidenData = new ResidenModel();
            OnDateEvent(null, null);
            StartTimer();
            ComandoFecha = new Command(() => OnDateEvent(null, null));
            ComandoHora = new Command(() => OnTimedEvent(null, null));
        } 
        private void OnDateEvent(object sender, ElapsedEventArgs e)
        {
            ResidenData.CurrentDate = DateTime.Now.ToString("dddd, d 'de' MMMM");
        }

        private void StartTimer()
        {
            timer = new Timer(1000); // 1000 ms = 1 second
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            ResidenData.CurrentTime = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public void StopTimer()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
