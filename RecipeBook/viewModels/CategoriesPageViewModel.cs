using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
      public Category SelectedCategory { get; set; }
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
            App.dbContext.Categories.Load();
            Categories=App.dbContext.Categories.Local.ToObservableCollection();
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand((selectedItem) =>
                       {
                           var message = MessageBox.Show("Вы хотите удалить категорию ?", "Предупреждение", MessageBoxButton.OKCancel);
                           if (selectedItem == null || message == MessageBoxResult.Cancel) { return; }
                           Category category = selectedItem as Category;
                           Categories.Remove(category);
                           App.dbContext.Categories.Remove(category);
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
                           if (addCategoryWindow.ShowDialog() == true )
                           {
                               Category category = addCategoryWindow.Category;
                               Categories.Add(category);
                               App.dbContext.Categories.Add(category);
                               App.dbContext.SaveChanges();
                           }
                       }));
            }
        }

        public RelayCommand EditCommand => editCommand ??
                       (editCommand = new RelayCommand((selectedItem) =>
                       {
                           if (selectedItem == null) return;
                           int newIndex = Categories.IndexOf((Category) selectedItem);
                           Category category = selectedItem as Category;
                           Category vm = (Category)category.Clone();
                           CreateOrEditCategory phoneWindow = new CreateOrEditCategory(vm);
                           if (phoneWindow.ShowDialog() == true)
                           {
                               Categories.Remove((Category) selectedItem);

                               category = App.dbContext.Categories.Find(phoneWindow.Category.IdCategory);
                               
                               if (category != null)
                               {
                                 
                                   category.Name = phoneWindow.Category.Name;
                                   category.Image = phoneWindow.Category.Image;
                                   category.IdCategory = phoneWindow.Category.IdCategory;
                                   category.ListCategories = phoneWindow.Category.ListCategories;
                                   Categories.Remove((Category)selectedItem);
                                   Categories.Add(category);
                                   int oldIndex = Categories.IndexOf(category);
                                   Categories.Move(oldIndex,newIndex);
                                   App.dbContext.Entry(category).State = EntityState.Modified;
                                   App.dbContext.SaveChanges();
                                   

                               }
                           }
                       }));
    }
}
