using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ ���� ����
public enum ItemType 
{
    Crystal,         //ũ����Ż
    Plant,           // �Ĺ�
    Bush,            // ��Ǯ
    Tree,           // ����
    VegetableStewk, // ��ä ��Ʃ (��� ȸ����)
    FruitSalad,     // ���� ������ (��� ȸ����)
    RepairKit       // ���� ŰƮ (���ֺ� ������)
}
public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                 // ������ ���� ����
    private Vector3 lastPosition;                   // �÷��̾��� ������ ��ġ ���� (�÷��̾ �̵��� ���� ��� �ֺ��� �����ؼ� ������ ȹ��)
    private float moveThreshold = 0.1f;             // �̵� ���� �Ӱ谪 (�÷��̾ �̵��ؾ� �� �ּҰŸ�)
    private CollectibleItem currentNearbyItem;      // ���� ���� ������ �ִ� ���� ������ ������

    void Start()
    {
        lastPosition = transform.position;      //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForItems();
    }

    void Update()
    {
        // �÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        if  (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForItems();                                // �̵��� ������ üũ
            lastPosition = transform.position;              // ���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        // ����� �������� �ְ� E Ű�� ������ �� ������ ����
        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());         // PlayerInventroy�� �����Ͽ� ������ ����
        } 
    }

    private void CheckForItems()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, checkRadius);       // ���� ���� ���� ��� �ݶ��̴��� ã��

        float closestDistance = float.MaxValue;     // ���� ����� �Ÿ��� �ʱⰪ
        CollectibleItem collectibleItem = null;     // ���� ����� ������ �ʱⰪ

        foreach (Collider collider in hitcolliders) // �� �ݶ��̴��� �˻��Ͽ� ���� ������ �������� ã��
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();        // ������ ����
            if (item != null && item.canCollect)            // �������� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);     // �Ÿ� ���
                if (distance < closestDistance)   // �� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    collectibleItem = item;
                }
            }
        }
        if (collectibleItem != currentNearbyItem) // ���� ����� �������� ����Ǿ��� ���� �޼��� ǥ��
        {
            currentNearbyItem = collectibleItem;        // ���� ����� ������ ������Ʈ

            if (currentNearbyItem != null)
            {
                Debug.Log($"[E] {currentNearbyItem.itemName} ���� ");         // ���ο� ������ ���� �޼��� ǥ��
            }
        }
    }

    private void OnDrawGizmos()         /// ����Ƽ Sceneâ���� ���̴� Debug �׸�
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, checkRadius);
    }
}
