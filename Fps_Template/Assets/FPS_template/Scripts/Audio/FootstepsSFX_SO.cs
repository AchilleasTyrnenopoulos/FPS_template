using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Footsteps", menuName = "ScriptableObjects/Audio/Footsteps")]
public class FootstepsSFX_SO : ScriptableObject
{
    [Header("Footsteps")]
    [SerializeField] private List<AudioClip> _woodFootsteps;
    [SerializeField] private List<AudioClip> _gravelFootsteps;
    [SerializeField] private List<AudioClip> _grassFootsteps;

    [Header("Landing")]
    [SerializeField] private AudioClip _woodLanding;
    [SerializeField] private AudioClip _gravelLanding;
    [SerializeField] private AudioClip _grassLanding;

    public List<AudioClip> GetFootstepsGroup(string materialName)
    {
        switch (materialName.ToLower())
        {
            case "wood":
                return _woodFootsteps;
            case "gravel":
                return _gravelFootsteps;
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
            case "gravel":
                return _gravelLanding;
            case "grass":
                return _grassLanding;
            default:
                return null;
        }
    }
}
