using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using RecipeBook.databaseClasses;
using RecipeBook.viewModels;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for CreateOrEditCategory.xaml
    /// </summary>
    public partial class CreateOrEditCategory : Window
    {
        public CategoryViewModel Category { get; private set; }
        public CreateOrEditCategory(CategoryViewModel category,bool isEdit=false)
        {
            InitializeComponent();
            Category = category;
            this.DataContext = Category;
            if (!isEdit)
            {
                TextMode.Text = "Создание категории";
            }
            else
            {
                TextMode.Text = "Редакатирование";
            }
        }
        private void ImageButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image File (*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png",
                CheckPathExists = true,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                ImageCategory.Source =  new BitmapImage(fileUri);;
            }

        }
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            Category.Image = BitmapSourceToByteArray((BitmapSource)ImageCategory.Source);
            this.DialogResult = true;
        }

        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
