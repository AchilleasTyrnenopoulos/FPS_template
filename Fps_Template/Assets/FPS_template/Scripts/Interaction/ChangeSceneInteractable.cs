using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneChanger))]
public class ChangeSceneInteractable : Interactable
{
    [SerializeField] private float _delay;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneChanger = GetComponent<SceneChanger>();
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        EventAggregator.GetEvent<OnSceneChangeEvent>().Subscribe(LoadScene);
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        EventAggregator.GetEvent<OnSceneChangeEvent>().UnSubscribe(LoadScene);
    }

    protected override void Interact(string interactableId)
    {
        Debug.Log("ChangeSceneInteractable - Interact - STARTED");
        if (interactableId != this._id)
        {
            Debug.Log($"Interactable {interactableId} is not this one {this._id}");
            return;
        }

        // TODO play sfx
        // ...

        //_sceneChanger.LoadNewScene();

        Debug.Log("ChangeSceneInteractable - Interact - Triggered. Id: " + this._id);
        EventAggregator.GetEvent<OnSceneChangeStartEvent>().Publish(this._id);
        //TODO set spawnpoint of next scene
        // ..

        // reset interaction prompt
        EventAggregator.GetEvent<CannotInteractEvent>().Publish(this._id);
               
        _canInteract = false;        
    }

    private void LoadScene(string interactableId)
    {
        if (interactableId != this._id)
            return;
        
        Debug.Log("ChangeSceneInteractable - LoadScene called");
        _sceneChanger.LoadNewScene();
    }
}
