using System;

using System.Collections.Generic;



namespace POE_PART_TWO


{


    namespace Recipe

    {


        // Delegate for notifying when a recipe exceeds 300 calories 

        public delegate void RecipeCaloriesExceededHandler(string recipeName, double totalCalories);



        // Class to manage recipes 

        class RecipeManager

        {

            // Generic collection to store recipes 

            private List<Recipe> recipes;



            // Event to handle recipe calories exceeding 300 

            public event RecipeCaloriesExceededHandler RecipeCaloriesExceeded;



            public RecipeManager()

            {

                recipes = new List<Recipe>();

            }


            // Method to add a recipe 

            public void AddRecipe()

            {
                Console.ForegroundColor = ConsoleColor.Green;
                Recipe recipe = new Recipe();

                recipe.EnterRecipeDetails();

                recipes.Add(recipe);

                // Subscribe to the recipe's CaloriesExceeded event 

                recipe.CaloriesExceeded += OnRecipeCaloriesExceeded;

            }



            // Method to display all recipes 

            public void DisplayAllRecipes()

            {
                Console.ForegroundColor = ConsoleColor.Blue;
                recipes.Sort(); // Sort recipes alphabetically by name 

                Console.WriteLine("Recipes:");

                foreach (Recipe recipe in recipes)

                {

                    Console.WriteLine(recipe.Name);

                }

                { Console.WriteLine("************************************"); }

            }



            // Method to display a specific recipe 

            public void DisplayRecipe(string recipeName)

            {

                Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)

                {

                    recipe.DisplayRecipe();

                }

                else

                {

                    Console.WriteLine($"Recipe '{recipeName}' not found.");

                }

                { Console.WriteLine("************************************"); }

            }



            // Event handler for when recipe calories exceed 300 
            
            private void OnRecipeCaloriesExceeded(string recipeName, double totalCalories)

            {
               
                Console.WriteLine($"Warning: Recipe '{recipeName}' exceeds 300 calories ({totalCalories} calories).");

            }



            // Method to calculate and display the total calories of a recipe 

            public void DisplayTotalCalories(string recipeName)

            {

                Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)

                {

                    double totalCalories = recipe.CalculateTotalCalories();

                    Console.WriteLine($"Total calories of recipe '{recipeName}': {totalCalories} calories.");

                    // Check if total calories exceed 300 and invoke event 

                    if (totalCalories > 300)

                    {

                        RecipeCaloriesExceeded?.Invoke(recipeName, totalCalories);

                    }

                }

                else

                {

                    Console.WriteLine($"Recipe '{recipeName}' not found.");

                }

                { Console.WriteLine("************************************"); }

            }



            // Method to clear all recipes 

            public void ClearAllRecipes()

            {

                recipes.Clear();

            }



            // Method to scale a recipe by a factor 

            public void ScaleRecipe(string recipeName, double factor)

            {

                Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)

                {

                    recipe.Scale(factor);

                    Console.WriteLine($"Recipe '{recipeName}' scaled by a factor of {factor}.");

                }

                else

                {

                    Console.WriteLine($"Recipe '{recipeName}' not found.");

                }

                { Console.WriteLine("************************************"); }

            }



            // Method to reset the quantities of a recipe to their original values 

            public void ResetRecipeQuantities(string recipeName)

            {

                Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)

                {

                    recipe.ResetQuantities();

                    Console.WriteLine($"Quantities of recipe '{recipeName}' reset to original values.");

                }

                else

                {

                    Console.WriteLine($"Recipe '{recipeName}' not found.");

                }

                { Console.WriteLine("************************************"); }

            }

        }



        // Class representing a recipe 

        class Recipe : IComparable<Recipe>

        {

            // Properties 

            public string Name { get; set; }

            public List<Ingredient> Ingredients { get; set; }

            public List<string> Steps { get; set; }

            public Action<string, double> CaloriesExceeded { get; internal set; }



            // Constructor 

            public Recipe()

            {

                Ingredients = new List<Ingredient>();

                Steps = new List<string>();

            }



            // Method to enter recipe details 

            public void EnterRecipeDetails()

            {

                Console.Write("Enter recipe name: ");

                Name = Console.ReadLine();



                Console.Write("Enter the number of ingredients: ");

                int ingredientCount = int.Parse(Console.ReadLine());



                for (int i = 0; i < ingredientCount; i++)

                {

                    Ingredient ingredient = new Ingredient();

                    ingredient.EnterIngredientDetails();

                    Ingredients.Add(ingredient);

                }



                Console.Write("Enter the number of steps: ");

                int stepCount = int.Parse(Console.ReadLine());



                for (int i = 0; i < stepCount; i++)

                {

                    Console.Write($"Enter step {i + 1}: ");

                    Steps.Add(Console.ReadLine());

                }

                { Console.WriteLine("************************************"); }

            }



            // Method to display recipe 

            public void DisplayRecipe()

            {

                Console.WriteLine($"Recipe: {Name}");

                Console.WriteLine("Ingredients:");

                foreach (Ingredient ingredient in Ingredients)

                {

                    Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}, Calories: {ingredient.Calories}, Food Group: {ingredient.FoodGroup}");

                }

                Console.WriteLine("Steps:");

                for (int i = 0; i < Steps.Count; i++)

                {

                    Console.WriteLine($"{i + 1}. {Steps[i]}");

                }

                { Console.WriteLine("************************************"); }

            }



            // Method to calculate total calories of all ingredients 

            public double CalculateTotalCalories()

            {

                double totalCalories = 0;

                foreach (Ingredient ingredient in Ingredients)

                {

                    totalCalories += ingredient.Calories;

                }

                return totalCalories;

            }



            // Implement IComparable interface to enable sorting by recipe name 

            public int CompareTo(Recipe other)

            {

                return string.Compare(this.Name, other.Name, StringComparison.Ordinal);

            }



            // Method to scale the recipe by a factor 

            public void Scale(double factor)

            {

                foreach (Ingredient ingredient in Ingredients)

                {

                    ingredient.Quantity *= factor;

                }

                { Console.WriteLine("************************************"); }

            }



            // Method to reset the quantities of all ingredients to their original values 

            public void ResetQuantities()

            {

                // Assuming original values are stored somewhere (not shown in the current code) 

                // For demonstration purposes, resetting quantities to their initial values (assuming 1) 

                foreach (Ingredient ingredient in Ingredients)

                {

                    ingredient.Quantity = 1;

                }

                { Console.WriteLine("************************************"); }

            }

        }



        // Class representing an ingredient 

        class Ingredient

        {

            // Properties 

            public string Name { get; set; }

            public double Quantity { get; set; }

            public string Unit { get; set; }

            public double Calories { get; set; }

            public string FoodGroup { get; set; }



            // Method to enter ingredient details 

            public void EnterIngredientDetails()

            {

                Console.Write("Enter ingredient name: ");

                Name = Console.ReadLine();



                Console.Write($"Enter quantity for {Name}: ");

                Quantity = double.Parse(Console.ReadLine());



                Console.Write($"Enter unit for {Name}: ");

                Unit = Console.ReadLine();



                Console.Write($"Enter calories for {Name}: ");

                Calories = double.Parse(Console.ReadLine());



                Console.Write($"Enter food group for {Name}: ");

                FoodGroup = Console.ReadLine();

            }



        }





        class Program

        {

            static void Main(string[] args)

            {

                RecipeManager recipeManager = new RecipeManager();

                bool continueProgram = true;



                // Subscribe to the RecipeCaloriesExceeded event 

                recipeManager.RecipeCaloriesExceeded += OnRecipeCaloriesExceeded;



                while (continueProgram)

                {
                    { Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("============Let's Start Cooking!============");
                    Console.WriteLine("============================================"); }
 
                        Console.WriteLine("1. Add a Recipe");

                    Console.WriteLine("2. Display All Recipes");

                    Console.WriteLine("3. Display Recipe Details");

                    Console.WriteLine("4. Display Total Calories of a Recipe");

                    Console.WriteLine("5. Scale Recipe");

                    Console.WriteLine("6. Reset Recipe Quantities");

                    Console.WriteLine("7. Clear All Recipes");

                    Console.WriteLine("8. Exit");

                    Console.Write("Select an option: ");

                    string option = Console.ReadLine();

                    Console.WriteLine("============================================");

                    switch (option)

                    {

                        case "1":

                            recipeManager.AddRecipe();

                            break;
                            Console.WriteLine("============================================");

                        case "2":

                            recipeManager.DisplayAllRecipes();

                            break;

                        case "3":

                            Console.Write("Enter recipe name: ");

                            string recipeName = Console.ReadLine();

                            recipeManager.DisplayRecipe(recipeName);

                            break;

                        case "4":

                            Console.Write("Enter recipe name: ");

                            string recipeNameForCalories = Console.ReadLine();

                            recipeManager.DisplayTotalCalories(recipeNameForCalories);

                            break;

                        case "5":

                            Console.Write("Enter recipe name: ");

                            string recipeNameToScale = Console.ReadLine();

                            Console.Write("Enter scale factor (0,5 , 2, or 3): ");

                            double scaleFactor = double.Parse(Console.ReadLine());

                            recipeManager.ScaleRecipe(recipeNameToScale, scaleFactor);

                            break;

                        case "6":

                            Console.Write("Enter recipe name: ");

                            string recipeNameToReset = Console.ReadLine();

                            recipeManager.ResetRecipeQuantities(recipeNameToReset);

                            break;

                        case "7":

                            recipeManager.ClearAllRecipes();

                            Console.WriteLine("All recipes cleared.");

                            break;

                        case "8":

                            continueProgram = false;

                            break;

                        default:

                            Console.WriteLine("Incorrect option. Please try again.");

                            break;

                    }

                    Console.WriteLine();

                }

                { Console.WriteLine(); }

            }

            // Event handler for when recipe calories exceed 300 

            static void OnRecipeCaloriesExceeded(string recipeName, double totalCalories)

            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning: Recipe '{recipeName}' exceeds 300 calories ({totalCalories} calories).");

            }

        }

    }
}
