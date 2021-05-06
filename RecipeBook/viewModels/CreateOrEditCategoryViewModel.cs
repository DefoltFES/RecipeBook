using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using RecipeBook.databaseClasses;
using RecipeBook.service;

namespace RecipeBook.viewModels
{
    public class CreateOrEditCategoryViewModel:ViewModel
    {
        private Category category;
        
        public Category Category
        {
            get => category;
            set { category = value; }
        }

        public CreateOrEditCategoryViewModel(Category category)
        {
            Category = category;
        }

        private RelayCommand addImage;
        private RelayCommand saveChanges;
        public string Image
        {
            get => Category.Image;
            set
            {
                Category.Image = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => Category.Name;
            set
            {
                Category.Name = value;
                OnPropertyChanged();
            }
        }

        private string CopyAndSaveImages(string path)
        {
            if (path != null)
            {

                var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"images\categories";
                if (!Directory.Exists(fullDirectoryPath))
                {
                    Directory.CreateDirectory(fullDirectoryPath);
                }

                var nameImage = Path.GetFileName(path);
                var newImageSource = Path.Combine(fullDirectoryPath, nameImage);
                if (Path.IsPathFullyQualified(path))
                {
                    File.Copy(path, newImageSource, true);
                    return Path.Combine(@"images\categories\", nameImage);
                }
                else
                {
                    return path;
                }

            }
            else
            {
                return null;
            }

        }

        public RelayCommand AddImage
        {
            get
            {
                return addImage ??
                       (addImage = new RelayCommand((selectedItem) =>
                       {
                           if (selectedItem == null)
                           {
                               return;
                           }
                           var image = selectedItem as Image;
                           OpenFileDialog openFileDialog = new OpenFileDialog()
                           {
                               Filter = "Image File (*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png",
                               CheckPathExists = true,
                               Multiselect = false
                           };

                           if (openFileDialog.ShowDialog() == true)
                           {
                               image.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                           }

                       }));
            }
        }

        public RelayCommand SaveChanges
        {
            get
            {
                return saveChanges ??= new RelayCommand((o) =>
                {

                    if (this.Image != null)
                    {
                        if (Path.IsPathFullyQualified(this.Image))
                        {
                            this.Image = CopyAndSaveImages(new Uri(this.Image).LocalPath);
                        }
                    }
                    
                });
            }
        }
    }
}
