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
            Expand(chosenRoom);
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

    public void ExpandHorizontally()
    {

    }

    public void Expand(LayoutRoom room)
    {
        //on détecte les free slots autours
        List<Vector2Int> possiblePos = GetEmptyValidSpotsAtPos(room.Position);

        //on fait spawn des rooms
        int rdm = Random.Range(0, possiblePos.Count);

        for (int i = 0; i < rdm; i++)
        {
           SpawnRoom(new LayoutRoom(possiblePos[Random.Range(0, possiblePos.Count)], 0));
        }
    }




    public LayoutRoom PickRandomRoom()
    {
        return spawnedRooms[Random.Range(0, spawnedRooms.Count)];
    }

    public List<LayoutRoom> GetRooms1Neighbor()
    {
        List<LayoutRoom> rooms = new List<LayoutRoom>();

        foreach (LayoutRoom room in spawnedRooms)
        {
            if (room.NbNeighbors == 1) { rooms.Add(room); }
        }
        return rooms;
    }

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
                    List<Vector2Int> freeSlots = GetEmptyValidSpotsAtPos(chosenRoom.Position);
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
                List<Vector2Int> freeSlots = GetEmptyValidSpotsAtPos(chosenRoom.Position);
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




    public void SpawnRoom(LayoutRoom room)
    {
        rooms[room.Position.x, room.Position.y] = room;
        spawnedRooms.Add(room);
        UpdateAllRoomsNeighbors();
    }

    public bool IsInBounds(Vector2Int pos)
    {
        return pos.x < mapSize.x && pos.x >= 0 && pos.y < mapSize.y && pos.y >= 0;
    }

    public void UpdateAllRoomsNeighbors()
    {
        foreach (LayoutRoom room in spawnedRooms)
        {
            room.UpdateNeighbors(spawnedRooms);
        }
    }

    public List<Vector2Int> GetEmptyValidSpotsAtPos(Vector2Int pos)
    {
        List<Vector2Int> possiblePos = new List<Vector2Int>();

        Vector2Int up = pos + new Vector2Int(0, 1);
        Vector2Int right = pos + new Vector2Int(1, 0);
        Vector2Int down = pos + new Vector2Int(0, -1);
        Vector2Int left = pos + new Vector2Int(-1, 0);

        //on détecte les free slots autours
        if (IsInBounds(up) && rooms[up.x, up.y] == null) { possiblePos.Add(up); }
        if (IsInBounds(right) && rooms[right.x, right.y] == null) { possiblePos.Add(right); }
        if (IsInBounds(down) && rooms[down.x, down.y] == null) { possiblePos.Add(down); }
        if (IsInBounds(left) && rooms[left.x, left.y] == null) { possiblePos.Add(left); }

        return possiblePos;
    }

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
