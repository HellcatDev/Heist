using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
	public Discord.Discord discord;
	public NotificationSystem notificationSystem;
	public bool discordRichPresence = true;
	public bool discordPresent = false;

	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (discordRichPresence)
		{
			StartDRP();
		}
	}

	public void StartDRP()
	{
		discord = new Discord.Discord(743408930482552852, (ulong)CreateFlags.NoRequireDiscord);
		discordPresent = true;
		ActivityManager activityManager = discord.GetActivityManager();
		Activity activity = new Activity
		{
			Details = "",
			State = "In Main Menu",
			Timestamps =
			{
				Start = DateTimeOffset.Now.ToUnixTimeSeconds(),
			},
			Assets =
			{
				LargeImage = "heist_logo",
				LargeText = "Heist",
				SmallImage = "map-gas_station",
				SmallText = "Gas Station Robbery",
			}
		};
		activityManager.UpdateActivity(activity, (res) =>
		{
			if (res == Result.Ok)
			{
				Debug.Log("Discord Rich Presence active.");
			}
		});
	}

	public void UpdateActivity()
	{
		if (discordPresent == true)
		{
			var activityManager = discord.GetActivityManager();
			//var lobbyManager = discord.GetLobbyManager();

			var activity = new Activity
			{
				Details = "Playing Solo",
				State = "In Game",
				Timestamps =
				{
					Start = DateTimeOffset.Now.ToUnixTimeSeconds(),
					End = DateTimeOffset.Now.ToUnixTimeSeconds() + 900,
				},
				Assets =
				{
					LargeImage = "map-gas_station",
					LargeText = "Gas Station Robbery",
					SmallImage = "difficulty-nightmare",
					SmallText = "Nightmare Difficulty",
				},
				Party =
				{
				   Size =
					{
						CurrentSize = 1,
						MaxSize = 1,
					},
				}
			};
			activityManager.UpdateActivity(activity, (res) =>
			{
				if (res == Result.Ok)
				{
					Debug.Log("Discord Rich Presence updated.");
				}
			});
		}
		else
		{
			Debug.Log("Discord is not present.");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (discordPresent == true)
		{
			discord.RunCallbacks();
		}
	}

	public void DRPShutdown()
	{
		if (discordPresent == true)
		{
			Debug.Log("Disposing DRP...");
			discordPresent = false;
			discord.Dispose();
			Debug.Log("Disposed.");
		}
	}
}