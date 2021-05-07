using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CreateOrEditBookViewModel:ViewModel
    {
        private Book book;
        public Book Book
        {
            get => book;
        }

        private ObservableCollection<BookRecipe> recipes;
        
        public ObservableCollection<BookRecipe> Recipes
        {
            get => recipes;
            set
            {
                recipes = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => book.Name;
            set
            {
                book.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => book.Description;
            set
            {
                book.Description = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get => book.Image;
            set
            {
                book.Image = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand addImage;
        private RelayCommand saveChanges;
        private RelayCommand addRecipes;
        public CreateOrEditBookViewModel(Book book)
        {
            this.book = book;
            Recipes=new ObservableCollection<BookRecipe>(Book.Recipes);
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

                    this.book.Recipes = Recipes;

                });
            }
        }

        public RelayCommand AddRecipe
        {
            get
            {
                return addRecipes ??= new RelayCommand(o =>
                {
                    AddRecipeBook listRecipes=new AddRecipeBook();
                    if (listRecipes.ShowDialog()==true)
                    {
                        //foreach (var category in listRecipes)
                        //{
                            
                        //}
                    }
                }); 

            }
        }
        private string CopyAndSaveImages(string path)
        {
            if (path != null)
            {

                var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"images\book";
                if (!Directory.Exists(fullDirectoryPath))
                {
                    Directory.CreateDirectory(fullDirectoryPath);
                }

                var nameImage = Path.GetFileName(path);
                var newImageSource = Path.Combine(fullDirectoryPath, nameImage);
                if (Path.IsPathFullyQualified(path))
                {
                    File.Copy(path, newImageSource, true);
                    return Path.Combine(@"images\recipes\", nameImage);
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

    }
}
