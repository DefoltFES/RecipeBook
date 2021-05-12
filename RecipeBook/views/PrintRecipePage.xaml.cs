using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for FullRecipePagePrint.xaml
    /// </summary>
    public partial class PrintRecipePage : Page
    {
        public FullRecipeViewModel Context { get; private set; }

        public PrintRecipePage(FullRecipeViewModel recipe)
        {
            InitializeComponent();
            Context = recipe;
            DataContext = Context;


        }

        private void FullRecipePagePrint_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Context.Instructions.Count == 0)
            {
                return;
            }
            List<StackPanel> pages = new List<StackPanel>();
            if (Context.Instructions.Count % 4 != 0)
            {
                pages.Add(new StackPanel()
                {
                    Width = 1000,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(70, 19, 70, 19)
                });
            }
            for (int i = 0; i < Context.Instructions.Count; i += 4)
            {
                pages.Add(new StackPanel()
                {
                    Width = 1000,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(70,19,70,19)
                });
            }

            var listInstructions = Context.Instructions.ToList();
            for (int i = 1, j = 0; i < listInstructions.Count+1; i++)
            { 
               
               Border mainBorder = new Border()
                {
                    Background = (SolidColorBrush)this.TryFindResource("MainColor"),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Padding = new Thickness(20),
                    Margin = new Thickness(10),
                    CornerRadius = new CornerRadius(5)
                };
              
               DockPanel panel = new DockPanel()
               {
                   HorizontalAlignment = HorizontalAlignment.Stretch,
                   DataContext = listInstructions[i-1]
               };
               Image imageStep = new Image()
               {
                   MinHeight = 0,
                   MinWidth = 0,
                   MaxHeight = 300,
                   MaxWidth = 300,
                   Stretch = Stretch.Fill
               };
                TextBlock description=new TextBlock(){
                    Margin = new Thickness(38,0,0,0),
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = (SolidColorBrush)this.TryFindResource("RecipeTextColor"),
                    FontFamily = (FontFamily)this.TryFindResource("SemiBold"),
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
                if (i % 4 == 0 || i==listInstructions.Count)
                {
                    PageContent pageContent = new PageContent();
                    FixedPage fix=new FixedPage()
                    {
                        Width = 1100,
                        Height = 1517
                    };
                    fix.Children.Add(pages[j]);
                    ((IAddChild) pageContent).AddChild(fix);
                    Document.Pages.Add(pageContent);
                    j++;
                }

               
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}

