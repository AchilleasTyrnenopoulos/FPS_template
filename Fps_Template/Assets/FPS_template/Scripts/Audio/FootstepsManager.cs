using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private Transform _footstepsTrans;
    [SerializeField] private AudioSource _footstepsAudioSource;
    [SerializeField] private FootstepsSFX_SO _footstepsSfxGroups;
    [SerializeField] private List<AudioClip> _currentFoostepsGroup;
    //[SerializeField] private float _footstepsFrequency = .3f;
    //public float footstepDistanceCounter = 0f;
    [SerializeField] private string _currentGroundMaterial = "";
    [SerializeField] private AudioClip _lastPlayedFootstepSfx;
    // Start is called before the first frame update
    void Start()
    {
        GetGroundMaterial();
        SetCurrentFootstepsSfxGroup();
    }

    private void FixedUpdate()
    {
        GetGroundMaterial(); //maybe do it in update
    }    

    private void GetGroundMaterial()
    {
        if (Physics.Raycast(_footstepsTrans.position, -Vector3.up, out RaycastHit hit, _groundLayers))
        {
            Material mat = hit.collider.gameObject.GetComponent<MeshRenderer>().material;
            string previousGroundMaterial = _currentGroundMaterial;
            _currentGroundMaterial = mat.name.Replace(" (Instance)", "").ToLower();

            //check if material has changed
            if (previousGroundMaterial != _currentGroundMaterial)
            {
                _footstepsAudioSource.Stop();
                SetCurrentFootstepsSfxGroup();
            }

            Debug.Log(_currentGroundMaterial);
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }
    }

    //trigger when material has changed
    private void SetCurrentFootstepsSfxGroup()
    {
        //depending on the _currentGroundMaterial
        //set the footsteps sfx array                        
        if (!string.IsNullOrWhiteSpace(_currentGroundMaterial))
        {
            _currentFoostepsGroup = _footstepsSfxGroups?.GetFootstepsGroup(_currentGroundMaterial);
            Debug.Log("Set new footsteps sfx group");

#if UNITY_EDITOR
            if(_currentFoostepsGroup == null)
            {
                Debug.LogError($"{nameof(FootstepsManager)} - {nameof(SetCurrentFootstepsSfxGroup)} \nMaterial name does not match a footsteps sfx group");
            }
#endif
        }
    }

    public void PlayFootstepSfx()
    {
        //available footsteps
        List<AudioClip> availFootsteps = new List<AudioClip>(_currentFoostepsGroup);
        if (availFootsteps.Contains(_lastPlayedFootstepSfx))
            availFootsteps.Remove(_lastPlayedFootstepSfx);

        //get random index
        int rnd = UnityEngine.Random.Range(0, availFootsteps.Count);
        AudioClip sfx = availFootsteps[rnd];

        //play foostep sfx
        _footstepsAudioSource.PlayOneShot(sfx);

        //store last playedFootstepSfx
        _lastPlayedFootstepSfx = sfx;
    }

    public void PlayLandingSfx()
    {
        AudioClip sfx = _footstepsSfxGroups.GetLandingSfx(_currentGroundMaterial);

        _footstepsAudioSource.PlayOneShot(sfx);
    }
}
