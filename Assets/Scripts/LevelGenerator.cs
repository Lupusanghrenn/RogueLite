using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public int nbRoom;

    private List<Vector2> posTaken = new List<Vector2>();

    private Vector2 debugRed;
    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    public void Test()
    {
        posTaken = new List<Vector2>();
        List<Room> spawnedRooms = new List<Room>();

        Room room1 = new Room(new Vector2(0,1));
        Room room2 = new Room(new Vector2(0,-1));
        Room room3 = new Room(new Vector2(1,0));
        Room room4 = new Room(new Vector2(-1, 0));
        Room room5 = new Room(new Vector2(0, 0));

        spawnedRooms.Add(room1);
        spawnedRooms.Add(room2);
        spawnedRooms.Add(room3);
        spawnedRooms.Add(room4);
        spawnedRooms.Add(room5);

        posTaken.Add(room1.Pos);
        posTaken.Add(room2.Pos);
        posTaken.Add(room3.Pos);
        posTaken.Add(room4.Pos);
        posTaken.Add(room5.Pos);

        UpdateNeighborsSlots(room1);
        UpdateNeighborsSlots(room2);
        UpdateNeighborsSlots(room3);
        UpdateNeighborsSlots(room4);
        UpdateNeighborsSlots(room5);

        Debug.Log("Room 1 : "+room1.PrintNeighborSlots());
        Debug.Log("Room 2 : "+room2.PrintNeighborSlots());
        Debug.Log("Room 3 : "+room3.PrintNeighborSlots());
        Debug.Log("Room 4 : "+room4.PrintNeighborSlots());
        Debug.Log("Room 5 : "+room5.PrintNeighborSlots());

        Debug.Log(room1.isSurrounded());

        int rdm = Random.Range(0, 0);
        Debug.Log(rdm);
    }

    public void GenerateLayout()
    {
        posTaken = new List<Vector2>();
        List<Room> spawnedRooms = new List<Room>();
        int nbSpawnedRoom = 0;

        //first Room
        Room firstRoom = new Room(Vector2.zero);
        spawnedRooms.Add(firstRoom);
        posTaken.Add(firstRoom.Pos);
        nbSpawnedRoom++;

        while (nbSpawnedRoom < nbRoom)
        {
            //choisir UNE room parmis celles déjà spawn
            Room chosenRoom = ChooseChosenRoom(spawnedRooms);
            Debug.Log(chosenRoom.isSurrounded());
            //debugRed = chosenRoom.Pos;

            //faire la propagation
            Room newRoom = new Room(PickSpawnLocation(chosenRoom.Pos));
            spawnedRooms.Add(newRoom);
            posTaken.Add(newRoom.Pos);

            //On update les voisins autours de la chosenRoom et de celle qu'on vient de spawn
            UpdateNeighborsSlots(chosenRoom);
            UpdateNeighborsSlots(newRoom);

            nbSpawnedRoom++;
        }
    } 

    public Room ChooseChosenRoom(List<Room> spawnedRooms)
    {
        Room chosenRoom = spawnedRooms[Random.Range(0, spawnedRooms.Count)];

        while (chosenRoom.isSurrounded())
        {
            chosenRoom = spawnedRooms[Random.Range(0, spawnedRooms.Count)];
        }

        return chosenRoom;
    }

    private Vector2 PickSpawnLocation(Vector2 currentPos)
    {
        List<Vector2> possiblePos = new List<Vector2>();

        Vector2 right = currentPos + Vector2.right;
        Vector2 left = currentPos + Vector2.left;
        Vector2 up = currentPos + Vector2.up;
        Vector2 down = currentPos + Vector2.down;

        if (!posTaken.Contains(up))
        {
            possiblePos.Add(up);
        }

        if (!posTaken.Contains(down))
        {
            possiblePos.Add(down);
        }

        if (!posTaken.Contains(right))
        {
            possiblePos.Add(right);
        }

        if (!posTaken.Contains(left))
        {
            possiblePos.Add(left);
        }
        return possiblePos[Random.Range(0, possiblePos.Count)];
    }

    public void UpdateNeighborsSlots(Room room)
    {
        Vector2 right = room.Pos + Vector2.right;
        Vector2 left = room.Pos + Vector2.left;
        Vector2 up = room.Pos + Vector2.up;
        Vector2 down = room.Pos + Vector2.down;

        if (posTaken.Contains(up))
        {
            room.NeighborSlots[0] = true;
        }

        if (posTaken.Contains(down))
        {
            room.NeighborSlots[2] = true;
        }

        if (posTaken.Contains(right))
        {
            room.NeighborSlots[1] = true;
        }

        if (posTaken.Contains(left))
        {
            room.NeighborSlots[3] = true;
        }
    }
    

    private void OnDrawGizmos()
    {
        foreach (Vector2 pos in posTaken)
        {
            Gizmos.DrawSphere(pos, 0.2f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(debugRed, 0.2f);
    }
}
