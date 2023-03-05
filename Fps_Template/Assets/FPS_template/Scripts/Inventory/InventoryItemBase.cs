using Assets.FPS_template.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New InventoryItem", menuName ="ScriptableObjects/Inventory/InventoryItem")]
public class InventoryItemBase : ScriptableObject
{
    public string name;
    public Sprite icon;
    public InventoryItemType type;
    public InventoryIdentifiers inventoryIdentifier;
}
