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

    private bool _playedSound = false;
    private float _playDelay = .2f;
    private float _playDelayCounter = 0f;

    private const string DEFAULT_MATERIAL = "default-material";
    // Start is called before the first frame update
    void Start()
    {
        GetGroundMaterial();
        SetCurrentFootstepsSfxGroup();

    }

    private void Update()
    {
        if (_playedSound)
        {
            _playDelayCounter += Time.deltaTime;
            if (_playDelayCounter >= _playDelay)
            {
                _playDelayCounter = 0f;
                _playedSound = false;
            }

        }

        GetGroundMaterial();
    }

    private void GetGroundMaterial()
    {
        if (Physics.Raycast(_footstepsTrans.position, -Vector3.up, out RaycastHit hit, _groundLayers))
        {
            string previousGroundMaterial = _currentGroundMaterial;

            // if the player is currently on a mesh
            if (hit.collider.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                Material mat = meshRenderer.material;
                //Debug.Log("Ground material: " + mat.name.Replace(" (Instance)", "").ToLower());
                _currentGroundMaterial = mat.name.Replace(" (Instance)", "").ToLower();

            }
            else if (hit.collider.gameObject.TryGetComponent(out Terrain terrain)) // else if the player is currently on a terrain
            {

                // solution by Natty Creations
                // https://www.youtube.com/watch?v=wXcjxeetg70&ab_channel=NattyCreations
                // ---------------------------------------------------------------------

                Vector3 tPos = Terrain.activeTerrain.transform.position;
                TerrainData terrainData = Terrain.activeTerrain.terrainData;
                int mapX = Mathf.RoundToInt((this.transform.position.x - tPos.x) / terrainData.size.x * terrainData.alphamapWidth);
                int mapZ = Mathf.RoundToInt((this.transform.position.z - tPos.z) / terrainData.size.z * terrainData.alphamapHeight);
                float[,,] splatMapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

                float[] cellMix = new float[splatMapData.GetUpperBound(2) + 1];
                for (int i = 0; i < cellMix.Length; i++)
                {
                    cellMix[i] = splatMapData[0, 0, i];
                }

                float strongest = 0;
                int maxIndex = 0;

                for (int i = 0; i < cellMix.Length; i++)
                {
                    if (cellMix[i] > strongest)
                    {
                        maxIndex = i;
                        strongest = cellMix[i];
                    }
                }

                _currentGroundMaterial = terrain.terrainData.terrainLayers[maxIndex].name;

                // ---------------------------------------------------------------------

            }

            //Debug.Log(_currentGroundMaterial);

            // check if material has changed
            if (previousGroundMaterial != _currentGroundMaterial)
            {
                _footstepsAudioSource.Stop();
                SetCurrentFootstepsSfxGroup();
            }

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
            //Debug.Log("Set new footsteps sfx group");

#if UNITY_EDITOR
            if (_currentFoostepsGroup == null)
            {
                Debug.LogError($"{nameof(FootstepsManager)} - {nameof(SetCurrentFootstepsSfxGroup)} \nMaterial name does not match a footsteps sfx group. Material: " + _currentGroundMaterial);
            }
#endif
        }
    }

    public void PlayFootstepSfx()
    {
        if (_playedSound) return;

        if (_currentFoostepsGroup == null || _currentFoostepsGroup.Count <= 0)
        {
            Debug.Log($"{_currentFoostepsGroup} is null or its count is zero");
            return;
        }

        //available footsteps
        List<AudioClip> availFootsteps = new List<AudioClip>(_currentFoostepsGroup);
        if (availFootsteps.Contains(_lastPlayedFootstepSfx))
            availFootsteps.Remove(_lastPlayedFootstepSfx);

        //get random index
        int rnd = UnityEngine.Random.Range(0, availFootsteps.Count);
        AudioClip sfx = availFootsteps[rnd];

        //play foostep sfx
        _footstepsAudioSource.PlayOneShot(sfx);
        Debug.Log("Player footstep SFX");
        _playedSound = true;

        //store last playedFootstepSfx
        _lastPlayedFootstepSfx = sfx;
    }

    public void PlayLandingSfx()
    {
        if (_playedSound) return;

        AudioClip sfx = _footstepsSfxGroups.GetLandingSfx(_currentGroundMaterial);

        if (sfx != null)
        {
            _footstepsAudioSource.PlayOneShot(sfx);
            _playedSound = true;
        }
    }
}
