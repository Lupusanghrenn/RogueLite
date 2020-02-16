﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Layout")]
    public int nbRoom;
    public Vector2Int mapSize;
    [Range(0.0f, 1.0f)]
    public float chanceBranch;

    //2D array of the room layout
    private LayoutRoom[,] rooms;
    private List<LayoutRoom> spawnedRooms;
    private Vector2Int mapCenter;

    private int cpt;

    public void Debuging()
    {
        Init();

        LayoutRoom firstRoom = new LayoutRoom(mapCenter, 0);
        SpawnRoom(firstRoom);
        /*LayoutRoom room = new LayoutRoom(firstRoom.Position + new Vector2Int(1, 0), 0);
        SpawnRoom(room);
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(-1, 0), 0));
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(0, -1), 0));
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(0, 1), 0));
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(1, 1), 0));
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(-1, 1), 0));
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(1, -1), 0));
        SpawnRoom(new LayoutRoom(firstRoom.Position + new Vector2Int(-1, -1), 0));*/

        while (spawnedRooms.Count < nbRoom && cpt < 1000)
        {
            LayoutRoom chosenRoom = PickRandomRoom();

            float rdmBranch = Random.Range(0.0f, 1.0f);

            if (rdmBranch > 1 - chanceBranch) //branch
            {
                ExpandVertically(chosenRoom);
            }
            else //expand normally
            {
                ExpandHorizontally(chosenRoom);
            }

            cpt++;
        }

        DisplayLayout();
    }

    public void Init()
    {
        cpt = 0;
        spawnedRooms = new List<LayoutRoom>();
        rooms = new LayoutRoom[mapSize.x, mapSize.y];
        mapCenter = new Vector2Int(Mathf.RoundToInt(mapSize.x / 2), Mathf.RoundToInt(mapSize.y / 2));
        Debug.Log("Initialisation terminée ! Taille : " + mapSize.x + " x " + mapSize.y);
    }

    public void ExpandVertically(LayoutRoom room)
    {
        //on détecte les free slots autours
        List<Vector2Int> possiblePos = GetEmptyValidSpotsAtPos(room.Position, false, true);

        //on fait spawn des rooms
        int rdm;

        if (possiblePos.Count == 1)
            rdm = Random.Range(0, possiblePos.Count + 1);
        else
            rdm = Random.Range(0, possiblePos.Count);


        for (int i = 0; i < rdm; i++)
        {
            SpawnRoom(new LayoutRoom(possiblePos[Random.Range(0, possiblePos.Count)], 0));
        }
    }

    public void ExpandHorizontally(LayoutRoom room)
    {
        //on détecte les free slots autours
        List<Vector2Int> possiblePos = GetEmptyValidSpotsAtPos(room.Position, true, false);

        //on fait spawn des rooms
        int rdm;

        if (possiblePos.Count == 1)
            rdm = Random.Range(0, possiblePos.Count + 1);
        else
            rdm = Random.Range(0, possiblePos.Count);


        for (int i = 0; i < rdm; i++)
        {
            SpawnRoom(new LayoutRoom(possiblePos[Random.Range(0, possiblePos.Count)], 0));
        }
    }

    public void Expand(LayoutRoom room)
    {
        //on détecte les free slots autours
        List<Vector2Int> possiblePos = GetEmptyValidSpotsAtPos(room.Position, true, true);

        //on fait spawn des rooms
        int rdm;

        if (possiblePos.Count == 1)
            rdm = Random.Range(0, possiblePos.Count + 1);
        else
            rdm = Random.Range(0, possiblePos.Count);


        for (int i = 0; i < rdm; i++)
        {
           SpawnRoom(new LayoutRoom(possiblePos[Random.Range(0, possiblePos.Count)], 0));
        }
    }



    /// <summary> 
    /// returns a random room from the spawned rooms
    /// </summary>
    /// <returns></returns>
    public LayoutRoom PickRandomRoom()
    {
        return spawnedRooms[Random.Range(0, spawnedRooms.Count)];
    }


    //TODO : FIX
    public LayoutRoom Pick1NeighborRoom()
    {
        List<LayoutRoom> possibleRooms = GetRooms1Neighbor();
        return possibleRooms[Random.Range(0, possibleRooms.Count)];
    }

    /// <summary>
    /// returns le list of rooms that has only one neighbor
    /// </summary>
    /// <returns></returns>
    public List<LayoutRoom> GetRooms1Neighbor()
    {
        List<LayoutRoom> rooms = new List<LayoutRoom>();

        foreach (LayoutRoom room in spawnedRooms)
        {
            if (room.NbNeighbors == 1) { rooms.Add(room); }
        }
        return rooms;
    }

    /// <summary>
    /// Create a room that has only one neighbor
    /// </summary>
    public void CreateRoom1Neighbor()
    {
        bool done = false;
        int cptInfinite = 0;
        int cptTries = 0;

        while (!done)
        {
            LayoutRoom chosenRoom = PickRandomRoom();
            //on préfère faire spawn une nouvelle room de façon à ne pas transformer une room qui a déjà qu'un seul voisin en une qui en a deux
            if (cptTries < 100) 
            {
                if (chosenRoom.NbNeighbors > 1)
                {
                    List<Vector2Int> freeSlots = GetEmptyValidSpotsAtPos(chosenRoom.Position, true, true);
                    if (freeSlots.Count > 0)
                    {
                        Vector2Int spawnPosition = freeSlots[Random.Range(0, freeSlots.Count)];
                        if (CountNeighborsAtPos(spawnPosition) == 1)
                        {
                            SpawnRoom(new LayoutRoom(spawnPosition, 0));
                            done = true;
                        }
                    }
                }
                cptTries++;
            }
            else //Si aucune possibilité de spawn 1 room sans altérer celle qui n'ont déjà qu'un seul voisin, on spawn quand même une pour débloquer des possiblités
            {
                List<Vector2Int> freeSlots = GetEmptyValidSpotsAtPos(chosenRoom.Position, true, true);
                if (freeSlots.Count > 0)
                {
                    Vector2Int spawnPosition = freeSlots[Random.Range(0, freeSlots.Count)];
                    if (CountNeighborsAtPos(spawnPosition) == 1)
                    {
                        SpawnRoom(new LayoutRoom(spawnPosition, 0));
                        done = true;
                    }
                }
            }

            //protection anti boucle infinie de haute technologie
            cptInfinite++;
            if (cptInfinite > 1000)
            {
                Debug.Log("boucle infinie ");
                break;
            }
        }
        DisplayLayout();
    }



    /// <summary>
    /// Add the roon in parameters  to all the necessary lists and update all the rooms' neighbors
    /// </summary>
    /// <param name="room"></param>
    public void SpawnRoom(LayoutRoom room)
    {
        rooms[room.Position.x, room.Position.y] = room;
        spawnedRooms.Add(room);
        UpdateAllRoomsNeighbors();
    }

    /// <summary>
    /// Return if the pos is in bounds of the 2D array
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsInBounds(Vector2Int pos)
    {
        return pos.x < mapSize.x && pos.x >= 0 && pos.y < mapSize.y && pos.y >= 0;
    }

    /// <summary>
    /// update NbNeighbors according to the neihbors aroud a room
    /// </summary>
    public void UpdateAllRoomsNeighbors()
    {
        foreach (LayoutRoom room in spawnedRooms)
        {
            room.UpdateNeighbors(spawnedRooms);
        }
    }

    /// <summary>
    /// return all the enpty positions around "pos"
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public List<Vector2Int> GetEmptyValidSpotsAtPos(Vector2Int pos, bool hor, bool vert)
    {
        List<Vector2Int> possiblePos = new List<Vector2Int>();

        Vector2Int up = pos + new Vector2Int(0, 1);
        Vector2Int right = pos + new Vector2Int(1, 0);
        Vector2Int down = pos + new Vector2Int(0, -1);
        Vector2Int left = pos + new Vector2Int(-1, 0);

        //on détecte les free slots autours
        if (hor)
        {
            if (IsInBounds(right) && rooms[right.x, right.y] == null) { possiblePos.Add(right); }
            if (IsInBounds(left) && rooms[left.x, left.y] == null) { possiblePos.Add(left); }
        }
        if (vert)
        {
            if (IsInBounds(up) && rooms[up.x, up.y] == null) { possiblePos.Add(up); }
            if (IsInBounds(down) && rooms[down.x, down.y] == null) { possiblePos.Add(down); }
        }

        return possiblePos;
    }

    /// <summary>
    /// return the number of room in the neighborhood of "pos"
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public int CountNeighborsAtPos(Vector2Int pos)
    {
        int nb = 0;

        Vector2Int up = pos + new Vector2Int(0, 1);
        Vector2Int right = pos + new Vector2Int(1, 0);
        Vector2Int down = pos + new Vector2Int(0, -1);
        Vector2Int left = pos + new Vector2Int(-1, 0);

        if (spawnedRooms.Exists(r => r.Position == up)) { nb++; }
        if (spawnedRooms.Exists(r => r.Position == right)) { nb++; }
        if (spawnedRooms.Exists(r => r.Position == down)) { nb++; }
        if (spawnedRooms.Exists(r => r.Position == left)) { nb++; }

        return nb;
    }





    #region Debug
    public void DisplayLayout()
    {
        ClearLayout();

        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                if (rooms[i,j] == null)
                {
                    Instantiate(Resources.Load("DebugSquare"), new Vector3(i,j,0), Quaternion.identity);
                }
                else
                {
                    Instantiate(Resources.Load("DebugCircle"), new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    public void ClearLayout()
    {
        GameObject[] circles = GameObject.FindGameObjectsWithTag("DebugCircle");

        foreach (GameObject go in circles)
        {
            DestroyImmediate(go);
        }
    }
    #endregion
}
