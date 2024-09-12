using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    /// This class will manage all the keys in the level and manages spawning them in the rooms and assigning them to unlockable objects.
    /// </summary>
    /// <remarks>
    /// Author: Chase Bennett-Hill
    /// Date: 29 / 08 / 2024
    /// </remarks>

public class KeysManager : MonoBehaviour
{
    /// <summary>
    /// The parent object that holds all the rooms in the level.
    /// </summary>
    [SerializeField] private GameObject roomParent;

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

        if (!CheckEnoughRooms())
        {
            Debug.LogError("Not enough rooms to place keys.");
            return;
        }
       RandomizeKeys();
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
    /// Checks if there are enough rooms to place the keys. so that one key is placed in one room.
    /// </summary>
    /// <returns>True If there are more rooms than keys</returns>
    private bool CheckEnoughRooms()
    {
      return roomParent.transform.childCount >= keys.Count;
    }

    /// <summary>
    /// Iterates through all the keys and places them in a random room.
    /// </summary>
    private void RandomizeKeys()
    {

        foreach (Key key in keys)
        {
            key.gameObject.transform.position = GetRandomRoom().transform.position;
        }
    }

    /// <summary>
    /// Returns a random room from the room parent.
    /// </summary>
    /// <returns>A random child of the rooms parent</returns>
    private GameObject GetRandomRoom()
    {
        return roomParent.transform.GetChild(Random.Range(0, roomParent.transform.childCount)).gameObject;
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
