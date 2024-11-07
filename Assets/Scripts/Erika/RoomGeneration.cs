/// <remarks>
/// Author: Erika Stuart
/// Date Created: 29/07/2024
/// Date Modified: 9/08/2024
/// </remarks>

/// <summary>
/// This script randomly generated an amount of rooms from prefabs
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] roomPrefabs; // A list of the rooms that will be pulled from the prefabs
    private List<GameObject> currentRooms = new List<GameObject>(); // A list of the generated rooms so none overlap
    private List<Vector2> placedRoomPos = new List<Vector2>();

    // Constants
    private const int ROOM_SIZE_AMOUNT = 25;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom(); // At the start, generate the rooms
    }

    /// <summary>
    /// Chooses which prefab to use
    /// </summary>
    private GameObject ChooseRoom()
    {
        return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    }

    /// <summary>
    /// Places the rooms in the scene
    /// </summary>
    /// <param name="roomPrefab"></param>
    private void PlaceRoom(GameObject roomPrefab)
    {
        Vector2 newPos;

        if (currentRooms.Count == 0) // If there are no rooms in the scene, place one at 0,0
        {
            newPos = Vector2.zero;
        }
        else // The rooms following will be checked that they are adjacent to the previous room
        {
            Vector2 lastRoomPos = placedRoomPos[placedRoomPos.Count - 1]; // Check the position of the last room placed
            newPos = CheckNewPosition(lastRoomPos); // Check the new position
        }

        Instantiate(roomPrefab, newPos, Quaternion.identity); // Instantiate the room at the new position
        currentRooms.Add(roomPrefab); // Add it to the placed rooms list to be used for checking for future rooms
        placedRoomPos.Add(newPos); // Add the position to the list of placed room positions
    }

    /// <summary>
    /// This method is for checking the new position of the room and placing it adjacent to the previous room
    /// </summary>
    /// <param name="lastRoomPos"></param>
    private Vector2 CheckNewPosition(Vector2 lastRoomPos)
    {
        Vector2 newPos = lastRoomPos;

        while (true)
        {
            switch(Random.Range(1,5)) // 1 - 4 for left, above, right, below
            {
                case 1:
                    newPos = new Vector2(lastRoomPos.x - ROOM_SIZE_AMOUNT, lastRoomPos.y); // left
                    break;
                case 2:
                    newPos = new Vector2(lastRoomPos.x, lastRoomPos.y + ROOM_SIZE_AMOUNT); // above
                    break;
                case 3:
                    newPos = new Vector2(lastRoomPos.x + ROOM_SIZE_AMOUNT, lastRoomPos.y); // right
                    break;
                case 4:
                    newPos = new Vector2(lastRoomPos.x, lastRoomPos.y - ROOM_SIZE_AMOUNT); // below
                    break;
            
            }

            if (!CheckPosition(newPos)) // If the position is not already taken, return the new position
            {
                return newPos;
            }
            else // If the position is taken, try again
            {
                lastRoomPos = newPos;
            }
        }
    }

    private bool CheckPosition(Vector2 pos) //gets called in place room
    {
        foreach (Vector2 placedPos in placedRoomPos)
        {
            if (placedPos == pos)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Generates each room
    /// </summary>
    private void GenerateRoom()
    {
        for (int i = 0; i < ROOM_SIZE_AMOUNT; i++)
        {
            GameObject room = ChooseRoom(); // Picks a random prefab
            PlaceRoom(room); // Sends it to PlaceRoom for the position
        }
    }
}
