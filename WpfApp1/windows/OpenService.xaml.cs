using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для OpenService.xaml
    /// </summary>
    public partial class OpenService : Window
    {
        public OpenService()
        {
            InitializeComponent();
            this.DataContext = this;
            ServiceList = Core.DB.Service.ToList();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private List<Service> _ServiceList;

        public List<Service> ServiceList
        {
            get
            {

                return _ServiceList;
                
              
            }
            set
            {
                _ServiceList = value;

            }
        }
    }

}
