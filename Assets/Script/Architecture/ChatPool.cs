using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPool : MonoBehaviour {
	private static ChatPool instance;

	public static ChatPool Instance{
		get {
			if (instance == null) {
				var newChatPool = new GameObject ("ChatPool");
				instance = newChatPool.AddComponent<ChatPool> ();
			}
			return instance;
		}
	}
		
	public List<GameObject> emptyChatList;

	public void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}

	public void InitChats()
	{
		for (int i = 0; i < 200; i++)
		{
			GameObject newChat = Resources.Load ("Prefabs/Chats/Chat") as GameObject;
			newChat.transform.parent = transform;
			emptyChatList.Add (newChat);
		}
	}

	public Chat BorrowEmptyChat ()
	{
		for (int i = 0 ; i < 200; i++)
		{
			if (!emptyChatList[i].activeSelf)
			{
				emptyChatList [i].SetActive (true);
				return emptyChatList [i].GetComponent<Chat> ();
			}
		}
		return null;
	}

	public void ReturnChat(GameObject usedChat)
	{
		ClearChat (usedChat.GetComponent<Chat> ());
		usedChat.SetActive (false);
	}

	private void ClearChat(Chat chat)
	{
		chat.title = "";
		chat.page = 0;
		chat.nextPage = 0;
		chat.content = "";
		chat.autoFlipTime = 0;
	}
}
