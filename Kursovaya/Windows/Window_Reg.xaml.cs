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
using System.Windows.Shapes;

namespace Diplom.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window_Reg.xaml
    /// </summary>
    public partial class Window_Reg : Window
    {
        public User Reg;
       
        public Window_Reg()
        {
            this.Reg = new User();
            this.DataContext = Reg;
            InitializeComponent();
        }

        private void Show_Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Show();
            Close();
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Status.Text = "User";

            if (Reg.Name == null || Reg.Name.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Имя' должно быть заполнено!")));
                return;
            }
            if (Reg.Name.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: Поле 'Имя' не должно содержать цифр или любых других знаков!");
                return;
            }
            Reg.Name = Reg.Name.Trim();

            if (Reg.Surname == null || Reg.Surname.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Фамилия' должно быть заполнено!")));
                return;
            }
            if (Reg.Surname.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: Поле 'Фамилия' не должно содержать цифр или любых других знаков!");
                return;
            }
            Reg.Surname = Reg.Surname.Trim();

            if (Reg.Lastname == null || Reg.Lastname.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Отчество' должно быть заполнено!")));
                return;
            }
            if (Reg.Lastname.Any(char.IsDigit))
            {
                MessageBox.Show("Ошибка: Поле 'Отчество' не должно содержать цифр или любых других знаков!");
                return;
            }
            Reg.Lastname = Reg.Lastname.Trim();

            if (Reg.Login == null || Reg.Login.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Логин' должно быть заполнено!")));
                return;
            }
            Reg.Login = Reg.Login.Trim();

            if (Reg.Password == null || Reg.Password.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Пароль' должно быть заполнено!")));
                return;
            }
            Reg.Password = Reg.Password.Trim();

            if(Reg.UsersStatus == null)
            {
                Reg.UsersStatus = "User";
            }
            
            if (Reg.IdUsers == 0)
            {
                CoreModel.init().Users.Add(Reg);
            }

            CoreModel.init().SaveChanges();

            ((MainWindow)Application.Current.MainWindow).Show();
            Close();
        }
    }
}
