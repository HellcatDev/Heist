﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ExitArea : MonoBehaviour
{
    public PocketManager pockets;
    public Score score;
    public Image endScreen;
    public TMP_Text percentageText;
    public TMP_Text scoreText;
    public TMP_Text pocketSlotsText;
    public TMP_Text pocketPercentageText;
    public TMP_Text highscoreText;
    public TMP_Text messageText;
    public LogReader reader;

    private int highscore;

    private void Start()
    {
        highscore = reader.LoadIntByKey("highscore");
        Debug.Log(highscore);
    }
    /// <summary>
    /// checks to see if the player has more than 0 items in their pockets, then updates UI if true.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (pockets.pocketSlots > 0f)
            {
                endScreen.gameObject.SetActive(true);
                messageText.text = "Congratulations!";
                if (score.playerScore > highscore)
                {
                    reader.SaveKeyValuePair("highscore", score.playerScore.ToString());
                    messageText.text = "New highscore!";
                    highscore = score.playerScore;
                }
                highscoreText.text = "Highscore: " + highscore.ToString();
                percentageText.text = "You escaped with your pockets " + pockets.pocketPercentage.ToString() + "% full";
                scoreText.text = "Score: " + score.playerScore;
                pocketSlotsText.text = "Pocket Slots: " + pockets.pocketSlots.ToString() + "/" + pockets.maxPocketSlots.ToString();
                pocketPercentageText.text = "Pocket Percentage: " + pockets.pocketPercentage.ToString() + "%";
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}