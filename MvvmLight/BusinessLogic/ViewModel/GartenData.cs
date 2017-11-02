using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel
{
    public class GartenData
    {
        #region Fields

        private string _Name;
        private double _Width;
        private double _Height;

        #endregion Fields

        #region Properties

        public string GartenName
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public double GartenWidth
        {
            get
            {
                return _Width;
            }

            set
            {
                _Width = value;
            }
        }

        public double GartenHeight
        {
            get
            {
                return _Height;
            }

            set
            {
                _Height = value;
            }
        }

        public ObservableCollection<BeetliData> TheBeetlis { get; set; }

        #endregion Properties

        #region Constructor

        public GartenData()
        {
            TheBeetlis = new ObservableCollection<BeetliData>();

            TheBeetlis.Add(new BeetliData { BeetliLeft = 50, BeetliTop = 50, BeetliHeight = 50, BeetliWidth = 50 });
            TheBeetlis.Add(new BeetliData { BeetliLeft = 150, BeetliTop = 100, BeetliHeight = 100, BeetliWidth = 25 });
            TheBeetlis.Add(new BeetliData { BeetliLeft = 250, BeetliTop = 50, BeetliHeight = 25, BeetliWidth = 100 });
        }

        #endregion Constructor
    }
}
