using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public DiscordController discordController;

    public void LoadNewScene(string sceneName)
    {
        discordController.UpdateActivity();
        SceneManager.LoadScene(sceneName);
    }
}
