using POEprog6221;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RecipeApp
{
    public partial class AddRecipeWindow : Window
    {
        private ObservableCollection<Recipe.Ingredient> ingredients;
        private ObservableCollection<string> steps;
        private Recipe newRecipe;

        public Recipe NewRecipe
        {
            get => newRecipe;
            private set
            {
                newRecipe = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Recipe.Ingredient> Ingredients
        {
            get => ingredients;
            set
            {
                ingredients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Steps
        {
            get => steps;
            set
            {
                steps = value;
                OnPropertyChanged();
            }
        }

        public AddRecipeWindow()
        {
            InitializeComponent();
            DataContext = this;

            Ingredients = new ObservableCollection<Recipe.Ingredient>();
            Steps = new ObservableCollection<string>();
        }

        

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string name = ingredientNameTextBox.Text;
            string quantityText = ingredientQuantityTextBox.Text;
            string unit = ingredientUnitTextBox.Text;
            string caloriesText = ingredientCaloriesTextBox.Text;
            string foodGroup = ingredientFoodGroupTextBox.Text;

            if (double.TryParse(quantityText, out double quantity) && int.TryParse(caloriesText, out int calories))
            {
                Recipe.Ingredient ingredient = new Recipe.Ingredient(name, quantity, unit, calories, foodGroup);
                Ingredients.Add(ingredient);

                ingredientNameTextBox.Clear();
                ingredientQuantityTextBox.Clear();
                ingredientUnitTextBox.Clear();
                ingredientCaloriesTextBox.Clear();
                ingredientFoodGroupTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Invalid quantity or calories value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            string step = stepTextBox.Text;
            Steps.Add(step);

            stepTextBox.Clear();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = recipeNameTextBox.Text;
            Recipe recipe = new Recipe(recipeName, Ingredients.ToList(), Steps.ToArray());
            NewRecipe = recipe;
            Close();
        }
       


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(scaleFactorTextBox.Text, out double factor))
            {
                NewRecipe.ScaleRecipe(factor);
                MessageBox.Show("Recipe scaled successfully.", "Scale Recipe", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid scale factor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            NewRecipe.ResetQuantities();
            MessageBox.Show("Quantities reset successfully.", "Reset Quantities", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            Ingredients.Clear();
            Steps.Clear();
            recipeNameTextBox.Clear();
            ingredientNameTextBox.Clear();
            ingredientQuantityTextBox.Clear();
            ingredientUnitTextBox.Clear();
            ingredientCaloriesTextBox.Clear();
            ingredientFoodGroupTextBox.Clear();
            stepTextBox.Clear();
            MessageBox.Show("Data cleared successfully.", "Clear Data", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
