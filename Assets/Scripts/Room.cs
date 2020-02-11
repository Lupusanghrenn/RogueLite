using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private Vector2 pos;
    private bool[] doors = new bool[4];

    public Room(Vector2 p)
    {
        pos = p;
    }

    public Vector2 Pos
    {
        get { return pos; }
    }

    public bool[] Doors
    {
        get { return doors; }
        set { doors = value; }
    }
}
