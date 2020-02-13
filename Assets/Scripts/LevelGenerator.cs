using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Layout")]
    public int nbRoom;
    [Range(0.0f, 1.0f)]
    public float branchOutProbability;

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
        LayoutRoom firstRoom = new LayoutRoom(Vector2.zero, Vector2.zero);
        spawnedRooms.Add(firstRoom);
        nbSpawnedRoom++;

        while (nbSpawnedRoom < nbRoom)
        {
            RepaintWhiteCircle();
            //Chose the room to expend from
            LayoutRoom chosenRoom = ChooseChosenRoom(spawnedRooms);

            //Expend
            float branchOutRdm = Random.Range(0.0f, 1.0f);
            Debug.Log("rdm : " + branchOutRdm);

            if (chosenRoom.Forward == Vector2.zero) //on est sur la room de départ
            {
                Debug.Log("first room");
                CreateBranch(chosenRoom.Position, spawnedRooms, 4);
            }
            else if (branchOutRdm > 1 - branchOutProbability) //Branch out
            {
                Debug.Log("branching out");
                int nbBranchRoom = Random.Range(1, 4);
                CreateBranch(chosenRoom.Position, spawnedRooms, nbBranchRoom);
            }
            else //spawn room forward
            {
                Debug.Log("forward room");
                SpawnForwardRoom(chosenRoom.Position, spawnedRooms);
            }
        }
    }

    public void SpawnForwardRoom(Vector2 currentPos, List<LayoutRoom> spawnedRooms)
    {
        LayoutRoom currentRoom = spawnedRooms.Find(r => r.Position == currentPos);
        Vector2 newRoomPos = currentRoom.Position + currentRoom.Forward;

        LayoutRoom newRoom = new LayoutRoom(newRoomPos, currentRoom.Forward);

        if (spawnedRooms.FindIndex(r => r.Position == newRoomPos) == -1)
        {
            spawnedRooms.Add(newRoom);
        }
        else
        {
            spawnedRooms[spawnedRooms.FindIndex(r => r.Position == newRoomPos)] = newRoom;
        }
        UpdateNeighborsSlots(newRoom, spawnedRooms);
        nbSpawnedRoom++;
        Instantiate(Resources.Load("DebugCircle"), newRoomPos, Quaternion.identity);
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
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.up);
                spawnedRooms.Add(newRoom);
                UpdateNeighborsSlots(newRoom, spawnedRooms);
                nbSpawnedRoom++;

                Instantiate(Resources.Load("DebugCircle"), newRoomPos, Quaternion.identity);
            }
            else if (newRoomPos == currentPos + Vector2.right)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.right);
                spawnedRooms.Add(newRoom);
                UpdateNeighborsSlots(newRoom, spawnedRooms);
                nbSpawnedRoom++;

                Instantiate(Resources.Load("DebugCircle"), newRoomPos, Quaternion.identity);
            }
            else if (newRoomPos == currentPos + Vector2.down)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.down);
                spawnedRooms.Add(newRoom);
                UpdateNeighborsSlots(newRoom, spawnedRooms);
                nbSpawnedRoom++;

                Instantiate(Resources.Load("DebugCircle"), newRoomPos, Quaternion.identity);
            }
            else if (newRoomPos == currentPos + Vector2.left)
            {
                LayoutRoom newRoom = new LayoutRoom(newRoomPos, Vector2.left);
                spawnedRooms.Add(newRoom);
                UpdateNeighborsSlots(newRoom, spawnedRooms);
                nbSpawnedRoom++;

                Instantiate(Resources.Load("DebugCircle"), newRoomPos, Quaternion.identity);
            }
            branchNbSpawnedRoom++;
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
