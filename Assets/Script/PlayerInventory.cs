using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats suvivalStats;            // Ŭ���� ����
    // ������ ������ ������ �����ϴ� ����
    public int crystalCount = 0;        // Ŭ����Ż ����
    public int plantCount = 0;         // �Ĺ� ����
    public int bushCount = 0;         // ��Ǯ ����
    public int treeCount = 0;         // ���� ����

    // �߰��� ������
    public int vegetableStewCount = 0;      // ��ä ��Ʈ ����
    public int fruitSaledCount = 0;         // ���� ������ ����
    public int repairKitCount = 0;          // ���� ŰƮ ����

    public void Start()
    {
        suvivalStats = GetComponent<SurvivalStats>();
    }

    public void UseItem(ItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)        // �ش� �������� �ִ��� Ȯ��
        {
            return;

        }

        switch (itemType)                   // ������ Ÿ�Կ� ���� ȿ�� ����
        {
            case ItemType.VegetableStewk:
                Removeitem(ItemType.VegetableStewk, 1);
                suvivalStats.EatFood(RecipeList.KitchenRecipes[0].HungerRestoreAmount);
                break;

            case ItemType.FruitSalad:
                Removeitem(ItemType.FruitSalad, 1);
                suvivalStats.EatFood(RecipeList.KitchenRecipes[1].HungerRestoreAmount);
                break;
            case ItemType.RepairKit:
                Removeitem(ItemType.RepairKit, 1);
                suvivalStats.EatFood(RecipeList.KitchenRecipes[0].repairAmount);
                break;

        }
    }

    // ���� �������� �Ѳ����� ȹ��
    public void AddItem(ItemType itemType, int amount)
    {
        // amount ��ŭ ������ AddItem ȣ��
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    // �������� �߰��ϴ� �Լ�, ������ ������ ���� �ش� �������� ������ ������Ŵ

    public void AddItem(ItemType itemType)
    {
        // ������ ������ ���� �ٸ� ���� ����
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++; // ũ����Ż ���� ����
                Debug.Log($" ũ����Ż ȹ��! ���� ���� : {crystalCount}");     //���� ũ����Ż ���� ���
                break;
            case ItemType.Plant:
                plantCount++; // �Ĺ� ���� ����
                Debug.Log($" �Ĺ� ȹ��! ���� ���� : {plantCount}");     //���� �Ĺ� ���� ���
                break;
            case ItemType.Bush:
                bushCount++; // ��Ǯ ���� ����
                Debug.Log($"��Ǯ ȹ��! ���� ���� : {bushCount}");       // ���� ��Ǯ ���� ���
                break;
            case ItemType.Tree:
                treeCount++; // ���� ���� ����
                Debug.Log($"���� ȹ��! ���� ���� : {treeCount}");       // ���� ���� ���� ���
                break;

            case ItemType.VegetableStewk:
                vegetableStewCount++;
                Debug.Log($"��ä ��Ʃ ȹ��! ���� ���� : {vegetableStewCount}");       // ���� ���� ���� ���
                break;

            case ItemType.FruitSalad:
                fruitSaledCount++;
                Debug.Log($"��ä ��Ʃ ȹ��! ���� ���� : {fruitSaledCount}");       // ���� ���� ������ ���� ���
                break;

            case ItemType.RepairKit:
                repairKitCount++;
                Debug.Log($"����ŰƮ ȹ��! ���� ���� : {repairKitCount}");       // ���� ���� ŰƮ ���� ���
                break;
        }
    }

    // �������� �����ϴ� �Լ� 
    public bool Removeitem(ItemType itemType, int amount = 1)
    {
        // ������ ������ ���� �ٸ� ���� ����
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    crystalCount -= amount; // ũ����Ż ���� ����
                    Debug.Log($" ũ����Ż {amount} ���! ���� ���� : {crystalCount}");     //���� ũ����Ż ���� ���
                    return this;
                }
                break;
            case ItemType.Plant:
                if (plantCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    plantCount -= amount; // �Ĺ� ���� ����
                    Debug.Log($" �Ĺ�  {amount} ���! ���� ���� : {plantCount}");     //���� �Ĺ� ���� ���
                    return this;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    bushCount -= amount; // ��Ǯ ���� ����
                    Debug.Log($"��Ǯ {amount} ���! ���� ���� : {bushCount}");       // ���� ��Ǯ ���� ���
                    return this;
                }

                break;
            case ItemType.Tree:
                if (treeCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    treeCount -= amount; // ���� ���� ����
                    Debug.Log($"���� {amount} ���! ���� ���� : {treeCount}");       // ���� ���� ���� ���
                    return this;
                }
                break;

            case ItemType.VegetableStewk:
                if (vegetableStewCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    vegetableStewCount= amount; // ��ä ��Ʃ ���� ����
                    Debug.Log($"��ä ��Ʃ {amount} ���! ���� ���� : {vegetableStewCount}");       // ���� ��ä ��Ʃ ���� ���
                    return this;
                }
                break;

            case ItemType.FruitSalad:
                if (fruitSaledCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    fruitSaledCount = amount; // ���� ������ ���� ����
                    Debug.Log($"���� ������{amount} ���! ���� ���� : {fruitSaledCount}");       // ���� ���� ������ ���� ���
                    return this;
                }
                break;

            case ItemType.RepairKit:
                if (repairKitCount >= amount) // ������ �ִ� ������ ������� Ȯ��
                {
                    repairKitCount = amount; // ���� ŰƮ ���� ����
                    Debug.Log($"���� ŰƮ{amount} ���! ���� ���� : {repairKitCount}");       // ���� ���� ŰƮ ���� ���
                    return this;
                }
                break;
        }
        Debug.Log($"{itemType} �������� �����մϴ�.");
        return false;
    }

    // Ư�� �������� ���� ������ ����ȯ �ϴ� �Լ�
    public int GetItemCount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;

            case ItemType.VegetableStewk:
                return vegetableStewCount;
            case ItemType.FruitSalad:
                return fruitSaledCount;
            case ItemType.RepairKit:
                return repairKitCount;
            default:
                return 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();        // �κ��丮 ��� �Լ� ȣ��
        }
    }

    private void ShowInventory()
    {
        Debug.Log("====�κ��丮====");
        Debug.Log($"ũ����Ż:{crystalCount}��");          // ũ����Ż ���� ���
        Debug.Log($"�Ĺ�:{plantCount}��");               // �Ĺ� ���� ���
        Debug.Log($"��Ǯ:{bushCount}��");               // ��Ǯ ���� ���
        Debug.Log($"����:{treeCount}��");               // ���� ���� ���
    }
}
