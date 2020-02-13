using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Layout")]
    public int nbRoom;

    #region Layout Generation
    /// <summary>
    /// Generates the layout of the level. ie: Where the room will appear later
    /// </summary>
    public void GenerateLayout()
    {
        ClearDebugCircles();

        List<Vector2> posTaken = new List<Vector2>();
        List<LayoutRoom> spawnedRooms = new List<LayoutRoom>();
        int nbSpawnedRoom = 0;

        //First Room
        LayoutRoom firstRoom = new LayoutRoom(Vector2.zero);
        spawnedRooms.Add(firstRoom);
        posTaken.Add(firstRoom.Pos);
        nbSpawnedRoom++;

        while (nbSpawnedRoom < nbRoom)
        {
            //Chose the room we are going to expend from
            LayoutRoom chosenRoom = ChooseChosenRoom(spawnedRooms);

            //Expend by spawning a new room
            LayoutRoom newRoom = new LayoutRoom(PickSpawnLocation(chosenRoom.Pos, posTaken));
            spawnedRooms.Add(newRoom);
            posTaken.Add(newRoom.Pos);

            Instantiate(Resources.Load("DebugCircle"), newRoom.Pos, Quaternion.identity);

            //Update neibhors of the new room
            UpdateNeighborsSlots(newRoom, spawnedRooms);

            nbSpawnedRoom++;
        }
    }

    /// <summary>
    /// Pick a room in the list of already spawned rooms thta has at least one free slot
    /// </summary>
    /// <param name="spawnedRooms"></param>
    /// <returns></returns>
    public LayoutRoom ChooseChosenRoom(List<LayoutRoom> spawnedRooms)
    {
        int rdm = Random.Range(0, spawnedRooms.Count);

        LayoutRoom chosenRoom = spawnedRooms[rdm];

        while (chosenRoom.isSurrounded())
        {
            rdm = Random.Range(0, spawnedRooms.Count);
            chosenRoom = spawnedRooms[rdm];
        }

        return chosenRoom;
    }

    /// <summary>
    /// Pick a slot to spawn the room in around the currentPos in parameters
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="posTaken"></param>
    /// <returns></returns>
    private Vector2 PickSpawnLocation(Vector2 currentPos, List<Vector2> posTaken)
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

    /// <summary>
    /// Update the neighbors of the room in parameters and of it's neighbors
    /// </summary>
    /// <param name="room"></param>
    /// <param name="spawnedRooms"></param>
    public void UpdateNeighborsSlots(LayoutRoom room, List<LayoutRoom> spawnedRooms)
    {
        Vector2 right = room.Pos + Vector2.right;
        Vector2 left = room.Pos + Vector2.left;
        Vector2 up = room.Pos + Vector2.up;
        Vector2 down = room.Pos + Vector2.down;

        if (spawnedRooms.Exists(r => r.Pos == up))
        {
            room.NeighborSlots[0] = true;
            spawnedRooms.Find(r => r.Pos == up).NeighborSlots[2] = true;
        }

        if (spawnedRooms.Exists(r => r.Pos == down))
        {
            room.NeighborSlots[2] = true;
            spawnedRooms.Find(r => r.Pos == down).NeighborSlots[0] = true;
        }

        if (spawnedRooms.Exists(r => r.Pos == right))
        {
            room.NeighborSlots[1] = true;
            spawnedRooms.Find(r => r.Pos == right).NeighborSlots[3] = true;
        }

        if (spawnedRooms.Exists(r => r.Pos == left))
        {
            room.NeighborSlots[3] = true;
            spawnedRooms.Find(r => r.Pos == left).NeighborSlots[1] = true;
        }
    }
    #endregion

    #region Debug
    public void ClearDebugCircles()
    {
        GameObject[] circles = GameObject.FindGameObjectsWithTag("DebugCircle");

        foreach (GameObject go in circles)
        {
            DestroyImmediate(go);
        }
    }

    public void RepaintWhiteCircle()
    {
        GameObject[] circles = GameObject.FindGameObjectsWithTag("DebugCircle");

        foreach (GameObject go in circles)
        {
            go.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    #endregion
}
