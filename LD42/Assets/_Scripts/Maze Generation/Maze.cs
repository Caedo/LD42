using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public Vector2Int m_MapSize;

    public MazeRoom m_RoomPrefab;

    private MazeRoom[,] m_Map;

    private void Start()
    {
        GenerateMap();
    }

    [ContextMenu("Generate")]
    void GenerateMap()
    {
        m_Map = new MazeRoom[m_MapSize.x, m_MapSize.y];
        List<MazeRoom> activeRooms = new List<MazeRoom>();
        activeRooms.Add(CreateRoom(new Vector2Int(m_MapSize.x / 2, m_MapSize.y / 2)));

        while (activeRooms.Count > 0)
        {
            DoNextGenerationStep(activeRooms);
        }
    }

    MazeRoom CreateRoom(Vector2Int coord)
    {
        var room = Instantiate(m_RoomPrefab, new Vector3(coord.x, coord.y), Quaternion.identity, transform);
        room.Initialize(coord, CalculateFillPecent(m_MapSize, coord));
        room.name = $"{coord.x}\t{coord.y}";
        
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
        float maxValue = mapSize.magnitude;
        float currentValue = (mapSize - coord).magnitude;

        return currentValue / maxValue;
    }
}