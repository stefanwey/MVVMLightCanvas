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

        private double _Left;
        private double _Top;
        private double _Width;
        private double _Height;

        public double BeetliLeft
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;

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
                OnPropertyChanged("BeetliHeight");
            }
        }

    }
}
