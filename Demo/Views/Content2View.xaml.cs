using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Demo.Views
{
    /// <summary>
    /// Interaction logic for Content2View
    /// </summary>
    public partial class Content2View : UserControl
    {
        public Content2View()
        {
            InitializeComponent();
            // InitializeLoadingComponent();
            // Task.Factory.StartNew(async () =>
            // {
            //     await Task.Delay(3000);
            //     ((UIElement) this.Content).Visibility = Visibility.Collapsed;
            //     InitializeComponent();
            // });
        }
        // public void InitializeLoadingComponent()
        // {
        //     if (_contentLoaded)
        //     {
        //         return;
        //     }
        //     _contentLoaded = true;
        //     var resourceLocater = new System.Uri("Demo;V1.0.0.0;component/views/Loading.xaml", System.UriKind.Relative);
        //     System.Windows.Application.LoadComponent(this, resourceLocater);
        //
        //
        // }

    }
}
