using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.databaseClasses;

namespace RecipeBook.viewModels
{
    class CategoryViewModel:ViewModel
    {
        private Category category;

        public CategoryViewModel(Category c)
        {
            category = c;
        }

        public string Name
        {
            get { return category.Name; }
            set
            {
                category.Name = value;
                OnPropertyChanged();
            }
        }
    }
}
