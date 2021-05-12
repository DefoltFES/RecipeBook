using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RecipeBook.databaseClasses;
using RecipeBook.service;
using RecipeBook.viewModels;

namespace RecipeBook.views
{
    /// <summary>
    /// Interaction logic for PrintBookPage.xaml
    /// </summary>
    public partial class PrintBookPage : Page
    {
        public FullBookViewModel Context { get; private set; }
        public PrintBookPage(FullBookViewModel book)
        {
             InitializeComponent();
             Context = book;
             DataContext = Context;
        }

        private void PrintBookPage_OnLoaded(object sender, RoutedEventArgs e)
        {

            foreach (var recipe in Context.Recipes)
            {
                PageContent contentRecipe = new PageContent();
                FixedPage fix = new FixedPage();
                fix.Width = 1100;
                fix.Height = 1517;
                StackPanel mainStack = new StackPanel()
                {
                    Margin = new Thickness(70, 19, 70, 19),
                    Width = 1000,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                TextBlock nameRecipe = new TextBlock()
                {
                    FontSize = 36,
                    Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                    FontFamily = (FontFamily) this.TryFindResource("Regular"),
                    Text = recipe.Recipe.Name
                };
                DockPanel imageAndCategory = new DockPanel()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(0, 33, 0, 0),
                    DataContext = recipe.Recipe
                };
                Image imageRecipe = new Image()
                {
                    Stretch = Stretch.Fill,
                    Width = 704,
                    Height = 420,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                imageRecipe.SetBinding(Image.SourceProperty, new Binding("Image")
                {
                    Converter = new PathToImageConvertor()
                });
                StackPanel recipeInfo = new StackPanel()
                {
                    Margin = new Thickness(10, 0, 0, 0)
                };
                TextBlock time = new TextBlock()
                {
                    FontSize = 20,
                    Margin = new Thickness(0, 20, 0, 20),
                    Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                    FontFamily = (FontFamily) this.TryFindResource("Regular")
                };
                time.SetBinding(TextBlock.TextProperty, new Binding("CookTime")
                {
                    StringFormat = $"Время:{0} мин"
                });
                TextBlock category = new TextBlock()
                {
                    FontSize = 20,
                    Margin = new Thickness(0, 20, 0, 5),
                    Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                    FontFamily = (FontFamily) this.TryFindResource("Regular"),
                    Text = "Категории"
                };
                recipeInfo.Children.Add(time);
                recipeInfo.Children.Add(category);
                foreach (var itemCategory in recipe.Recipe.ListCategories)
                {
                    TextBlock textCategory = new TextBlock()
                    {
                        FontSize = 20,
                        Margin = new Thickness(0, 5, 0, 5),
                        Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                        FontFamily = (FontFamily) this.TryFindResource("Regular"),
                        Text = itemCategory.Category.Name
                    };
                    recipeInfo.Children.Add(textCategory);
                }

                TextBlock numService = new TextBlock()
                {
                    FontSize = 20,
                    Margin = new Thickness(0, 10, 0, 0),
                    Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                    FontFamily = (FontFamily) this.TryFindResource("Regular"),
                };
                numService.SetBinding(TextBlock.TextProperty, new Binding("NumService")
                {
                    StringFormat = $"Количество порций: {0}"
                });
                recipeInfo.Children.Add(numService);
                TextBlock DescriptionTitle = new TextBlock()
                {
                    FontSize = 24,
                    Margin = new Thickness(0, 10, 0, 0),
                    Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                    FontFamily = (FontFamily) this.TryFindResource("SemiBold"),
                    Text = "Описание"

                };
                TextBlock Description = new TextBlock()
                {
                    FontSize = 24,
                    Margin = new Thickness(0, 10, 0, 0),
                    Foreground = (SolidColorBrush) this.TryFindResource("TextColor"),
                    FontFamily = (FontFamily) this.TryFindResource("SemiBold"),
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Text = recipe.Recipe.Description

                };
                DockPanel.SetDock(imageRecipe, Dock.Left);
                DockPanel.SetDock(recipeInfo, Dock.Right);
                imageAndCategory.Children.Add(imageRecipe);
                imageAndCategory.Children.Add(recipeInfo);
                mainStack.Children.Add(nameRecipe);
                mainStack.Children.Add(imageAndCategory);
                mainStack.Children.Add(DescriptionTitle);
                mainStack.Children.Add(Description);
                Border ingridientBorder = new Border()
                {
                    Margin = new Thickness(0, 20, 0, 20),
                    Padding = new Thickness(18, 15, 18, 40),
                    CornerRadius = new CornerRadius(5),
                    Background = (SolidColorBrush) this.TryFindResource("MainColor")
                };
                StackPanel ingridient = new StackPanel();
                ingridient.Children.Add(new TextBlock()
                {
                    FontSize = 36,
                    FontFamily = (FontFamily) this.TryFindResource("SemiBold"),
                    Foreground = (SolidColorBrush) this.TryFindResource("RecipeTextColor"),
                    Text = "Ингридиенты",
                    Margin = new Thickness(0, 0, 0, 22)
                });
                foreach (var item in recipe.Recipe.RecipeIngridients)
                {
                    TextBlock ingridients = new TextBlock()
                    {
                        FontSize = 36,
                        FontFamily = (FontFamily) this.TryFindResource("SemiBold"),
                        Foreground = (SolidColorBrush) this.TryFindResource("RecipeTextColor"),
                        Margin = new Thickness(0, 5, 0, 0),
                        Text = item.Product.Name + " - " + item.Amount + " " + item.MeasurementUnit.Name
                    };
                    ingridient.Children.Add(ingridients);
                }

                ingridientBorder.Child = ingridient;
                mainStack.Children.Add(ingridientBorder);
                fix.Children.Add(mainStack);
                ((IAddChild) contentRecipe).AddChild(fix);
                uiReport.Pages.Add(contentRecipe);

                if (recipe.Recipe.Instructions.Count == 0)
                {
                    return;
                }

                List<StackPanel> pages = new List<StackPanel>();
                if (recipe.Recipe.Instructions.Count % 4 != 0)
                {
                    pages.Add(new StackPanel()
                    {
                        Width = 1000,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Margin = new Thickness(70, 19, 70, 19)
                    });
                }

                for (int i = 0; i < recipe.Recipe.Instructions.Count; i += 4)
                {
                    pages.Add(new StackPanel()
                    {
                        Width = 1000,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Margin = new Thickness(70, 19, 70, 19)
                    });
                }

                var listInstructions = recipe.Recipe.Instructions.ToList();
                for (int i = 1, j = 0; i < listInstructions.Count + 1; i++)
                {

                    Border mainBorder = new Border()
                    {
                        Background = (SolidColorBrush) this.TryFindResource("MainColor"),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Padding = new Thickness(20),
                        Margin = new Thickness(10),
                        CornerRadius = new CornerRadius(5)
                    };

                    DockPanel panel = new DockPanel()
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        DataContext = listInstructions[i - 1]
                    };
                    Image imageStep = new Image()
                    {
                        MinHeight = 0,
                        MinWidth = 0,
                        MaxHeight = 300,
                        MaxWidth = 300,
                        Stretch = Stretch.Fill
                    };
                    TextBlock description = new TextBlock()
                    {
                        Margin = new Thickness(38, 0, 0, 0),
                        TextWrapping = TextWrapping.Wrap,
                        Foreground = (SolidColorBrush) this.TryFindResource("RecipeTextColor"),
                        FontFamily = (FontFamily) this.TryFindResource("SemiBold"),
                        FontSize = 26
                    };
                    description.SetBinding(TextBlock.TextProperty, "DescriptionStep");
                    imageStep.SetBinding(Image.SourceProperty, new Binding("ImageStep")
                    {
                        Converter = new PathToImageConvertor()
                    });
                    panel.Children.Add(imageStep);
                    panel.Children.Add(description);
                    DockPanel.SetDock(imageStep, Dock.Left);
                    DockPanel.SetDock(description, Dock.Right);
                    mainBorder.Child = panel;
                    pages[j].Children.Add(mainBorder);
                    if (i % 4 == 0 || i == listInstructions.Count)
                    {
                        PageContent pageContent = new PageContent();
                        FixedPage fixedPage = new FixedPage()
                        {
                            Width = 1100,
                            Height = 1517
                        };
                        fixedPage.Children.Add(pages[j]);
                        ((IAddChild) pageContent).AddChild(fixedPage);
                        uiReport.Pages.Add(pageContent);
                        j++;
                    }
                }

            }
        }


        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
