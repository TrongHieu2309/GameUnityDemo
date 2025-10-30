using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour {
    private PlayerController playerController;
    public void Play()
    {
        SceneManager.LoadScene(1);
        playerController = PlayerController.instance;
        if (playerController != null)
        {
            playerController.PlayingGame();
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}