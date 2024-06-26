using Diplom.BdModels;
using Diplom.Windows;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Diplom
{
    /// <summary>
    ///1nteraction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user;
 
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AutorClick(object sender, RoutedEventArgs e)
        {

            if (LoginTB.Text == "" || PasswordBox.Password == "")
            {
                MessageBox.Show("Ошибка: заполните все поля");
                return;
            }

            this.user = CoreModel.init().Users.FirstOrDefault(p => p.Login == LoginTB.Text &&
               (p.Password == PassTB.Text || p.Password == PasswordBox.Password));

            if (user != null)
            {

                if (user.UsersStatus == "Admin")
                {
                    Window_Menu wind = new Window_Menu();
                    wind.Show();
                    Hide();
                }
                else if (user.UsersStatus == "User")
                {
                    Window_Menu_Users wind = new Window_Menu_Users();
                    wind.Show();
                    Hide();
                }
                else if(user.UsersStatus == "TopAdmin")
                {
                    Window_Menu wind = new Window_Menu();
                    wind.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Пользователь неправильно зарегестрирован!");
                }
                return;

            }

            MessageBox.Show("Ошибка авторизации: неправильный логин или пароль!");
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox.IsChecked.Value)
            {
                PassTB.Text = PasswordBox.Password;
                PassTB.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordBox.Password = PassTB.Text;
                PassTB.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
            }
        }

        private void Registrate(object sender, RoutedEventArgs e)
        {    
            Window_Reg wind = new Window_Reg();
            wind.Show();
            Hide();
        }
    }
}
