using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.databaseClasses;
using RecipeBook.pages;
using RecipeBook.service;

namespace RecipeBook.viewModels
{
    class BookPageViewModel:ViewModel
    {
        private ObservableCollection<Book> books;

        public ObservableCollection<Book> Books
        {
            get => books;
            set
            {
                books = value;
                OnPropertyChanged();
            }
        }

        RelayCommand deleteCommand;
        RelayCommand addCommand;
        RelayCommand editCommand;

        public BookPageViewModel()
        {
            Books=new ObservableCollection<Book>(App.dbContext.Books.ToList());
           
        }

        public RelayCommand AddCommand
        {
            get { return addCommand ??= new RelayCommand((o) =>
            {
                CreateOrEditBook addBook=new CreateOrEditBook(new Book());
                if (addBook.ShowDialog() == true)
                {
                    Book book  = (Book)addBook.Context.Book.Clone();
                    Books.Add(book);
                    App.dbContext.Books.Add(book);
                    App.dbContext.SaveChanges();
                }
            });
            }

        }

    }
}
