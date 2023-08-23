using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneDoor : Door
{
    public override void Action() 
    {
        GameManager.Instance.LoadScene(GameManager.Scenes.DeckDay1);
    }
}
