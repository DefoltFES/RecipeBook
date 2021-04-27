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
                           var message = MessageBox.Show("Вы хотите удалить категорию ?", "Предупреждение", MessageBoxButton.OKCancel);
                           if (selectedItem == null || message == MessageBoxResult.Cancel) { return; }
                           Category categories = selectedItem as Category;
                           Categories.Remove(categories);
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
                               Category categories = addCategoryWindow.Categories;
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
                           int newIndex = Categories.IndexOf((Category)selectedItem);
                           Category category = selectedItem as Category;
                           Category vm = (Category)category.Clone();
                           CreateOrEditCategory editCategory = new CreateOrEditCategory(vm);
                           if (editCategory.ShowDialog() == true)
                           {
                               Categories.Remove((Category)selectedItem);

                               category = App.dbContext.Categories.Find(editCategory.Categories.IdCategory);

                               if (category != null)
                               {

                                   category.Name = editCategory.Categories.Name;
                                   category.Image = editCategory.Categories.Image;
                                   category.IdCategory = editCategory.Categories.IdCategory;
                                   category.ListCategories = editCategory.Categories.ListCategories;
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
