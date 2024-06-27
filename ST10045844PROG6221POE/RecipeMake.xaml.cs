using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for RecipeMake.xaml
    /// </summary>
    public partial class RecipeMake : Window
    {
        public recIngred RecIngre { get; set; }
        //getter and setter
        private List<Steps> steps;
        //https://www.geeksforgeeks.org/c-sharp-list-class/
        //Kirti_Mangal
        public RecipeMake()
        {
            InitializeComponent();
            RecIngre = new recIngred();
            steps=new List<Steps>();
            DataContext = this;
        }
        private void UpdateListBoxSteps()
        {
            StepsListBox.ItemsSource = null;
            StepsListBox.ItemsSource=steps;
        }//Method to update the list box
         //https://wpf-tutorial.com/list-controls/listbox-control/#
         //WPF Tutorial


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveStep_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtQuantity.Text) && !string.IsNullOrWhiteSpace(txtMeasurementUnit.Text) && !string.IsNullOrWhiteSpace(txtFoodGroup.Text))
          //if statement for when name,quantity and measurement unit is not null
            {
                RecIngre.Name= txtName.Text;
                RecIngre.Quantity= Convert.ToDouble(txtQuantity.Text);
                RecIngre.Calories=Convert.ToDouble(txtCalorie.Text);
                RecIngre.MeasurementUnit=txtMeasurementUnit.Text;
                RecIngre.FoodGroup=txtFoodGroup.Text;
                RecIngre.Steps=steps.Select(step=>step.Descriptions).ToList();
                DialogResult = true;

            }//linking the textboxes to the variables to tranfer ther input to stored data.
            else
            {
                MessageBox.Show("Please enter all of the Details for the recipe");
            }//Error message to fill in the required details for name , qauntity and measurement unit.
             //https://www.geeksforgeeks.org/if-else-statement-in-programming/
             //pratyusujhr

        }

        private void DeleteSteps_Click(object sender, RoutedEventArgs e)
        {
            var but=(Button)sender;  
            var step=but.DataContext as Steps;
            if (step != null) { 
            steps.Remove(step);
                UpdateListBoxSteps();
            }//delete button to remove steps from the details of the ingredients.
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= false; 
        }

        private void AddAStep_Click(object sender, RoutedEventArgs e)
        {
            var description = Microsoft.VisualBasic.Interaction.InputBox("Please enter a step description","Add a step");
            //https://www.codeproject.com/Questions/5357607/Using-a-visual-basic-input-box-in-Csharp
            //RickZeeland
            if (!string.IsNullOrWhiteSpace(description) )
            {
                var step= new Steps { Descriptions = description };
         
                steps.Add(step);
             UpdateListBoxSteps();
            }//Uses method to display new step in listbox
        }//Add steps button here
    }
    public class StepDescriptionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureinfo)
        {
            string description = (value ?? "").ToString();
            if (string.IsNullOrEmpty(description))
                return new ValidationResult(false, "Step has to have a description");
            if (description.Length > 150)
                return new ValidationResult(false, "The description is too long ,please lower the character count to below 150 ");
            return ValidationResult.ValidResult;//sets rules that the descriptions of the steps cannot be longer thant 150 nor can be null.
        }//https://www.codeproject.com/Tips/5257153/How-to-Create-Custom-Validation-Attribute-in-Cshar
        //kr.is
    }
}
