using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze m_Maze;
    public PlayerController m_PlayerPrefab;
    public EndGameCube m_EndGameCubePrefab;

    private void Awake()
    {
        m_Maze.GenerateMap();
        var startRoomTransform = m_Maze.StartRoom.transform;
        var player = Instantiate(m_PlayerPrefab, startRoomTransform.position, Quaternion.identity);
        
        //JUST FOR NOW!
        Camera.main.GetComponent<SimpleCameraFollow>().m_Target = player.transform;

        var endRoomTransform = m_Maze.EndRoom.transform;
        var cube = Instantiate(m_EndGameCubePrefab, endRoomTransform.position, Quaternion.identity);

        cube.OnCubeDestroyed += EndGame;
    }

    void EndGame()
    {
        Debug.Log("End Game");
    }
}
