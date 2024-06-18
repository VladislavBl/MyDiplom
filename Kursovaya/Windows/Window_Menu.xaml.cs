using Diplom.Admin_pages;
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
using System.Windows.Shapes;

namespace Diplom.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window_Menu.xaml
    /// </summary>
    public partial class Window_Menu : Window
    {
        public Window_Menu()
        {
            InitializeComponent();
        }

        private void Show_User_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.Navigate(new Users_Page());
        }

        private void Show_Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Show();
            Close();
        }

        private void Show_Produtc_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.Navigate(new Product_Page());
        }

        private void Orders(object sender, RoutedEventArgs e)
        {
            FrameNav.Navigate(new OrdersForUsers());
        }

        private void usermode(object sender, RoutedEventArgs e)
        {
            Window_Menu_Users wind = new Window_Menu_Users();
            wind.Show();
            Close();
        }
    }
}
