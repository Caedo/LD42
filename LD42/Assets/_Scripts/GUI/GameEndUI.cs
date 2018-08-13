using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{

    public Image m_GameOverPanel;
    public Image m_GameWonPanel;

    private void Start()
    {
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
        BossController.OnBossDestroyed += OnBossDeath;
    }

    void OnPlayerDeath()
    {
        m_GameOverPanel.gameObject.SetActive(true);
    }

    void OnBossDeath()
    {
        m_GameWonPanel.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
