using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BusinessLogic.ViewModel
{
    public class GartenData
    {
        #region Fields

        #endregion Fields

        #region Properties

        public string GartenName { get; set; }
        public double GartenWidth { get; set; }
        public double GartenHeight { get; set; }

        public ObservableCollection<BeetliData> TheBeetlis { get; set; }

        private bool DragInProgress { get; set; }
        private Point LastPoint { get; set; }
        private HitType HitType { get; set; }

        #endregion Properties

        #region Constructor

        public GartenData()
        {
            TheBeetlis = new ObservableCollection<BeetliData>();
        }

        #endregion Constructor

        #region Public Methods
        public HitType StartDrag(Point point)
        {
            DragInProgress = true;
            LastPoint = point;
            return SetHitType(TheBeetlis, point);
        }

        public void StopDrag(Point point)
        {
            DragInProgress = false;
        }

        public HitType MouseMove(Point point)
        {
            HitType = SetHitType(TheBeetlis, point);

            if (DragInProgress)
            {
                // See how much the mouse has moved.
                Point position = point;
                double offset_x = point.X - LastPoint.X;
                double offset_y = point.Y - LastPoint.Y;

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
                        switch (HitType)
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
                            LastPoint = point;
                        }
                    }
                }
            }

            return HitType;
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

        #endregion Private Methods
    }
}
