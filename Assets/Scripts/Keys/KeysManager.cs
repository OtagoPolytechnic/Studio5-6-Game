using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    /// <summary>
    /// This will manage all the keys in the level and manages spawning them in the rooms and assigning them to unlockable objects.
    /// </summary>
    [SerializeField] private GameObject roomParent;

    public static KeysManager Instance;

   private List<Key> keys = new List<Key>();
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

        if (!CheckEnoughRooms())
        {
            Debug.LogError("Not enough rooms to place keys.");
            return;
        }
       RandomizeKeys();
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
