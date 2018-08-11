using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : MonoBehaviour
{
    public float m_FillPercent;
    public Vector2Int m_Coord;

    private List<MazeDirection> m_DirectionsList;
    private Dictionary<MazeDirection, MazeRoom> m_ActiveDirections = new Dictionary<MazeDirection, MazeRoom>();

    public bool IsFullyInitialized
    {
        get { return m_DirectionsList.Count == 0; }
    }

    public MazeDirection NextRandomDirection
    {
        get
        {
            var kappa = m_DirectionsList[0];
            m_DirectionsList.RemoveAt(0);
            return kappa;
        }
    }

    public void Initialize(Vector2Int coord, float fillPercent)
    {
        m_Coord = coord;
        m_FillPercent = fillPercent;

        m_DirectionsList = MazeHelper.GetRandomDirectionsQueue();

        GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, fillPercent);
    }

    public void AddPassage(MazeDirection dir, MazeRoom neighbour)
    {
        if(!m_ActiveDirections.ContainsKey(dir))
            m_ActiveDirections.Add(dir, neighbour);
        
        
        //m_DirectionsList.Remove(dir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var room in m_ActiveDirections)
        {
            Gizmos.DrawLine(transform.position + Vector3.forward, room.Value.transform.position + Vector3.forward);
        }
    }
}