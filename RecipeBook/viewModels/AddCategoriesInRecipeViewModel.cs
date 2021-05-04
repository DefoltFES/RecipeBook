using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.databaseClasses;

namespace RecipeBook.viewModels
{
    public class AddCategoriesInRecipeViewModel:ViewModel
    {
        public ICollection<Category> AllCategories { get; set; }
        public ICollection<Category> AddCategories { get; set; }
        public AddCategoriesInRecipeViewModel()
        {
            AllCategories = App.dbContext.Categories.Where(x => x.IdCategory != 1).ToList();
            AddCategories=new List<Category>();
        }
    }
}
