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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10045844PROG6221POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }//Exit button code

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RecipeChooser recipeChooser = new RecipeChooser();
            this.Hide();
            
            recipeChooser.Show();
            //hides main window and shows the recipeChooser window.
        }//code that takes the user to recipe chooser page when pressing the button.
    }
}
