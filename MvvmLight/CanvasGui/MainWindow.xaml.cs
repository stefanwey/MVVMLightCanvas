using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanvasGui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _beetliList = null;

        public MainWindow()
        {
            InitializeComponent();

            _beetliList = new MainViewModel();

            _beetliList.TheBeetlis.Add(new BeetliData { BeetliLeft = 50, BeetliTop = 50, BeetliHeight = 50, BeetliWidth = 50 });
            _beetliList.TheBeetlis.Add(new BeetliData { BeetliLeft = 150, BeetliTop = 100, BeetliHeight = 100, BeetliWidth = 25 });
            _beetliList.TheBeetlis.Add(new BeetliData { BeetliLeft = 250, BeetliTop = 50, BeetliHeight = 25, BeetliWidth = 100 });

            this.DataContext = _beetliList;
        }
    }
}
