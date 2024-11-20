public class RecipeList
{
    public static CraftionRecipe[] KitchenRecipes = new CraftionRecipe[]
    {
        new CraftionRecipe
        {
             itemName = "��ü ��Ʃ",
             resultItem = ItemType.VegetableStewk,
             resultAmount = 1,
             HungerRestoreAmount = 40f,
             requiredxItems = new ItemType[] {ItemType.Plant , ItemType.Bush},
             requiredAmounts = new int[] {2 ,1}
        },

         new CraftionRecipe
        {
             itemName = "���� ������",
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
           itemName = "���� ������",
             resultItem = ItemType.RepairKit,
             resultAmount = 1,
             HungerRestoreAmount = 60f,
             requiredxItems = new ItemType[] {ItemType.Plant , ItemType.Bush},
             requiredAmounts = new int[] {3 ,3}
        },
    };
}
