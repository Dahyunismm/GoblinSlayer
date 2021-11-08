using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject clickBegin;

    public void onClickBegin()
    {
        anim.enabled = true;
        Destroy(clickBegin);
    }

    public void onNewGame()
    {
        Application.LoadLevel("AdventureWorld");
    }

    public void tutorialWorld()
    {
        Application.LoadLevel("GuildWorld");
    }

    public void onQuit()
    {
        Application.Quit();
    }

    public void startMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
