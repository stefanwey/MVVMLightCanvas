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
            if( _Rect.Contains(point) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsOverlapping(double x1, double y1, double x2, double y2)
        {
            Rect rect = new Rect(new Point(x1, y1), new Point(x2, y2));

            if(_Rect.IntersectsWith(rect) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
