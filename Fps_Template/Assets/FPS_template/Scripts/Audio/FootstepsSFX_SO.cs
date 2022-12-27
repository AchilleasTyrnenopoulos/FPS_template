using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Footsteps", menuName = "ScriptableObjects/Audio/Footsteps")]
public class FootstepsSFX_SO : ScriptableObject
{
    [Header("Footsteps")]
    [SerializeField] private List<AudioClip> _woodFootsteps;
    [SerializeField] private List<AudioClip> _dirtFootsteps;
    [SerializeField] private List<AudioClip> _grassFootsteps;

    [Header("Landing")]
    [SerializeField] private AudioClip _woodLanding;
    [SerializeField] private AudioClip _dirtLanding;
    [SerializeField] private AudioClip _grassLanding;

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

    public AudioClip GetLandingSfx(string materialName)
    {
        switch (materialName.ToLower())
        {
            case "wood":
                return _woodLanding;
            case "dirt":
                return _dirtLanding;
            case "grass":
                return _grassLanding;
            default:
                return null;
        }
    }
}
