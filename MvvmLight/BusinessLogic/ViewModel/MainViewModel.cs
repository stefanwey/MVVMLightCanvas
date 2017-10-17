using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace BusinessLogic.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                Title = "Hello MVVM Light (Design Mode)";
            }
            else
            {
                Title = "Hello MVVM Light";
            }

            Canvas ctx = new Canvas();
            ctx.Width = 150;
            ctx.Height = 100;
            ctx.Background = Brushes.Aqua;

            Canvas.SetLeft(ctx, 100);
            Canvas.SetTop(ctx, 100);

            Canvas ctx2 = new Canvas();
            ctx2.Width = 150;
            ctx2.Height = 100;
            ctx2.Background = Brushes.Red;

            Canvas.SetLeft(ctx2, 350);
            Canvas.SetTop(ctx2, 150);

            Canvas ctx3 = new Canvas();
            ctx3.Width = 50;
            ctx3.Height = 150;
            ctx3.Background = Brushes.Green;

            Canvas.SetLeft(ctx3, 270);
            Canvas.SetTop(ctx3, 30);

            LayerItems = new List<Canvas>();
            LayerItems.Add(ctx);
            LayerItems.Add(ctx2);
            LayerItems.Add(ctx3);
        }

        public string Title { get; set; }
        public List<Canvas> LayerItems { get; set; }

    }
}