using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame(bool param) {
        SceneManager.UnloadSceneAsync("MainMenu");
        GameManager.Instance.LoadScene(GameManager.Scenes.RoomDay1);
    }

    public void ToggleCredits(GameObject go) {
        go.SetActive(!go.activeInHierarchy);
    }


}
