using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using RecipeBook.databaseClasses;
using RecipeBook.service;

namespace RecipeBook.viewModels
{
  public  class CategoriesPageViewModel:ViewModel
  {

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }


        RelayCommand deleteCommand;
        RelayCommand addCommand;
        RelayCommand editCommand;

        public CategoriesPageViewModel()
        {
            App.dbContext.Categories.Include(x => x.ListCategories).ThenInclude(x=>x);
            Categories = new ObservableCollection<Category>(App.dbContext.Categories.ToList());;
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand((selectedItem) =>
                       {
                           if (selectedItem == null)
                           {
                               return;
                           }
                           Category categories = selectedItem as Category;
                           if (categories.IdCategory == 1)
                           {
                               MessageBox.Show("Нельзя удалить эту категорию");
                               return;
                           }
                           var message = MessageBox.Show("Вы хотите удалить категорию ?", "Предупреждение", MessageBoxButton.OKCancel);
                           var path = System.AppDomain.CurrentDomain.BaseDirectory + categories.Image;
                           if (message == MessageBoxResult.Cancel) { return; }
                           Categories.Remove(categories);
                           if (categories.Image==null)
                           {
                               File.Delete(path);
                           }
                           App.dbContext.ListCategories.RemoveRange(categories.ListCategories);
                           App.dbContext.Categories.Remove(categories);
                           App.dbContext.SaveChanges();
                           
                       }));
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand((o) =>
                       {
                           CreateOrEditCategory addCategoryWindow = new CreateOrEditCategory(new Category());
                           if (addCategoryWindow.ShowDialog() == true)
                           {
                               Category categories =addCategoryWindow.Categories;
                               Categories.Add(categories);
                               App.dbContext.Categories.Add(categories);
                               App.dbContext.SaveChanges();
                           }
                       }));
            }
        }

        public RelayCommand EditCommand => editCommand ??
                       (editCommand = new RelayCommand((selectedItem) =>
                       {
                           if (selectedItem == null) return;
                           Category category = selectedItem as Category;
                           if (category.IdCategory == 1)
                           {
                               MessageBox.Show("Нельзя редактировать эту категорию");
                               return;
                           }
                           int newIndex = Categories.IndexOf((Category)selectedItem);
                          
                           Category vm = (Category)category.Clone();
                           CreateOrEditCategory editCategory = new CreateOrEditCategory(vm);
                           if (editCategory.ShowDialog() == true)
                           {
                               Categories.Remove((Category)selectedItem);

                               category = App.dbContext.Categories.Find(editCategory.Categories.IdCategory);

                               if (category != null)
                               {
                                   var path = System.AppDomain.CurrentDomain.BaseDirectory + category.Image;
                                   File.Delete(path);
                                   category.Image = editCategory.Categories.Image;
                                   category.Name = editCategory.Categories.Name;
                                   Categories.Remove((Category)selectedItem);
                                   Categories.Add(category);
                                   int oldIndex = Categories.IndexOf(category);
                                   Categories.Move(oldIndex, newIndex);
                                   App.dbContext.Entry(category).State = EntityState.Modified;
                                   App.dbContext.SaveChanges();


                               }
                           }
                       }));
    }
}
