using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public ObjectiveDatabase objDb;

    /**
    * These are the objectives that will be 'searched' for their conditions 
    * Cached in a list so we wont have to get them from the ObjectivesDatabase each frame
    * Each time that an objective will be marked as active, it will be added in this list
    */
    public List<Objective> activeObjectives = new List<Objective>();

    private void Update()
    {
        // check for all the active objectives
        foreach(var obj in activeObjectives)
        {
            if (obj.CheckPrerequisites() == true)
            {
                obj.CompleteObjective();
            }
        }
    }
}
