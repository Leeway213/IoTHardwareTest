using GalaSoft.MvvmLight.Messaging;
using IoTHardwareTest.Modules.MainFrame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace IoTHardwareTest.Modules.MainFrame.Converter
{
    public class SelectedIndexFrameConverter : IValueConverter
    {
        ObservableCollection<NaviListModel> _list;

        int savedIndex = -1;


        public SelectedIndexFrameConverter()
        {
            Messenger.Default.Register<ObservableCollection<NaviListModel>>(this, "selectedIndex", list =>
            {
                _list = list;
            });
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return savedIndex;
            }
            if (_list == null || _list.Count == 0)
            {
                return -1;
            }
            for (int i = 0; i < _list.Count; i++)
            {
                if (value.GetType() == _list[i].PageType)
                {
                    savedIndex = i;
                    return i;
                }
            }
            return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
