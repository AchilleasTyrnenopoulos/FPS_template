using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjectiveDatabase", menuName = "Objectives System/Objective Database")]
public class ObjectiveDatabase : ScriptableObject
{
    // list of objectives
    public List<Objective> objectives = new List<Objective>();
}
