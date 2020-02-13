using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class' goal is to store data while making the room layout of the Level (first step of the level generation)
/// </summary>
public class LayoutRoom
{
    private Vector2 position; //Grid position of trhe room (not world position)
    private bool[] neighborSlots; //True is there is a neighbor, false otherwise. Starts at 12h and goes clockwise.
    private Vector2 forward; //If we consider "backward' as the direction the room has been spawned from. forward is the opposite

    #region constructors
    public LayoutRoom(Vector2 pos, Vector2 forward)
    {
        position = pos;
        this.forward = forward;
        neighborSlots = new bool[] { false, false, false, false };
    }
    #endregion

    #region getter setters
    public Vector2 Position
    {
        get { return position; }
    }

    public Vector2 Forward
    {
        get { return forward; }
        set { forward = value; }
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
        return this.position == r2.position;
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
