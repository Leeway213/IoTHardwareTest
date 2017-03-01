using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace IoTHardwareTest.Tools.DeviceOperators
{
    class DataReceivedEventArgs
    {
        public DataReceivedEventArgs(string data)
        {
            Data = data;
        }
        public string Data { get; set; }
    }

    class ComPortDevice
    {
        /// <summary>
        /// Represent if Com port is connected
        /// </summary>
        public static bool IsConnected;

        /// <summary>
        /// Represent if Com port is listening 
        /// </summary>
        public static bool IsListening;

        public delegate void ListenStateChangedEventHandler();
        /// <summary>
        /// Event invoked once the com port listening status is changed
        /// </summary>
        public static event ListenStateChangedEventHandler ListenStateChanged;


        public delegate void DataReceivedHandler(DataReceivedEventArgs args);
        public static event DataReceivedHandler DataReceived;

        private static ComPortDevice port;

        /// <summary>
        /// Single instance for COM port
        /// </summary>
        public static SerialDevice ComPort
        {
            get
            {
                if (port == null)
                {
                    port = new ComPortDevice();
                }
                return port.Dev;
            }
        }

        /// <summary>
        /// Scan available serial devices
        /// </summary>
        /// <returns>Task returned a collection containing all serial device information</returns>
        public static async Task<DeviceInformationCollection> ScanSerialDevices()
        {
            string aqs = SerialDevice.GetDeviceSelector();
            return await DeviceInformation.FindAllAsync(aqs);
        }

        /// <summary>
        /// Connect a serial device
        /// </summary>
        /// <param name="devId">serial device ID</param>
        /// <returns></returns>
        public static async Task Connect(string devId)
        {
            try
            {
                if (port == null)
                {
                    port = new ComPortDevice();
                }
                port.Dev = await SerialDevice.FromIdAsync(devId);
                if (port.Dev != null)
                    port.Dev.ErrorReceived += Dev_ErrorReceived;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsConnected = ComPort != null;
            }
        }

        private static void Dev_ErrorReceived(SerialDevice sender, ErrorReceivedEventArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send data to serial device for TX
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<UInt32> SendData(byte[] data)
        {
            return await port.WriteAsync(data);
        }

        public static async Task<UInt32> SendData(string strData)
        {
            return await port.WriteAsync(strData);
        }

        /// <summary>
        /// Listen COM port for read
        /// </summary>
        public static async Task Listen()
        {
            port.readCts = new CancellationTokenSource();
            await port.ListenCOM();
        }

        public static void StopListen()
        {
            port.CancelReadTask();
        }

        public static void Disconnect()
        {
            if (IsListening)
                StopListen();
            if (port.Dev != null)
            {
                port.Dev.Dispose();
                port.Dev = null;
            }
            IsConnected = false;
        }

        private ComPortDevice()
        {
            IsConnected = false;
        }

        private async Task ListenCOM()
        {
            try
            {
                if (Dev != null)
                {
                    dataReaderObject = new DataReader(Dev.InputStream);

                    IsListening = true;
                    ListenStateChanged?.Invoke();
                    while (true)
                    {
                        await ReadAsync(readCts.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "TaskCanceledException")
                {
                    //TODO:
                }
                else
                    throw ex;
            }
            finally
            {
                IsListening = false;
                ListenStateChanged?.Invoke();
                if (dataReaderObject != null)
                {
                    dataReaderObject.DetachStream();
                    dataReaderObject = null;
                }
                if (readCts != null)
                {
                    readCts.Dispose();
                    readCts = null;
                }
                if (readStr != null)
                {
                    readStr.Clear();
                    readStr = null;
                }
            }
        }

        private async Task ReadAsync(CancellationToken token)
        {


            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            token.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(token);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;

            if (bytesRead > 0)
            {
                if (readStr == null)
                {
                    readStr = new StringBuilder();
                }
                byte[] data = new byte[bytesRead];
                dataReaderObject.ReadBytes(data);
                var str = Encoding.ASCII.GetString(data);
                readStr.Append(str);
                DataReceived?.Invoke(new DataReceivedEventArgs(str));
            }
        }

        private void CancelReadTask()
        {
            if (readCts != null)
            {
                if (!readCts.IsCancellationRequested)
                {
                    readCts.Cancel();
                    readCts.Dispose();
                    readCts = null;
                }
            }
        }

        /// <summary>
        /// Write operation for serial device
        /// </summary>
        /// <param name="data">bytes array to write</param>
        /// <returns>written data size</returns>
        private async Task<UInt32> WriteAsync(byte[] data)
        {
            if (dataWriterObject == null)
            {
                dataWriterObject = new DataWriter(Dev.OutputStream);
            }
            dataWriterObject.WriteBytes(data);
            return await dataWriterObject.StoreAsync();
        }

        private async Task<UInt32> WriteAsync(string strData)
        {
            if (dataWriterObject == null)
            {
                dataWriterObject = new DataWriter(Dev.OutputStream);
            }
            dataWriterObject.WriteString(strData);
            return await dataWriterObject.StoreAsync();
        }


        private SerialDevice Dev;

        private CancellationTokenSource readCts;

        private DataReader dataReaderObject;

        private DataWriter dataWriterObject;

        private StringBuilder readStr;

    }
}
