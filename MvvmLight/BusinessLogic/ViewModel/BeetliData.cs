using System;
using System.Collections.Generic;
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

        private double _Left;
        private double _Top;
        private double _Width;
        private double _Height;

        #endregion Fields

        #region Properties

        public double BeetliLeft
        {
            get
            {
                return _Left;
            }
            set
            {
                _left = value;
                _right = value + _width;
                OnPropertyChanged("BeetliLeft");
            }
        }

        public double BeetliTop
        {
            get
            {
                return _Top;
            }

            set
            {
                _top = value;
                _bottom = value + _height;
                OnPropertyChanged("BeetliTop");
            }
        }

        public double BeetliWidth
        {
            get
            {
                return _Width;
            }

            set
            {
                _width = value;
                _right = _left + value;
                OnPropertyChanged("BeetliWidth");
            }
        }

        public double BeetliHeight
        {
            get
            {
                return _Height;
            }

            set
            {
                _height = value;
                _bottom = _top + value;
                OnPropertyChanged("BeetliHeight");
            }
        }


        public bool IsPointInside(double x, double y)
        {
            if( _left < x && x < (_left + _width) && _top < y && y < ( _top + _height) )
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
            double possibleX1, possibleY1, possibleX2, possibleY2;

            possibleX1 = Math.Max(_left, x1);
            possibleY1 = Math.Max(_top, y1);
            possibleX2 = Math.Min(_right, x2);
            possibleY2 = Math.Min(_bottom, y2);

            if( possibleX2 > possibleX1 && possibleY2 > possibleY1)
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
