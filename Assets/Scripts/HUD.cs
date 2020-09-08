using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public bool enableDiscord = true;
    public DiscordController discord;

    public void Play()
    {

    }

    public void Settings()
    {

    }
    /// <summary>
    /// Quits the game and closes discord (if present)
    /// </summary>
    public void QuitGame()
    {
        if (enableDiscord)
        {
            discord.DRPShutdown();
        }
        Debug.Log("Game Quit.");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
