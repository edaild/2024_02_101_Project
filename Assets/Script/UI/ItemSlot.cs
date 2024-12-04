using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;            //������ �̸� (UI)
    public TextMeshProUGUI countText;               //������ ���� (UI)
    public Button useButton;                        //��� ��ư

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

    private string GetItemDisplayName(ItemType type)                        // ������ ������ ǥ�� �Ǵ� �̸� ����
    {
        switch (type)
        {
            case ItemType.VegetableStewk: return "��ä ��Ʃ";
            case ItemType.FruitSalad: return "���� ������";
            case ItemType.RepairKit: return "���� ŰƮ";
            default: return type.ToString();
        }
    }

    private void UseItem()                                                                   // ������ ��� �Լ�
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();                 // ���� �κ��丮�� ����
        SurvivalStats stats = FindAnyObjectByType<SurvivalStats>();                         // ���� ���� ����

        switch (itemType)
        {
            case ItemType.VegetableStewk:
                if (inventory.Removeitem(itemType, 1))                                      // ��ä ��Ʃ �� ���
                {
                    stats.EatFood(40f);                                                     // ��� +40
                    InventoryUiManager.Instance.RetreshInventory();          
                }
                break;
            case ItemType.FruitSalad:                                                       // ���� ������
                if (inventory.Removeitem(itemType, 1))                                      // �κ��丮���� ������ 1�� ����
                {
                    stats.EatFood(50f);                                                     // ��� +50
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
