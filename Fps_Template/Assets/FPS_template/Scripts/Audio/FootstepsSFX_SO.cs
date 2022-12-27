using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Footsteps", menuName = "ScriptableObjects/Audio/Footsteps")]
public class FootstepsSFX_SO : ScriptableObject
{
    [SerializeField] List<AudioClip> _woodFootsteps;
    [SerializeField] List<AudioClip> _dirtFootsteps;
    [SerializeField] List<AudioClip> _grassFootsteps;

    public List<AudioClip> GetFootstepsGroup(string materialName)
    {
        switch (materialName.ToLower())
        {
            case "wood":
                return _woodFootsteps;
            case "dirt":
                return _dirtFootsteps;
            case "grass":
                return _grassFootsteps;
            default:
                return null;                
        }
    }
}
