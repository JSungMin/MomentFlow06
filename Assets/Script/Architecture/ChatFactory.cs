using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatFactory : MonoBehaviour {
	private static ChatFactory instance;

	public static ChatFactory Instance {
		get {
			if (instance == null)
			{
				var newWeaponFactory = new GameObject ("ChatFactory");
				instance = newWeaponFactory.AddComponent<ChatFactory> ();
			}
			return instance;
		}
	}

	public List<Chat> chatList;

	public static bool isChatting;

	public void Awake()
	{
		instance = this;
		LoadChatInfo ();
	}

	public void LoadChatInfo()
	{
		Debug.Log (SceneManager.GetActiveScene().name + "ChatXml");
		var doc = XmlManager.Instance.GetXmlDocument (SceneManager.GetActiveScene().name + "ChatXml");
		if (null == doc) {
			Debug.LogError ("Doc is Null");
			return;
		}
		var nodes = doc.SelectNodes ("ChatSet/Chat");

		foreach (XmlNode node in nodes) {
			var chat = new Chat ();
			chat.title = (node.SelectSingleNode ("Title").InnerText);
			chat.page = int.Parse(node.SelectSingleNode("Page").InnerText);
			chat.option = node.SelectSingleNode ("Option").InnerText;
			chat.nextPage = int.Parse(node.SelectSingleNode("NextPage").InnerText);
			chat.content = (node.SelectSingleNode("Content").InnerText);

			chatList.Add (chat);
		}
	}

	public void LoadChatInfo(string chatName)
	{
		Debug.Log (chatName + "ChatXml");
		var doc = XmlManager.Instance.GetXmlDocument (SceneManager.GetActiveScene().name + "ChatXml");
		if (null == doc) {
			Debug.LogError ("Doc is Null");
			return;
		}
		var nodes = doc.SelectNodes ("ChatSet/Chat");

		foreach (XmlNode node in nodes) {
			var chat = new Chat ();
			chat.title = (node.SelectSingleNode ("Title").InnerText);
			chat.page = int.Parse(node.SelectSingleNode("Page").InnerText);
			chat.nextPage = int.Parse(node.SelectSingleNode("NextPage").InnerText);
			chat.content = (node.SelectSingleNode("Content").InnerText);

			chatList.Add (chat);
		}
	}

	public List<Chat> GetEqualTitlePages (string title)
	{
		Debug.Log ("GetEqual");
		List<Chat> chats = new List<Chat> ();

		chatList.ForEach (delegate(Chat chat) {
			Debug.Log(chat.title);
			if (chat.title == title)
			{
				chats.Add(chat);
			}
		});
		return chats;
	}

	public Chat GetPage(int page)
	{
		Chat returnChat = null;
		chatList.ForEach (delegate(Chat chat) {
			if (chat.page == page)
			{
				returnChat = chat;
			}
		});
		return returnChat;
	}
}
