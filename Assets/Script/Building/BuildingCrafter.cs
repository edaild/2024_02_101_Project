using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingTypes buildingType;                  // �ǹ� Ÿ��
    public CraftionRecipe[] recipes;                    // ��� ������ ���Ǽ� �迭
    private SurvivalStats survivalStats;                // ���� ���� ����
    private ConstructibleBuilding building;             // �ǹ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch (buildingType)                               // �ǹ� Ÿ�Կ� ���� ������ ����
        {
            case BuildingTypes.Kitchen:
                recipes = RecipeList.KitchenRecipes;
                break;
            case BuildingTypes.CraftingTable:
                recipes = RecipeList.workbenchRecipes;
                break;
        }
    }

    public void TryCraft(CraftionRecipe recipe, PlayerInventory inventory)      // ������ ���� �õ�
    {
        if (!building.isConstructed)                                    // �Ǽ��� �Ϸ���� �ʾҴٸ� ���� �Ұ�
        {
            FloationgTextManager.Instance?.Show("�Ǽ��� �Ϸ� ���� �ʾҽ��ϴ�.!", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredxItems.Length; i++)      // ��� üũ
        {
            if (inventory.GetItemCount(recipe.requiredxItems[i]) < recipe.requiredAmounts[i])
            {
                FloationgTextManager.Instance?.Show("��ᰡ ���� �մϴ�..!", transform.position + Vector3.up);
                return;
            }
        } 
        
        for (int i = 0; i < recipe.requiredxItems.Length; i++)          // ��� ����
        {
            inventory.Removeitem(recipe.requiredxItems[i], recipe.requiredAmounts[i]);

            survivalStats.DamageOnCrafting();                   // ���ֺ� ������ ����

            inventory.AddItem(recipe.resultItem, recipe.resultAmount);      // ������ ����
            FloationgTextManager.Instance?.Show($"{recipe.itemName} ���� �Ϸ�!", transform.position + Vector3.up);
        }
    }


}
