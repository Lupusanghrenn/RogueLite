using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private Vector2 pos;

    /// <summary>
    /// represents the neighbors around the Room. true means there is a neighbor, false means empty. Starts at noon and goes clockwise.
    /// </summary>
    private bool[] neighborSlots;

    #region constructors
    public Room()
    {
        pos = Vector2.zero;
        neighborSlots = new bool[] { false, false, false, false};
    }
    
    public Room(Vector2 p)
    {
        pos = p;
        neighborSlots = new bool[] { false, false, false, false };
    }

    public Room(Vector2 p, bool[] b)
    {
        pos = p;
        neighborSlots = b;
    }
    #endregion

    #region getter setters
    public Vector2 Pos
    {
        get { return pos; }
    }

    public bool[] NeighborSlots
    {
        get { return neighborSlots; }
        set { neighborSlots = value; }
    }
    #endregion

    #region Methods
    public bool Equals(Room r2)
    {
        return this.pos == r2.pos;
    }

    public bool isSurrounded()
    {
        bool res = true;

        for (int i = 0; i < neighborSlots.Length; i++)
        {
            if (!neighborSlots[i])
            {
                res = false;
            }
        }
        return res;
    }

    public string PrintNeighborSlots()
    {
        string res = "";

        for (int i = 0; i < neighborSlots.Length; i++)
        {
            if (neighborSlots[i])
            {
                res += "true ";
            }
            else
            {
                res += "false ";
            }
        }

        return res;
    }
    #endregion
}
