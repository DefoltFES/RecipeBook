using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using RecipeBook.Annotations;
using RecipeBook.databaseClasses;
using RecipeBook.pages;
using RecipeBook.service;

namespace RecipeBook.viewModels
{
    class MainPageViewModel:ViewModel
    {


        public ObservableCollection<Category> PopularCategoriesList { get; set; }


        public ObservableCollection<Book> LastBooks { get; set; }



        public MainPageViewModel()
        {
            App.dbContext.Categories.Load();
            PopularCategoriesList = new ObservableCollection<Category>(App.dbContext.Categories.Local.ToBindingList());
            LastBooks = new ObservableCollection<Book>(App.dbContext.Books.ToList());
        }





    }
}
