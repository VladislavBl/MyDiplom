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
    /// Логика взаимодействия для Page_info.xaml
    /// </summary>
    public partial class Page_info : Page
    {
        User info;
        User user;
        public Page_info()
        {  
            this.info = ((MainWindow)Application.Current.MainWindow).user;
            this.DataContext = info;
            InitializeComponent();
            this.user = ((MainWindow)Application.Current.MainWindow).user;
        }


        private void Redact(object sender, RoutedEventArgs e)
        {
            if (info.Name == null || info.Name.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Имя' должно быть заполнено!")));
                return;
            }
            if (info.Name.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: поле 'Имя' не должно содержать цифр или любых других знаков!");
                return;
            }
            info.Name = info.Name.Trim();

            if (info.Surname == null || info.Surname.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Фамилия' должно быть заполнено!")));
                return;
            }
            if (info.Surname.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: поле 'Фамиля' не должно содержать цифр или любых других знаков!");
                return;
            }
            info.Surname = info.Surname.Trim();

            if (info.Lastname.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: поле 'Отчество' не должно содержать цифр или любых других знаков!");
                return;
            }

            if (info.IdUsers == 0)
            {
                CoreModel.init().Users.Add(info);
            }

            if (MessageBox.Show("Вы уверены, что хотите применить изменения?", "Применить изменения?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                CoreModel.init().SaveChanges();
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Изменения успешно применены!")));
            }

        }

        private void Next(object sender, RoutedEventArgs e)
        {

            if ((currentPassword.Password == "") || (newPassword.Password == ""))
            {
                MessageBox.Show("Ошибка: Не заполнены поля ввода!");
                return;
            }

            if (user.Password != currentPassword.Password)
            {
                MessageBox.Show("Ошибка: Текущий пароль указан неверно!");
                return;
            }

            if (newPassword.Password != confirmPassword.Password)
            {
                MessageBox.Show("Ошибка: Новый пароль и повтор пароля не совпадают!");
                return;
            }

            if(user.Password == newPassword.Password)
            {
                MessageBox.Show("Ошибка: Новый пароль не должен совпадать со старым");
                return;
            }

            user.Password = newPassword.Password;
            CoreModel.init().Users.Update(user);
            CoreModel.init().SaveChanges();
            Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Пароль изменён!")));
        }

        private void Doc_vis_change(object sender, DependencyPropertyChangedEventArgs e)
        {
            CoreModel.init().Entry(user).Reload();
        }
    }
}
