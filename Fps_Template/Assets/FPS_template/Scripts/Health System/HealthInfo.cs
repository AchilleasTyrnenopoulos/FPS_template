using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthInfo
{
    public int MaxHealth = 100;
    public int CurrentHealth = 0;
    public int TemporalHealth = 20;

    public HealthInfo()
    {
        CurrentHealth = MaxHealth;
    }
}
