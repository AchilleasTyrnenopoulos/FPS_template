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

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
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
}
