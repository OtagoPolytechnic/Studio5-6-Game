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
    //private List<Vector3> placedRoomPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject ChooseRoom()
    {
        Debug.Log("Choosing Room");
        return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    }

    private void PlaceRoom(GameObject room)
    {
        if (currentRooms.Count == 0)
        {
            Debug.Log("Placing Room at 0,0");
            room.transform.position = Vector3.zero;
            currentRooms.Add(room);
        }
        else
        {
            Debug.Log("Placing Room at Random Position");
            switch(Random.Range(1, 5)) // 1 left, 2, up, 3 right, 4 down
            {
                case 1:
                    room.transform.position = new Vector3(currentRooms[currentRooms.Count - 1].transform.position.x - 10, currentRooms[currentRooms.Count - 1].transform.position.y, 0); // the x position of the last room placed - 10
                    CheckPosition(room);
                    break;
                case 2:
                    room.transform.position = new Vector3(currentRooms[currentRooms.Count - 1].transform.position.x, currentRooms[currentRooms.Count - 1].transform.position.y + 10, 0); // the y position of the last room placed + 10
                    CheckPosition(room);
                    break;
                case 3:
                    room.transform.position = new Vector3(currentRooms[currentRooms.Count - 1].transform.position.x + 10, currentRooms[currentRooms.Count - 1].transform.position.y, 0); // the x position of the last room placed + 10
                    CheckPosition(room);
                    break;
                case 4:
                    room.transform.position = new Vector3(currentRooms[currentRooms.Count - 1].transform.position.x, currentRooms[currentRooms.Count - 1].transform.position.y - 10, 0); // the y position of the last room placed - 10
                    CheckPosition(room);
                    break;
            }
        }

    }

    private void CheckPosition(GameObject room) //gets called in place room
    {
        for (int i = 0; i < currentRooms.Count; i++) // check through the list of currently placed rooms
        {
            // if none of the current rooms overlap with the next room, place the room
            if (room.transform.position != currentRooms[i].transform.position)
            {
                room.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                currentRooms.Add(room);
            }
        }
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
