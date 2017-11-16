using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BusinessLogic.ViewModel
{
    public class GartenData
    {
        #region Fields

        private Rect _Rect = new Rect();

        #endregion Fields

        #region Properties

        public string GartenName { get; set; }

        public double GartenWidth
        {
            get
            {
                return _Rect.Width+200;
            }

            set
            {
                _Rect.Width = value-200;
            }
        }

        public double GartenHeight
        {
            get
            {
                return _Rect.Height+200;
            }

            set
            {
                _Rect.Height = value-200;
            }
        }


        public ObservableCollection<BeetliData> TheBeetlis { get; set; }

        private bool DragInProgress { get; set; }
        private BeetliData ActiveBeetli { get; set; }
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
            HitType hitType = HitType.None;
            LastPoint = point;
            foreach (var beetli in TheBeetlis)
            {
                // Check if we have a beetli at the mouse position
                if (beetli.Contains(point))
                {
                    hitType = GetHitType(beetli, point);
                    ActiveBeetli = beetli;
                    DragInProgress = true;
                }
            }
            return hitType;
        }

        public void StopDrag(Point point)
        {
            DragInProgress = false;
        }

        public HitType MouseMove(Point point)
        {
            if (DragInProgress)
            {
                // See how much the mouse has moved.
                Point position = point;
                double offset_x = point.X - LastPoint.X;
                double offset_y = point.Y - LastPoint.Y;

                // Get the rectangle's current position.
                double new_x = ActiveBeetli.BeetliLeft;
                double new_y = ActiveBeetli.BeetliTop;
                double new_width = ActiveBeetli.BeetliWidth;
                double new_height = ActiveBeetli.BeetliHeight;

                Rect newRect = new Rect(new_x, new_y, new_width, new_height);

                // Update the rectangle.
                switch (HitType)
                {
                    case HitType.Body:
                        new_x += offset_x;
                        new_y += offset_y;

                        newRect.Location = new Point(new_x, new_y);
                            
                        // Check if Bettli ist still in the Garden
                        if(new_x < _Rect.X)
                        {
                            new_x = _Rect.X;
                        }
                        if ((new_x + new_width) > _Rect.Right)
                        {
                            new_x = _Rect.Right - new_width;
                        }
                        if (new_y < _Rect.Y)
                        {
                            new_y = _Rect.Y;
                        }
                        if ((new_y + new_height) > _Rect.Bottom)
                        {
                            new_y = _Rect.Bottom - new_height;
                        }

                        foreach (var beetli in TheBeetlis)
                        {
                            if (!beetli.Equals(ActiveBeetli))
                            {
                                if(beetli.IntersectsWithTheInside(newRect))
                                {
                                    if(ActiveBeetli.BeetliLeft >= beetli.BeetliRight)
                                    {
                                        new_x = beetli.BeetliRight;
                                    }
                                    else if(ActiveBeetli.BeetliRight <= beetli.BeetliLeft)
                                    {
                                        new_x = beetli.BeetliLeft - ActiveBeetli.BeetliWidth;
                                    }
                                    else if (ActiveBeetli.BeetliTop >= beetli.BeetliBottom)
                                    {
                                        new_y = beetli.BeetliBottom;
                                    }
                                    else if(ActiveBeetli.BeetliBottom <= beetli.BeetliTop)
                                    {
                                        new_y = beetli.BeetliTop - ActiveBeetli.BeetliHeight;
                                    }
                                }
                            }
                        }

                        break;
                    case HitType.UL:
                        new_x += offset_x;
                        new_y += offset_y;
                        new_width -= offset_x;
                        new_height -= offset_y;

                        if (new_x < _Rect.X)
                        {
                            new_x = _Rect.X;
                        }
                        if (new_y < _Rect.Y)
                        {
                            new_y = _Rect.Y;
                        }
                        break;
                    case HitType.UR:
                        new_y += offset_y;
                        new_width += offset_x;
                        new_height -= offset_y;
                        
                        if ((new_x + new_width) > _Rect.Right)
                        {
                            new_width = _Rect.Right - new_x;
                        }
                        if (new_y < _Rect.Y)
                        {
                            new_y = _Rect.Y;
                        }

                        break;
                    case HitType.LR:
                        new_width += offset_x;
                        new_height += offset_y;

                        if ((new_x + new_width) > _Rect.Right)
                        {
                            new_width = _Rect.Right - new_x;
                        }
                        if ((new_y + new_height) > _Rect.Bottom)
                        {
                            new_height = _Rect.Bottom - new_y;
                        }
                        break;
                    case HitType.LL:
                        new_x += offset_x;
                        new_width -= offset_x;
                        new_height += offset_y;

                        if (new_x < _Rect.X)
                        {
                            new_x = _Rect.X;
                        }
                        if ((new_y + new_height) > _Rect.Bottom)
                        {
                            new_height = _Rect.Bottom - new_y;
                        }
                        break;
                    case HitType.L:
                        new_x += offset_x;
                        new_width -= offset_x;

                        if (new_x < _Rect.X)
                        {
                            new_x = _Rect.X;
                        }

                        break;
                    case HitType.R:
                        new_width += offset_x;
                        
                        if ((new_x + new_width) > _Rect.Right)
                        {
                            new_width = _Rect.Right - new_x;
                        }
                        break;
                    case HitType.B:
                        new_height += offset_y;

                        if ((new_y + new_height) > _Rect.Bottom)
                        {
                            new_height = _Rect.Bottom - new_y;
                        }
                        break;
                    case HitType.T:
                        new_y += offset_y;
                        new_height -= offset_y;
                        
                        if (new_y < _Rect.Y)
                        {
                            new_y = _Rect.Y;
                        }
                        break;
                }

                // Don't use negative width or height.
                if ((new_width > 0) && (new_height > 0))
                {
                    // Update the rectangle.
                    ActiveBeetli.BeetliLeft = new_x;
                    ActiveBeetli.BeetliTop = new_y;
                    ActiveBeetli.BeetliWidth = new_width;
                    ActiveBeetli.BeetliHeight = new_height;

                    // Save the mouse's new location.
                    LastPoint = point;
                }
            }
            else
            {
                HitType = GetHitType(TheBeetlis, point);
            }

            return HitType;
        }

        #endregion Public Methods

        #region Private Methods

        // Return a HitType value to indicate what is at the point.
        private HitType GetHitType(BeetliData beetli, Point point)
        {
            double left = beetli.BeetliLeft;
            double top = beetli.BeetliTop;
            double right = left + beetli.BeetliWidth;
            double bottom = top + beetli.BeetliHeight;

            if (beetli.Contains(point))
            {
                const double GAP = 10;
                if (point.X - left < GAP)
                {
                    // Left edge.
                    if (point.Y - top < GAP)
                    {
                        return HitType.UL;
                    }
                    if (bottom - point.Y < GAP)
                    {
                        return HitType.LL;
                    }
                    return HitType.L;
                }
                if (right - point.X < GAP)
                {
                    // Right edge.
                    if (point.Y - top < GAP)
                    {
                        return HitType.UR;
                    }
                    if (bottom - point.Y < GAP)
                    {
                        return HitType.LR;
                    }
                    return HitType.R;
                }
                if (point.Y - top < GAP)
                {
                    return HitType.T;
                }
                if (bottom - point.Y < GAP)
                {
                    return HitType.B;
                }
                return HitType.Body;
            }
            else
            {
                return HitType.None;
            }
        }

        private HitType GetHitType(ObservableCollection<BeetliData> beetlies, Point point)
        {
            foreach (var beetli in beetlies)
            {
                double left = beetli.BeetliLeft;
                double top = beetli.BeetliTop;
                double right = left + beetli.BeetliWidth;
                double bottom = top + beetli.BeetliHeight;

                if (beetli.Contains(point))
                {
                    const double GAP = 10;
                    if (point.X - left < GAP)
                    {
                        // Left edge.
                        if (point.Y - top < GAP)
                        {
                            return HitType.UL;
                        }
                        if (bottom - point.Y < GAP)
                        {
                            return HitType.LL;
                        }
                        return HitType.L;
                    }
                    if (right - point.X < GAP)
                    {
                        // Right edge.
                        if (point.Y - top < GAP)
                        {
                            return HitType.UR;
                        }
                        if (bottom - point.Y < GAP)
                        {
                            return HitType.LR;
                        }
                        return HitType.R;
                    }
                    if (point.Y - top < GAP)
                    {
                        return HitType.T;
                    }
                    if (bottom - point.Y < GAP)
                    {
                        return HitType.B;
                    }
                    return HitType.Body;
                }
                else
                {
                    continue;
                }
            }

            return HitType.None;
        }

        #endregion Private Methods
    }
}
