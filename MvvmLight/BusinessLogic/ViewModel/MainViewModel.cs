using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

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
        public ObservableCollection<BeetliData> TheBeetlis { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TheBeetlis = new ObservableCollection<BeetliData>();

            TheBeetlis.Add(new BeetliData { BeetliLeft = 50, BeetliTop = 50, BeetliHeight = 50, BeetliWidth = 50 });
            TheBeetlis.Add(new BeetliData { BeetliLeft = 150, BeetliTop = 100, BeetliHeight = 100, BeetliWidth = 25 });
            TheBeetlis.Add(new BeetliData { BeetliLeft = 250, BeetliTop = 50, BeetliHeight = 25, BeetliWidth = 100 });
        }
    }
}