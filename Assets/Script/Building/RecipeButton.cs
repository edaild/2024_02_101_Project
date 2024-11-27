using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipename;              // 레시피 이름
    public TextMeshProUGUI materialsText;           // 필요 재료 텍스트
    public Button craftButton;                       // 제작 버튼

    private CraftionRecipe recipe;                  // 레시피 데이터
    private BuildingCrafter cratter;                // 건물의 제작 시스템
    private PlayerInventory playerInventory;        // 플레이어 인벤토리

    public void Setup(CraftionRecipe recipe, BuildingCrafter crafter)
    {
        this.recipe = recipe;
        this.cratter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipename.text = recipe.itemName;                          // 레시피 정보 표시

        craftButton.onClick.AddListener(OnCraftButtonClicked);      // 제작 버튼에 이벤트 연결
    }

    private void UpdateMaterialstext()                          // 재료 정보 업데이트
    {
        string materials = "필요 재료 :\n";
        for (int i = 0; i < recipe.requiredxItems.Length; i++)
        {
            ItemType itme = recipe.requiredxItems[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(itme);
            materials += $"{itme} : {has}/{recipe}+\n";
        }
        materialsText.text = materials;
    }
    private void OnCraftButtonClicked()                 // 제작 버튼 클릭 처리
    {
        cratter.TryCraft(recipe, playerInventory);
        UpdateMaterialstext();
    }
}
