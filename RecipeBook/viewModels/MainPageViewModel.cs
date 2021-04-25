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
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Category> popularCategoriesList;
        RelayCommand deleteCommand;
        RelayCommand addCommand;
        public ObservableCollection<Category> PopularCategoriesList
        {
            get { return popularCategoriesList; }
            set
            {
                popularCategoriesList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Book> LastBooks { get; set; }

       

        public MainPageViewModel()
        {
            App.dbContext.Categories.Load();
            PopularCategoriesList = new ObservableCollection<Category>(App.dbContext.Categories.Local.ToBindingList());
            LastBooks=new ObservableCollection<Book>(App.dbContext.Books.ToList());
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand((selectedItem) =>
                       {
                           var message =  MessageBox.Show("Вы хотите удалить категорию ?","Предупреждение",MessageBoxButton.OKCancel);
                           if (selectedItem == null || message == MessageBoxResult.Cancel){ return;}
                           Category category = selectedItem as Category;
                           PopularCategoriesList.Remove(category);
                           App.dbContext.Categories.Remove(category);

                           App.dbContext.SaveChanges();
                       }));
            }
        }
       

       
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
