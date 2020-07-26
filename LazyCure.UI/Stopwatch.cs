using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    public partial class Stopwatch : UserControl
    {
        public Stopwatch()
        {
            InitializeComponent();
            ShowTime();
            Start();
        }

        private DateTime _lastUpdate = DateTime.MinValue;

        private TimeSpan _value = TimeSpan.Zero;

        public void Start()
        {
            _lastUpdate = DateTime.Now;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            UpdateTime();
        }

        public void Reset()
        {
            _value = TimeSpan.Zero;
            ShowTime();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            var now = DateTime.Now;
            _value += (now - _lastUpdate);
            _lastUpdate = now;

            ShowTime();
        }

        private void ShowTime()
        {
            var hours = (int)Math.Floor(_value.TotalHours);


            _labelDisplay.Text = $"{hours:#00} : {_value.Minutes:00} : {_value.Seconds:00} . {_value.Milliseconds/100:0}";
        }
    }
}
