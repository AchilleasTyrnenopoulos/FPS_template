using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeapon : WeaponBase
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] private GameObject _decalPrefab;
    
    public override void PrimaryAction()
    {
        if (canUseAction)
        {
            Debug.Log("shooting layers " + _layers.ToString());

            // TODO play vfx, sfx & animation 
            // ...

            //raycast        
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, _layers))
            {
                Debug.Log("HitscanWeapon raycast hit " + hit.transform.gameObject.name);
                
                // check if enemy or destructable and call approritate methods (apply damage etc.)                
                if(hit.collider.gameObject.TryGetComponent(out IHealthManager enemyHealth))
                {
                    enemyHealth.TakeDamage(new DamageInfo { DamageAmount = 50 });
                }

                // spawn decal
                Quaternion decalRotation = Quaternion.FromToRotation(Vector3.up, hit.normal); //get decal rotation
                Instantiate(_decalPrefab, hit.point, decalRotation);
            }
        }
    }

    public override void SecondaryAction()
    {
        Debug.Log("triggered secondary action");
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
