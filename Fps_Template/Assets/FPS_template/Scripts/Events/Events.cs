using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Initialization events
public class OnPlayerUnitSpawn : PubSubEvent<Transform> { }
#endregion

#region Interaction events
// Event for when the player interacts with an interactable
public class InteractEvent : PubSubEvent<string> { }

// Event for when the player starts interacting with an interactable
public class CanInteractEvent : PubSubEvent<Interactable> { }

// Event for when the player stops interacting with an interactable
public class CannotInteractEvent : PubSubEvent<string> { }
#endregion

#region Pause Menu events
public class PauseStartEvent : PubSubEvent { }
public class PauseEndEvent : PubSubEvent { }
#endregion

#region Inventory events
public class EquipWeaponEvent : PubSubEvent<int> { }
public class SetSecondaryWeapon : PubSubEvent<WeaponBase> { }
#endregion

#region Scene management
public class OnSceneChangeEvent : PubSubEvent<string> { }
public class OnSceneChangeStart_SetSpawnpointEvent : PubSubEvent<int> { }
public class OnSceneChangeStartEvent : PubSubEvent<string> { }
public class OnMainMenuSceneStart : PubSubEvent { }
#endregion
