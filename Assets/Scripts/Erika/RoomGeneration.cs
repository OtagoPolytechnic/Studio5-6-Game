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

    private void PlaceRoom(GameObject roomPrefab)
    {
        if (currentRooms.Count == 0)
        {
            Debug.Log("Placing Room at 0,0");
            Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
            currentRooms.Add(roomPrefab);
        }
        else
        {
            Debug.Log("Placing Room at Random Position");
            switch(Random.Range(1, 5)) // 1 left, 2, up, 3 right, 4 down
            {
                case 1:
                    // check the room position 25 units left, if any room in the currentRoom list has that vector2, while check room is false, keep checking and increase the x value by 25
                    if (CheckPosition(new Vector2(-25, 0)))
                    {
                        Instantiate(roomPrefab, new Vector3(-25, 0, 0), Quaternion.identity);
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

    }

    private bool CheckPosition(Vector2 pos) //gets called in place room
    {
        for (int i = 0; i < currentRooms.Count; i++)
        {
            if (currentRooms[i].transform.position == pos)
            {
                return false;
            }
            else
            {
                return true;
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
