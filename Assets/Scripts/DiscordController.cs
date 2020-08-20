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
		if (discordRichPresence)
		{
			discord = new Discord.Discord(743408930482552852, (ulong)CreateFlags.NoRequireDiscord);
			discordPresent = true;
			ActivityManager activityManager = discord.GetActivityManager();
			Activity activity = new Activity
			{
				State = "In Main Menu",
				Details = "Debugging :)",
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
	}

  //  static void UpdateActivity(Discord.Discord discord, Lobby lobby)
  //  {
  //      var activityManager = discord.GetActivityManager();
  //      var lobbyManager = discord.GetLobbyManager();

  //      var activity = new Activity
		//{
		//	State = "Heist",
		//	Details = "Debugging! Yay!",
		//	Assets =
		//	{
		//		LargeImage = "heist_logo",
		//		LargeText = "Heist",
		//		SmallImage = "map-gas_station",
		//		SmallText = "Gas Station Robbery",
		//	}
		//};

		//activityManager.UpdateActivity(activity, result =>
  //      {
  //          Debug.Log("Discord Rich Presence active.");
  //      });
  //  }

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
			discordPresent = true;
			discord.Dispose();
			Debug.Log("Disposed.");
		}
	}
}