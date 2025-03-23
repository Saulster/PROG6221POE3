using POEprog6221;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp
{
    public class RecipeManager
    {
        public delegate void RecipeCaloriesExceededDelegate(string recipeName);
        public event RecipeCaloriesExceededDelegate recipeExceedsCalories;
        public List<Recipe> Recipes { get { return recipes; } }

        private List<Recipe> recipes;

        public RecipeManager()
        {
            recipes = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            recipe.recipeExceedsCalories += RecipeExceedsCaloriesNotification;
        }

        public List<string> DisplayRecipes()
        {
            List<string> recipeNames = new List<string>();
            foreach (Recipe recipe in recipes)
            {
                recipeNames.Add(recipe.Name);
            }
            return recipeNames;
        }

        public Recipe ChooseRecipe(int recipeIndex)
        {
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                return recipes[recipeIndex];
            }

            return null;
        }

        public Recipe ChooseRecipe(string recipeName)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipe.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase))
                {
                    return recipe;
                }
            }

            return null;
        }

        public void SortRecipes()
        {
            recipes.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
        }

        public void ClearRecipes()
        {
            recipes.Clear();
        }

        private void RecipeExceedsCaloriesNotification(string recipeName)
        {
            recipeExceedsCalories?.Invoke(recipeName);
        }
    }
}

