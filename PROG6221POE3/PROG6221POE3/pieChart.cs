using POEprog6221;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace RecipeApp
{
    public class PieChart
    {
        public void DisplayFoodGroupPercentage(List<Recipe> recipes)
        {
            var chart = new Chart();
            var pieSeries = new PieSeries
            {
                Title = "Food Group Percentage",
                ItemsSource = GetGroupedIngredients(recipes),
                DependentValuePath = "Value",
                IndependentValuePath = "Key"
            };

            chart.Series.Add(pieSeries);

            var window = new Window
            {
                Content = chart,
                Title = "Food Group Percentage"
            };

            window.ShowDialog();
        }

        private IEnumerable<KeyValuePair<string, int>> GetGroupedIngredients(List<Recipe> recipes)
        {
            return recipes
                .SelectMany(recipe => recipe.Ingredients)
                .GroupBy(ingredient => ingredient.FoodGroup)
                .Select(group => new KeyValuePair<string, int>(group.Key, group.Count()));
        }
    }
}
