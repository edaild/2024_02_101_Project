using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                 // 건물 감지 범위
    private Vector3 lastPosition;                   // 플레이어의 마지막 위치 저장 (플레이어가 이동이 있을 경우 주변을 감지해서 아이템 획득)
    private float moveThreshold = 0.1f;             // 이동 감지 임계값 (플레이어가 이동해야 할 최소거리)
    private ConstructibleBuilding currentNearbyBuilding;        // 현재 가까이 있는 건설 가능한 건물
    private BuildingCrafter currentBuildingCrafter;              // 추가 : 현재 건물의 제작 시스템

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;         
        CheckForBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 일정 거리 이상 이동했는지 체크
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                                // 이동시 아이템 체크
            lastPosition = transform.position;              // 현재 위치를 마지막 위치로 업데이트
        }

        // 가까운 아이템이 있고 E 키를 눌렀을 때 아이템 수집
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());         // PlayerInventroy를 참조하여 아이템 수집
            }else if(currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName}의 제작 메뉴 열기");           // 싱글톤으로 접근해서 UI 표시를 한다.
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);
            }

        }

    }

    private void CheckForBuilding()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, checkRadius);       // 감지 범위 내의 모든 콜라이더를 찾음

        float closestDistance = float.MaxValue;     // 가장 가까운 거리의 초기값
        ConstructibleBuilding collectiBuilding = null;     // 가장 가까운 아이템 초기값
        BuildingCrafter closestCrafter = null;              // 추가


        foreach (Collider collider in hitcolliders) // 각 콜라이더를 검사하여 수집 가능한 아이템을 찾음
        {
            ConstructibleBuilding bullding = collider.GetComponent<ConstructibleBuilding>();        // 아이템 감지
            if (bullding != null)            // 모든 건물 감지로 변경
            {
                float distance = Vector3.Distance(transform.position, bullding.transform.position);     // 거리 계산
                if (distance < closestDistance)   // 더 가까운 아이템을 발견 시 업데이트
                {
                    closestDistance = distance;
                    collectiBuilding = bullding;
                    closestCrafter = bullding.GetComponent<BuildingCrafter>();              // 여기서 크래프터 가져오기
                }
            }
        }
        if (collectiBuilding != currentNearbyBuilding) // 가장 가까운 아이템이 변경되었을 때만 메세지 표시
        {
            currentNearbyBuilding = collectiBuilding;        // 가장 가까운건물 업데이트
            currentBuildingCrafter = closestCrafter;        // 추가
            if (FloationgTextManager.Instance != null)
            {
                FloationgTextManager.Instance.Show(
                    $"[F]키로 {currentNearbyBuilding.buildingName},건설 (나무 {currentNearbyBuilding.requiredTree} 개 필요)",
                    currentNearbyBuilding.transform.position + Vector3.up
                    );

            }
        }
    }
}
