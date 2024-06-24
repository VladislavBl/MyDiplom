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

namespace Diplom.Admin_pages
{
    /// <summary>
    /// Логика взаимодействия для Users_Page.xaml
    /// </summary>
    public partial class Users_Page : Page
    {

        public Users_Page()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            DataGridUsers.ItemsSource = CoreModel.init().Users.ToList();
        }

        private void Del_Users_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ошибка: Нужно выбрать хотя бы одну строку!");
                return;
            }
            foreach (var selectedItem in DataGridUsers.SelectedItems)
            {
                var user = selectedItem as User;
                if (user != null && user.UsersStatus == "Admin")
                {
                    MessageBox.Show("Ошибка: Нельзя удалять пользователя со статусом администратора");
                    return;
                }
            }
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранные поля?", "Удалить выбранные поля?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            var context = CoreModel.init();
            List<User> usersToRemove = new List<User>();

            foreach (User item in DataGridUsers.SelectedItems)
            {
                bool hasOrders = context.Orders.Any(order => order.Buyer == item.IdUsers);

                if (hasOrders)
                {
                    MessageBox.Show($"Ошибка: Невозможно удалить пользователя {item.Login}, у которого имеются заказы.");
                }
                else
                {
                    usersToRemove.Add(item);
                }
            }

            if (usersToRemove.Count > 0)
            {
                context.Users.RemoveRange(usersToRemove);
                context.SaveChanges();
                Update();
                MessageBox.Show("Выбранные пользователи успешно удалены.");
            }
        }

        private void Add_Show_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_User_Page(new User()));
        }

        private void Redact_Show_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsers.SelectedItems.Count > 1)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: выбрана более, чем одна строка!")));
                return;
            }
            else if (DataGridUsers.SelectedItems.Count == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: не выбран ни один пользователь!")));
                return;
            }

            User? User1Edit = DataGridUsers.SelectedItem as User;
            if (User1Edit == null)
            {
                {
                    Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: не выбрана строка для редактирования!")));
                }
                return;
            }
            NavigationService.Navigate(new Add_User_Page(User1Edit));
        }

        private void Doc_Vis_Change(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void Text_for_search(object sender, TextChangedEventArgs e)
        {
            string searchText = Search.Text.ToLower();

            var users = CoreModel.init().Users.ToList();

            var filters = users.Where(user => user.Name.ToLower().Contains(searchText) ||
            user.Surname.ToLower().Contains(searchText) ||
            user.Lastname.ToLower().Contains(searchText)).ToList();

            DataGridUsers.ItemsSource = filters;
        }
    }
}
