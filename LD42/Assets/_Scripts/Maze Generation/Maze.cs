using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public Vector2Int m_MapSize;
    public RoomData m_RoomData;

    public MazeRoom m_RoomPrefab;

    private MazeRoom[,] m_Map;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        m_Map = new MazeRoom[m_MapSize.x, m_MapSize.y];
        List<MazeRoom> activeRooms = new List<MazeRoom>();
        activeRooms.Add(CreateRoom(new Vector2Int(m_MapSize.x / 2, m_MapSize.y / 2)));

        while (activeRooms.Count > 0)
        {
            DoNextGenerationStep(activeRooms);
        }

        foreach (var room in m_Map)
        {
            if (room != null)
                room.CreateRoom();
        }
    }

    MazeRoom CreateRoom(Vector2Int coord)
    {
        float posX = coord.x * m_RoomData.m_Size.x + m_RoomData.m_Size.x / 2f;
        float posY = coord.y * m_RoomData.m_Size.y + m_RoomData.m_Size.y / 2f;

        var room = Instantiate(m_RoomPrefab, new Vector3(posX, posY), Quaternion.identity, transform);
        room.Initialize(coord, CalculateFillPecent(m_MapSize, coord), m_RoomData);

        m_Map[coord.x, coord.y] = room;

        return room;
    }

    void DoNextGenerationStep(List<MazeRoom> list)
    {
        int index = Random.Range(0, list.Count);
        var room = list[index];

        if (room.IsFullyInitialized)
        {
            list.RemoveAt(index);
            return;
        }

        var nextDir = room.NextRandomDirection;
        var coord = room.m_Coord + nextDir.ToVector2Int();

        if (IsInsideMap(coord) && m_Map[coord.x, coord.y] == null)
        {
            var newRoom = CreateRoom(coord);
            list.Add(newRoom);

            room.AddPassage(nextDir, newRoom);
            newRoom.AddPassage(nextDir.GetOpposite(), room);
        }
    }

    bool IsInsideMap(Vector2Int coord)
    {
        return coord.x >= 0 && coord.x < m_MapSize.x && coord.y >= 0 && coord.y < m_MapSize.y;
    }

    float CalculateFillPecent(Vector2Int mapSize, Vector2Int coord)
    {
        var halfMapSize = new Vector2Int(mapSize.x / 2, mapSize.y / 2);

        float maxValue = halfMapSize.magnitude;
        float currentValue = (halfMapSize - coord).magnitude;

        return currentValue / maxValue;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < m_MapSize.x; x++)
        {
            for (int y = 0; y < m_MapSize.y; y++)
            {
                float posX = x * m_RoomData.m_Size.x + m_RoomData.m_Size.x / 2f;
                float posY = y * m_RoomData.m_Size.y + m_RoomData.m_Size.y / 2f;
                Gizmos.DrawWireCube(new Vector3(posX, posY), new Vector3(m_RoomData.m_Size.x, m_RoomData.m_Size.y, 1));
            }
        }
    }

    public void DestroyMaze()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}