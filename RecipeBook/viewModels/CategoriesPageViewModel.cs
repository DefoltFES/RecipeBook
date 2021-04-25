using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using RecipeBook.databaseClasses;

namespace RecipeBook.viewModels
{
  public  class CategoriesPageViewModel:ViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }

        public CategoriesPageViewModel()
        {
            
            Categories=new ObservableCollection<Category>(App.dbContext.Categories.ToList());
        }
        
    }
}
