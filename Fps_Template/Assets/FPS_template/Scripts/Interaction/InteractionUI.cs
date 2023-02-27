using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;    
    
    private void OnEnable()
    {
        EventAggregator.GetEvent<CanInteractEvent>().Subscribe(UpdateText);
        EventAggregator.GetEvent<CannotInteractEvent>().Subscribe(ResetText);
        EventAggregator.GetEvent<InteractEvent>().Subscribe(ResetText);
    }

    private void OnDisable()
    {
        EventAggregator.GetEvent<CanInteractEvent>().Unsubscribe(UpdateText);
        EventAggregator.GetEvent<CannotInteractEvent>().Unsubscribe(ResetText);
        EventAggregator.GetEvent<InteractEvent>().Unsubscribe(ResetText);
    }


    private void UpdateText(Interactable interactable)
    {
        if (interactable != null)
            _text.text = "Press E " + interactable.GetPromptMessage();
    }

    private void ResetText()
    {
        _text.text = string.Empty;
    }

    private void ResetText(Interactable interactable)
    {
        _text.text = string.Empty;
    }
}
