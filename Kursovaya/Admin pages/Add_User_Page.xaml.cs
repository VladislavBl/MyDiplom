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
    /// Логика взаимодействия для Add_User_Page.xaml
    /// </summary>
    public partial class Add_User_Page : Page
    {
        User Users;
        public Add_User_Page(User Use)
        {
            this.Users = Use;
            DataContext = Users;

            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Users.Name == null || Users.Name.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Имя' должно быть заполнено!")));
                return;
            }
            if (Users.Name.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: поле 'Имя' не должно содержать цифр или любых других знаков!");
                return;
            }
            Users.Name = Users.Name.Trim();

            if (Users.Surname == null || Users.Surname.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Фамилия' должно быть заполнено!")));
                return;
            }
            if (Users.Surname.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: поле 'Фамилия' не должно содержать цифр или любых других знаков!");
                return;
            }
            Users.Surname = Users.Surname.Trim();

            if (Users.Login == null || Users.Login.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Логин' должно быть заполнено!")));
                return;
            }
            Users.Login = Users.Login.Trim();

            if (Users.Lastname == null || Users.Lastname.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Отчество' должно быть заполнено!")));
                return;
            }
            if (Users.Lastname.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: поле 'Отчество' не должно содержать цифр или любых других знаков!");
                return;
            }

            if (Users.Password == null || Users.Password.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Пароль' должно быть заполнено!")));
                return;
            }
            Users.Password = Users.Password.Trim();

            if (Users.UsersStatus == null || Users.UsersStatus.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Статус' должно быть заполнено!")));
                return;
            }
            Users.UsersStatus = Users.UsersStatus.Trim();

            if(Users.UsersStatus != "User" && Users.UsersStatus != "Admin")
            {

                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Статус указан не верно")));
                return;
            }

            if (Users.IdUsers == 0)
            {
                CoreModel.init().Users.Add(Users);
            }

            CoreModel.init().SaveChanges();
            Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Изменения успешно применены!")));
            NavigationService.GoBack();
        }

        private void Back_Users_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Doc_vis_change(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Users.IdUsers != 0)
            {
                CoreModel.init().Entry(Users).Reload();
            }
        }
    }
}
