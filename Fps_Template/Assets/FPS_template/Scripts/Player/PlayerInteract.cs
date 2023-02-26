using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private float _distance = 3f;
    [SerializeField]
    private LayerMask _layerMask;
    private Interactable _interactable;
    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //cast the ray
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _distance);        

        RaycastHit hitInfo;
        
        //check if ray collides with something
        if(Physics.Raycast(ray, out hitInfo, _distance, _layerMask))
        {            
            //if object has interactable component
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //cache the new interactable
                Interactable newInteractable = hitInfo.collider.GetComponent<Interactable>();
                //if player is not looking at another interactable and new interactable can be interacted with
                if (_interactable == null && newInteractable.GetCanInteract())
                {
                    //set the interactable and prompt message to player
                    _interactable = newInteractable;
                    EventAggregator.GetEvent<CanInteractEvent>().Publish(_interactable);
                }

                //if player currently looks at an interactable and the interact button is pressed 
                if(_interactable != null && _controller.GetInteractTriggered()) 
                {
                    //trigger the interaction event and set _interactable to null
                    EventAggregator.GetEvent<InteractEvent>().Publish(_interactable);
                    ResetInteractable();
                }
            }
        }
        else
        {
            //if the player stops looking at an interactable trigger the event
            //to stop showing the prompt message
            if (_interactable != null)
            {
                ResetInteractable();
                EventAggregator.GetEvent<CannotInteractEvent>().Publish();
            }
        }

    }

    private void ResetInteractable()
    {
        _interactable = null;
    }    
}
