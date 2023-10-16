using System;
using System.Collections;
using System.Globalization;

namespace Doc_Historico.Converters
{
    public class SeparatorVisibilityConverter : IValueConverter
	{

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collectionView = parameter as CollectionView;

            if (value == null || collectionView == null)
                return false;

            var items = collectionView.ItemsSource as IList;
            if (items == null)
                return false;

            return items[items.Count - 1] != value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

