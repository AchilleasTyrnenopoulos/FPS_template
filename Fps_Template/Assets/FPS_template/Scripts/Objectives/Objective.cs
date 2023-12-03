using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public string Id;
    public string Description;
    public ObjectiveType Type;
    // all the prerequisites needed for the objective to be complete eg. a) collect 3 items and b) kill enemy 
    public List<ObjectivePrerequisite> Prerequisites;
    public bool IsActive;
    public bool IsCompleted;
    // list of events that will be triggered when the Objective gets completed
    //public List<ObjectiveCompleteEvent> CompletedEvents = new List<ObjectiveCompleteEvent>();

    public bool CheckPrerequisites()
    {
        if (Prerequisites == null || Prerequisites.Count > 0) return false;

        Debug.Log("Checking prerequisites of objective: " + this.Id);

        // check that all the conditions for all the prerequisites are met
        if(Prerequisites.Where(pre => pre.AreAllConditionsMet() == true).Count() == Prerequisites.Count)
        {
            return true;
        }

        return false;
    }

    public void CompleteObjective()
    {
        Debug.Log("CompleteObjective with Id: " + this.Id);

        IsActive = false;
        IsCompleted = true;

        // trigger all the appropriate events
        // ...
    }
}
