using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModel
{
    public class BeetliData : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged Members

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Fields
        
        private Rect _Rect = new Rect();

        #endregion Fields

        #region Properties

        public double BeetliLeft
        {
            get
            {
                return _Rect.Left;
            }
            set
            {
                _Rect.X = value;
                OnPropertyChanged("BeetliLeft");
            }
        }

        public double BeetliTop
        {
            get
            {
                return _Rect.Top;
            }

            set
            {
                _Rect.Y = value;
                OnPropertyChanged("BeetliTop");
            }
        }

        public double BeetliWidth
        {
            get
            {
                return _Rect.Width;
            }

            set
            {
                _Rect.Width = value;
                OnPropertyChanged("BeetliWidth");
            }
        }

        public double BeetliHeight
        {
            get
            {
                return _Rect.Height;
            }

            set
            {
                _Rect.Height = value;
                OnPropertyChanged("BeetliHeight");
            }
        }

        #endregion Properties

        public bool Contains(Point point)
        {
            return _Rect.Contains(point);
        }

        public bool IsInside(Point point)
        {
            Rect rect = new Rect(_Rect.X + 1, _Rect.Y + 1, _Rect.Width - 2, _Rect.Height - 2);

            if (rect.Contains(point))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IntersectsWith(Point point1, Point point2)
        {
            Rect rect = new Rect(point1, point2);

            return _Rect.IntersectsWith(rect);
        }

        public bool IntersectsWithTheInside(Point point1, Point point2)
        {
            Rect rect1 = new Rect(_Rect.X + 1, _Rect.Y + 1, _Rect.Width - 2, _Rect.Height - 2);
            Rect rect2 = new Rect(point1, point2);

            return rect1.IntersectsWith(rect2);
        }

    }
}
