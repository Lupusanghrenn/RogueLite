using System;
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

    public void Debuging()
    {
        Init();

        LayoutRoom firstRoom = new LayoutRoom(mapCenter, 0);
        SpawnRoom(firstRoom);

        DisplayLayout();
    }

    public void Init()
    {
        spawnedRooms = new List<LayoutRoom>();
        rooms = new LayoutRoom[mapSize.x, mapSize.y];
        mapCenter = new Vector2Int(Mathf.RoundToInt(mapSize.x / 2), Mathf.RoundToInt(mapSize.y / 2));
        Debug.Log("Initialisation terminée ! Taille : " + mapSize.x + " x " + mapSize.y);
    }

    public void ExpandHorizontally()
    {

    }

    public void ExpandVertically()
    {

    }



    public void SpawnRoom(LayoutRoom room)
    {
        rooms[room.Position.x, room.Position.y] = room;
        spawnedRooms.Add(room);
    }


    public LayoutRoom PickRandomRoom()
    {
        return spawnedRooms[Random.Range(0, spawnedRooms.Count)];
    }

    public bool IsInBounds(Vector2Int pos)
    {
        return pos.x < mapSize.x && pos.x > 0 && pos.y < mapSize.y && pos.y > 0;
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
