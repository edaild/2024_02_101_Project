public class RecipeList
{
    public static CraftionRecipe[] KitchenRecipes = new CraftionRecipe[]
    {
        new CraftionRecipe
        {
             itemName = "¾ßÃ¼ ½ºÆ©",
             resultItem = ItemType.VegetableStewk,
             resultAmount = 1,
             HungerRestoreAmount = 40f,
             requiredxItems = new ItemType[] {ItemType.Plant , ItemType.Bush},
             requiredAmounts = new int[] {2 ,1}
        },

         new CraftionRecipe
        {
             itemName = "°úÀÏ ¼¿·¯µå",
             resultItem = ItemType.FruitSalad,
             resultAmount = 1,
             HungerRestoreAmount = 60f,
             requiredxItems = new ItemType[] {ItemType.Plant , ItemType.Bush},
             requiredAmounts = new int[] {3 ,3}
        },

    };


    public static CraftionRecipe[] workbenchRecipes = new CraftionRecipe[]
    {
        new CraftionRecipe
        {
           itemName = "°úÀÏ ¼¿·¯µå",
             resultItem = ItemType.RepairKit,
             resultAmount = 1,
             HungerRestoreAmount = 60f,
             requiredxItems = new ItemType[] {ItemType.Plant , ItemType.Bush},
             requiredAmounts = new int[] {3 ,3}
        },
    };
}
