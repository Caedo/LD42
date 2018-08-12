using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze m_Maze;
    public PlayerController m_PlayerPrefab;

    private void Start()
    {
        m_Maze.GenerateMap();
        var startRoomTransform = m_Maze.StartRoom.transform;
        var player = Instantiate(m_PlayerPrefab, startRoomTransform.position, Quaternion.identity);
        
        //JUST FOR NOW!
        Camera.main.GetComponent<SimpleCameraFollow>().m_Target = player.transform;
    }
}
