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
            Recipes = new ObservableCollection<BookRecipe>();
        }

        public long IdBook { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public virtual ObservableCollection<BookRecipe> Recipes { get; set; }
    }
}
