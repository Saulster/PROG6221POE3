using System;
using System.Collections.Generic;
using System.Text;

namespace POEprog6221
{
    public class Recipe
    {
        public delegate void RecipeCaloriesExceededDelegate(string recipeName);
        public event RecipeCaloriesExceededDelegate recipeExceedsCalories;
        

        public class Ingredient
        {
            //getters and setters for main variables
            public string Name { get; set; }
            public double Quantity { get; set; }
            public string Unit { get; set; }
            public int Calories { get; set; }
            public string FoodGroup { get; set; }
            //constructor for main variables
            public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
            {
                Name = name;
                Quantity = quantity;
                Unit = unit;
                Calories = calories;
                FoodGroup = foodGroup;
            }
        }

        public string Name { get; set; }
        public int NumIngredients { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int NumSteps { get; set; }
        public string[] Steps { get; set; }

        public Recipe(string name, List<Ingredient> ingredients, string[] steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
            NumIngredients = Ingredients.Count;
            NumSteps = Steps.Length;
        }

        //ingredient class method of display recipe
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Recipe: {Name}");
            stringBuilder.AppendLine("Ingredients:");

            foreach (Ingredient ingredient in Ingredients)
            {
                stringBuilder.AppendLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.FoodGroup})");
            }

            stringBuilder.AppendLine("\nSteps:");
            for (int i = 0; i < NumSteps; i++)
            {
                stringBuilder.AppendLine($"{i + 1}. {Steps[i]}");
            }

            stringBuilder.AppendLine($"Total Calories: {CalculateTotalCalories()}");

            return stringBuilder.ToString();
        }
        public void DisplayRecipe()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine("Ingredients:");

            foreach (Ingredient ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.FoodGroup})");
            }

            Console.WriteLine("\nSteps:");

            for (int i = 0; i < NumSteps; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
        //ingredient class method of scale recipe
        public void ScaleRecipe(double factor)
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        //ingredient class method of reset quantities for recipe
        public void ResetQuantities()
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity /= 2;
            }
        }
        //ingredient class method of CalculateTotalCalories for recipe
        public int CalculateTotalCalories()
        {
            int totalCalories = 0;
            foreach (Ingredient ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }
    
}