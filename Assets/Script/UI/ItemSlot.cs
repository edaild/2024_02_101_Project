using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;            //아이템 이름 (UI)
    public TextMeshProUGUI countText;               //아이템 개수 (UI)
    public Button useButton;                        //사용 버튼

    private ItemType itemType;
    private int itemCount;

    public void Setup(ItemType type, int count)
    {
        itemType = type;
        itemCount = count;

        itemNameText.text = GetItemDisplayName(type);
        countText.text = count.ToString();

        useButton.onClick.AddListener(UseItem);
    }

    private string GetItemDisplayName(ItemType type)                        // 아이템 슬릇에 표시 되는 이름 설정
    {
        switch (type)
        {
            case ItemType.VegetableStewk: return "야채 스튜";
            case ItemType.FruitSalad: return "과일 샐러드";
            case ItemType.RepairKit: return "수리 키트";
            default: return type.ToString();
        }
    }

    private void UseItem()                                                                   // 아이템 사용 함수
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();                 // 유저 인벤토리를 참조
        SurvivalStats stats = FindAnyObjectByType<SurvivalStats>();                         // 유저 스탯 참조

        switch (itemType)
        {
            case ItemType.VegetableStewk:
                if (inventory.Removeitem(itemType, 1))                                      // 야채 스튜 인 경우
                {
                    stats.EatFood(40f);                                                     // 허기 +40
                    InventoryUiManager.Instance.RetreshInventory();          
                }
                break;
            case ItemType.FruitSalad:                                                       // 과일 샐러드
                if (inventory.Removeitem(itemType, 1))                                      // 인벤토리에서 아이템 1개 삭제
                {
                    stats.EatFood(50f);                                                     // 허기 +50
                    InventoryUiManager.Instance.RetreshInventory();
                }
                break;
            case ItemType.RepairKit:
                if (inventory.Removeitem(itemType, 1))
                {
                    stats.EatFood(25f);
                    InventoryUiManager.Instance.RetreshInventory();
                }
                break;
        }
    }
}
