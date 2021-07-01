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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    
    

    public partial class Client
    {
        public Boolean MinSalary
        {
            get
            {
                return Balance < 100000;
            }
        }
        public Uri ImagePreview
        {
            get
            {
                var imageName = System.IO.Path.Combine(Environment.CurrentDirectory, Photo ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : new Uri("pack://application:,,,/Фото сотрудников/picture.jpg");
            }
        }
        public string Users
        {
            get
            {
                return FirstName + " " + LastName + " " + MiddleName;
            }
        }

        public double DiscountFloat
        {
            get
            {
                return Convert.ToSingle(Balance);
            }
        }
       

    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

 
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;


        }
      

        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var openUser = new windows.ShowUser();
            // тут нужно конечно навернуть проверку логина и пароля
            if (LoginBox.Text == "Admin" && PasswordBox.Password == "1111")
            {
                openUser.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
