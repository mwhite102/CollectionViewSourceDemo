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
        /// <summary>
        /// PropertyChanged event.
        /// </summary>
        /// <remarks>
        /// It's assigned an empty delegate so I don't have to check if
        /// it's assigned anything or not
        /// </remarks>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            // Set the DataContext to this instance
            this.DataContext = this;
            Name_TextBox.Focus();
        }

        #endregion

        /// <summary>
        /// An ObservableCollection of PersonModels
        /// </summary>
        public ObservableCollection<PersonModel> People { get; } = new ObservableCollection<PersonModel>();

        #region Pubic Properties

        private int? _Age;

        [Range(1, 100, ErrorMessage = "Age should be between 1 to 100")]
        [Required(ErrorMessage = "Please enter an age")]
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

        [Required(ErrorMessage = "Please enter a name")]
        public string PersonName
        {
            get { return _PersonName; }
            set
            {
                if (_PersonName != value)
                {
                    _PersonName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("PersonName"));
                }
            }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Add_Button_Click EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            // Add a new PersonModel to the People ObservableCollection
            People.Add(new PersonModel() { Name = this.PersonName, Age = this.Age.Value });
            // Reset the Person Properties
            this.PersonName = String.Empty;
            this.Age = null;
            // Set focus to the Name TextBox
            Name_TextBox.Focus();
        }

        #endregion

        #region IDataErrorInfo 

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

        #endregion
    }
}
