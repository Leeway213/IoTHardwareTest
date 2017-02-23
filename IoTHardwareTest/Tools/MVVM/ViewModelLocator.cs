using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using IoTHardwareTest.Modules.MainFrame.ViewModel;

namespace IoTHardwareTest.Tools.MVVM
{
    class ViewModelLocator
    {
        public static ViewModelLocator Locator
        {
            get
            {
                return new ViewModelLocator();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<FramePageViewModel>();
        }




        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FramePageViewModel FramePageViewModel => ServiceLocator.Current.GetInstance<FramePageViewModel>();


    }
}
