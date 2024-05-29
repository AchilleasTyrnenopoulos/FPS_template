using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeapon : WeaponBase
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] private GameObject _decalPrefab;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _cooldownTimer = 0f;
    [SerializeField] private bool _hasCooldown = false;
    public override void PrimaryAction()
    {
        if (canUseAction && !_hasCooldown)
        {
            _hasCooldown = true;

            // play vfx, sfx & animation             
            //armsAnim.SetTrigger("Shoot");

            //raycast        
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, _layers))
            {
                Debug.Log("HitscanWeapon raycast hit " + hit.transform.gameObject.name);

                // check if enemy or destructable and call approritate methods (apply damage etc.)                
                if (hit.collider.gameObject.TryGetComponent(out IHealthManager enemyHealth))
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

    private void Update()
    {
        if (_hasCooldown)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _fireRate)
            {
                _hasCooldown = false;
                _cooldownTimer = 0f;
            }
        }
    }
}
