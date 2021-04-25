using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using RecipeBook.databaseClasses;
using RecipeBook.service;

namespace RecipeBook.viewModels
{
  public  class CategoriesPageViewModel:ViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        RelayCommand deleteCommand;
        RelayCommand addCommand;
        public CategoriesPageViewModel()
        {
            Categories=new ObservableCollection<Category>(App.dbContext.Categories.ToList());
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


    }
}
