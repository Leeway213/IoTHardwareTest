using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using IoTHardwareTest.Tools;
using IoTHardwareTest.Tools.DeviceOperators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace IoTHardwareTest.Modules.UART.ViewModel
{
    public class UartPageViewModel : ViewModelBase
    {

        public UartPageViewModel()
        {
            SelDevIndex = -1;
            Idle = true;
            DevCollection = new ObservableCollection<DeviceInformation>();
            RestoreDefaultPara();
            GetSerialDevices();
        }

        /// <summary>
        /// Initial default serial parameters
        /// </summary>
        private void RestoreDefaultPara()
        {
            BaudRate = 115200;
            DataBits = 8;
            BreakSignalState = false;
            Handshake = SerialHandshake.None;
            IsDataTerminalReadyEnabled = true;
            IsRequestToSendEnabled = true;
            Parity = SerialParity.None;
            ReadTimeout = TimeSpan.FromMilliseconds(1000);
            WriteTimeout = TimeSpan.FromMilliseconds(1000);
            StopBits = SerialStopBitCount.One;
        }

        public RelayCommand SelectionChangedCmd
        {
            get
            {
                return new RelayCommand(async () =>
               {
                   if (ComPortDevice.IsConnected)
                       ComPortDevice.Disconnect();
                   await ComPortDevice.Connect(SelectedDev.Id);
               });
            }
        }

        public RelayCommand ListenCmd
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ComPortDevice.SetParam(BaudRate, nameof(BaudRate));
                    ComPortDevice.SetParam(IsDataTerminalReadyEnabled, nameof(IsDataTerminalReadyEnabled));
                    ComPortDevice.SetParam(DataBits, nameof(DataBits));
                    ComPortDevice.SetParam(StopBits, nameof(StopBits));
                    ComPortDevice.SetParam(IsRequestToSendEnabled, nameof(IsRequestToSendEnabled));
                    ComPortDevice.SetParam(BreakSignalState, nameof(BreakSignalState));
                    ComPortDevice.SetParam(Handshake, nameof(Handshake));
                    ComPortDevice.SetParam(Parity, nameof(Parity));
                    ComPortDevice.SetParam(ReadTimeout, nameof(ReadTimeout));
                    ComPortDevice.SetParam(WriteTimeout, nameof(WriteTimeout));

                    ComPortDevice.ListenStateChange += ComPortDevice_ListenStateChange;

                    ComPortDevice.Listen();
                });
            }
        }

        private void ComPortDevice_ListenStateChange()
        {
            Idle = !ComPortDevice.IsListening;
        }

        public RelayCommand StopListenCmd
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (ComPortDevice.IsListening)
                        ComPortDevice.StopListen();
                });
            }
        }

        private async void GetSerialDevices()
        {
            try
            {
                var devs = await ComPortDevice.ScanSerialDevices();
                foreach (var item in devs)
                {
                    DevCollection.Add(item);
                }
                if (DevCollection.Count > 0)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        //Sleep 100ms to wait all property & command binding
                        Task.Delay(100).Wait();
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            SelDevIndex = 0;
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                GlobalMethod.ShowMsg(ex.Message, MainFrame.ViewModel.MsgType.Exception);
            }
        }

        public ObservableCollection<DeviceInformation> DevCollection { get; set; }

        public DeviceInformation SelectedDev
        {
            get
            {
                return DevCollection.Count > 0 && SelDevIndex > -1 ? DevCollection[SelDevIndex] : null;
            }
        }

        private int _selDevIndex;

        public int SelDevIndex
        {
            get { return _selDevIndex; }
            set
            {
                Set(ref _selDevIndex, value);
                RaisePropertyChanged("SelectedDev");
            }
        }

        private UInt32 _baudrate;

        public UInt32 BaudRate
        {
            get { return _baudrate; }
            set
            {
                Set(ref _baudrate, value);
            }
        }

        private bool _idle;

        public bool Idle
        {
            get { return _idle; }
            set { Set(ref _idle, value); }
        }

        private UInt16 _databits;

        public UInt16 DataBits
        {
            get { return _databits; }
            set
            {
                Set(ref _databits, value);
            }
        }

        private Boolean _breaksignalstate;

        public Boolean BreakSignalState
        {
            get { return _breaksignalstate; }
            set
            {
                Set(ref _breaksignalstate, value);
            }
        }

        private SerialHandshake _handshake;

        public SerialHandshake Handshake
        {
            get { return _handshake; }
            set
            {
                Set(ref _handshake, value);
            }
        }

        private Boolean _isDataTerminalReadyEnabled;

        public Boolean IsDataTerminalReadyEnabled
        {
            get { return _isDataTerminalReadyEnabled; }
            set
            {
                Set(ref _isDataTerminalReadyEnabled, value);
            }
        }

        private Boolean _isRequestToSendEnabled;

        public Boolean IsRequestToSendEnabled
        {
            get { return _isRequestToSendEnabled; }
            set
            {
                Set(ref _isRequestToSendEnabled, value);
            }
        }

        private SerialParity _parity;

        public SerialParity Parity
        {
            get { return _parity; }
            set
            {
                Set(ref _parity, value);
            }
        }

        private TimeSpan _readTimeout;

        public TimeSpan ReadTimeout
        {
            get { return _readTimeout; }
            set
            {
                Set(ref _readTimeout, value);
            }
        }

        private SerialStopBitCount _stopBits;

        public SerialStopBitCount StopBits
        {
            get { return _stopBits; }
            set
            {
                Set(ref _stopBits, value);
            }
        }

        private TimeSpan _writeTimeout;

        public TimeSpan WriteTimeout
        {
            get { return _writeTimeout; }
            set
            {
                Set(ref _writeTimeout, value);
            }
        }

    }
}
