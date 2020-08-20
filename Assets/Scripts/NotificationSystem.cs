using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NotificationSystem : MonoBehaviour
{
    public TMP_Text notificationTitle;
    public TMP_Text notificationDescription;
    public GameObject notificationObject;
    public Image progressBar;

    private DiscordController discordController;
    private string notificationToRemove;
    private bool isHovering = false;
    private float sinceLastNotification = 0f;
    private Dictionary<string, string> notificationQueue = new Dictionary<string, string>();

    private void Start()
    {
        discordController = GameObject.Find("DiscordRichPresenceManager").GetComponent<DiscordController>();
        if (discordController.discordPresent == false)
        {
            QueueNotification("Discord not present", "Install discord to enable Rich Presence");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QueueNotification("New notification", "yeet yeet yeet yeet yeet yeet yeet");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            QueueNotification("Text sizing test", "this is a text sizing test to check how the text will wrap on to mutliple lines while also scaling elements and the background.");
        }
        UpdateGraphics();
        UpdateQueue();
    }

    /// <summary>
    /// QueueNotification allows you to add a notification into the notification queue.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    public void QueueNotification(string title, string description)
    {
        if (notificationQueue.ContainsKey(title) == false)
        {
            notificationQueue.Add(title, description);
        }
        else
        {
            Debug.LogError("A notification with that key already exists!", notificationObject);
        }
    }

    /// <summary>
    /// This function will dismiss the latest notification.
    /// </summary>
    public void CloseNotification()
    {
        sinceLastNotification = Time.time - 1f;
    }

    /// <summary>
    /// This function updates the notification queue every 5 seconds (called every frame). The notification queue
    /// will check if 5 seconds has passed and will then send in the next notification. When 5 seconds has passed for the
    /// new notification, it will remove it from the queue and restart from the beginning and repeat the process.
    /// </summary>
    private void UpdateQueue()
    {
        if (isHovering == true)
        {
            if (sinceLastNotification - Time.time < 5f)
            {
                sinceLastNotification += 10f * Time.deltaTime;
            }
        }
        if (Time.time >= sinceLastNotification) // If its been 5 seconds since last notification
        {
            if (notificationQueue.Count > 0)
            {
                sinceLastNotification = Time.time + 5f;
                notificationObject.SetActive(true);
                int count = 0;
                foreach (KeyValuePair<string, string> i in notificationQueue)
                {
                    if (count < 1)
                    {
                        notificationTitle.text = i.Key;
                        notificationDescription.text = i.Value;
                        notificationToRemove = i.Key;
                    } else { break; }
                    count++;
                }
                notificationQueue.Remove(notificationToRemove);
            }
            else
            {
                notificationObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// This function updates the graphical elements of the notification. The function will calculate a percentage
    /// to fill up the progress bar. Next it will calculate the needed size that the background has to be to allow
    /// the text to fit. Finally it will change the rect transform of both the description, title and background of the notification.
    /// </summary>
    private void UpdateGraphics()
    {
        // Calculations for progress bar.
        float count = (sinceLastNotification - Time.time) / 5;
        progressBar.fillAmount = count;

        // Calculations for background and text sizing.
        float paddingSize = 8f;
        Vector2 backgroundSize;
        Vector2 descriptionSize;

        descriptionSize = new Vector2(510, notificationDescription.preferredHeight);
        backgroundSize = new Vector2(612.5f, (notificationTitle.rectTransform.sizeDelta.y + notificationDescription.preferredHeight) + (paddingSize * 2f));

        notificationDescription.gameObject.GetComponent<RectTransform>().sizeDelta = descriptionSize;
        notificationTitle.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(notificationTitle.preferredWidth, 31f);
        notificationObject.GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }

    /// <summary>
    /// Function for event triggers. Changes isHovering to true.
    /// </summary>
    public void MouseEnter()
    {
        isHovering = true;
    }

    /// <summary>
    /// Function for event triggers. Changes isHovering to false.
    /// </summary>
    public void MouseExit()
    {
        isHovering = false;
    }
}
