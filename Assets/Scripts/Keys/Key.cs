using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class manages a single key item, the key is used to unlock an UnlockableObject such as a door.
/// </summary>
/// <remarks>
/// Author: Chase Bennett-Hill
/// Date: 29 / 08 / 2024
/// </remarks>
public class Key : MonoBehaviour
{
    /// <summary>
    /// The UnlockableObject that this key unlocks.
    /// </summary>
    [HideInInspector] public UnlockableObject targetObject; 

    void Awake()
    {
        KeysManager.Instance.AddKey(this); // Adds this key to the KeysManager list of keys.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (targetObject == null) //If the key doesn't have a target it will not be able to unlock anything.
            {
                Debug.LogError("Key does not have a target object.");
                return;
            }
            targetObject.UseKey(this); // Uses the key on the target object.
        }
    }

}
