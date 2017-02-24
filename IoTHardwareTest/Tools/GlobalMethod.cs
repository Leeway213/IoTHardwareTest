using IoTHardwareTest.Modules.MainFrame.ViewModel;
using IoTHardwareTest.Tools.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTHardwareTest.Tools
{
    public static class GlobalMethod
    {
        public static void ShowMsg(string msg, MsgType type = MsgType.Info)
        {
            ViewModelLocator.Locator.FramePageViewModel.ShowMsg(msg, type);
        }
    }
}
