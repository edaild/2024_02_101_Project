using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiManager : MonoBehaviour
{
    public static InventoryUiManager Instance {  get; private set; }

    [Header("UI References")]
    public GameObject inventoryPanel;           // �κ��丮 �г�
    public Transform itemContainer;             // ������ �������� �� �����̳�
    public GameObject itemSlotPrefab;           // ������ ���� ������
    public Button closeButton;                  // �ݱ� ��ư

    private PlayerInventory playerInventory;
    private SurvivalStats survivalStats;

    private void Awake()
    {
        Instance = this;
        inventoryPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        closeButton.onClick.AddListener(HideUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            if (inventoryPanel.activeSelf)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
        }
    }

    public void ShowUI()                                                    // UI â�� ���� ������     
    {
        inventoryPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        RetreshInventory();
    }
    public void HideUI()
    {
        inventoryPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void RetreshInventory()
    {
        // ���� ������ �������� ���� itemContaner ������ �ִ� ��� ������Ʈ ����
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
        CreatelItemSlot(ItemType.Crystal);
        CreatelItemSlot(ItemType.Plant);
        CreatelItemSlot(ItemType.Bush);
        CreatelItemSlot(ItemType.Tree);
        CreatelItemSlot(ItemType.VegetableStewk);
        CreatelItemSlot(ItemType.FruitSalad);
        CreatelItemSlot(ItemType.RepairKit);

    }

    private void CreatelItemSlot(ItemType type)
    {
        GameObject slotObj = Instantiate(itemSlotPrefab, itemContainer);
        ItemSlot slot = slotObj.GetComponent<ItemSlot>();
        slot.Setup(type, playerInventory.GetItemCount(type));               // �÷��̾� �κ��丮���� ������ ��ȯ�Ѵ�.
    }
}
