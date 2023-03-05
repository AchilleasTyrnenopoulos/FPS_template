using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : ItemBase
{
    public Animator armsAnim;
    public Animator weaponAnim;

    public GameObject meshPrefab;

    public bool canUseAction = true;

    public abstract void PrimaryAction();    
    public abstract void SecondaryAction();    
}
