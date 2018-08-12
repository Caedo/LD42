using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsHandler : MonoBehaviour
{
    public string SceneName;
    public string MenuScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == SceneName)
        {
            Invoke("LoadMenu", 35.0f);
        }
    }
    
    private void LoadMenu()
    {
        SceneManager.LoadScene(MenuScene);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CancelInvoke("LoadMenu");
            LoadMenu();
        }
    }
}
