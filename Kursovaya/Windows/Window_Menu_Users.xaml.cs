using Diplom.Admin_pages;
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

namespace Diplom.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window_Menu_Users.xaml
    /// </summary>
    public partial class Window_Menu_Users : Window
    {
        User user;
        public Window_Menu_Users()
        {
            InitializeComponent();
            this.user = ((MainWindow)Application.Current.MainWindow).user!;


            if (user.UsersStatus == "User")
            {
                button.Visibility = Visibility.Hidden;
            }

        }

        private void Show_Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Show();
            Close();
        }

        private void Show_Catalog_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.Navigate( new Product_Page());
        }


        private void infom(object sender, RoutedEventArgs e)
        {
            FrameNav.Navigate(new Page_info());
        }

        private void Orders(object sender, RoutedEventArgs e)
        {
            Orders_Users ordersPage = new Orders_Users();
            ordersPage.SetCurrentUser(user);
            FrameNav.Navigate(ordersPage);
        }

        private void Adminmode(object sender, RoutedEventArgs e)
        {
            if (user.UsersStatus == "Admin")
            {
                Window_Menu window = new Window_Menu();
                window.Show();
                Close();
            }
            
        }
    }
}
