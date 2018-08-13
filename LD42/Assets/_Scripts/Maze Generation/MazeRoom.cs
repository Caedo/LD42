using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class MazeRoom : MonoBehaviour
{
    public GameObject m_BorderCubePrefab;
    public List<PickUp> m_PossiblePickUps;

    float m_FillPercent;
    public Vector2Int m_Coord;

    private List<MazeDirection> m_DirectionsList;
    private Dictionary<MazeDirection, MazeRoom> m_ActiveDirections = new Dictionary<MazeDirection, MazeRoom>();

    private int[,] m_Cells;

    private RoomData m_RoomData;

    private MeshFilter m_MeshFilter;
    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uvs;

    public bool IsFullyInitialized => m_DirectionsList.Count == 0;
    public bool IsDeadEnd => m_ActiveDirections.Count == 1;
    public bool IsEndRoom { get; private set; }
    

    public MazeDirection NextRandomDirection
    {
        get
        {
            var kappa = m_DirectionsList[0];
            m_DirectionsList.RemoveAt(0);
            return kappa;
        }
    }

    public void Initialize(Vector2Int coord, float fillPercent, RoomData roomData)
    {
        m_Coord = coord;
        m_FillPercent = roomData.m_FillCurve.Evaluate(fillPercent);

        m_DirectionsList = MazeHelper.GetRandomDirectionsQueue();
        m_RoomData = roomData;
    }

    void CreateMesh()
    {
        m_MeshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh {name = "Room Mesh"};
        m_MeshFilter.mesh = mesh;

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        mesh.RecalculateNormals();
    }

    public void CreateRoom()
    {
        
        vertices = new List<Vector3>();
        triangles = new List<int>();
        name = "";
        
        var size = m_RoomData.m_Size;
        m_Cells = new int[size.x, size.y];
        
        if (!IsEndRoom)
        {
            foreach (var item in m_ActiveDirections)
            {
                name = name + item.Key.ToString() + " ";
            }


            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    m_Cells[x, y] = Random.Range(0f, 1f) < m_FillPercent ? 1 : 0;
                }
            }

            CreatePassages(m_Cells);

            for (int i = 0; i < m_RoomData.m_SmoothIteration; i++)
            {
                SmoothMap(m_Cells);
            }
        }
        else
        {
            Vector2 center = new Vector2(size.x, size.x) / 2;
            float radius = Mathf.Min(size.x, size.y) / 2f;
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (Vector2.Distance(center, new Vector2(x,y)) < radius)
                    {
                        m_Cells[x, y] = 0;
                    }
                    else
                    {
                        m_Cells[x, y] = 1;
                    }
                }
            }
            
            CreatePassages(m_Cells);
        }

        EnsureClosedWalls(m_Cells);

        InstantiateCubes();
        CreateMesh();
        CreatePickUps();
    }
    
    public void MakeEndRoom()
    {
        IsEndRoom = true;
    }

    private void CreatePickUps()
    {
        if (IsDeadEnd && ! IsEndRoom)
        {
            var index = Random.Range(0, m_PossiblePickUps.Count);
            Instantiate(m_PossiblePickUps[index], transform.position, Quaternion.identity);
        }
    }

    private void InstantiateCubes()
    {
        for (int x = 0; x < m_Cells.GetLength(0); x++)
        {
            for (int y = 0; y < m_Cells.GetLength(1); y++)
            {
                if (m_Cells[x, y] == 1)
                {
                    float posX = x - m_RoomData.m_Size.x / 2f + 0.5f;
                    float posY = y - m_RoomData.m_Size.y / 2f + 0.5f;

                    int neighbours = GetNeighboursCount(m_Cells, new Vector2Int(x, y));

                    if (neighbours != 8 || IsBorderCoord(x, y))
                    {
                        var cube = Instantiate(m_BorderCubePrefab, transform);
                        cube.transform.localPosition = new Vector3(posX, posY);
                    }
                    else
                    {
                        AddQuadAtPosition(posX, posY);
                    }
                }
            }
        }
    }

    void SmoothMap(int[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var neighbours = GetNeighboursCount(map, new Vector2Int(x, y));
                if (neighbours < 4)
                {
                    map[x, y] = 0;
                }
                else if (neighbours > 4)
                {
                    map[x, y] = 1;
                }
            }
        }
    }

    void AddQuadAtPosition(float x, float y)
    {
        int vertCount = vertices.Count;

        Vector3 topLeft = new Vector3(x - 0.5f, y + 0.5f, -.5f);
        Vector3 topRight = new Vector3(x + 0.5f, y + 0.5f, -.5f);
        Vector3 botLeft = new Vector3(x - 0.5f, y - 0.5f, -.5f);
        Vector3 botRight = new Vector3(x + 0.5f, y - 0.5f, -0.5f);

        vertices.Add(topLeft);
        vertices.Add(botLeft);
        vertices.Add(botRight);
        vertices.Add(topRight);

        triangles.Add(vertCount + 2);
        triangles.Add(vertCount + 1);
        triangles.Add(vertCount);

        triangles.Add(vertCount + 3);
        triangles.Add(vertCount + 2);
        triangles.Add(vertCount);
    }

    void CreatePassages(int[,] map)
    {
        int mapWidth = map.GetLength(0);
        int mapHeight = map.GetLength(1);

        int xStart = (mapWidth - m_RoomData.m_PassagesWidth) / 2;
        int xEnd = (mapWidth + m_RoomData.m_PassagesWidth) / 2;

        int yStart = (mapHeight - m_RoomData.m_PassagesWidth) / 2;
        int yEnd = (mapHeight + m_RoomData.m_PassagesWidth) / 2;

        int horizontalPassageLength = Mathf.RoundToInt(m_RoomData.m_Size.x / 2f) + 1;
        int verticalPassageLength = Mathf.RoundToInt(m_RoomData.m_Size.y / 2f) + 1;

        if (m_ActiveDirections.ContainsKey(MazeDirection.North))
        {
            for (int x = xStart; x <= xEnd; x++)
            {
                for (int y = (mapHeight - verticalPassageLength); y < mapHeight; y++)
                {
                    map[x, y] = 0;
                }
            }
        }

        if (m_ActiveDirections.ContainsKey(MazeDirection.South))
        {
            for (int x = xStart; x <= xEnd; x++)
            {
                for (int y = 0; y < verticalPassageLength; y++)
                {
                    map[x, y] = 0;
                }
            }
        }

        if (m_ActiveDirections.ContainsKey(MazeDirection.East))
        {
            for (int x = (mapWidth - horizontalPassageLength); x < mapWidth; x++)
            {
                for (int y = yStart; y <= yEnd; y++)
                {
                    map[x, y] = 0;
                }
            }
        }

        if (m_ActiveDirections.ContainsKey(MazeDirection.West))
        {
            for (int x = 0; x < horizontalPassageLength; x++)
            {
                for (int y = yStart; y <= yEnd; y++)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    void EnsureClosedWalls(int[,] map)
    {
        if (!m_ActiveDirections.ContainsKey(MazeDirection.North))
        {
            for (int i = 0; i < m_RoomData.m_Size.x; i++)
            {
                map[i, m_RoomData.m_Size.y - 1] = 1;
            }
        }

        if (!m_ActiveDirections.ContainsKey(MazeDirection.South))
        {
            for (int i = 0; i < m_RoomData.m_Size.x; i++)
            {
                map[i, 0] = 1;
            }
        }

        if (!m_ActiveDirections.ContainsKey(MazeDirection.East))
        {
            for (int i = 0; i < m_RoomData.m_Size.y; i++)
            {
                map[m_RoomData.m_Size.x - 1, i] = 1;
            }
        }

        if (!m_ActiveDirections.ContainsKey(MazeDirection.West))
        {
            for (int i = 0; i < m_RoomData.m_Size.y; i++)
            {
                map[0, i] = 1;
            }
        }
    }

    bool IsBorderCoord(int x, int y)
    {
        return x == 0 || x == m_RoomData.m_Size.x - 1 || y == 0 || y == m_RoomData.m_Size.y - 1;
    }

    int GetNeighboursCount(int[,] map, Vector2Int coord)
    {
        int count = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                var testCoord = coord + new Vector2Int(x, y);
                if (IsInsideMap(testCoord))
                {
                    count += m_Cells[testCoord.x, testCoord.y];
                }
                else
                {
                    count++;
                }
            }
        }

        return count;
    }

    bool IsInsideMap(Vector2Int coord)
    {
        return coord.x >= 0 && coord.x < m_RoomData.m_Size.x && coord.y >= 0 && coord.y < m_RoomData.m_Size.y;
    }

    public void AddPassage(MazeDirection dir, MazeRoom neighbour)
    {
        //if (!m_ActiveDirections.ContainsKey(dir))
        m_ActiveDirections.Add(dir, neighbour);

        if (m_DirectionsList.Contains(dir))
            m_DirectionsList.Remove(dir);
    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.green;
//        foreach (var room in m_ActiveDirections)
//        {
//            Gizmos.DrawLine(transform.position + Vector3.forward, room.Value.transform.position + Vector3.forward);
//        }
//    }
}