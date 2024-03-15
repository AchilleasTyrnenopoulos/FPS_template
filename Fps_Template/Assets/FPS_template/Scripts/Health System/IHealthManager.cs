using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthManager
{
    public HealthInfo HealthInfo { get; }
    public float InvulnerabilityTime { get; }
    void TakeDamage(DamageInfo damage);
    void OnDeath();
}
