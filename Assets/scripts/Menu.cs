using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false, isAlive = true;
    public static bool inMain;

    [SerializeField]
    GameObject pauseMenuUI;
    [SerializeField]
    GameObject mainMenuUI;
    [SerializeField]
    GameObject gameOverMenuUI;

    [SerializeField]
    PlayerDo player;

    void Start()
    {
        // player = GetComponent<PlayerDo>();
        EnterMain();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !inMain && isAlive)
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }


    // public void ReturnToMenu()
    // {
    //     SceneManager.LoadScene("Menu");
    // }


    public void Resume ()
    {
        player.LockMouse();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
    }
    public void Pause ()
    {
        player.UnlockMouse();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Pause");
    }
    public void EnterMain()
    {
        pauseMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        player.UnlockMouse();
        mainMenuUI.SetActive(true);
        inMain = true;
        Time.timeScale = 0f;
        Debug.Log("Enter Main");
    }
    public void ExitMain()
    {
        player.LockMouse();
        mainMenuUI.SetActive(false);
        inMain = false;
        Time.timeScale = 1f;
        Debug.Log("Exit Main");
    }
// specefics
    public void PauseFromMain()
    {
        pauseMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
        Debug.Log("settings");
    }

        public void MainFromPause()
    {
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Debug.Log("main");
    }

        public void GameOver()
    {
        player.UnlockMouse();
        pauseMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isAlive = false;
        Debug.Log("Game Over");
    }

        public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}