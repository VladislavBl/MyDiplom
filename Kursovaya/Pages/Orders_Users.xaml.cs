using Diplom.Admin_pages;
using Diplom.BdModels;
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
    /// Логика взаимодействия для Orders_Users.xaml
    /// </summary>
    public partial class Orders_Users : Page
    {
        User currentUser;
        public Orders_Users()
        {
            InitializeComponent();
            Update();
            this.currentUser = ((MainWindow)Application.Current.MainWindow).user!;

            if(currentUser.UsersStatus == "User")
            {
                Back_click.Visibility = Visibility.Hidden;
            }
            if(currentUser.UsersStatus == "Admin")
            {
                buy.Visibility = Visibility.Hidden;
            }
        }

        public void SetCurrentUser(User user)
        {
            currentUser = user;
            Update();
        }

        public void Update()
        {
            if (currentUser != null)
            {
                if (currentUser.UsersStatus == "User")
                {
                    var userOrders = CoreModel.init().Orders
                        .Where(order => order.Buyer == currentUser.IdUsers)
                        .Select(order => new
                        {
                            order.IdOrder,
                            order.OrderNumber,
                            order.OrderDate,
                            OrderItemsCount = CoreModel.init().OrderGoods
                                .Where(og => og.Idorders == order.IdOrder)
                                .Sum(og => og.Quantity)
                        })
                        .ToList();

                    orders.ItemsSource = userOrders;
                }
                else if (currentUser.UsersStatus == "Admin")
                {
                    var allOrders = CoreModel.init().Orders
                        .Select(order => new
                        {
                            order.IdOrder,
                            order.OrderNumber,
                            order.OrderDate,
                            OrderItemsCount = CoreModel.init().OrderGoods
                                .Where(og => og.Idorders == order.IdOrder)
                                .Sum(og => og.Quantity)
                        })
                        .ToList();

                    orders.ItemsSource = allOrders;
                }
            }
        }

        private void Doc_Vis_Change(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void new_order(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Product_Page());
        }

        private void remove_click(object sender, RoutedEventArgs e)
        {
            if (orders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку");
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранные поля?", "Удалить выбранные поля?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            foreach (Order orderToRemove in orders.SelectedItems)
            {
                var order = CoreModel.init().Orders.FirstOrDefault(o => o.IdOrder == orderToRemove.IdOrder);

                if (order != null)
                {
                   
                    var orderGoods = CoreModel.init().OrderGoods.Where(og => og.Idorders == order.IdOrder).ToList();
                    CoreModel.init().OrderGoods.RemoveRange(orderGoods);

                    
                    CoreModel.init().Orders.Remove(order);
                }
            }

            
            CoreModel.init().SaveChanges();
            Update();
        }

        private void Check_order(object sender, RoutedEventArgs e)
        {
            if (orders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку");
                return;
            }
            if (orders.SelectedItems.Count > 1)
            {
                MessageBox.Show("Ошибка: Выбрано более одного заказа");
                return;
            }

            var selectedOrderData = orders.SelectedItem as dynamic;
            if (selectedOrderData != null)
            {
                int orderId = selectedOrderData.IdOrder;
                Order selectedOrder = CoreModel.init().Orders.FirstOrDefault(o => o.IdOrder == orderId);

                if (selectedOrder != null)
                {
                    products_in_order PIO = new products_in_order(selectedOrder);
                    NavigationService.Navigate(PIO);
                }
            }
        }


        private void back_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void thisProduct(object sender, MouseButtonEventArgs e)
        {
            if (orders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку");
                return;
            }
            if (orders.SelectedItems.Count > 1)
            {
                MessageBox.Show("Ошибка: Выбрано более одного заказа");
                return;
            }

            var selectedOrderData = orders.SelectedItem as dynamic;
            if (selectedOrderData != null)
            {
                int orderId = selectedOrderData.IdOrder;
                Order selectedOrder = CoreModel.init().Orders.FirstOrDefault(o => o.IdOrder == orderId);

                if (selectedOrder != null)
                {
                    products_in_order PIO = new products_in_order(selectedOrder);
                    NavigationService.Navigate(PIO);
                }
            }
        }
    }
}
