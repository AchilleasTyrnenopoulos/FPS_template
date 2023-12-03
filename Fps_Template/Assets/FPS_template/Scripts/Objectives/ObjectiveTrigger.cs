using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this will mark objectives as active so they can be 'searched' in the objectives database. k
public class ObjectiveTrigger : MonoBehaviour
{
    public ObjectivesManager ObjManager { get; private set; }
    public string ObjectiveId = string.Empty;

    private void Awake()
    {
        ObjManager = FindObjectOfType<ObjectivesManager>();
    }

    public virtual void SetObjectiveAsActive(Objective objective)
    {
        objective.IsActive = true;
    }

    public virtual void SetObjectiveAsActive()
    {
        var obj = ObjManager.objDb.objectives.Where(o => o.Id == ObjectiveId).SingleOrDefault();
        obj.IsActive = true;
    }
}
