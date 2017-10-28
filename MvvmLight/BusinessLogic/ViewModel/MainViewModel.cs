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
        // The part of the rectangle the mouse is over.
        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        public const string LastPositionPropertyName = "LastPosition";

        private string _lastPosition = "Click somewhere";
        private string _lastPositionRight = "Click somewhere";
        private string _lastPositionMouseMove = "Click somewhere";
        private RelayCommand<Point> _mouseLeftButtonDownCommand;
        private RelayCommand<Point> _mouseLeftButtonUpCommand;
        private RelayCommand<Point> _mouseMoveCommand;

        // True if a drag is in progress.
        private bool _dragInProgress = false;

        // The drag's last point.
        private Point _lastPoint;

        // The part of the rectangle under the mouse.
        HitType _mouseHitType = HitType.None;

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

        public RelayCommand<Point> MouseLeftButtonDownCommand
        {
            get
            {
                return _mouseLeftButtonDownCommand ?? (_mouseLeftButtonDownCommand =
                    new RelayCommand<Point>(point =>
                    {
                        LastPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        _mouseHitType = SetHitType(TheBeetlis, point);
                        SetMouseCursor();
                        if (_mouseHitType == HitType.None) return;

                        _lastPoint = point;
                        _dragInProgress = true;
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
                        LastPositionRight = string.Format("{0:N1}, {1:N1}", point.X, point.Y);

                        _dragInProgress = false;
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

                        if (_dragInProgress == false)
                        {
                            _mouseHitType = SetHitType(TheBeetlis, point);
                            SetMouseCursor();
                        }
                        else
                        {
                            // See how much the mouse has moved.
                            Point position = point;
                            double offset_x = point.X - _lastPoint.X;
                            double offset_y = point.Y - _lastPoint.Y;

                            foreach (var beetli in TheBeetlis)
                            {
                                // Check if we have a beetli at the mouse position
                                if (new Rect(beetli.BeetliLeft, beetli.BeetliTop, beetli.BeetliWidth, beetli.BeetliHeight).Contains(point))
                                {
                                    // Get the rectangle's current position.
                                    double new_x = beetli.BeetliLeft;
                                    double new_y = beetli.BeetliTop;
                                    double new_width = beetli.BeetliWidth;
                                    double new_height = beetli.BeetliHeight;

                                    // Update the rectangle.
                                    switch (_mouseHitType)
                                    {
                                        case HitType.Body:
                                            new_x += offset_x;
                                            new_y += offset_y;
                                            break;
                                        case HitType.UL:
                                            new_x += offset_x;
                                            new_y += offset_y;
                                            new_width -= offset_x;
                                            new_height -= offset_y;
                                            break;
                                        case HitType.UR:
                                            new_y += offset_y;
                                            new_width += offset_x;
                                            new_height -= offset_y;
                                            break;
                                        case HitType.LR:
                                            new_width += offset_x;
                                            new_height += offset_y;
                                            break;
                                        case HitType.LL:
                                            new_x += offset_x;
                                            new_width -= offset_x;
                                            new_height += offset_y;
                                            break;
                                        case HitType.L:
                                            new_x += offset_x;
                                            new_width -= offset_x;
                                            break;
                                        case HitType.R:
                                            new_width += offset_x;
                                            break;
                                        case HitType.B:
                                            new_height += offset_y;
                                            break;
                                        case HitType.T:
                                            new_y += offset_y;
                                            new_height -= offset_y;
                                            break;
                                    }

                                    // Don't use negative width or height.
                                    if ((new_width > 0) && (new_height > 0))
                                    {
                                        // Update the rectangle.
                                        beetli.BeetliLeft = new_x;
                                        beetli.BeetliTop = new_y;
                                        beetli.BeetliWidth = new_width;
                                        beetli.BeetliHeight = new_height;

                                        // Save the mouse's new location.
                                        _lastPoint = point;
                                    }
                                }
                            }
                        }

                    }));
            }
        }

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
            //// See what cursor we should display.
            //Cursor desired_cursor = Cursors.Arrow;
            //switch (MouseHitType)
            //{
            //    case HitType.None:
            //        desired_cursor = Cursors.Arrow;
            //        break;
            //    case HitType.Body:
            //        desired_cursor = Cursors.ScrollAll;
            //        break;
            //    case HitType.UL:
            //    case HitType.LR:
            //        desired_cursor = Cursors.SizeNWSE;
            //        break;
            //    case HitType.LL:
            //    case HitType.UR:
            //        desired_cursor = Cursors.SizeNESW;
            //        break;
            //    case HitType.T:
            //    case HitType.B:
            //        desired_cursor = Cursors.SizeNS;
            //        break;
            //    case HitType.L:
            //    case HitType.R:
            //        desired_cursor = Cursors.SizeWE;
            //        break;
            //}

            //// Display the desired cursor.
            //if (Cursor != desired_cursor) Cursor = desired_cursor;
        }

    }
}