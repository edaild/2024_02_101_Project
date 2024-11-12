using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // ������ ������ ������ �����ϴ� ����
    public int crystalCount = 0;        // Ŭ����Ż ����
    public int PlantCount = 0;         // �Ĺ� ����
    public int bushCount = 0;         // ��Ǯ ����
    public int treeCount = 0;         // ���� ����

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
                PlantCount++; // �Ĺ� ���� ����
                Debug.Log($" �Ĺ� ȹ��! ���� ���� : {PlantCount}");     //���� �Ĺ� ���� ���
                break;
            case ItemType.Bush:
                bushCount++; // ��Ǯ ���� ����
                Debug.Log($"��Ǯ ȹ��! ���� ���� : {bushCount}");       // ���� ��Ǯ ���� ���
                break;
            case ItemType.Tree:
                treeCount++; // ���� ���� ����
                Debug.Log($"���� ȹ��! ���� ���� : {treeCount}");       // ���� ���� ���� ���
                break;
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
        Debug.Log($"�Ĺ�:{PlantCount}��");               // �Ĺ� ���� ���
        Debug.Log($"��Ǯ:{bushCount}��");               // ��Ǯ ���� ���
        Debug.Log($"����:{treeCount}��");               // ���� ���� ���
    }
}