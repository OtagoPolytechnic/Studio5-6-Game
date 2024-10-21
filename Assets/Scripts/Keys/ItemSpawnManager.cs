using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    /// This class manages the spawning of items in the level and randomly assigns them to rooms.
    /// </summary>
    /// <remarks>
    /// Author: Chase Bennett-Hill
    /// Date: 09 / 09 / 2024
    /// </remarks>

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject roomParent; 
    private List<GameObject> emptyRooms = new List<GameObject>();

    private List<GameObject> items = new List<GameObject>();
    public static ItemSpawnManager Instance;

    private void Awake()
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

    private void Start()
    {
        foreach (Transform room in roomParent.transform)
        {
            emptyRooms.Add(room.gameObject);
        }

        if (!CheckEnoughRooms())
        {
            Debug.LogError("Not enough rooms to place items.");
            return;
        }
       RandomizeItems();

    }



    /// <summary>
    /// Checks if there are enough rooms to place the items. so that one item is placed in one room.
    /// </summary>
    /// <returns>True If there are more rooms than spawned items</returns>
    private bool CheckEnoughRooms()
    {
      return emptyRooms.Count >= items.Count;
    }

    
    /// <summary>
    /// Gets random room from the empty room list and then removes the gameobject from the list.
    /// </summary>
    /// <returns>room GameObject</returns>
    private GameObject GetRandomRoom()
    {
        GameObject selectedRoom = emptyRooms[Random.Range(0, emptyRooms.Count)];
        emptyRooms.Remove(selectedRoom);
        return selectedRoom;
    }


    /// <summary>
    /// Adds an item to the list of Items.
    /// </summary>
    /// <param name="key">The item Gameobject to add</param>
    public void AddItem(GameObject item)
    {
        items.Add(item);
    }


      /// <summary>
    /// Iterates through all the items and places them in a random room.
    /// </summary>
    private void RandomizeItems()
    {
        foreach (GameObject item in items)
        {
           item.transform.position = GetRandomRoom().transform.position;
        }
    }
}