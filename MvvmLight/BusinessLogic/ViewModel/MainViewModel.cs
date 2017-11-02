using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
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

        // The part of the rectangle the mouse is over.
        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        private const string ShowCursorPropertyName = "ShowCursor";
        private const string GartenNamePropertyName = "GartenName";
        private const string GartenWidthPropertyName = "GartenName";
        private const string GartenHeightPropertyName = "GartenName";

        private string _leftButtonDownPosition = "Click somewhere";
        private string _leftButtonUpPosition = "Click somewhere";
        private string _lastPositionMouseMove = "Click somewhere";
        private Cursor _showCursor;
        private RelayCommand<Point> _mouseLeftButtonDownCommand;
        private RelayCommand<Point> _mouseLeftButtonUpCommand;
        private RelayCommand<Point> _mouseMoveCommand;

        // True if a drag is in progress.
        private bool _dragInProgress = false;

        // The drag's last point.
        private Point _lastPoint;

        // The part of the rectangle under the mouse.
        HitType _mouseHitType = HitType.None;

        #endregion Fields

        #region Properties

        public GartenData TheGarten { get; private set; }
        public string GartenName
        {
            get
            {
                return TheGarten.GartenName;
            }
            set
            {
                TheGarten.GartenName = value;
                RaisePropertyChanged(ShowCursorPropertyName);
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
                RaisePropertyChanged(GartenWidthPropertyName);
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
                RaisePropertyChanged(GartenHeightPropertyName);
            }
        }

        public string LeftButtonDownPosition
        {
            get
            {
                return _leftButtonDownPosition;
            }
            set
            {
                Set(() => LeftButtonDownPosition, ref _leftButtonDownPosition, value);
            }
        }
        public string LeftButtonUpPosition
        {
            get
            {
                return _leftButtonUpPosition;
            }
            set
            {
                Set(() => LeftButtonUpPosition, ref _leftButtonUpPosition, value);
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
        public Cursor ShowCursor
        {
            get
            {
                return _showCursor;
            }
            set
            {
                if (_showCursor == value)
                {
                    return;
                }

                _showCursor = value;
                RaisePropertyChanged(ShowCursorPropertyName);
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TheGarten = new GartenData() { GartenWidth = 250, GartenHeight = 500, GartenName = "Marcel's Garte" };
        }

        #endregion Constructor

        #region Public Methods

        public RelayCommand<Point> MouseLeftButtonDownCommand
        {
            get
            {
                return _mouseLeftButtonDownCommand ?? (_mouseLeftButtonDownCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LeftButtonDownPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        //_mouseHitType = SetHitType(TheBeetlis, point);
                        //SetMouseCursor();
                        //if (_mouseHitType == HitType.None) return;

                        //_lastPoint = point;
                        //_dragInProgress = true;
                    }));
            }
        }
        public RelayCommand<Point> MouseLeftButtonUpCommand
        {
            get
            {
                return _mouseLeftButtonUpCommand ?? (_mouseLeftButtonUpCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LeftButtonUpPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        //_dragInProgress = false;
                    }));
            }
        }
        public RelayCommand<Point> MouseMoveCommand
        {
            get
            {
                return _mouseMoveCommand ?? (_mouseMoveCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LastPositionMouseMove = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        //if (_dragInProgress == false)
                        //{
                        //    _mouseHitType = SetHitType(TheBeetlis, point);
                        //    SetMouseCursor();
                        //}
                        //else
                        //{
                        //    // See how much the mouse has moved.
                        //    Point position = point;
                        //    double offset_x = point.X - _lastPoint.X;
                        //    double offset_y = point.Y - _lastPoint.Y;

                        //    foreach (var beetli in TheBeetlis)
                        //    {
                        //        // Check if we have a beetli at the mouse position
                        //        if (new Rect(beetli.BeetliLeft, beetli.BeetliTop, beetli.BeetliWidth, beetli.BeetliHeight).Contains(point))
                        //        {
                        //            // Get the rectangle's current position.
                        //            double new_x = beetli.BeetliLeft;
                        //            double new_y = beetli.BeetliTop;
                        //            double new_width = beetli.BeetliWidth;
                        //            double new_height = beetli.BeetliHeight;

                        //            // Update the rectangle.
                        //            switch (_mouseHitType)
                        //            {
                        //                case HitType.Body:
                        //                    new_x += offset_x;
                        //                    new_y += offset_y;
                        //                    break;
                        //                case HitType.UL:
                        //                    new_x += offset_x;
                        //                    new_y += offset_y;
                        //                    new_width -= offset_x;
                        //                    new_height -= offset_y;
                        //                    break;
                        //                case HitType.UR:
                        //                    new_y += offset_y;
                        //                    new_width += offset_x;
                        //                    new_height -= offset_y;
                        //                    break;
                        //                case HitType.LR:
                        //                    new_width += offset_x;
                        //                    new_height += offset_y;
                        //                    break;
                        //                case HitType.LL:
                        //                    new_x += offset_x;
                        //                    new_width -= offset_x;
                        //                    new_height += offset_y;
                        //                    break;
                        //                case HitType.L:
                        //                    new_x += offset_x;
                        //                    new_width -= offset_x;
                        //                    break;
                        //                case HitType.R:
                        //                    new_width += offset_x;
                        //                    break;
                        //                case HitType.B:
                        //                    new_height += offset_y;
                        //                    break;
                        //                case HitType.T:
                        //                    new_y += offset_y;
                        //                    new_height -= offset_y;
                        //                    break;
                        //            }

                        //            // Don't use negative width or height.
                        //            if ((new_width > 0) && (new_height > 0))
                        //            {
                        //                // Update the rectangle.
                        //                beetli.BeetliLeft = new_x;
                        //                beetli.BeetliTop = new_y;
                        //                beetli.BeetliWidth = new_width;
                        //                beetli.BeetliHeight = new_height;

                        //                // Save the mouse's new location.
                        //                _lastPoint = point;
                        //            }
                        //        }
                        //    }
                        //}

                    }));
            }
        }

        #endregion Public Methods

        #region Private Methods

        // Return a HitType value to indicate what is at the point.
        private HitType SetHitType(ObservableCollection<BeetliData> beetlies, Point point)
        {
            foreach (var beetli in beetlies)
            {
                double left = beetli.BeetliLeft;
                double top = beetli.BeetliTop;
                double right = left + beetli.BeetliWidth;
                double bottom = top + beetli.BeetliHeight;
                if (point.X < left) continue;
                if (point.X > right) continue;
                if (point.Y < top) continue;
                if (point.Y > bottom) continue;

                const double GAP = 10;
                if (point.X - left < GAP)
                {
                    // Left edge.
                    if (point.Y - top < GAP) return HitType.UL;
                    if (bottom - point.Y < GAP) return HitType.LL;
                    return HitType.L;
                }
                if (right - point.X < GAP)
                {
                    // Right edge.
                    if (point.Y - top < GAP) return HitType.UR;
                    if (bottom - point.Y < GAP) return HitType.LR;
                    return HitType.R;
                }
                if (point.Y - top < GAP) return HitType.T;
                if (bottom - point.Y < GAP) return HitType.B;

                return HitType.Body;
            }

            return HitType.None;
        }

        // Set a mouse cursor appropriate for the current hit type.
        private void SetMouseCursor()
        {
            // See what cursor we should display.
            switch (_mouseHitType)
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