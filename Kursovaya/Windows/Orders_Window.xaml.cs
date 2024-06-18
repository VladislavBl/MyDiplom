using Diplom.BdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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

namespace Diplom.Windows
{
    /// <summary>
    /// Логика взаимодействия для Orders_Window.xaml
    /// </summary>
    public partial class Orders_Window : Window
    {
        private readonly Product selectedProduct;
        private readonly User currentUser;

        public Orders_Window(Product product, User currentUser)
        {
            InitializeComponent();
            this.selectedProduct = product;
            this.currentUser = currentUser;
            Update();
        }

        public void Update()
        {
            if (currentUser != null)
            {
                var userOrders = CoreModel.init().Orders.Where(order => order.Buyer == currentUser.IdUsers).ToList();
                orders.ItemsSource = userOrders;
            }
        }

        private void doubleclick(object sender, MouseButtonEventArgs e)
        {
            Order selectedOrder = orders.SelectedItem as Order;

            if (selectedOrder == null || selectedProduct == null)
            {
                MessageBox.Show("Выберите заказ и продукт для добавления товара.");
                return;
            }

            var existingOrderGood = selectedOrder.OrderGoods.FirstOrDefault(og => og.Idproduct == selectedProduct.IdProduct);
            if (existingOrderGood != null)
            {
                existingOrderGood.Quantity++;
            }
            else
            {
                OrderGood newOrderGood = new OrderGood
                {
                    Idorders = selectedOrder.IdOrder,
                    Idproduct = selectedProduct.IdProduct,
                    Quantity = 1
                };
                selectedOrder.OrderGoods.Add(newOrderGood);
            }

            CoreModel.init().SaveChanges();

            MessageBox.Show("Товар успешно добавлен в заказ.");
            Close();
        }

        private void add_in_order(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = orders.SelectedItem as Order;

            if (selectedOrder == null || selectedProduct == null)
            {
                MessageBox.Show("Выберите заказ и продукт для добавления товара.");
                return;
            }

            var existingOrderGood = selectedOrder.OrderGoods.FirstOrDefault(og => og.Idproduct == selectedProduct.IdProduct);
            if (existingOrderGood != null)
            {
                existingOrderGood.Quantity++;
            }
            else
            {
                OrderGood newOrderGood = new OrderGood
                {
                    Idorders = selectedOrder.IdOrder,
                    Idproduct = selectedProduct.IdProduct,
                    Quantity = 1
                };
                selectedOrder.OrderGoods.Add(newOrderGood);
            }

            CoreModel.init().SaveChanges();

            MessageBox.Show("Товар успешно добавлен в заказ.");
            Close();
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private static int orderNumberCounter = 1;

        private void add_click(object sender, RoutedEventArgs e)
        {
            int maxOrderNumber = CoreModel.init().Orders.Max(o => o.OrderNumber);

            Order newOrder = new Order
            {
                OrderDate = new System.DateOnly(System.DateTime.Today.Year, System.DateTime.Today.Month, System.DateTime.Today.Day),
                Buyer = currentUser.IdUsers,
                OrderNumber = maxOrderNumber + 1
            };

            orderNumberCounter++;

            newOrder.OrderGoods = new List<OrderGood>();

            OrderGood orderGood = new OrderGood
            {
                Idorders = newOrder.IdOrder,
                Idproduct = selectedProduct.IdProduct,
                Quantity = 1
            };

            newOrder.OrderGoods.Add(orderGood);

            CoreModel.init().Orders.Add(newOrder);
            CoreModel.init().OrderGoods.Add(orderGood);
            CoreModel.init().SaveChanges();

            MessageBox.Show("Новый заказ успешно создан, и товар успешно добавлен в заказ.");
            Close();
        }

    }

}
