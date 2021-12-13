using System.Windows;
using WpfInfrastructure.XamlConverter;

namespace MaterialDesignUnityBootStrap.Converter
{
    public sealed class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        {
        }
    }
}