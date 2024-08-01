using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Randomly choose room from array
* place either 50 units above, below, or left or right from the last room
* cannot overlap if a room is already there
* 
*/

public class RoomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;

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
        return rooms[Random.Range(0, rooms.Length)];
    }

    private void PlaceRoom(GameObject room)
    {
        Vector3 position = Vector3.zero;
        if (transform.childCount == 0)
        {
            position = Vector3.zero;
        }
        else
        {
            Transform lastRoom = transform.GetChild(transform.childCount - 1);
            position = lastRoom.position;
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0:
                    position += new Vector3(0, 25, 0);
                    break;
                case 1:
                    position += new Vector3(0, -25, 0);
                    break;
                case 2:
                    position += new Vector3(25, 0, 0);
                    break;
                case 3:
                    position += new Vector3(-25, 0, 0);
                    break;
            }
        }
        Instantiate(room, position, Quaternion.identity, transform);
    }

    private void GenerateRoom()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject room = ChooseRoom();
            PlaceRoom(room);
        }
    }
}
