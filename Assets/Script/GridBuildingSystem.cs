using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GridCell
{
    public Vector3 Position;             // ���� �׸��� �� ��ġ
    public bool IsOccupied;             // ���� �ǹ��� ���ִ��� ����
    public GameObject Building;         // ���� ��ġ�� �ǹ� ��ü

    public GridCell(Vector3Int position)        // ������ ��ü�� ȣ�� �ɶ�
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private int width = 10;            // �׸��� ���� ũ��
    [SerializeField] private int height = 10;           // �׸��� ���� ũ��
    [SerializeField] private float cellSize = 1;        // �� ���� ũ��

    [SerializeField] private GameObject cellPrefab;         // �� ������
    [SerializeField] private GameObject buildingPrefads;      // �ǹ� ������
    [SerializeField] private PlayerController playerController;         // �÷��̾� ����ѷ� ����
    [SerializeField] private float maxBuilDistance = 5f;                 // �Ǽ� ������ �ִ� �Ÿ�

    [SerializeField] private Grid grid;                                      // �׸��� ���� �� �޾ƿ´�
    private GridCell[,] cells;                                               // 2�� �迭�� ���� Gridcell
    private Camera firstPersonCamera;                                        // �÷��̾��� 1��Ī ī�޶� �����´�
   // private GridCell[,] cells;

    void Start()
    {
        firstPersonCamera = playerController.firstPersonCamera;
        GetLookPosition();
    }

    private void Update()
    {
        Vector3 lookPosition = GetLookPosition();
        if (lookPosition != Vector3.zero)
        {
            Vector3Int gridPosilion = grid.WorldToCell(lookPosition);
            if (isValidGridPosition(gridPosilion))
            {
                HighlightCell(gridPosilion);
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(gridPosilion);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    RemoveBuilding(gridPosilion);
                }
            }
        }  
    }

    // �׸��带 �����ϰ� ���� �ʱ�ȭ�ϴ� �Լ�

    private void createGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize);      // ������ �� ����� �׸��� ������Ʈ�� �ִ´�.
        cells= new GridCell[width,height];
        Vector3 gridCenter = playerController.transform.position;
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2); // �׸��� �߽����� �̵� ��Ų��.

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);    
                Vector3 WorldPosition = grid.GetCellCenterWorld(cellPosition);  // grid�� ���� ���� �������� �����´�.
                GameObject cellObject = Instantiate(cellPrefab, WorldPosition, cellPrefab.transform.rotation);    // ������ �׸��带 �����.
                cellObject.transform.SetParent(transform);            // ���� ������Ʈ ������ ���� �Ѵ�.

                cells[x, z] = new GridCell(cellPosition);           // �� ������ ���� ���� �Ѵ�.
            }
        }
    }

    // ���õ� ���� ���̶���Ʈ�ϴ� �Լ�
    private void HighlightCell(Vector3Int gridPosition)
    {
        for(int x = 0; x < width; x++)
        {
            for( int z = 0; z < height; z++)
            {
                GameObject cellObject = 
                    cells[x, z].Building != null ? cells[x, z].Building : transform.GetChild(x * height + z).gameObject;
                cellObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        GameObject highlightObject =
            cell.Building != null ? cell.Building : transform.GetChild(gridPosition.x * height + gridPosition.z).gameObject;
        highlightObject.GetComponent<Renderer>().material.color = cell.IsOccupied?Color.red: Color.green;
    }

    //�׸��� ���� �ǹ��� ��ġ�ϴ� �Լ�

    private void PlaceBuilding(Vector3Int gridPosilion)
    {
        GridCell cell = cells[gridPosilion.x , gridPosilion.z];
        if(!cell.IsOccupied)
        {
            Vector3 WorldPosition = grid.GetCellCenterWorld(gridPosilion);
            GameObject building = Instantiate(buildingPrefads, WorldPosition, Quaternion.identity);
            cell.IsOccupied = true;
            cell.Building = building;
        }
    }

    // �÷��̾� �׸��� ������ �ǹ��� �����ϴ� �Լ�

    private void RemoveBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        if (!cell.IsOccupied)
        {
            
            Destroy(cell.Building);
            cell.IsOccupied = true;
            cell.Building = null;
        }
    }

    // �÷��̾ ���� �ִ� ��ġ�� ����ϴ� �޼���

    private Vector3 GetLookPosition()
    {
        if (playerController.isFirstPerson) // �÷��̾ 1��Ī ���
        {
            Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);       // �޾ƿ� 1��¡ ī�޶� ������ ray
            if(Physics.Raycast(ray, out RaycastHit hitInfo, maxBuilDistance))   // ������ ray�� ��ü�� ���� ���
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);     // Scene â���� ������ ������ ray�� �����ش�.
                return hitInfo.point;                                                       // raycast �� ��ü ������ �����Ѵ�.
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * maxBuilDistance, Color.white); // Scene â���� �Ͼ�� ������ ray�� �����ش�.
            }
        }
        // �÷��̾ 3��Ī ���
       else
        {
            Vector3 characterPosition = playerController.transform.position;                            // ĳ���� ��ġ ����
            Vector3 characterForward = playerController.transform.forward;
            Vector3 rayOrigin = characterPosition + Vector3.up * 1.5f + characterForward * 0.5f;
            Vector3 rayDirection = (characterForward - Vector3.up).normalized;

            Ray ray = new Ray(rayOrigin, rayDirection);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxBuilDistance))   // ������ ray�� ��ü�� ���� ���
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.blue);     // Scene â���� ��� �� ������ ray�� �����ش�.
                return hitInfo.point;                                                       // raycast �� ��ü ������ �����Ѵ�.
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * maxBuilDistance, Color.white);
            }
        }
        return Vector3.zero;
    }

    // �׸��� �������� ��ȣ���� Ȯ���ϴ� �Լ�

    private bool isValidGridPosition(Vector3Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width &&
            gridPosition.z >= 0 && gridPosition.z < height;
    }

    private void OnDrawGizmos()     // Gizmo�� ǥ�����ִ� �Լ�
    {
        Gizmos.color = Color.blue;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {

                Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(x, 0, z));      // �׸��� �߽� ���� �����´�.
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));     // ������ ĭ�� �׷��ش�.
            }
        }
    }
}
