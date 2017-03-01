using IoTHardwareTest.Modules.UART.ViewModel;
using IoTHardwareTest.Tools;
using IoTHardwareTest.Tools.DeviceOperators;
using IoTHardwareTest.Tools.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IoTHardwareTest.Modules.UART.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UartTestPage : Page
    {

        private UartPageViewModel Vm => ViewModelLocator.Locator.UartPageViewModel;

        public UartTestPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void rtbReceivedData_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            svRecvData.ChangeView(null, svRecvData.ScrollableHeight, null, false);
        }
    }
}
