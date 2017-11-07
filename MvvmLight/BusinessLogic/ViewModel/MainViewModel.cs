using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
        #region Fields

        private const string _ShowCursorPropertyName = "ShowCursor";
        private const string _GartenNamePropertyName = "GartenName";
        private const string _GartenWidthPropertyName = "GartenName";
        private const string _GartenHeightPropertyName = "GartenName";

        private string _LeftButtonDownPosition = "Click somewhere";
        private string _LeftButtonUpPosition = "Click somewhere";
        private string _LastPositionMouseMove = "Click somewhere";

        private Cursor _ShowCursor;

        private RelayCommand<Point> _MouseLeftButtonDownCommand;
        private RelayCommand<Point> _MouseLeftButtonUpCommand;
        private RelayCommand<Point> _MouseMoveCommand;

        #endregion Fields

        #region Properties

        public GartenData TheGarten { get; private set; }

        public ObservableCollection<BeetliData> TheBeetlis
        {
            get
            {
                return TheGarten.TheBeetlis;
            }
        }

        public string GartenName
        {
            get
            {
                return TheGarten.GartenName;
            }
            set
            {
                TheGarten.GartenName = value;
                RaisePropertyChanged(_ShowCursorPropertyName);
            }
        }
        public double GartenWidth
        {
            get
            {
                return TheGarten.GartenWidth;
            }
            set
            {
                TheGarten.GartenWidth = value;
                RaisePropertyChanged(_GartenWidthPropertyName);
            }
        }
        public double GartenHeight
        {
            get
            {
                return TheGarten.GartenHeight;
            }
            set
            {
                TheGarten.GartenHeight = value;
                RaisePropertyChanged(_GartenHeightPropertyName);
            }
        }

        public string LeftButtonDownPosition
        {
            get
            {
                return _LeftButtonDownPosition;
            }
            set
            {
                Set(() => LeftButtonDownPosition, ref _LeftButtonDownPosition, value);
            }
        }
        public string LeftButtonUpPosition
        {
            get
            {
                return _LeftButtonUpPosition;
            }
            set
            {
                Set(() => LeftButtonUpPosition, ref _LeftButtonUpPosition, value);
            }
        }
        public string LastPositionMouseMove
        {
            get
            {
                return _LastPositionMouseMove;
            }
            set
            {
                Set(() => LastPositionMouseMove, ref _LastPositionMouseMove, value);
            }
        }

        public HitType MouseHitType {get; set;}

        public Cursor ShowCursor
        {
            get
            {
                return _ShowCursor;
            }
            set
            {
                if (_ShowCursor == value)
                {
                    return;
                }

                _ShowCursor = value;
                RaisePropertyChanged(_ShowCursorPropertyName);
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TheGarten = new GartenData() { GartenWidth = 500, GartenHeight = 500, GartenName = "Marcel's Garte" };

            TheGarten.TheBeetlis.Add(new BeetliData { BeetliLeft = 50, BeetliTop = 50, BeetliHeight = 50, BeetliWidth = 50 });
            TheGarten.TheBeetlis.Add(new BeetliData { BeetliLeft = 150, BeetliTop = 100, BeetliHeight = 100, BeetliWidth = 25 });
            TheGarten.TheBeetlis.Add(new BeetliData { BeetliLeft = 250, BeetliTop = 50, BeetliHeight = 25, BeetliWidth = 100 });
        }

        #endregion Constructor

        #region Public Methods

        public RelayCommand<Point> MouseLeftButtonDownCommand
        {
            get
            {
                return _MouseLeftButtonDownCommand ?? (_MouseLeftButtonDownCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LeftButtonDownPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        MouseHitType = TheGarten.StartDrag(point);
                        SetMouseCursor();
                    }));
            }
        }
        public RelayCommand<Point> MouseLeftButtonUpCommand
        {
            get
            {
                return _MouseLeftButtonUpCommand ?? (_MouseLeftButtonUpCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LeftButtonUpPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        TheGarten.StopDrag(point);
                    }));
            }
        }
        public RelayCommand<Point> MouseMoveCommand
        {
            get
            {
                return _MouseMoveCommand ?? (_MouseMoveCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LastPositionMouseMove = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        MouseHitType = TheGarten.MouseMove(point);
                        SetMouseCursor();
                    }));
            }
        }

        #endregion Public Methods

        #region Private Methods

        // Set a mouse cursor appropriate for the current hit type.
        private void SetMouseCursor()
        {
            // See what cursor we should display.
            switch (MouseHitType)
            {
                case HitType.None:
                    ShowCursor = Cursors.Arrow;
                    break;
                case HitType.Body:
                    ShowCursor = Cursors.ScrollAll;
                    break;
                case HitType.UL:
                case HitType.LR:
                    ShowCursor = Cursors.SizeNWSE;
                    break;
                case HitType.LL:
                case HitType.UR:
                    ShowCursor = Cursors.SizeNESW;
                    break;
                case HitType.T:
                case HitType.B:
                    ShowCursor = Cursors.SizeNS;
                    break;
                case HitType.L:
                case HitType.R:
                    ShowCursor = Cursors.SizeWE;
                    break;
            }
        }
 
        #endregion Private Methods
    }
}