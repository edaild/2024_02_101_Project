using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                 // �ǹ� ���� ����
    private Vector3 lastPosition;                   // �÷��̾��� ������ ��ġ ���� (�÷��̾ �̵��� ���� ��� �ֺ��� �����ؼ� ������ ȹ��)
    private float moveThreshold = 0.1f;             // �̵� ���� �Ӱ谪 (�÷��̾ �̵��ؾ� �� �ּҰŸ�)
    private ConstructibleBuilding currentNearbyBuilding;        // ���� ������ �ִ� �Ǽ� ������ �ǹ�

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        CheckForBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                                // �̵��� ������ üũ
            lastPosition = transform.position;              // ���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        // ����� �������� �ְ� E Ű�� ������ �� ������ ����
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());         // PlayerInventroy�� �����Ͽ� ������ ����
        }

    }

    private void CheckForBuilding()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, checkRadius);       // ���� ���� ���� ��� �ݶ��̴��� ã��

        float closestDistance = float.MaxValue;     // ���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding collectiBuilding = null;     // ���� ����� ������ �ʱⰪ

        foreach (Collider collider in hitcolliders) // �� �ݶ��̴��� �˻��Ͽ� ���� ������ �������� ã��
        {
            ConstructibleBuilding bullding = collider.GetComponent<ConstructibleBuilding>();        // ������ ����
            if (bullding != null && bullding.canBuild && !bullding.isConstructed)            // �������� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, bullding.transform.position);     // �Ÿ� ���
                if (distance < closestDistance)   // �� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    collectiBuilding = bullding;
                }
            }
        }
        if (collectiBuilding != currentNearbyBuilding) // ���� ����� �������� ����Ǿ��� ���� �޼��� ǥ��
        {
            currentNearbyBuilding = collectiBuilding;        // ���� ����� ������ ������Ʈ

            if (FloationgTextManager.Instance != null)
            {
                FloationgTextManager.Instance.Show(
                    $"[F]Ű�� {currentNearbyBuilding},�Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)",
                    currentNearbyBuilding.transform.position + Vector3.up
                    );

            }
        }
    }
}
