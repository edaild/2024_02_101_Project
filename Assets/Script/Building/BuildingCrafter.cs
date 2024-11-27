using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingTypes buildingType;                  // 건물 타입
    public CraftionRecipe[] recipes;                    // 사용 가능한 레피서 배열
    private SurvivalStats survivalStats;                // 생존 스탯 참조
    private ConstructibleBuilding building;             // 건물 상태 참조

    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch (buildingType)                               // 건물 타입에 따라 레시피 설정
        {
            case BuildingTypes.Kitchen:
                recipes = RecipeList.KitchenRecipes;
                break;
            case BuildingTypes.CraftingTable:
                recipes = RecipeList.workbenchRecipes;
                break;
        }
    }

    public void TryCraft(CraftionRecipe recipe, PlayerInventory inventory)      // 아이템 제작 시도
    {
        if (!building.isConstructed)                                    // 건설이 완료되지 않았다며 제작 불가
        {
            FloationgTextManager.Instance?.Show("건설이 완료 되지 않았습니다.!", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredxItems.Length; i++)      // 재료 체크
        {
            if (inventory.GetItemCount(recipe.requiredxItems[i]) < recipe.requiredAmounts[i])
            {
                FloationgTextManager.Instance?.Show("재료가 부족 합니다..!", transform.position + Vector3.up);
                return;
            }
        } 
        
        for (int i = 0; i < recipe.requiredxItems.Length; i++)          // 재료 소진
        {
            inventory.Removeitem(recipe.requiredxItems[i], recipe.requiredAmounts[i]);

            survivalStats.DamageOnCrafting();                   // 우주복 내구도 감소

            inventory.AddItem(recipe.resultItem, recipe.resultAmount);      // 아이템 제작
            FloationgTextManager.Instance?.Show($"{recipe.itemName} 제작 완료!", transform.position + Vector3.up);
        }
    }


}
