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
    public enum DataType
    {
        String,
        Hex
    }

    public class UartPageViewModel : ViewModelBase
    {

        /// <summary>
        /// Constractor: 
        /// 1. Initial some properties
        /// 2. Get available serial devices
        /// </summary>
        public UartPageViewModel()
        {
            SelDevIndex = -1;
            SendDataType = DataType.String;
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
            IsDataTerminalReadyEnabled = false;
            IsRequestToSendEnabled = false;
            Parity = SerialParity.None;
            ReadTimeout = TimeSpan.FromMilliseconds(1000);
            WriteTimeout = TimeSpan.FromMilliseconds(1000);
            StopBits = SerialStopBitCount.One;
        }

        public RelayCommand<string> SendDataCmd
        {
            get
            {
                return new RelayCommand<string>(async (data) =>
                {
                    switch (SendDataType)
                    {
                        case DataType.String:
                            if (string.IsNullOrEmpty(data))
                            {
                                byte[] b = { 0x0D };
                                await ComPortDevice.SendData(b);
                            }
                            else
                            {
                                byte[] tmp = Encoding.ASCII.GetBytes(data);
                                byte[] bData = new byte[tmp.Length + 1];
                                for (int x = 0; x < tmp.Length; x++)
                                {
                                    bData[x] = tmp[x];
                                }
                                bData[tmp.Length] = 0x0D;
                                await ComPortDevice.SendData(bData);
                            }
                            break;
                        case DataType.Hex:
                            int i = Convert.ToInt32(data, 16);
                            var bInt = System.BitConverter.GetBytes(i);
                            await ComPortDevice.SendData(bInt);
                            break;
                    }

                });
            }
        }

        /// <summary>
        /// Serial device selection changed command
        /// </summary>
        public RelayCommand SelectionChangedCmd
        {
            get
            {
                return new RelayCommand(async () =>
               {
                   if (ComPortDevice.IsConnected)
                   {
                       ComPortDevice.ListenStateChanged -= ComPortDevice_ListenStateChange;
                       ComPortDevice.Disconnect();
                   }
                   await ComPortDevice.Connect(SelectedDev.Id);
                   ComPortDevice.ListenStateChanged += ComPortDevice_ListenStateChange;
               });
            }
        }

        /// <summary>
        /// Listen button command
        /// </summary>
        public RelayCommand ListenCmd
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {

                        if (SelectedDev == null)
                        {
                            return;
                        }
                        if (!ComPortDevice.IsConnected)
                        {
                            throw new Exception("serial device is not connected!");
                        }
                        //Set parameters for serial device listening
                        ComPortDevice.ComPort.BaudRate = BaudRate;
                        ComPortDevice.ComPort.BreakSignalState = BreakSignalState;
                        ComPortDevice.ComPort.DataBits = DataBits;
                        //ComPortDevice.ComPort.IsDataTerminalReadyEnabled = IsDataTerminalReadyEnabled;
                        //ComPortDevice.ComPort.IsRequestToSendEnabled = IsRequestToSendEnabled;
                        ComPortDevice.ComPort.StopBits = StopBits;
                        ComPortDevice.ComPort.Handshake = Handshake;
                        ComPortDevice.ComPort.Parity = Parity;
                        ComPortDevice.ComPort.ReadTimeout = ReadTimeout;
                        ComPortDevice.ComPort.WriteTimeout = WriteTimeout;


                        GlobalMethod.ShowMsg("Listen COM Port:" + ComPortDevice.ComPort.PortName
                            + "\nBaudRate:" + ComPortDevice.ComPort.BaudRate +
                            "\nDatabits:" + ComPortDevice.ComPort.DataBits, MainFrame.ViewModel.MsgType.Info);

                        //Watch the listening state for serial device
                        ComPortDevice.DataReceived += ComPortDevice_DataReceived;
                        await ComPortDevice.Listen();
                    }
                    catch (Exception ex)
                    {
                        GlobalMethod.ShowMsg(ex.StackTrace + "\n" + ex.Message, MainFrame.ViewModel.MsgType.Exception);
                    }
                });
            }
        }

        private void ComPortDevice_DataReceived(DataReceivedEventArgs args)
        {
            ReceivedData += args.Data;
            System.Diagnostics.Debug.Write(ReceivedData);
        }

        /// <summary>
        /// once serial device listening state changed, change Idle property
        /// </summary>
        private void ComPortDevice_ListenStateChange()
        {
            Idle = !ComPortDevice.IsListening;
            if (Idle)
            {
                ReceivedData = "";
            }
        }

        /// <summary>
        /// StopListen button command
        /// </summary>
        public RelayCommand StopListenCmd
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (ComPortDevice.IsListening)
                    {
                        ComPortDevice.DataReceived -= ComPortDevice_DataReceived;
                        ComPortDevice.StopListen();
                    }
                });
            }
        }

        /// <summary>
        /// get availables serial devices
        /// </summary>
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

        /// <summary>
        /// serial devices collection
        /// </summary>
        public ObservableCollection<DeviceInformation> DevCollection { get; set; }

        /// <summary>
        /// Current selected serial device
        /// </summary>
        public DeviceInformation SelectedDev
        {
            get
            {
                return DevCollection.Count > 0 && SelDevIndex > -1 ? DevCollection[SelDevIndex] : null;
            }
        }

        private int _selDevIndex;

        /// <summary>
        /// Selected device index in ComboBox
        /// </summary>
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

        /// <summary>
        /// Represent if the serial device is idle 
        /// </summary>
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

        private string _receivedData;

        public string ReceivedData
        {
            get { return _receivedData; }
            set
            {
                Set(ref _receivedData, value);
            }
        }

        private DataType _sendDataType;

        public DataType SendDataType
        {
            get { return _sendDataType; }
            set
            {
                Set(ref _sendDataType, value);
            }
        }

    }
}
