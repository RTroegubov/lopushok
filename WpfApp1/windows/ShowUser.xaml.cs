using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.windows
{
    /// <summary>
    /// Логика взаимодействия для ShowUser.xaml
    /// </summary>
    public partial class ShowUser : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Client> _UserService;

        public List<Client> UserService
        {
            get
            {
               

                var FilteredServiceList = _UserService.FindAll(item =>
                   item.DiscountFloat >= CurrentDiscountFilter.Item1 &&
                     item.DiscountFloat < CurrentDiscountFilter.Item2
                    );

                if (SearchFilter != "")
                    FilteredServiceList = FilteredServiceList.Where(item =>
                        item.Users.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) != -1 ).ToList();


                if (SortPriceAscending)
                {
                    
                    return FilteredServiceList.OrderBy(item =>(item.Balance))
                .ToList();

                }
                else
                {
                    
                    return FilteredServiceList.OrderByDescending(item => (item.Balance))
                .ToList();
                }
            }
            set
            {
                _UserService = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));
                }
            }
        }

        




        public ShowUser()
        {
            InitializeComponent();
            this.DataContext = this;
            UserService = Core.DB.Client.ToList();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private Boolean _SortPriceAscending = true;
        public Boolean SortPriceAscending
        {
            get { return _SortPriceAscending; }
            set
            {
                _SortPriceAscending = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortPriceAscending = (sender as RadioButton).Tag.ToString() == "1";
        }

        private void ServiceShow_Click(object sender, RoutedEventArgs e)
        {
            var isServiceShow = new windows.OpenService();
            isServiceShow.ShowDialog();
        }

        public List<string> FilterByDiscountNamesList
        {
            get
            {
                return FilterByDiscountValuesList
                    .Select(item => item.Item1)
                    .ToList();
            }
        }
        private Tuple<double, double> _CurrentDiscountFilter = Tuple.Create(double.MinValue, double.MaxValue);

        public Tuple<double, double> CurrentDiscountFilter
        {
            get
            {
                return _CurrentDiscountFilter;
            }
            set
            {
                _CurrentDiscountFilter = value;
                if (PropertyChanged != null)
                {
                    // при изменении фильтра список перерисовывается
                    PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));
                }
            }
        }

        private void DiscountFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DiscountFilterComboBox.SelectedIndex >= 0)
                CurrentDiscountFilter = Tuple.Create(
                    FilterByDiscountValuesList[DiscountFilterComboBox.SelectedIndex].Item2,
                    FilterByDiscountValuesList[DiscountFilterComboBox.SelectedIndex].Item3

                );
        }

        private List<Tuple<string, double, double>> FilterByDiscountValuesList =
         new List<Tuple<string, double, double>>() {
        Tuple.Create("Все записи", 0d, 1000000d),
        Tuple.Create("от 10000 до 60000", 10000d, 60000d),
        Tuple.Create("от 60000 до 100000", 60000d, 1000000d),
       

    };

        private string _SearchFilter = "";
        public string SearchFilter
        {
            get { return _SearchFilter; }
            set
            {
                _SearchFilter = value;
                if (PropertyChanged != null)
                {
                    // при изменении фильтра список перерисовывается
                    PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));

                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
        }

        public int ServicesCount
        {
            get
            {
                return _UserService.Count;
            }

        }
        public int FilteredServicesCount
        {
            get
            {
                return UserService.Count;
            }
        }

        private void ServiceShow_Click_1(object sender, RoutedEventArgs e)
        {
            var ServiseShows = new windows.OpenService();
            ServiseShows.ShowDialog();
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
           //  создаем новую услугу
            var NewService = new Client();
           
            var NewServiceWindow = new UserWindows(NewService);
            if ((bool)NewServiceWindow.ShowDialog())
            {
             //список услуг нужно перечитать с сервера
                UserService = Core.DB.Client.ToList();
                PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
                PropertyChanged(this, new PropertyChangedEventArgs("FilteredProductsCount"));
                PropertyChanged(this, new PropertyChangedEventArgs("ProductsCount"));
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // у DataGrid-a есть свойство SelectedItem - его приводим к типу Service
            var item = ProductListView.SelectedItem as Client;

         
            Core.DB.Client.Remove(item);

            // сохраняем изменения
            Core.DB.SaveChanges();

            // перечитываем изменившийся список, не забывая в сеттере вызвать PropertyChanged
            UserService = Core.DB.Client.ToList();
            PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
        }
        
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var SelectedService = ProductListView.SelectedItem as Client;
            var EditServiceWindow = new UserWindows(SelectedService);
            if ((bool)EditServiceWindow.ShowDialog())
            {
                // при успешном завершении не забываем перерисовать список услуг
                PropertyChanged(this, new PropertyChangedEventArgs("UserService"));
                // и еще счетчики - их добавьте сами
            }
        }
    }
}
