using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public override void PrimaryAction()
    {
        Debug.Log("melee weapon - primary action triggered");

        Debug.Log($"animator is active and enabled: {weaponAnim.isActiveAndEnabled}");
        Debug.Log($"animator game object is active: {weaponAnim.gameObject.activeInHierarchy}");
        Debug.Log($"game object of this weapon is: {this.gameObject.name}");
        Debug.Log("playing animation");
        weaponAnim.SetTrigger("Attack");
        
    }

    public override void SecondaryAction()
    {
        Debug.Log("melee weapon - secondary action triggered");
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO remove - for testing
        weaponAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
