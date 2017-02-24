using GalaSoft.MvvmLight;
using IoTHardwareTest.Tools;
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
        private SerialDevice serialPort;

        private CancellationTokenSource readCts;

        public UartPageViewModel()
        {
            SelDevIndex = -1;
            NotConnected = true;
            DevCollection = new ObservableCollection<DeviceInformation>();
            ScanSerialDevice();
            RestoreDefaultPara();
        }

        /// <summary>
        /// Initial default serial parameters
        /// </summary>
        private void RestoreDefaultPara()
        {
            BaudRate = 9600;
            DataBits = 16;
            BreakSignalState = false;
            HandShake = SerialHandshake.None;
            IsDataTerminalReadyEnabled = false;
            IsRequestToSendEnabled = false;
            Parity = SerialParity.None;
            ReadTimeout = TimeSpan.FromMilliseconds(1000);
            WriteTimeout = TimeSpan.FromMilliseconds(1000);
            StopBit = SerialStopBitCount.One;
        }

        private async void Connect()
        {
            try
            {
                serialPort = await SerialDevice.FromIdAsync(SelectedDev.Id);
                if (serialPort != null)
                {
                    NotConnected = false;
                    GlobalMethod.ShowMsg("Connected to serial device:" + "\n" + SelectedDev.Name + "\n" + SelectedDev.Id, MainFrame.ViewModel.MsgType.Info);
                    serialPort.BaudRate = BaudRate;
                    serialPort.BreakSignalState = BreakSignalState;
                    serialPort.DataBits = DataBits;
                    serialPort.Handshake = HandShake;
                    serialPort.IsDataTerminalReadyEnabled = IsDataTerminalReadyEnabled;
                    serialPort.IsRequestToSendEnabled = IsRequestToSendEnabled;
                    serialPort.Parity = Parity;
                    serialPort.ReadTimeout = ReadTimeout;
                    serialPort.StopBits = StopBit;
                    serialPort.WriteTimeout = WriteTimeout;

                    readCts = new CancellationTokenSource();

                    Listen();
                }
                else
                    GlobalMethod.ShowMsg("Cannot get serial device:" + "\n" + SelectedDev.Name + "\n" + SelectedDev.Id, MainFrame.ViewModel.MsgType.Error);
            }
            catch (Exception ex)
            {
                GlobalMethod.ShowMsg(ex.Message, MainFrame.ViewModel.MsgType.Exception);
            }
        }

        private void Disconnect()
        {
            if (serialPort != null)
            {
                serialPort.Dispose();
                serialPort = null;
                //RestoreDefaultPara();
            }
        }

        private void CancelReadTask()
        {
            if (readCts != null)
            {
                if (!readCts.IsCancellationRequested)
                {
                    readCts.Cancel();
                }
            }
        }

        private void Listen()
        {
        }

        private async void ScanSerialDevice()
        {
            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var devs = await DeviceInformation.FindAllAsync(aqs);
                foreach (var item in devs)
                {
                    DevCollection.Add(item);
                }
                if (DevCollection.Count > 0)
                {
                    SelDevIndex = 0;
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
            set { Set(ref _baudrate, value); }
        }

        private bool _notConnected;

        public bool NotConnected
        {
            get { return _notConnected; }
            set { Set(ref _notConnected, value); }
        }

        private UInt16 _databits;

        public UInt16 DataBits
        {
            get { return _databits; }
            set { Set(ref _databits, value); }
        }

        private Boolean _breaksignalstate;

        public Boolean BreakSignalState
        {
            get { return _breaksignalstate; }
            set { Set(ref _breaksignalstate, value); }
        }

        private SerialHandshake _handshake;

        public SerialHandshake HandShake
        {
            get { return _handshake; }
            set { Set(ref _handshake, value); }
        }

        private Boolean _isDataTerminalReadyEnabled;

        public Boolean IsDataTerminalReadyEnabled
        {
            get { return _isDataTerminalReadyEnabled; }
            set { Set(ref _isDataTerminalReadyEnabled, value); }
        }

        private Boolean _isRequestToSendEnabled;

        public Boolean IsRequestToSendEnabled
        {
            get { return _isRequestToSendEnabled; }
            set { Set(ref _isRequestToSendEnabled, value); }
        }

        private SerialParity _parity;

        public SerialParity Parity
        {
            get { return _parity; }
            set { Set(ref _parity, value); }
        }

        private TimeSpan _readTimeout;

        public TimeSpan ReadTimeout
        {
            get { return _readTimeout; }
            set { Set(ref _readTimeout, value); }
        }

        private SerialStopBitCount _stopBit;

        public SerialStopBitCount StopBit
        {
            get { return _stopBit; }
            set { Set(ref _stopBit, value); }
        }

        private TimeSpan _writeTimeout;

        public TimeSpan WriteTimeout
        {
            get { return _writeTimeout; }
            set { Set(ref _writeTimeout, value); }
        }

    }
}
