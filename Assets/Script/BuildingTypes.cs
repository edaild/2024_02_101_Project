using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingTypes 
{
    CraftingTalbe,      // ���۴�
    Furnace,            // �뱤��
    Kitchen,            // �ֹ�
    Storage             // â��
}

[System.Serializable]

public class CraftionRecipe
{
    public string itemName;                 // ����Ȱ ������ �̸�
    public ItemType resultItem;             // �����
    public int resultAmount = 1;            // ����� ����
    public float HungerRestoreAmount;       // ��� ȸ���� (������ ���)
    public float repairAmount;              // ������ (���� ŰƮ�� ���)

    public ItemType[] requiredxItems;       // �ʿ��� ����
    public int[] requiredAmounts;           // �ʿ��� ��� ����
}
