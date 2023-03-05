using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    private PlayerController _controller;
    [SerializeField] private WeaponBase _currentWeapon;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_controller.GetPrimaryActionTrigger())
        {
            _currentWeapon.PrimaryAction();
        }
        else if(_controller.GetSecondaryActionTrigger() )
        {
            _currentWeapon.SecondaryAction();
        }
    }
}
