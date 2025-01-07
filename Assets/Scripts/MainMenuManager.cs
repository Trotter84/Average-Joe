using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{


    public void PlayGame()
    {
        print("Loading New Game...");
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        print("Returning to Main Menu...");
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        print("Quitting game...");
        Application.Quit();
    }
}
