using CollectionViewSourceDemo.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace CollectionViewSourceDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Init();
        }

        public ObservableCollection<PersonModel> People { get; } = new ObservableCollection<PersonModel>();

        private void Init()
        {
            People.Add(new PersonModel() { Name = "Mary Ann", Age = 24 });
            People.Add(new PersonModel() { Name = "Ginger", Age = 28 });
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            People.Add(new PersonModel() { Name = Name_TextBox.Text, Age = int.Parse(Age_TextBox.Text) });
        }
    }
}
