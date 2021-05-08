using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= (deleteCommand = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null)
                    {
                        return;
                    }
                    var book = selectedItem as Book;
                    var message = MessageBox.Show("Вы хотите удалить книгу ?", "Предупреждение", MessageBoxButton.OKCancel);
                    var path = System.AppDomain.CurrentDomain.BaseDirectory + book.Image;
                    if (message == MessageBoxResult.Cancel) { return; }
                    if (book.Image != null)
                    {
                        File.Delete(path);
                    }
                    Books.Remove(book);
                    App.dbContext.BookRecipes.RemoveRange(book.Recipes);
                    App.dbContext.Books.Remove(book);
                    App.dbContext.SaveChanges();
                }));
            }
        }

        public RelayCommand EditCommand
        {
            get { return editCommand ??= new RelayCommand((selectedItem) =>
            {
                if (selectedItem == null)
                {
                    return;
                }
                Book book = selectedItem as Book;
                int newIndex = Books.IndexOf((Book)selectedItem);
                Book clone = (Book) book.Clone();
                CreateOrEditBook editBook=new CreateOrEditBook(clone);
                if (editBook.ShowDialog() == true)
                {
                    Books.Remove(book);
                    book = App.dbContext.Books.Find(editBook.Context.Book.IdBook);
                    if (book != null)
                    {
                        if (book.Image != editBook.Context.Image)
                        {
                            var path = System.AppDomain.CurrentDomain.BaseDirectory + book.Image;
                            File.Delete(path);
                        }
                        book.Name = editBook.Context.Name;
                        book.Image = editBook.Context.Image;
                        book.Recipes = editBook.Context.Recipes;
                        Books.Remove((Book)selectedItem);
                        Books.Add(book);
                        int oldIndex = Books.IndexOf(book);
                        Books.Move(oldIndex, newIndex);
                        App.dbContext.Update(book);
                        App.dbContext.SaveChanges();
                    }
                }
            });
            }
        }

    }
}
