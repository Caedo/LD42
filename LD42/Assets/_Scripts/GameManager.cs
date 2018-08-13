using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze m_Maze;
    public PlayerController m_PlayerPrefab;
    public GameObject m_Enemy;

    Vector3 pos;

    private void Start()
    {
        m_Maze.GenerateMap();
        var startRoomTransform = m_Maze.StartRoom.transform;
        pos = startRoomTransform.position;
        var player = Instantiate(m_PlayerPrefab, startRoomTransform.position, Quaternion.identity);


        //JUST FOR NOW!
        Camera.main.GetComponent<SimpleCameraFollow>().m_Target = player.transform;

        InvokeRepeating("SpawnEnemy", 10.0f, 0.5f);
    }

    private void SpawnEnemy()
    {
        Instantiate(m_Enemy, pos, Quaternion.identity);
    }
}
