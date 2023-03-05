using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponInventoryItem", menuName = "ScriptableObjects/Inventory/WeaponInventoryItem")]
public class WeaponInventoryItem : InventoryItemBase
{
    [SerializeField] private GameObject weaponPrefab;
}
