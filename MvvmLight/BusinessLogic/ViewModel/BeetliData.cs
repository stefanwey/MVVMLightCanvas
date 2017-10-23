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

        #endregion

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private double _left;
        private double _top;
        private double _width;
        private double _height;

        public double BeetliLeft
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;

                OnPropertyChanged("BeetliLeft");
            }
        }

        public double BeetliTop
        {
            get
            {
                return _top;
            }

            set
            {
                _top = value;
                OnPropertyChanged("BeetliTop");
            }
        }

        public double BeetliWidth
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
                OnPropertyChanged("BeetliWidth");
            }
        }

        public double BeetliHeight
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
                OnPropertyChanged("BeetliHeight");
            }
        }

    }
}
