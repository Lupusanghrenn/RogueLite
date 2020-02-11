using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int size;
    public int spaceBetweenRooms;
    private Room[,] rooms = new Room[0,0];
    // Start is called before the first frame update
    void Start()
    {
        GenerateLayout();
    }

    public void GenerateLayout()
    {
        rooms = new Room[size,size];

        List<Vector2> posTaken = new List<Vector2>();

        //first Room
        Room firstRoom = new Room(new Vector2((size/2) * spaceBetweenRooms, (size/2) * spaceBetweenRooms));
        rooms[size / 2,size / 2] = firstRoom;
    }

    public void Clear()
    {
        rooms = new Room[size, size];
    }



    private void OnDrawGizmos()
    {
        for (int j = 0; j < rooms.GetLength(1); j++)
        {
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                if (rooms[i,j] != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(new Vector2(i * spaceBetweenRooms, j * spaceBetweenRooms), 0.5f);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(new Vector2(i * spaceBetweenRooms, j * spaceBetweenRooms), 0.5f);
                }
            }
        }
    }
}
