using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiManager : MonoBehaviour
{
    public static InventoryUiManager Instance {  get; private set; }

    [Header("UI References")]
    public GameObject inventoryPanel;           // 인벤토리 패널
    public Transform itemContainer;             // 아이탬 슬릇들이 들어갈 컨테이너
    public GameObject itemSlotPrefab;           // 아이템 슬릇 프리펩
    public Button closeButton;                  // 닫기 버튼

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

    public void ShowUI()                                                    // UI 창이 열렸 있을때     
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
        // 기존 아이템 슬릇들을 제거 itemContaner 하위에 있는 모든 오브젝트 제거
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
        slot.Setup(type, playerInventory.GetItemCount(type));               // 플레이어 인벤토리에서 개수를 반환한다.
    }
}
