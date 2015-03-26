using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LAB2ProgressBar.Services;
using LAB2ProgressBar.Helpers;
using System.Windows.Input;

namespace LAB2ProgressBar.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private int _progress;
        public int Progress
        {
            get
            {
                return _progress;
            }

            set
            {
                if (value != _progress)
                {
                    _progress = value > 100 ? 100 : (value < 0 ? 0 : value);
                    RaisePropertyChanged(() => Progress);
                }
            }
        }

        private ICommand _startProgressCommand;
        public ICommand StartProgressCommand
        {
            get
            {
                return _startProgressCommand;
            }

            private set { }
        }

        private ICommand _cancelProgressCommand;
        public ICommand CancelProgressCommand
        {
            get
            {
                return _cancelProgressCommand;
            }

            private set { }
        }

        private ICommand _resetProgressesCommand;
        public ICommand ResetProgressesCommand
        {
            get
            {
                return _resetProgressesCommand;
            }

            private set { }
        }

        private BackgroundService _backgroundService;
        private bool _isRunning = false;

        public MainWindowViewModel()
        {
            InitializeService();
            InitializeCommands();
        }

        private void InitializeService()
        {
            _backgroundService = new BackgroundService(500); // 1000ms = 1s
            _backgroundService.Tick += _backgroundService_Tick;
        }

        private void InitializeCommands()
        {
            _startProgressCommand = new DelegateCommand(StartProgressExecute, StartProgressCanExecute);
            _cancelProgressCommand = new DelegateCommand(CancelProgressExecute, CancelProgressCanExecute);
            _resetProgressesCommand = new DelegateCommand(ResetProgressesExecute);
        }

        private void StartProgressExecute()
        {
            if (!_backgroundService.IsRunning)
            {
                _backgroundService.RunServiceAsync();
                _isRunning = true;
            }
        }

        private bool StartProgressCanExecute()
        {
            return !_isRunning;
        }

        private void CancelProgressExecute()
        {
            if (_backgroundService.IsRunning)
            {
                _backgroundService.CancelServiceAsync();
                _isRunning = false;
            }
        }

        private bool CancelProgressCanExecute()
        {
            return _isRunning;
        }

        private void ResetProgressesExecute()
        {
            Progress = 0;
        }

        private void _backgroundService_Tick(object sender, int tick)
        {
            Progress++;
        }
    }
}
