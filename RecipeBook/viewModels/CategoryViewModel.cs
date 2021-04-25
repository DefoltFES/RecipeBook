using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.databaseClasses;

namespace RecipeBook.viewModels
{
    public class CategoryViewModel:ViewModel
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

        public byte[] Image
        {
            get { return category.Image; }
            set
            {
                category.Image = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ListCategory> ListCategories
        {
            get { return category.ListCategories; }
            set
            {
                category.ListCategories = value;
                OnPropertyChanged();
            }
        }
    }
}
