using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptMsg;
    [SerializeField] private TextMeshProUGUI _interactableMsgText;
    [SerializeField] private GameObject _interactionUI_GO;

    private void OnEnable()
    {
        EventAggregator.GetEvent<CanInteractEvent>().Subscribe(EnableObject);
        EventAggregator.GetEvent<CanInteractEvent>().Subscribe(UpdateText);
        EventAggregator.GetEvent<CannotInteractEvent>().Subscribe(ResetText);
        EventAggregator.GetEvent<CannotInteractEvent>().Subscribe(DisableObject);
        EventAggregator.GetEvent<InteractEvent>().Subscribe(ResetText);
        EventAggregator.GetEvent<InteractEvent>().Subscribe(DisableObject);
    }

    private void EnableObject(Interactable interactable)
    {
        _interactionUI_GO.SetActive(true);
    }

    private void DisableObject()
    {
        _interactionUI_GO.SetActive(false);
    }

    private void DisableObject(Interactable interactable)
    {
        _interactionUI_GO.SetActive(false);
    }

    private void OnDisable()
    {
        EventAggregator.GetEvent<CanInteractEvent>().UnSubscribe(EnableObject);
        EventAggregator.GetEvent<CanInteractEvent>().UnSubscribe(UpdateText);
        EventAggregator.GetEvent<CannotInteractEvent>().UnSubscribe(ResetText);
        EventAggregator.GetEvent<CannotInteractEvent>().UnSubscribe(DisableObject);
        EventAggregator.GetEvent<InteractEvent>().UnSubscribe(ResetText);
        EventAggregator.GetEvent<InteractEvent>().Subscribe(DisableObject);
    }


    private void UpdateText(Interactable interactable)
    {
        if (interactable != null)
        {
            _promptMsg.text = "Press ";
            _interactableMsgText.text = interactable.GetPromptMessage();
        }
    }

    private void ResetText()
    {
        Debug.Log("InteractionUI - ResetText");
        _promptMsg.text = string.Empty;
        _interactableMsgText.text = string.Empty;
    }

    private void ResetText(Interactable interactable)
    {
        _promptMsg.text = string.Empty;
        _interactableMsgText.text = string.Empty;
    }
}
