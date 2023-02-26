using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event for when the player interacts with an interactable
public class InteractEvent : PubSubEvent<Interactable> { }

// Event for when the player starts interacting with an interactable
public class CanInteractEvent : PubSubEvent<Interactable> { }

// Event for when the player stops interacting with an interactable
public class CannotInteractEvent : PubSubEvent { }
