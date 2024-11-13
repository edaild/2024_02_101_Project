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
        // 초기 상태 설정 (반투명)
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed) return;                      // 건설 가능, 완료 변수 체크하여 리턴 시킨다.

        if (inventory.treeCount >= requiredTree)                    // 건설에 핅요한 나무 숫자를 확인한후
        {
            inventory.Removeitem(ItemType.Tree, requiredTree);  // 해당 나무 숫자 만큼 차감
            if (FloationgTextManager.Instance != null)
            {
                FloationgTextManager.Instance.Show($"{buildingName} 건설시각!", transform.position + Vector3.up);
            }
            StartCoroutine(CostructionRoutine());
        }
        else
        {
            if (FloationgTextManager.Instance != null)
            {
                FloationgTextManager.Instance.Show($"나무가 부족합니다! ({inventory.treeCount} / {requiredTree}) ", transform.position + Vector3.up);
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
            FloationgTextManager.Instance.Show($"{buildingName} 건설와료", transform.position + Vector3.up);
        }
    }
}
