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
            //raycast        
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _layers))
            {                
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
