using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    GameManager gameManager;
    public bool canPause;

    public UnityEvent escPressed;

    public void PlayGame()
    {
        print("Loading New Game...");
        SceneManager.LoadScene(1);
        canPause = true;
    }

    public void ReturnToMainMenu()
    {
        print("Returning to Main Menu...");
        SceneManager.LoadScene(0);
        canPause = false;
    }

    public void Resume()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Game Manager : GameManager on the Scene Manager is NULL.");
        }
        gameManager.gameSpeed = 1.0f;
        Cursor.visible = false;
    }

    public void PauseMenu()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Game Manager : GameManager on the Scene Manager is NULL.");
        }
        gameManager.gameSpeed = 0.0f;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        print("Quitting game...");
        Application.Quit();
    }

    void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                escPressed.Invoke();
            }
        }
    }
}
