using Microsoft.EntityFrameworkCore;
using RecipeBook.databaseClasses;
using System.Collections.ObjectModel;
using System.Linq;

namespace RecipeBook.viewModels
{
    class MainPageViewModel : ViewModel
    {


        public ObservableCollection<Category> PopularCategoriesList { get; set; }


        public ObservableCollection<Book> LastBooks { get; set; }



        public MainPageViewModel()
        {
            PopularCategoriesList = new ObservableCollection<Category>(App.dbContext.Categories.OrderByDescending(x=>x.ListCategories.Count()).Take(5));
            LastBooks = new ObservableCollection<Book>(App.dbContext.Books.ToList());
        }





    }
}
