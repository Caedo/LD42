using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public Maze m_Maze;
    public PlayerController m_PlayerPrefab;

    public BossController m_BossPrefab;
    public Cinemachine.CinemachineVirtualCamera m_VirtualCamera;

    public GameObject m_Enemy;

    Vector3 pos;


    private void Awake()
    {
        m_Maze.GenerateMap();
        var startRoomTransform = m_Maze.StartRoom.transform;
        pos = startRoomTransform.position;
        var player = Instantiate(m_PlayerPrefab, startRoomTransform.position, Quaternion.identity);


        //JUST FOR NOW!
        Camera.main.GetComponent<SimpleCameraFollow>().m_Target = player.transform;


        var endRoomTransform = m_Maze.EndRoom.transform;
        var cube = Instantiate(m_BossPrefab, endRoomTransform.position, Quaternion.identity);

        BossController.OnBossDestroyed += EndGame;
        m_VirtualCamera.Follow = player.transform;
    }

    void EndGame()
    {
        Debug.Log("End Game");
        InvokeRepeating("SpawnEnemy", 10.0f, 0.5f);
    }

    private void SpawnEnemy()
    {
        Instantiate(m_Enemy, pos, Quaternion.identity);
    }
}
