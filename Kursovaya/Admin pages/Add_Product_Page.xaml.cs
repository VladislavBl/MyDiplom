using Diplom.BdModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Логика взаимодействия для Add_Product_Page.xaml
    /// </summary>
    public partial class Add_Product_Page : Page
    {

        Product Products;
        public Add_Product_Page(Product product)
        {
            this.Products = product;
            DataContext = Products;

            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Products.ProductName == null || Products.ProductName.Trim().Length == 0)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Название товара' должно быть заполнено!")));
                return;
            }
            Products.ProductName = Products.ProductName.Trim();

            if (Products.ProductPrice == null)
            {
                Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Ошибка: Поле 'Цена товара' должно быть заполнено!")));
                return;
            }

            if (Products.IdProduct == 0)
            {
                CoreModel.init().Products.Add(Products);
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
            if (Products.IdProduct != 0)
            {
                CoreModel.init().Entry(Products).Reload();
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                byte[] imageData = File.ReadAllBytes(imagePath);
                Products.ProductPicture = imageData;

                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                SelectedImage.Source = bitmap;
            }
        }
    }
}
