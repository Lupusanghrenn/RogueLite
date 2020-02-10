using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Direction parentDirection;

    public List<GameObject> LeftRooms;
    public List<GameObject> RightRooms;
    public List<GameObject> TopRooms;
    public List<GameObject> BottomRooms;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnRoom", 0.1f);
    }

    public void SpawnRoom()
    {
        switch (parentDirection)
        {
            case Direction.Bottom:
                Instantiate(BottomRooms[Random.Range(0, BottomRooms.Count)], transform.position, Quaternion.identity);
                break;
            case Direction.Top:
                Instantiate(TopRooms[Random.Range(0, TopRooms.Count)], transform.position, Quaternion.identity);
                break;
            case Direction.Right:
                Instantiate(RightRooms[Random.Range(0, RightRooms.Count)], transform.position, Quaternion.identity);
                break;
            case Direction.Left:
                Instantiate(LeftRooms[Random.Range(0, LeftRooms.Count)], transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(gameObject.transform.position, 0.4f);
    }

    enum Direction { Bottom, Top, Right, Left };
}
