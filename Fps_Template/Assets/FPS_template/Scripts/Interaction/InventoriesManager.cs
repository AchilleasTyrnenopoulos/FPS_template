using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoriesManager : MonoBehaviour
{
    public static InventoriesManager Instance {get; private set;}
    [SerializeField] private List<InventoryBase> _children = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning($"There are multiple GameManagers in the scene.\n Name: {this.gameObject.name}\n Position: {this.transform}");
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InventoryBase GetInventory(int index)
    {
        return _children[index];
    }

    public InventoryBase GetInventory(InventoryIdentifiers identifier)
    {
        return _children.Where(c => c.GetInventoryIdentifier() == identifier).FirstOrDefault();
    }
}