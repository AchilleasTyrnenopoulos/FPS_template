using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Interaction events
// Event for when the player interacts with an interactable
public class InteractEvent : PubSubEvent<Interactable> { }

// Event for when the player starts interacting with an interactable
public class CanInteractEvent : PubSubEvent<Interactable> { }

// Event for when the player stops interacting with an interactable
public class CannotInteractEvent : PubSubEvent { }
#endregion

#region Pause Menu events
public class PauseStartEvent : PubSubEvent { }
public class PauseEndEvent : PubSubEvent { }
#endregion

#region Inventory events
public class EquipWeaponEvent : PubSubEvent<int> { }
#endregion
