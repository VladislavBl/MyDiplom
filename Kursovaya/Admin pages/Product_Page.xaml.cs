using Diplom.BdModels;
using Diplom.Pages;
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

namespace Diplom.Admin_pages
{
    /// <summary>
    /// Логика взаимодействия для Product_Page.xaml
    /// </summary>
    public partial class Product_Page : Page
    {
        User user;
        List<Product> cachedProducts; 
        List<Product> filteredProducts;
        public Product_Page()
        {
            InitializeComponent();
            Update();

            this.user = ((MainWindow)Application.Current.MainWindow).user!;

            if(user.UsersStatus == "User")
            {
                grida.Visibility = Visibility.Hidden;
                gridu.Visibility = Visibility.Visible;
            }
        }


        public void Update()
        {
            if (cachedProducts == null)
            {
                cachedProducts = CoreModel.init().Products.ToList();
            }
            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                ListViewProduct.ItemsSource = cachedProducts;
            }
            else
            {
                FilterProducts(Search.Text.ToLower());
            }
        }


        private void FilterProducts(string searchText)
        {
            filteredProducts = cachedProducts
                .Where(product => product.ProductName.ToLower().Contains(searchText))
                .ToList();
            ListViewProduct.ItemsSource = filteredProducts;
        }

        private void UpdateCache()
        {
            cachedProducts = CoreModel.init().Products.ToList();
            if (!string.IsNullOrWhiteSpace(Search.Text))
            {
                FilterProducts(Search.Text.ToLower());
            }
        }

        private void ListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var sItem = ListViewProduct.SelectedItem as Product;

            NavigationService.Navigate(new DetailPage(sItem));
        }


        private void Del_Users_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку!");
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите удалить данный товар?", "Удалить товар?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            foreach (Product item in ListViewProduct.SelectedItems)
            {
                CoreModel.init().Products.Remove(item);

            }
            CoreModel.init().SaveChanges();
            UpdateCache();
            Update();
        }

        private void Add_Show_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Product_Page(new Product()));
        }

        private void Redact_Show_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewProduct.SelectedItems.Count > 1)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: выбрана более, чем одна строка!")));
                return;
            }
            else if (ListViewProduct.SelectedItems.Count == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: не выбран ни один пользователь!")));
                return;
            }

            Product Product1Edit = ListViewProduct.SelectedItem as Product;
            if (Product1Edit == null)
            {
                {
                    Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: не выбрана строка для редактирования!")));
                }
                return;
            }
            NavigationService.Navigate(new Add_Product_Page(Product1Edit));
        }

        private void Doc_Vis_Change(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateCache();
            Update();
        }

        private void buy_click(object sender, RoutedEventArgs e)
        {
            if (ListViewProduct.SelectedItem == null)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Выбирите товар для покупки!")));
                return;
            }
            else
            {
                var sItem = ListViewProduct.SelectedItem as Product;

                NavigationService.Navigate(new DetailPage(sItem));
            }
        }

        private void Text_for_search(object sender, TextChangedEventArgs e)
        {
            string searchText = Search.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText)) 
            {
                ListViewProduct.ItemsSource = cachedProducts;
            }
            else
            {
                FilterProducts(searchText);
            }
        }
    }
}
