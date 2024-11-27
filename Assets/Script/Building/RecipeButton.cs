using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipename;              // ������ �̸�
    public TextMeshProUGUI materialsText;           // �ʿ� ��� �ؽ�Ʈ
    public Button craftButton;                       // ���� ��ư

    private CraftionRecipe recipe;                  // ������ ������
    private BuildingCrafter cratter;                // �ǹ��� ���� �ý���
    private PlayerInventory playerInventory;        // �÷��̾� �κ��丮

    public void Setup(CraftionRecipe recipe, BuildingCrafter crafter)
    {
        this.recipe = recipe;
        this.cratter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipename.text = recipe.itemName;                          // ������ ���� ǥ��

        craftButton.onClick.AddListener(OnCraftButtonClicked);      // ���� ��ư�� �̺�Ʈ ����
    }

    private void UpdateMaterialstext()                          // ��� ���� ������Ʈ
    {
        string materials = "�ʿ� ��� :\n";
        for (int i = 0; i < recipe.requiredxItems.Length; i++)
        {
            ItemType itme = recipe.requiredxItems[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(itme);
            materials += $"{itme} : {has}/{recipe}+\n";
        }
        materialsText.text = materials;
    }
    private void OnCraftButtonClicked()                 // ���� ��ư Ŭ�� ó��
    {
        cratter.TryCraft(recipe, playerInventory);
        UpdateMaterialstext();
    }
}
