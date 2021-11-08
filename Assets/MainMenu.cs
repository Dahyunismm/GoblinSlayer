using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject clickBegin;

    /// <summary>
    /// Enabling animation for start menu when the player clicks the screen
    /// </summary>
    public void onClickBegin()
    {
        anim.enabled = true;
        Destroy(clickBegin);
    }

    /// <summary>
    /// Loads the scene adventure world.
    /// </summary>
    public void onNewGame()
    {
        SceneManager.LoadScene("AdventureWorld");
    }

    /// <summary>
    /// Loads the scene adventure world.
    /// </summary>
    public void tutorialWorld()
    {
        SceneManager.LoadScene("GuildWorld");
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void onQuit()
    {
        Application.Quit();
    }

    public void startMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
