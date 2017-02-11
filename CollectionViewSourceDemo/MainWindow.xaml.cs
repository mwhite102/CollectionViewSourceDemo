using CollectionViewSourceDemo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionViewSourceDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MainWindow()
        {
            InitializeComponent();
            // Set the DataContext to this instance
            this.DataContext = this;
        }

        public ObservableCollection<PersonModel> People { get; } = new ObservableCollection<PersonModel>();

        private int? _Age;

        [Range(1, 100, ErrorMessage = "Age should be between 1 to 100")]
        [Required(ErrorMessage = "Age is required")]
        public int? Age
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

        private string _PersonName;

        [Required(ErrorMessage = "Name is required")]
        public string PersonName
        {
            get { return _PersonName; }
            set
            {
                if (_PersonName != value)
                {
                    _PersonName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            People.Add(new PersonModel() { Name = this.PersonName, Age = this.Age.Value });
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                return OnValidate(columnName);
            }
        }

        /// <summary>
        /// Validates properties using DataAnnotations
        /// </summary>
        /// <param name="propertyName">The instance property name to be validated</param>
        /// <returns>An error message string if the property is not valid, or an empty string if the value is valid</returns>
        protected virtual string OnValidate(string propertyName)
        {
            string result = string.Empty;

            // Get the value for the propertyName to be validated
            var value = this.GetType().GetProperty(propertyName).GetValue(this);

            // Create a list of ValidationResults
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Create the ValidationContext
            ValidationContext context = new ValidationContext(this);

            // Set the MemberName on the ValidationContext to the propertyName being validated
            context.MemberName = propertyName;

            // Do the validation
            bool isValid = Validator.TryValidateProperty(value, context, validationResults);

            if (!isValid)
            {
                // Get the error message
                result = validationResults.First().ErrorMessage;
            }

            return result;
        }
    }
}
