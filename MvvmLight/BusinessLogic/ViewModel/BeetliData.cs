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

        double _left = 0;
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

        double _top;
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
    }
}
