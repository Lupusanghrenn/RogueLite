using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class' goal is to store data while making the room layout of the Level (first step of the level generation)
/// </summary>
public class LayoutRoom
{
    /// <summary>
    /// 2D pos of the room
    /// </summary>
    private Vector2 pos;

    /// <summary>
    /// represents the neighbors around the Room. true means there is a neighbor, false means empty. Starts at noon and goes clockwise.
    /// </summary>
    private bool[] neighborSlots;

    #region constructors
    public LayoutRoom()
    {
        pos = Vector2.zero;
        neighborSlots = new bool[] { false, false, false, false};
    }
    
    public LayoutRoom(Vector2 p)
    {
        pos = p;
        neighborSlots = new bool[] { false, false, false, false };
    }

    public LayoutRoom(Vector2 p, bool[] b)
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
    public bool Equals(LayoutRoom r2)
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
    #endregion
}
