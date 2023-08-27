using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneDoor : Door
{
    public string currentSceneName;
    public string nextSceneName;

    public override void Action() 
    {
        GameObject.Find("AvatarPlayer").SetActive(false);
        SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
