using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Seetiongs")]
    public BuildingTypes buildingType;
    public string buildingName;
    public int requiredTree = 5;
    public float constructionTiem = 2.0f;

    public bool canBuild = true;
    public bool isConstructed = false;

    private Material buildingMaterial;

     void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        // �ʱ� ���� ���� (������)
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed) return;                      // �Ǽ� ����, �Ϸ� ���� üũ�Ͽ� ���� ��Ų��.

        if (inventory.treeCount >= requiredTree)                    // �Ǽ��� ������ ���� ���ڸ� Ȯ������
        {
            inventory.Removeitem(ItemType.Tree, requiredTree);  // �ش� ���� ���� ��ŭ ����
            if (FloationgTextManager.Instance != null)
            {
                FloationgTextManager.Instance.Show($"{buildingName} �Ǽ��ð�!", transform.position + Vector3.up);
            }
            StartCoroutine(CostructionRoutine());
        }
        else
        {
            if (FloationgTextManager.Instance != null)
            {
                FloationgTextManager.Instance.Show($"������ �����մϴ�! ({inventory.treeCount} / {requiredTree}) ", transform.position + Vector3.up);
            }
        }
    }

   private IEnumerator CostructionRoutine()
    {
        canBuild = false;
        float timer = 0;
        Color color = buildingMaterial.color;

        while (timer < constructionTiem)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTiem);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstructed = true;

        if(FloationgTextManager.Instance != null)
        {
            FloationgTextManager.Instance.Show($"{buildingName} �Ǽ��ͷ�", transform.position + Vector3.up);
        }
    }
}
