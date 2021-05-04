﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using RecipeBook.databaseClasses;
using RecipeBook.service;
using SQLitePCL;

namespace RecipeBook.viewModels
{
    public class CreateOrEditRecipeViewModel:ViewModel
    {
        private Recipe recipe;
        public Recipe Recipe { get=>recipe; }
        private ObservableCollection<ListCategory> categories;
        private ObservableCollection<RecipeIngridient> ingridients;
        private ObservableCollection<Instruction> instructions;

        private RelayCommand addCategories;
        private RelayCommand addIngridient;
        private RelayCommand addInstruction;
        private RelayCommand addImage;
        private RelayCommand saveChanges;
        public CreateOrEditRecipeViewModel(Recipe r)
        {
            recipe = r;
            Categories = new ObservableCollection<ListCategory>(r.ListCategories);
            Ingridients=new ObservableCollection<RecipeIngridient>(r.RecipeIngridients);
            Instructions=new ObservableCollection<Instruction>(r.Instructions);
        }
        public string Name
        {
            get { return recipe.Name; }
            set
            {
                recipe.Name = value;
                OnPropertyChanged();
            }
        }

        public long Id
        {
            get { return recipe.IdRecipe; }
        }
        public int? CookTime
        {
            get { return recipe.CookTime; }
            set
            {

                recipe.CookTime = value;
                OnPropertyChanged();
            }
        }

        public int? NumService
        {
            get { return recipe.NumService; }
            set
            {
                recipe.NumService = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ListCategory> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return recipe.Image; }
            set
            {
                recipe.Image = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return recipe.Description; }
            set
            {
                recipe.Description = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RecipeIngridient> Ingridients
        {
            get { return ingridients; }
            set
            {
                ingridients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Instruction> Instructions
        {
            get { return instructions; }
            set
            {
                instructions = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCategories
        {
            get
            {
                return addCategories ??
                       (addCategories = new RelayCommand((o) =>
                       {
                          AddCategoriesInRecipe listCategoriesInRecipe= new AddCategoriesInRecipe();
                          
                          if (listCategoriesInRecipe.ShowDialog()==true)
                          {
                              this.Categories.Clear();
                               foreach (var category in listCategoriesInRecipe.context.AddCategories)
                              {
                                  this.Categories.Add(new ListCategory()
                                  {
                                      Category = category
                                  });
                              }
                          }
                          

                       }));
            }
        }

        public RelayCommand AddIngridient
        {
            get
            {
                return addIngridient ??
                       (addIngridient = new RelayCommand((o) => { Ingridients.Add(new RecipeIngridient()
                       {
                           Product = new Product(),
                           MeasurementUnit = new MeasurementUnit()
                       }); }));
            }
        }

        public RelayCommand AddInstruction
        {
            get
            {
                return addInstruction ??
                       (addInstruction = new RelayCommand((o) => { Instructions.Add(new Instruction()); }));
            }
        }

        public RelayCommand AddImage
        {
            get
            {
                return addImage ??
                       (addImage = new RelayCommand((selectedItem) =>
                       {
                           if (selectedItem == null)
                           {
                               return;
                           }
                           var image = selectedItem as Image;
                           OpenFileDialog openFileDialog = new OpenFileDialog()
                           {
                               Filter = "Image File (*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png",
                               CheckPathExists = true,
                               Multiselect = false
                           };

                           if (openFileDialog.ShowDialog() == true)
                           {
                               image.Source = new BitmapImage(new Uri(openFileDialog.FileName,UriKind.Absolute));
                           }

                       }));
            }
        }

        public RelayCommand SaveChanges
        {
            get
            {
                return saveChanges ??
                       (saveChanges = new RelayCommand((o) =>
                       {
                           if (Ingridients == null)
                           {
                               MessageBox.Show("Нужен хотя бы один ингридиент");
                               return;
                           }

                           if (this.Image != null )
                           {
                               if (Path.IsPathFullyQualified(this.Image))
                               {
                                   this.Image = CopyAndSaveImages(new Uri(this.Image).LocalPath);
                               }
                           }

                           foreach (var image in Instructions)
                           {
                               if (image.ImageStep != null )
                               {
                                   if (Path.IsPathFullyQualified(image.ImageStep))
                                   {
                                       image.ImageStep = CopyAndSaveImages(new Uri(image.ImageStep).LocalPath);
                                   }
                               }
                           }
                           this.recipe.Instructions = Instructions.ToList();
                           this.recipe.ListCategories = Categories.ToList();
                           this.recipe.RecipeIngridients = Ingridients.ToList();
                           
                       }));
            }
        }

        private string CopyAndSaveImages(string path)
        {
            if (path != null)
            {
                
                var fullDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + @"images\recipes" ;
                if (!Directory.Exists(fullDirectoryPath))
                {
                    Directory.CreateDirectory(fullDirectoryPath);
                }
                
                var nameImage = Path.GetFileName(path);
                var newImageSource = Path.Combine(fullDirectoryPath, nameImage);
                if (Path.IsPathFullyQualified(path))
                {
                    File.Copy(path, newImageSource, true);
                    return Path.Combine(@"images\recipes\", nameImage);
                }
                else
                {
                    return path;
                }

            }
            else
            {
                return null;
            }

        }


    }
}
