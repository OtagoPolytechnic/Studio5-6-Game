using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    /// This class will manage all the keys in the level and manages assigning them to unlockable objects.
    /// </summary>
    /// <remarks>
    /// Author: Chase Bennett-Hill
    /// Date: 29 / 08 / 2024
    /// </remarks>

public class KeysManager : MonoBehaviour
{

    /// <summary>
    /// The instance of the KeysManager class that can be referenced in other classes.
    /// </summary>
    public static KeysManager Instance;

    /// <summary>
    /// The list of keys in the level.
    /// </summary>
   private List<Key> keys = new List<Key>();
   
   /// <summary>
   ///  The list of unlockable objects in the level.
   /// </summary>
    private List<UnlockableObject> unlockableObjects = new List<UnlockableObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        foreach (Key key in keys)
        {
            if (!VerifyUniqueUnlockable(key))
            {
                Debug.LogError("The Key" + key.gameObject.name + " is assigned to more than one unlockable object.");
                continue;
            }
        }

    //     if (!CheckEnoughRooms())
    //     {
    //         Debug.LogError("Not enough rooms to place keys.");
    //         return;
    //     }
    //    RandomizeKeys();
    }


    /// <summary>
    /// Verifies that a key is only assigned to one unlockable object.
    /// </summary>
    /// <param name="key">The Key to check</param>
    /// <returns>True if the key is unique to one object</returns>
    public bool VerifyUniqueUnlockable(Key key)
    {
        int count = 0;
        foreach (UnlockableObject unlockableObject in unlockableObjects)
        {
            if (unlockableObject.requiredKeys.Contains(key))
            {
                count++;
            }
        }
        if (count > 1)
        {
            return false;
        }
        return true;
    }



    /// <summary>
    /// Adds an unlockable object to the list of unlockable objects.
    /// </summary>
    /// <param name="unlockableObject">The unlockable object to add</param>
    public void AddUnlockableObject(UnlockableObject unlockableObject)
    {
        if (unlockableObjects.Contains(unlockableObject))
        {
            Debug.LogError("Unlockable object already exists.");
            return;
        }
        unlockableObjects.Add(unlockableObject);
    }
     


    /// <summary>
    /// Adds a key to the list of keys.
    /// </summary>
    /// <param name="key">The key to add</param>
    public void AddKey(Key key)
    {
        if (keys.Contains(key))
        {
            Debug.LogError("Key already exists.");
            return;
        }
        keys.Add(key);
    }


}
