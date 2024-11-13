using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingTypes 
{
    CraftingTalbe,      // 제작대
    Furnace,            // 용광로
    Kitchen,            // 주방
    Storage             // 창고
}

[System.Serializable]

public class CraftionRecipe
{
    public string itemName;                 // 저작활 아이템 이름
    public ItemType resultItem;             // 결과물
    public int resultAmount = 1;            // 결과물 개수
    public ItemType[] requiredxItems;       // 필요한 재료들
    public int[] requiredAmounts;           // 필요한 재료 개수
}
