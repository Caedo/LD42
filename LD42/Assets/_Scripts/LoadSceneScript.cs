using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void LoadSceneByName(string SceneName)
    {
        Debug.Log(SceneName);
        SceneManager.LoadScene(SceneName);
    }
}
