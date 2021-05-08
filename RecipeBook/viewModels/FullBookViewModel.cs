﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeBook.databaseClasses;

namespace RecipeBook.viewModels
{
    class FullBookViewModel
    {
        private Book book;

        public FullBookViewModel(Book book)
        {
            App.dbContext.Books.Where(b => b.IdBook == book.IdBook).Include(x => x.Recipes).ThenInclude(r => r.Recipe).Load();
            this.book= book;
        }

        public string Name
        {
            get => book.Name;
        }

        public string Image
        {
            get => book.Image;
        }
        public string Description
        {
            get => book.Description;
        }

        public ICollection<BookRecipe> Recipes
        {
            get => book.Recipes;
        }
    }
}
