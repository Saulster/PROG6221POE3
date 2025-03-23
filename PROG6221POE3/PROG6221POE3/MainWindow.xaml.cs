using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using POEprog6221;
using RecipeApp;
namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        //ObservableCollection<string> listBoxItems;
        private RecipeManager recipeManager;
        private Recipe selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                selectedRecipe = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> comboBoxItems;

        public MainWindow()
        {
            comboBoxItems = new ObservableCollection<string>();
            InitializeComponent();
            recipeManager = new RecipeManager();
            DataContext = recipeManager;
            recipeManager.recipeExceedsCalories += RecipeExceedsCaloriesNotification;
            recipeComboBox.ItemsSource = comboBoxItems;

            RefreshRecipeComboBox(); // Populate the combo box initially
        }





        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the AddRecipeWindow
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();

            // Display the window as a dialog and wait for the user to close it
            bool? result = addRecipeWindow.ShowDialog();

            // If the user clicked the "Add" button and the recipe was successfully added
            if (result == true && addRecipeWindow.NewRecipe != null)
            {
                // Add the new recipe to the recipe manager
                recipeManager.AddRecipe(addRecipeWindow.NewRecipe);

                RefreshRecipeComboBox(); // Update the combo box with the new recipe

                recipeDetailsPanel.Visibility = Visibility.Visible;
            }
        }







        private void DisplayRecipes_Click(object sender, RoutedEventArgs e)
        {
            // If a recipe is selected, show its details
            if (recipeComboBox.SelectedItem is string selectedRecipeName)
            {
                Recipe selectedRecipe = recipeManager.Recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);
                SelectedRecipe = selectedRecipe;
                recipeDetailsPanel.Visibility = Visibility.Visible;
                List<Recipe> selectedRecipes = new List<Recipe> { selectedRecipe };

                // Create an instance of the PieChart class
                PieChart pieChart = new PieChart();
                pieChart.DisplayFoodGroupPercentage(selectedRecipes);
            }
            else
            {
                MessageBox.Show("Please select a recipe.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void RefreshRecipeComboBox()
        {
            comboBoxItems.Clear();

            foreach (Recipe recipe in recipeManager.Recipes)
            {
                comboBoxItems.Add(recipe.Name);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshRecipeComboBox();
        }





        private void RecipeExceedsCaloriesNotification(string recipeName)
        {
            MessageBox.Show($"The recipe '{recipeName}' exceeds 300 calories!", "Calorie Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
