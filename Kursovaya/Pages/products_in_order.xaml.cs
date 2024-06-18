using Diplom.BdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Diplom.Pages
{
    /// <summary>
    /// Логика взаимодействия для products_in_order.xaml
    /// </summary>
    public partial class products_in_order : Page
    {
        private Order selectedOrder;
        private decimal orderSum;

        public products_in_order(Order order)
        {
            InitializeComponent();
            this.selectedOrder = order;
            Update();
        }

        private void Update()
        {
            if (selectedOrder != null)
            {
                var orderDetails = CoreModel.init().OrderGoods
                    .Include(og => og.IdproductNavigation)
                    .Where(og => og.Idorders == selectedOrder.IdOrder)
                    .Select(og => new
                    {
                        ProductName = og.IdproductNavigation.ProductName,
                        Quantity = og.Quantity,
                        Price = og.IdproductNavigation.ProductPrice,
                        Sum = og.Quantity * og.IdproductNavigation.ProductPrice
                    })
                    .ToList();

                orderSum = orderDetails.Sum(od => od.Sum);

                datagridorders.ItemsSource = orderDetails;

                TextBlock sumTextBlock = FindName("SumTextBlock") as TextBlock;
                if (sumTextBlock != null)
                {
                    sumTextBlock.Text = $"Сумма заказа: {orderSum:F2}";
                }
            }
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

}
