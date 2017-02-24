using IoTHardwareTest.Modules.MainFrame.ViewModel;
using IoTHardwareTest.Tools.MVVM;
using IoTHardwareTest.Tools.Navigation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IoTHardwareTest.Modules.MainFrame.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FramePage : Page
    {
        public FramePageViewModel Vm
        {
            get
            {
                return ViewModelLocator.Locator.FramePageViewModel;
            }
        }


        public FramePage()
        {
            this.InitializeComponent();
            NavigationHelper.Regist(mainFrame);

            this.Loaded += FramePage_Loaded;
        }

        private void FramePage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += FramePage_BackRequested;
        }

        private void FramePage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (splitView.DisplayMode == SplitViewDisplayMode.CompactOverlay || splitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                if (splitView.IsPaneOpen)
                {
                    splitView.IsPaneOpen = false;
                    return;
                }
            }
            if (mainFrame.CanGoBack)
            {
                mainFrame.GoBack();
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = mainFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Vm.ItemClickCommand?.Execute(e.ClickedItem);
        }

        private void Setting_Click(object sender, ItemClickEventArgs e)
        {
            NavigationHelper.Navigate(typeof(SettingPage.View.SettingPage));
        }

        private void Canvas_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}
