using Diplom.BdModels;
using Diplom.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplom.Pages
{
    /// <summary>
    /// Логика взаимодействия для DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        User user;
        Product Products;
        public DetailPage(Product product)
        {
            this.Products = product;
            DataContext = Products;

            InitializeComponent();
            this.user = ((MainWindow)Application.Current.MainWindow).user!;

            if (user.UsersStatus == "User")
            {
                GridA.Visibility = Visibility.Hidden;
                GridU.Visibility = Visibility.Visible;

                panela.Visibility = Visibility.Hidden;
                panelu.Visibility = Visibility.Visible;
            }
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            CoreModel.init().SaveChanges();
            Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Изменения успешно применены!")));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buy_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product selectedProduct = button.Tag as Product;

            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар для добавления в заказ.");
                return;
            }

            var currentUser = ((MainWindow)Application.Current.MainWindow).user;
            Orders_Window orders = new Orders_Window(selectedProduct, currentUser);
            orders.ShowDialog();
        }
    }
}
