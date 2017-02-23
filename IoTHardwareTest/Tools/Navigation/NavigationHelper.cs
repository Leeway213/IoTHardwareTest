using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IoTHardwareTest.Tools.Navigation
{
    public class NavigationHelper
    {
        private static NavigationHelper _naviHelper;

        private static NavigationHelper NaviHelper
        {
            get
            {
                if (_naviHelper == null)
                {
                    throw new Exception("You need call NavigationHelper.Regist(frame) first");
                }
                return _naviHelper;
            }
            set
            {
                _naviHelper = value;
            }
        }

        private NavigationHelper(Frame frame)
        {
            this._frame = frame;
        }

        private Frame _frame;

        public static void Regist(Frame frame)
        {
            NaviHelper = new NavigationHelper(frame);
        }

        public static void Navigate(Type page, object param = null)
        {
            if (param == null)
            {
                NaviHelper._frame.Navigate(page);
            }
            else
            {
                NaviHelper._frame.Navigate(page, param);
            }
        }

        public static void GoBack()
        {
            NaviHelper._frame.GoBack();
        }

        public static void GoForward()
        {
            NaviHelper._frame.GoForward();
        }

        public static bool CanGoBack
        {
            get
            {
                return NaviHelper._frame.CanGoBack;
            }
        }

        public static bool CanGoForward
        {
            get
            {
                return NaviHelper._frame.CanGoForward;
            }
        }

        public static IList<PageStackEntry> BackStack
        {
            get
            {
                return NaviHelper._frame.BackStack;
            }
        }
    }
}
