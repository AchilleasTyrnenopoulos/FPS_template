using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthManager
{
    [SerializeField] private bool _tookDamage = false;

    //[SerializeField] private bool _hasTemporalHealth = false;
    //[SerializeField] private int _currentTemporalHealth = 20;
    //[SerializeField] private bool _isRemoveTempHealthTriggered = false;

    public HealthInfo HealthInfo => _healthInfo;
    [SerializeField] private HealthInfo _healthInfo = new();

    public float InvulnerabilityTime => _invulnerabilityCounter;
    [SerializeField] private float _invulnerabilityTime = 0.1f;
    private float _invulnerabilityCounter = 0f;

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
        Debug.Log("Player died");

        // trigger OnPlayerDeathEvent
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
