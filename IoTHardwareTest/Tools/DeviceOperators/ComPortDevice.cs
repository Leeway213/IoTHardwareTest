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
    class ComPortDevice
    {
        public static bool IsConnected;

        public static bool IsListening;

        public delegate void ListenStateChangedEventHandler();
        public static event ListenStateChangedEventHandler ListenStateChange;

        private static ComPortDevice port;

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

        public static async Task<DeviceInformationCollection> ScanSerialDevices()
        {
            string aqs = SerialDevice.GetDeviceSelector();
            return await DeviceInformation.FindAllAsync(aqs);
        }

        public static async Task Connect(string devId)
        {
            try
            {
                if (port == null)
                {
                    port = new ComPortDevice();
                }
                port.Dev = await SerialDevice.FromIdAsync(devId);
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

        public static async Task<UInt32> SendData(byte[] data)
        {
            return await port.WriteAsync(data);
        }

        public static async void Listen()
        {
            try
            {
                if (IsConnected)
                {
                    if (port.readCts == null)
                    {
                        port.readCts = new CancellationTokenSource();
                    }
                    if (port.dataReaderObject == null)
                    {
                        port.dataReaderObject = new DataReader(ComPort.InputStream);
                    }

                    IsListening = true;
                    ListenStateChange?.Invoke();
                    while (true)
                    {
                        await port.ReadAsync();
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
                ListenStateChange?.Invoke();
                if (port.dataReaderObject != null)
                {
                    port.dataReaderObject.DetachStream();
                    port.dataReaderObject = null;
                }
                if (port.readStr != null)
                {
                    port.readStr.Clear();
                    port.readStr = null;
                }
            }
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

        public static void SetParam(object value, string property)
        {
            try
            {
                if (IsConnected)
                {
                    PropertyInfo propInfo = typeof(SerialDevice).GetProperty(property);
                    propInfo.SetValue(ComPort, value, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ComPortDevice()
        {
            IsConnected = false;
        }

        private async Task ReadAsync()
        {
           

            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            readCts.Token.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(readCts.Token);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;

            if (bytesRead > 0)
            {
                if (readStr == null)
                {
                    readStr = new StringBuilder();
                }
                readStr.Append(dataReaderObject.ReadString(bytesRead));
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

        private async Task<UInt32> WriteAsync(byte[] data)
        {
            if (dataWriterObject == null)
            {
                dataWriterObject = new DataWriter(Dev.OutputStream);
            }
            dataWriterObject.WriteBytes(data);
            return await dataWriterObject.StoreAsync();
        }


        private SerialDevice Dev;

        private CancellationTokenSource readCts;

        private DataReader dataReaderObject;

        private DataWriter dataWriterObject;

        private StringBuilder readStr;

    }
}
