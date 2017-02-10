using System.ComponentModel;

namespace CollectionViewSourceDemo.Models
{
    public class PersonModel : INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChanged Implementation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string _Name;

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private int _Age;

        /// <summary>
        /// Gets or sets the Age
        /// </summary>
        public int Age
        {
            get { return _Age; }
            set
            {
                if (_Age != value)
                {
                    _Age = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }
    }
}
