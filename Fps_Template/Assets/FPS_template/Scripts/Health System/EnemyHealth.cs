using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealthManager
{
    [SerializeField] private bool _tookDamage = false;

    public float InvulnerabilityTime => _invulnerabilityTime;
    [SerializeField] private float _invulnerabilityTime = .1f;    
    [SerializeField] private float _invulnerabilityCounter = 0f;

    public HealthInfo HealthInfo => _healthInfo;
    [SerializeField] private HealthInfo _healthInfo;


    private void OnEnable()
    {
        _healthInfo = new HealthInfo();
    }

    private void Update()
    {
        if (_tookDamage)
        {
            _invulnerabilityCounter += Time.deltaTime;
            if (_invulnerabilityCounter >= _invulnerabilityTime)
            {
                // can take damage again
                _invulnerabilityCounter = 0f;
                _tookDamage = false;
            }
        }
    }

    public void OnDeath()
    {
        Debug.Log($"Enemy {this.gameObject.name} died!");
    }

    public void TakeDamage(DamageInfo damage)
    {
        if (_tookDamage) return;

        _healthInfo.CurrentHealth -= damage.DamageAmount;
        _tookDamage = true;

        // handle death
        if (_healthInfo.CurrentHealth <= 0)
            OnDeath();
    }
}
