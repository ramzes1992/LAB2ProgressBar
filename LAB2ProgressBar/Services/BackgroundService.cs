using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LAB2ProgressBar.Services
{
    public class BackgroundService
    {
        private BackgroundWorker _worker;

        private int _ticks = 0;

        private int _sleepTime;

        public BackgroundService(int sleepTime)
        {
            this._sleepTime = sleepTime;
            InitializeWorker();
        }

        private void InitializeWorker()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!_worker.CancellationPending)
            {
                Thread.Sleep(_sleepTime);
                RaiseTickEvent();
            }
        }

        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        public event TickEventHandler Tick;
        public delegate void TickEventHandler(object sender, int tick);

        public void RunServiceAsync()
        {
            if (_worker != null
                && !_worker.IsBusy)
            {
                _worker.RunWorkerAsync();
            }
        }

        public void CancelServiceAsync()
        {
            if (_worker != null)
            {
                _worker.CancelAsync();
            }
        }

        public bool IsRunning
        {
            get
            {
                return (_worker != null) ? _worker.IsBusy : false;
            }
        }

        private void RaiseTickEvent()
        {
            var copy = Tick;
            if (Tick != null)
            {
                copy(this, _ticks++);
            }
        }
    }
}
