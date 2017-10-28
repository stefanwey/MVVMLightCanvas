using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows;

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
        public const string LastPositionPropertyName = "LastPosition";

        private string _lastPosition = "Click somewhere";
        private string _lastPositionRight = "Click somewhere";
        private string _lastPositionMouseMove = "Click somewhere";
        private RelayCommand<Point> _showPositionCommand;
        private RelayCommand<Point> _showPositionRightCommand;
        private RelayCommand<Point> _showPositionMouseMoveCommand;


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

        public string LastPosition
        {
            get
            {
                return _lastPosition;
            }
            set
            {
                Set(() => LastPosition, ref _lastPosition, value);
            }
        }

        public string LastPositionRight
        {
            get
            {
                return _lastPositionRight;
            }
            set
            {
                Set(() => LastPositionRight, ref _lastPositionRight, value);
            }
        }
        public string LastPositionMouseMove
        {
            get
            {
                return _lastPositionMouseMove;
            }
            set
            {
                Set(() => LastPositionMouseMove, ref _lastPositionMouseMove, value);
            }
        }

        public RelayCommand<Point> ShowPositionCommand
        {
            get
            {
                return _showPositionCommand
                        ?? (_showPositionCommand = new RelayCommand<Point>(
                            point =>
                            {
                                LastPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);
                            }));
            }
        }


        public RelayCommand<Point> ShowPositionRightCommand
        {
            get
            {
                return _showPositionRightCommand
                        ?? (_showPositionRightCommand = new RelayCommand<Point>(
                            point =>
                            {
                                LastPositionRight = string.Format("{0:N1}, {1:N1}", point.X, point.Y);
                            }));
            }
        }
        public RelayCommand<Point> ShowPositionMouseMoveCommand
        {
            get
            {
                return _showPositionMouseMoveCommand
                        ?? (_showPositionMouseMoveCommand = new RelayCommand<Point>(
                            point =>
                            {
                                LastPositionMouseMove = string.Format("{0:N1}, {1:N1}", point.X, point.Y);
                            }));
            }
        }
    }
}