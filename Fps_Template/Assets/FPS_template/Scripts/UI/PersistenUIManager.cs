using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentUIManager : MonoBehaviour
{
    public static PersistentUIManager Instance { get; private set; }

    [Header("Curtain")]
    [SerializeField] private GameObject _curtainGO;
    [SerializeField] private Animator _curtainAnim;
    private string _changeSceneInteractableId;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("PersistenUIManager - Two instances in the scene!");
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);

        _curtainAnim = _curtainGO.GetComponentInParent<Animator>();
    }

    private void Start()
    {
        _curtainGO.SetActive(true);
    }

    private void OnEnable()
    {
        EventAggregator.GetEvent<OnSceneChangeStartEvent>().Subscribe(StartFadeOut);
        EventAggregator.GetEvent<OnPlayerUnitSpawn>().Subscribe(StartFadeIn);
        EventAggregator.GetEvent<OnMainMenuSceneStart>().Subscribe(StartFadeIn);
    }

    private void OnDisable()
    {
        EventAggregator.GetEvent<OnSceneChangeStartEvent>().UnSubscribe(StartFadeOut);
        EventAggregator.GetEvent<OnPlayerUnitSpawn>().UnSubscribe(StartFadeIn);        
        EventAggregator.GetEvent<OnMainMenuSceneStart>().UnSubscribe(StartFadeIn);
    }

    private void StartFadeIn(Transform playerTrans)//TODO see how to handle this
    {
        _curtainAnim.SetBool("IsCurtainOpen", true);
    }

    private void StartFadeIn()//TODO see how to handle this
    {
        _curtainAnim.SetBool("IsCurtainOpen", true);
    }

    private void StartFadeOut(string interactableId)
    {
        Debug.Log("Starting fade out ...");
        _changeSceneInteractableId = interactableId;

        _curtainAnim.SetBool("IsCurtainOpen", false);
    }

    public void FadeOutEnded()
    {
        Debug.Log("Fade out ended");
        EventAggregator.GetEvent<OnSceneChangeEvent>().Publish(_changeSceneInteractableId);
    }

}
