using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public override void PrimaryAction()
    {
        Debug.Log("melee weapon - primary action triggered");

        //trigger animation
    }

    public override void SecondaryAction()
    {
        Debug.Log("melee weapon - secondary action triggered");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
