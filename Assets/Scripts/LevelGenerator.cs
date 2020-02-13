using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public bool debug = false;

    [Header("Layout")]
    public int nbRoom;
    [Range(0.0f, 1.0f)]
    public float branchOutProbability;
    public bool allowRoomStacks;

    private int nbSpawnedRoom;

    #region Layout Generation
    /// <summary>
    /// Generates the layout of the level. ie: Where the room will appear later
    /// </summary>
    public void GenerateLayout()
    {
        ClearDebugCircles();
        nbSpawnedRoom = 0;

        List<LayoutRoom> spawnedRooms = new List<LayoutRoom>();

        //First Room
        LayoutRoom firstRoom = new LayoutRoom(Vector2.zero, Vector2.zero, 0);
        spawnedRooms.Add(firstRoom);
        nbSpawnedRoom++;
        if (debug) { Instantiate(Resources.Load("DebugCircle"), firstRoom.Position, Quaternion.identity); }

        while (nbSpawnedRoom < nbRoom)
        {
            RepaintWhiteCircle();

            //Chose the room to expend from
            LayoutRoom chosenRoom;
            if (allowRoomStacks)
            {
                chosenRoom = ChoseValidRoom(spawnedRooms);
            }
            else
            {
                chosenRoom = ChoseRoom1Neighbor(spawnedRooms);
            }



            //Expend
            float branchOutRdm = Random.Range(0.0f, 1.0f);

            if (chosenRoom.Forward == Vector2.zero) //on est sur la room de départ
            {
                CreateBranch(chosenRoom.Position, spawnedRooms, 4);
            }
            else if (branchOutRdm > 1 - branchOutProbability) //Branch out
            {
                int nbBranchRoom = Random.Range(1, 4);
                CreateBranch(chosenRoom.Position, spawnedRooms, nbBranchRoom);
            }
            else //spawn room forward
            {
                SpawnForwardRoom(chosenRoom.Position, spawnedRooms);
            }
        }

        ChoseBossRoom(spawnedRooms);
        ChoseLevelUpRoom(spawnedRooms);
        ChoseShopRoom(spawnedRooms);
    }

    public void ChoseLevelUpRoom(List<LayoutRoom> spawnedRooms)
    {
        //on choisi n'importe quelle room avec un seul voisin
        LayoutRoom room;
        do
        {
            room = ChoseRoom1Neighbor(spawnedRooms);
        } while (room.RoomType != 0);

        room.RoomType = 2;

        if (debug)
        {
            GameObject yellowCircle = (GameObject)Instantiate(Resources.Load("DebugCircle"), room.Position, Quaternion.identity); ;
            yellowCircle.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    public void ChoseShopRoom(List<LayoutRoom> spawnedRooms)
    {
        //on choisi n'importe quelle room avec un seul voisin
        LayoutRoom room;
        do
        {
            room = ChoseRoom1Neighbor(spawnedRooms);
        } while (room.RoomType != 0);

        room.RoomType = 3;

        if (debug)
        {
            GameObject blueCircle = (GameObject)Instantiate(Resources.Load("DebugCircle"), room.Position, Quaternion.identity); ;
            blueCircle.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    /// <summary>
    /// choses the furthest room from the spawn to be the boss room
    /// </summary>
    /// <param name="spawnedRooms"></param>
    public void ChoseBossRoom(List<LayoutRoom> spawnedRooms)
    {
        LayoutRoom bossRoom = FindFurthestRoom(spawnedRooms);
        bossRoom.RoomType = 1;
        if (debug)
        {
            GameObject redCircle = (GameObject)Instantiate(Resources.Load("DebugCircle"), bossRoom.Position, Quaternion.identity); ;
            redCircle.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public LayoutRoom FindFurthestRoom(List<LayoutRoom> spawnedRooms)
    {
        int index = -1;
        float distance = 0;
        for (int i = 0; i < spawnedRooms.Count; i++)
        {
            if (index == -1 || spawnedRooms[i].Position.magnitude > distance)
            {
                distance = spawnedRooms[i].Position.magnitude;
                index = i;
            }
        }

        return spawnedRooms[index];
    }

    public void SpawnForwardRoom(Vector2 currentPos, List<LayoutRoom> spawnedRooms)
    {
        LayoutRoom currentRoom = spawnedRooms.Find(r => r.Position == currentPos);
        Vector2 newRoomPos = currentRoom.Position + currentRoom.Forward;

        LayoutRoom newRoom = new LayoutRoom(newRoomPos, currentRoom.Forward, 0);

        if (spawnedRooms.FindIndex(r => r.Position == newRoomPos) == -1)
        {
            spawnedRooms.Add(newRoom);
            SpawnRoom(newRoom, spawnedRooms);
        }
        else
        {
            spawnedRooms[spawnedRooms.FindIndex(r => r.Position == newRoomPos)] = newRoom;
            UpdateNeighborsSlots(newRoom, spawnedRooms);

            if (debug) { Instantiate(Resources.Load("DebugCircle"), newRoom.Position, Quaternion.identity); }
        }
    }

    public void CreateBranch(Vector2 currentPos, List<LayoutRoom> spawnedRooms, int nbRoomToSpawn)
    {
        int branchNbSpawnedRoom = 0;
        LayoutRoom currentRoom = spawnedRooms.Find(r => r.Position == currentPos);

        while (!currentRoom.isSurrounded() && branchNbSpawnedRoom < nbRoomToSpawn)
        {
            Vector2 newRoomPos = PickSpawnLocation(currentPos, spawnedRooms);

            if (newRoomPos == currentPos + Vector2.up)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.up, 0);
                SpawnRoom(newRoom, spawnedRooms);

                LayoutRoom newRoom2 = new LayoutRoom(newRoom.Position + Vector2.up, Vector2.up, 0);
                SpawnRoom(newRoom2, spawnedRooms);
            }
            else if (newRoomPos == currentPos + Vector2.right)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.right, 0);
                SpawnRoom(newRoom, spawnedRooms);

                LayoutRoom newRoom2 = new LayoutRoom(newRoom.Position + Vector2.right, Vector2.right, 0);
                SpawnRoom(newRoom2, spawnedRooms);
            }
            else if (newRoomPos == currentPos + Vector2.down)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.down, 0);
                SpawnRoom(newRoom, spawnedRooms);

                LayoutRoom newRoom2 = new LayoutRoom(newRoom.Position + Vector2.down, Vector2.down, 0);
                SpawnRoom(newRoom2, spawnedRooms);
            }
            else if (newRoomPos == currentPos + Vector2.left)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.left, 0);
                SpawnRoom(newRoom, spawnedRooms);

                LayoutRoom newRoom2 = new LayoutRoom(newRoom.Position + Vector2.left, Vector2.left, 0);
                SpawnRoom(newRoom2, spawnedRooms);
            }
            branchNbSpawnedRoom++;
        }
    }

    public void SpawnRoom(LayoutRoom roomToSpawn, List<LayoutRoom> spawnedRooms)
    {
        spawnedRooms.Add(roomToSpawn);
        UpdateNeighborsSlots(roomToSpawn, spawnedRooms);
        nbSpawnedRoom++;

        if (debug){ Instantiate(Resources.Load("DebugCircle"), roomToSpawn.Position, Quaternion.identity); }
    }

    /// <summary>
    /// Pick a room in the list of already spawned rooms that has at least one free slot
    /// </summary>
    /// <param name="spawnedRooms"></param>
    /// <returns></returns>
    public LayoutRoom ChoseValidRoom(List<LayoutRoom> spawnedRooms)
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
    /// Pick a room in the list of already spawned rooms that has only 1 neighbor
    /// </summary>
    public LayoutRoom ChoseRoom1Neighbor(List<LayoutRoom> spawnedRooms)
    {
        int rdm = Random.Range(0, spawnedRooms.Count);

        LayoutRoom chosenRoom = spawnedRooms[rdm];

        while ((chosenRoom.NbNeighbors() > 1 || chosenRoom.isSurrounded()))
        {
            rdm = Random.Range(0, spawnedRooms.Count);
            chosenRoom = spawnedRooms[rdm];
        }

        return chosenRoom;
    }


    /// <summary>
    /// Scout the surroundings of a position and returns the number of neighbors
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="spawnedRooms"></param>
    /// <returns></returns>
    public int NbNeighborsAtPos(Vector2 pos, List<LayoutRoom> spawnedRooms)
    {
        int cpt = 0;
        List<Vector2> surroundings = new List<Vector2>();

        Vector2 right = pos + Vector2.right;
        Vector2 left = pos + Vector2.left;
        Vector2 up = pos + Vector2.up;
        Vector2 down = pos + Vector2.down;

        if (!spawnedRooms.Exists(r => r.Position == up))
        {
            cpt++;
        }

        if (!spawnedRooms.Exists(r => r.Position == down))
        {
            cpt++;
        }

        if (!spawnedRooms.Exists(r => r.Position == right))
        {
            cpt++;
        }

        if (!spawnedRooms.Exists(r => r.Position == left))
        {
            cpt++;
        }

        return cpt;
    }

    /// <summary>
    /// Pick a slot to spawn the room in around the currentPos in parameters
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="posTaken"></param>
    /// <returns></returns>
    private Vector2 PickSpawnLocation(Vector2 currentPos, List<LayoutRoom> spawnedRooms)
    {
        List<Vector2> possiblePos = new List<Vector2>();

        Vector2 right = currentPos + Vector2.right;
        Vector2 left = currentPos + Vector2.left;
        Vector2 up = currentPos + Vector2.up;
        Vector2 down = currentPos + Vector2.down;

        if (!spawnedRooms.Exists(r => r.Position == up))
        {
            possiblePos.Add(up);
        }

        if (!spawnedRooms.Exists(r => r.Position == down))
        {
            possiblePos.Add(down);
        }

        if (!spawnedRooms.Exists(r => r.Position == right))
        {
            possiblePos.Add(right);
        }

        if (!spawnedRooms.Exists(r => r.Position == left))
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
        Vector2 right = room.Position + Vector2.right;
        Vector2 left = room.Position + Vector2.left;
        Vector2 up = room.Position + Vector2.up;
        Vector2 down = room.Position + Vector2.down;

        if (spawnedRooms.Exists(r => r.Position == up))
        {
            room.NeighborSlots[0] = true;
            spawnedRooms.Find(r => r.Position == up).NeighborSlots[2] = true;
        }

        if (spawnedRooms.Exists(r => r.Position == down))
        {
            room.NeighborSlots[2] = true;
            spawnedRooms.Find(r => r.Position == down).NeighborSlots[0] = true;
        }

        if (spawnedRooms.Exists(r => r.Position == right))
        {
            room.NeighborSlots[1] = true;
            spawnedRooms.Find(r => r.Position == right).NeighborSlots[3] = true;
        }

        if (spawnedRooms.Exists(r => r.Position == left))
        {
            room.NeighborSlots[3] = true;
            spawnedRooms.Find(r => r.Position == left).NeighborSlots[1] = true;
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
