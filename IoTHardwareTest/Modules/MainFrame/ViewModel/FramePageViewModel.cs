using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IoTHardwareTest.Modules.Audio.View;
using IoTHardwareTest.Modules.MainFrame.Model;
using IoTHardwareTest.Tools;
using IoTHardwareTest.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace IoTHardwareTest.Modules.MainFrame.ViewModel
{
    public enum MsgType
    {
        Info,
        Error,
        Exception
    }

    public class FramePageViewModel : ViewModelBase
    {

        public ObservableCollection<NaviListModel> NaviList { get; set; }

        public RelayCommand<object> ItemClickCommand
        {
            get
            {
                return new RelayCommand<object>(obj =>
                {
                    if (SelectedItem == obj)
                    {
                        return;
                    }
                    NaviListModel item = obj as NaviListModel;
                    NavigationHelper.Navigate(item.PageType);

                });
            }
        }

        public RelayCommand MainFrameLoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send(NaviList, "selectedIndex");
                    //To-Do: 跳转到首页
                });
            }
        }

        public RelayCommand FrameNavigatedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SplitViewMode == SplitViewDisplayMode.Overlay || SplitViewMode == SplitViewDisplayMode.CompactOverlay)
                    {
                        IsPaneOpen = false;
                    }
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = NavigationHelper.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
                });
            }
        }

        public RelayCommand ClearMessage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MsboxAvailable = false;
                    MsboxVisibility = Visibility.Collapsed;
                });
            }
        }

        private Nullable<Boolean> _isPaneOpen;

        public Nullable<Boolean> IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { Set(ref _isPaneOpen, value); }
        }

        private SplitViewDisplayMode _splitViewMode;

        public SplitViewDisplayMode SplitViewMode
        {
            get { return _splitViewMode; }
            set { Set(ref _splitViewMode, value); }
        }



        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }

        private Visibility _msboxVisibility;

        public Visibility MsboxVisibility
        {
            get { return _msboxVisibility; }
            set { Set(ref _msboxVisibility, value); }
        }

        private bool _msboxAvailable;

        public bool MsboxAvailable
        {
            get { return _msboxAvailable; }
            set { Set(ref _msboxAvailable, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }


        private SolidColorBrush _msboxBackground;

        public SolidColorBrush MsboxBackground
        {
            get { return _msboxBackground; }
            set { Set(ref _msboxBackground, value); }
        }


        public FramePageViewModel()
        {
            var bounds = Window.Current.CoreWindow.Bounds;
            if (bounds.Width > 900)
            {
                SplitViewMode = SplitViewDisplayMode.CompactInline;
                IsPaneOpen = true;
            }
            else if (bounds.Width > 550)
            {
                SplitViewMode = SplitViewDisplayMode.CompactOverlay;
                IsPaneOpen = false;
            }
            else
            {
                SplitViewMode = SplitViewDisplayMode.Overlay;
                IsPaneOpen = false;
            }

            //init messagebox
            MsboxAvailable = false;
            MsboxVisibility = Visibility.Collapsed;
            Message = "";
            MsboxBackground = new SolidColorBrush(Colors.LightGray);

            NaviList = new ObservableCollection<NaviListModel>();

            AddPages();
        }

        public void ShowMsg(string msg, MsgType msgType)
        {
            Message = msg;
            MsboxVisibility = Visibility.Visible;
            MsboxAvailable = true;
            switch (msgType)
            {
                case MsgType.Info:
                    MsboxBackground.Color = Colors.Green;
                    break;
                case MsgType.Error:
                    MsboxBackground.Color = Colors.Red;
                    break;
                case MsgType.Exception:
                    MsboxBackground.Color = Colors.Red;
                    break;
                default:
                    break;
            }
        }

        public void AddPages()
        {
            if (NaviList != null)
            {
                //To-Do: 添加导航列表项
                NaviList.Add(new NaviListModel()
                {
                    Icon = GenerateGeo(Constants.AUDIO_TEST_PAGE_ICON),
                    Text = StringResHelpler.GetString("Audio"),
                    PageType = typeof(AudioTestPage)
                });

                NaviList.Add(new NaviListModel()
                {
                    Icon = GenerateGeo(Constants.UART_TEST_PAGE_ICON),
                    Text = "UART",
                    PageType = typeof(UART.View.UartTestPage)
                });

                NaviList.Add(new NaviListModel()
                {
                    Icon = GenerateGeo(Constants.GPIO_TEST_PAGE_ICON),
                    Text = "GPIO",
                    PageType = typeof(GPIO.View.GpioTestPage)
                });

                NaviList.Add(new NaviListModel()
                {
                    Icon = GenerateGeo(Constants.I2C_TEST_PAGE_ICON),
                    Text = "I2C",
                    PageType = typeof(I2C.View.I2CTestPage)
                });

                NaviList.Add(new NaviListModel()
                {
                    Icon = GenerateGeo(Constants.NETWORK_TEST_PAGE_ICON),
                    Text = StringResHelpler.GetString("Network"),
                    PageType = typeof(Network.View.NetworkTestPage)
                });

                NaviList.Add(new NaviListModel()
                {
                    Icon = GenerateGeo(Constants.STORAGE_TEST_PAGE_ICON),
                    Text = StringResHelpler.GetString("Storage"),
                    PageType = typeof(Storage.View.StorTestPage)
                });
            }

        }


        private Geometry GenerateGeo(string pathStr)
        {
            return (Geometry)XamlReader.Load(
                "<Geometry xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>"
                        + pathStr + "</Geometry>");
        }

    }
}
