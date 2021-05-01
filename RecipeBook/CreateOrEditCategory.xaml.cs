using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for CreateOrEditCategory.xaml
    /// </summary>
    public partial class CreateOrEditCategory : Window
    {
        public Category Categories { get; private set; }
        public CreateOrEditCategory(Category categories)
        {
            InitializeComponent();
            Categories = categories;
            this.DataContext = Categories;

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
                Categories.Image = openFileDialog.FileName;
            }
        }
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length > 0)
            {
                Categories.Image = CopyAndSaveImages();
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Название категории не может быть пустым");
                return;
            }
        }

        private string CopyAndSaveImages()
        {
            if (Categories.Image != null)
            {
                var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory+ @"images\categories";
                if (!Directory.Exists(fullDirectoryPath))
                {
                    Directory.CreateDirectory(fullDirectoryPath);
                }
                var nameImage = Path.GetFileName(Categories.Image);
                var newImageSource = Path.Combine(fullDirectoryPath,nameImage);
                if (Path.IsPathFullyQualified(Categories.Image))
                {
                    File.Copy(Categories.Image, newImageSource, true);
                    return Path.Combine(@"\images\categories", nameImage);
                }
                else
                {
                    return Categories.Image;
                }

            }
            else
            {
                return null;
            }

        }

        
    }
}
