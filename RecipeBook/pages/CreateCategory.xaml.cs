﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using RecipeBook.databaseClasses;

namespace RecipeBook.pages
{
    /// <summary>
    /// Interaction logic for CreateCategory.xaml
    /// </summary>
    public partial class CreateCategory : Page
    {
        public Category Category { get; private set; }
        public bool isClicked { get;private  set;}
        public CreateCategory(Category category=null,bool isEdit=false)
        {
           
            InitializeComponent();
            if (isEdit == false)
            {
                TextMode.Text = "Создание категории";
            }
            else
            {
                TextMode.Text = "Редактирование";
                category.Image= BitmapSourceToByteArray((BitmapSource)App.Current.Resources.FindName("SmallImage"));

            }
            Category = category;
           
            this.DataContext = Category;
            

           
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
                ImageCategory.Source = new BitmapImage(fileUri);
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            isClicked = true;
            NavigationService.GoBack();
            
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
