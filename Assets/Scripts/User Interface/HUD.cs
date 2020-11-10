using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public DiscordController discord;

    /// <summary>
    /// Quits the game and closes discord (if present)
    /// </summary>
    public void QuitGame()
    {
        if (DiscordController.discordPresent)
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
