using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    private PlayerController _controller;

    [SerializeField] private Transform _weaponRoot;

    [SerializeField] private WeaponBase _currentWeapon;
    [SerializeField] private WeaponBase _mainWeapon;
    [SerializeField] private WeaponBase _secondaryWeapon;
    [SerializeField]
    private bool _secondaryWeaponEquipped = false;

    [SerializeField] private InventoryBase _weaponInventory;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _weaponInventory = InventoriesManager.Instance.GetInventory(InventoryIdentifiers.WEAPONS_INVENTORY);
    }

    private void OnEnable()
    {
        EventAggregator.GetEvent<EquipWeaponEvent>().Subscribe(EquipWeapon);
    }

    private void OnDisable()
    {
        EventAggregator.GetEvent<EquipWeaponEvent>().UnSubscribe(EquipWeapon);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(_controller.GetPrimaryActionTrigger())
        {
            _currentWeapon?.PrimaryAction();
        }
        else if(_controller.GetSecondaryActionTrigger() )
        {
            _currentWeapon?.SecondaryAction();
        }

        if(_controller.GetSwapWeaponTrigger())
        {
            SwapWeapon();
        }
    }

    private void GetWeapon()
    {
        _currentWeapon = _mainWeapon;
    }

    private void SwapWeapon()
    {
        // check if player has a main/secondary weapon
        if ((!_secondaryWeaponEquipped && _secondaryWeapon == null) || (_secondaryWeaponEquipped && _mainWeapon == null)) return;

        _currentWeapon.gameObject.SetActive(false);

        if (!_secondaryWeaponEquipped)
        {
            _currentWeapon = _secondaryWeapon;
            _secondaryWeaponEquipped = true;
        }
        else
        {
            _currentWeapon = _mainWeapon;
            _secondaryWeaponEquipped = false;
        }

        _currentWeapon.gameObject.SetActive(true);
    }

    private void EquipWeapon(int weaponId)
    {
        // get weapon from weapon inventory
        var weaponInvItem = _weaponInventory.GetItem(weaponId);
        var weapon = weaponInvItem.itemPrefab;

        // set as main weapon 
        _mainWeapon = weapon.GetComponent<WeaponBase>();
        GetWeapon();
        
        var instWeapon = Instantiate(weapon, _weaponRoot); // TODO do this properly
        instWeapon.SetActive(true);
        //Debug.Log("Equipping " + weaponGO.name);
    }
}
