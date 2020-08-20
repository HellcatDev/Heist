using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public DiscordController discord;

    public void Play()
    {

    }

    public void Settings()
    {

    }

    public void QuitGame()
    {
        discord.DRPShutdown();
        Debug.Log("Game Quit.");
        Application.Quit();
    }
}
