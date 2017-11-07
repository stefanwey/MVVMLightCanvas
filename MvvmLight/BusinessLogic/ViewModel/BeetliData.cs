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

        private double _Right;
        private double _Bottom;

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
                _Left = value;
                _Right = value + _Width;
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
                _Top = value;
                _Bottom = value + _Height;
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
                _Width = value;
                _Right = _Left + value;
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
                _Height = value;
                _Bottom = _Top + value;
                OnPropertyChanged("BeetliHeight");
            }
        }

        #endregion Properties

        public bool IsPointInside(double x, double y)
        {
            if( _Left < x && x < (_Left + _Width) && _Top < y && y < ( _Top + _Height) )
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

            possibleX1 = Math.Max(_Left, x1);
            possibleY1 = Math.Max(_Top, y1);
            possibleX2 = Math.Min(_Right, x2);
            possibleY2 = Math.Min(_Bottom, y2);

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
