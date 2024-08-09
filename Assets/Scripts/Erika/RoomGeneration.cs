using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Picks a random room
* If no rooms are placed, place the room at 0,0
* Add room to list
* 
*/

public class RoomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] roomPrefabs; // A list of the rooms that will be pulled from the prefabs
    private List<GameObject> currentRooms = new List<GameObject>(); // A list of the generated rooms so none overlap
    private List<Vector2> placedRoomPos = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom();
    }

    private GameObject ChooseRoom()
    {
        Debug.Log("Choosing Room");
        return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    }

    private void PlaceRoom(GameObject roomPrefab)
    {
        Vector2 newPos;

        if (currentRooms.Count == 0)
        {
            Debug.Log("Placing Room at 0,0");
            newPos = Vector2.zero;
        }
        else
        {
            Debug.Log("Placing Room at Random Position");
            Vector2 lastRoomPos = placedRoomPos[placedRoomPos.Count - 1];
            newPos = CheckNewPosition(lastRoomPos);
        }

        Instantiate(roomPrefab, newPos, Quaternion.identity);
        currentRooms.Add(roomPrefab);
        placedRoomPos.Add(newPos);
    }

    private Vector2 CheckNewPosition(Vector2 lastRoomPos)
    {
        Vector2 newPos = lastRoomPos;

        while (true)
        {
            switch(Random.Range(1,5))
            {
                case 1:
                    newPos = new Vector2(lastRoomPos.x - 25, lastRoomPos.y); // left
                    break;
                case 2:
                    newPos = new Vector2(lastRoomPos.x, lastRoomPos.y + 25); // above
                    break;
                case 3:
                    newPos = new Vector2(lastRoomPos.x + 25, lastRoomPos.y); // right
                    break;
                case 4:
                    newPos = new Vector2(lastRoomPos.x, lastRoomPos.y - 25); // below
                    break;
            
            }

            if (!CheckPosition(newPos))
            {
                return newPos;
            }
            else
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

    private void GenerateRoom()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("Generating Room " + i);
            GameObject room = ChooseRoom();
            PlaceRoom(room);
        }
    }
}
