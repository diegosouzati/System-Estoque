using ControladorDePedidos.Modelo;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ControladorDePedidos.WPF
{
    public class ConversorDeEstoque : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var produto = value as Produto;
            if (produto.QuantidadeEmEstoque < produto.QuantidadeMinimaEmEstoque)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
