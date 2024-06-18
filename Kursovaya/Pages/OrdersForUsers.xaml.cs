using Diplom.BdModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Логика взаимодействия для OrdersForUsers.xaml
    /// </summary>
    public partial class OrdersForUsers : Page
    {
        public OrdersForUsers()
        {
            InitializeComponent();
            Update();

        }
        public void Update()
        {
            DataGridUsers.ItemsSource = CoreModel.init().Users.ToList();
        }

        private void orders_del(object sender, RoutedEventArgs e)
        {
            if(DataGridUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку!");
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранные поля?", "Удалить выбранные поля?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            foreach (User item in DataGridUsers.SelectedItems)
            {
                CoreModel.init().Users.Remove(item);
            }
            CoreModel.init().SaveChanges();
            Update();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку");
                return;
            }
            if (DataGridUsers.SelectedItems.Count > 1)
            {
                MessageBox.Show("Ошибка: Выбрано более одного заказа");
                return;
            }

            var selectedUser = DataGridUsers.SelectedItem as User;

            if (selectedUser != null)
            {
                Orders_Users ordersUsersPage = new Orders_Users();
                ordersUsersPage.SetCurrentUser(selectedUser);
                NavigationService.Navigate(ordersUsersPage);
            }
        }

        private void Text_for_search(object sender, TextChangedEventArgs e)
        {
            string searchText = Search.Text.ToLower();

            var users = CoreModel.init().Users.ToList();

            var filters = users.Where(user => user.Name.ToLower().Contains(searchText) ||
            user.Surname.ToLower().Contains(searchText) ||
            user.Lastname.ToLower().Contains(searchText) ||
            user.UsersStatus.ToLower().Contains(searchText)).ToList();

            DataGridUsers.ItemsSource = filters;
        }

        private void double_click(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = DataGridUsers.SelectedItem as User;

            if (selectedUser != null)
            {
                Orders_Users ordersUsersPage = new Orders_Users();
                ordersUsersPage.SetCurrentUser(selectedUser);
                NavigationService.Navigate(ordersUsersPage);
            }
        }
    }
}
