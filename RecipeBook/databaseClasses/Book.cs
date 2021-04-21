using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Book
    {
        public Book()
        {
            BookRecipes = new ObservableCollection<BookRecipe>();
        }

        public long IdBook { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public  ObservableCollection<BookRecipe> BookRecipes { get; set; }
    }
}
