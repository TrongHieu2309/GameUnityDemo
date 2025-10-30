using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour {
    public void Play()
    {
        SceneManager.LoadScene(1);
        PlayerController.instance.PlayingGame();
    }
    
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}