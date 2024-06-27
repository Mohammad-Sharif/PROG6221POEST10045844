using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ST10045844PROG6221POE
{
    /// <summary>
    /// Interaction logic for RecipeChooser.xaml
    /// </summary>
    /// 
    public partial class RecipeChooser : Window
    {
        private RecHolder recManager;
        public RecipeChooser()
        {
            InitializeComponent();
            recManager = new RecHolder();
            recManager.CurrentRec = new Recipes();
        }
      

       
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddRecDetails_Click(object sender, RoutedEventArgs e)
        {
            var RecipeMakeWindow = new RecipeMake();
            RecipeMakeWindow.Owner = this;  
            if(RecipeMakeWindow.ShowDialog() == true ) { 
            
            }//button to added recipe diverts user to new page to add recipe details.
        }//https://wpf-tutorial.com/wpf-application/working-with-app-xaml/
        //WPF Tutorial

        private void ClearData()
        {
         txtNameRec.Clear();
            recManager.CurrentRec.Ingredients.Clear();
            lstIngredients.ItemsSource = null;
        }//clear data method

    
   

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbFdGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSaveRec_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNameRec.Text))
            {
                MessageBox.Show("Enter a recipe name.");
                return;
            }
            var sumCalorie = recManager.SumCaloriesCalc();
            if (sumCalorie > 300) {
                MessageBox.Show("The sum of calories exceed 300 in the recipe.");
            }
            //if statement for the caloories of the recipe exceeding the 300 calories mark throwing a message to the user.
            //https://www.geeksforgeeks.org/if-else-statement-in-programming/
            //pratyusujhr
            var rec = new Recipes()
            { 
                RecName=txtNameRec.Text,
                Ingredients=new List<recIngred>(recManager.CurrentRec.Ingredients)
            };
            recManager.AddRec(rec);
            listRec.ItemsSource = null;
            listRec.ItemsSource = recManager.GetRecList();
            ClearData();//clears data in the ingrredients list and displays the saved recipe in the recipe list when saving the recipe.
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
           ClearData();//calls the clear data method here
            listRec.ItemsSource= null;
            listRec.ItemsSource = recManager.GetRecList();
        }    //https://wpf-tutorial.com/list-controls/listbox-control/#
             //WPF Tutorial
             //Clear button
        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
            if (recManager.CurrentRec == null)
            {
                MessageBox.Show("Please select a recipe to scale");
                return;
            }
        if (recManager.CurrentRec.Ingredients == null)
            {

                MessageBox.Show("Please enter ingridients for recipe to scale");
                return;
            }
        ComboBoxItem selectItem=cmbScale.SelectedItem as ComboBoxItem;
            if (selectItem != null)
            {
                double scale = double.Parse(selectItem.Content.ToString());

                foreach (var ingredients in recManager.CurrentRec.Ingredients)
                {
                    ingredients.Quantity *= scale;

                }
                MessageBox.Show("Recipe is scaled");
            }//https://wpf-tutorial.com/list-controls/combobox-control/
            //WPF Tutorial
        }//scaling the recipe code by combo box option is selected and user pressed the scale button

        private void btnResetRec_Click(object sender, RoutedEventArgs e)
        {
            recManager.Recipe.Clear();
            listRec.ItemsSource = null;
            listRec.ItemsSource=recManager.GetRecList();
        } //reset the recipes button code

        private void btnQuantityReset_Click(object sender, RoutedEventArgs e)
        {
            if (recManager.CurrentRec == null) {
                MessageBox.Show("Recipe is not chosen.");
                return;
            }
            if (recManager.CurrentRec.Ingredients == null)
            {
                MessageBox.Show("Recipe Ingredient are null.");
                return;
            }
            foreach(var recingrdient in recManager.CurrentRec.Ingredients) {
                var firstRecipe = recManager.GetPreviousrec()?.Ingredients.FirstOrDefault(q => q.Name == recingrdient.Name);
                if (firstRecipe != null)
                {
                    recingrdient.Quantity = firstRecipe.Quantity;
                }
            }
            MessageBox.Show("The recipe is reseted.");
        }//resetting the quantity values button code

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            if (listRec.SelectedItem != null)
            {
                var rec = (Recipes)listRec.SelectedItem;
                recManager.CurrentRec = rec;
                var sumCal = recManager.SumCaloriesCalc();
                var recDetails = $"Recipe name={rec.RecName}\n" +
                    $"Sumof Calories={sumCal}\n" +
                    $"Ingredients:\n";
                if (sumCal > 300)
                {
                    MessageBox.Show("The recipe is has more than 300 calories.");
                }
                foreach (var ingredient in rec.Ingredients)
                {//foreach statement for the indgredients in the recipe
                    //https://www.w3schools.com/cs/cs_foreach_loop.php
                    //W3Schools 
                    recDetails += $"Name of ingredient={ingredient.Name}\n" +
                        $"Quantity is={ingredient.Quantity}\n" +
                        $"Measurement Unit={ingredient.MeasurementUnit}\n" +
                        $"Food group of ingridient is={ingredient.FoodGroup}\n" +
                        $"Calories of this ingrident is={ingredient.Calories}\n"+
                          $"Steps are=";
               

                    foreach (var recstep in ingredient.Steps)
                    {
                        recDetails += $"{recstep}\n";
                    }//string interpolation
                    recDetails += "\n";    //https://www.w3schools.com/cs/cs_foreach_loop.php
                                           //W3Schools 
                }
                MessageBox.Show(recDetails, "Recipe ingredient Details ",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
        //code for button display of recipe details
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var name = txtSearch.Text.ToLower();
            var FdGroup = cmbFdGroup.SelectedItem as string;
            //https://wpf-tutorial.com/list-controls/combobox-control/
            //WPF Tutorial
            double maximumCalorie;
            double.TryParse(txtMaximumCalorie.Text, out maximumCalorie);

            var filterRec = recManager.GetRecList().Where(rec => rec.RecName.ToLower().Contains(name) || rec.Ingredients.Any(q => q.Name.ToLower().Contains(name))).ToList();
            if (!string.IsNullOrWhiteSpace(FdGroup))
            {
                filterRec = filterRec.Where(rec => rec.Ingredients.Any(q => q.FoodGroup == FdGroup)).ToList();
            }
            if (maximumCalorie > 0)
            {
                filterRec = filterRec.Where(rec => recManager.SumCaloriesCalc(rec) <= maximumCalorie).ToList();
            }
            listRec.ItemsSource = filterRec;
        } //https://www.geeksforgeeks.org/if-else-statement-in-programming/
          //pratyusujhr
          //filter button code for the ingredients and food group
        private void btnRecDetail_Click(object sender, RoutedEventArgs e)
        {
            var recdetailsWindow = new RecipeMake();
            recdetailsWindow.Owner= this;
            if(recdetailsWindow.ShowDialog() == true) {
                var ingredient = recdetailsWindow.RecIngre;
                if (recManager.CurrentRec == null)
                    recManager.CurrentRec = new Recipes();
                if (recManager.CurrentRec.Ingredients == null)
                    recManager.CurrentRec.Ingredients = new List<recIngred>();
                recManager.CurrentRec.Ingredients.Add(ingredient);
                lstIngredients.ItemsSource = null;
                lstIngredients.ItemsSource = recManager.CurrentRec.Ingredients;
          
            }
        }

        private void listRec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(txtMaximumCalorie.Text, out var maxCalorie))
            {
                var filterRec = recManager.GetRecList().Where(r => recManager.SumCaloriesCalc() <= maxCalorie).ToList();
            }//filetering the recipes by the sum of the calories
            else
            {
                MessageBox.Show("Please enter a number for the max calories filter.");
            } //https://www.geeksforgeeks.org/if-else-statement-in-programming/
            //pratyusujhr
        }

        private void cmbScale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    public class Recipes : INotifyPropertyChanged
    {
        private string recName;
        private List<recIngred> ingredients;
        private List<recIngred> previousIngredients;
        //creating the  lists in the recipe
        //https://www.geeksforgeeks.org/c-sharp-list-class/
        //Kirti_Mangal
        public string RecName
        {
            get { return recName; }
            set { recName = value; OnPropertyChange("RecName"); }
        } //https://www.w3schools.com/cs/cs_properties.php
          //W3Schools 
        public List<recIngred> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChange("Ingredients"); }
        } //https://www.w3schools.com/cs/cs_properties.php
          //W3Schools 
        public List<recIngred> PreviousIngredients
        {
            get { return previousIngredients; }
            set { previousIngredients = value; OnPropertyChange("PreviousIngredients"); }
        } //https://www.w3schools.com/cs/cs_properties.php
          //W3Schools 
          //getter and setters
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    public class recIngred
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        //quantity and calories are double because they can be half scaled and thus need to accomadate decimals
        public string MeasurementUnit { get; set; }
        public List<string> Steps { get; set; }
        //steps is a list because multiple steps can be present
        //https://www.geeksforgeeks.org/c-sharp-list-class/
        //Kirti_Mangal
        public double Calories { get; set; }
        public string FoodGroup { get; set; }

    }//listing the variables in the ingredients class  with getter and setters
     //https://www.w3schools.com/cs/cs_properties.php
     //W3Schools 
    public class Steps
    {
        public string Descriptions { get; set; }
    }// steps class contains the variable description
     // https://www.w3schools.com/cs/cs_classes.php
     //W3Schools 
    public class RecHolder
    {
        public ObservableCollection<Recipes> Recipe { get; set; }
        //https://www.demo2s.com/csharp/csharp-observablecollection-tutorial-with-examples.html
        //yinpeng263@hotmail.com
        public Recipes CurrentRec { get; set; }

        public RecHolder()
        {
            Recipe = new ObservableCollection<Recipes>();
            CurrentRec = new Recipes();
        }//https://www.demo2s.com/csharp/csharp-observablecollection-tutorial-with-examples.html
        //yinpeng263@hotmail.com
        public void AddRec(Recipes recipe)
        {
            Recipe.Add(recipe);
        }//method to add a recipe into the Recipe class
        public double SumCaloriesCalc(Recipes recipe = null)
        {
            double sumCalories = 0;
            if (recipe == null)
                recipe = CurrentRec;
            foreach (var ingredient in recipe.Ingredients)
            {
                sumCalories += ingredient.Calories;
                //calculation of the total amount of the calories
            }
            return sumCalories;
        }//sum of the calories in the list of the recipe for all the ingredients.
        public ObservableCollection<Recipes> GetRecList()
        {
            var recipesList = Recipe.OrderBy(rec => rec.RecName).ToList();
            return new ObservableCollection<Recipes>(recipesList);
        }//getting the recipe rest
        public Recipes GetPreviousrec()
        {
            return Recipe.FirstOrDefault(rec => rec.RecName == CurrentRec.RecName);
        }//Getting the previous method

    }    // https://www.w3schools.com/cs/cs_classes.php
         //W3Schools 
}
