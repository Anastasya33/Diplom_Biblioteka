using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Library
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    // Класс для смены bit значения (true, false) из таблицы Book на string значения
    public class IntToStringConvBook : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool intValue)
            {
                if (intValue == false)
                    return "Не доступно";
                if (intValue == true)
                    return "Доступно";
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Класс для смены int значения из таблицы People на string значения
    public class IntToStringConvClients : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (intValue == 0)
                    return "Не вернул";
                if (intValue == 1)
                    return "Вернул";
                if (intValue == 2)
                    return "Просрочил";
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
