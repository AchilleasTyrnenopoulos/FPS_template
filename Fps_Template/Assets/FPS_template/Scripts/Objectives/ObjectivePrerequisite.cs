using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ObjectivePrerequisite
{
    public List<Condition> Conditions;
    public bool AreAllConditionsMet()
    {
        if (Conditions.Where(c => c.IsConditionTrue() == true).Count() == Conditions.Count)
            return true;

        return false;
    }
}
