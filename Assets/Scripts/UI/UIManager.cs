using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseGameUI;
    [SerializeField] private GameObject winGameUI;

    public bool isPause;
    public bool isWinning;

    void Awake()
    {
        instance = this;
        gameOverUI.SetActive(false);
        pauseGameUI.SetActive(false);
        winGameUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGameUI.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    #region Game Over
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        isWinning = false;
        Health.instance.isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region Pause Game
    private void PauseGame(bool status)
    {
        pauseGameUI.SetActive(status);

        if (status)
        {
            isPause = true;
            Time.timeScale = 0;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        PauseGame(false);
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.05f);
    }
    #endregion

    public void WinGame()
    {
        isWinning = true;
        winGameUI.SetActive(true);
    }
}
