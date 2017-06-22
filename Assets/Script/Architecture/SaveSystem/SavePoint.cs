using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SavePoint : MonoBehaviour {

	public static string sceneName;

	public void Start()
	{
		sceneName = SceneManager.GetActiveScene ().name;
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Player"))
		{
			SaveData ();
		}
	}

	public void SaveData()
	{
		var playerInfo = GameObject.FindObjectOfType<PlayerInfo> ();
		SavePlayerInfo (playerInfo);
		var enemies = GameObject.FindObjectsOfType<EnemyInfo> ();
		for (int i = 0; i < enemies.Length; i++)
		{
			Saver.SaveTransform (enemies [i].name, enemies [i].transform);
		}
		var interactables = GameObject.FindObjectsOfType<InteractableObject> ();
		for (int i = 0; i < interactables.Length; i++)
		{
			
		}
			
	}

	public static void LoadData()
	{
		var playerInfo = GameObject.FindObjectOfType<PlayerInfo> ();
		Loader.LoadTransform (playerInfo.gameObject.name,playerInfo.transform);
		playerInfo.hp = Loader.LoadFloat (playerInfo.gameObject.name, "HP");
		playerInfo.equiptInfo = Loader.LoadEquiptInfo (playerInfo.name, playerInfo.equiptInfo);
	}

	void SavePlayerInfo (PlayerInfo playerInfo)
	{
		Saver.SaveTransform (playerInfo.gameObject.name, playerInfo.playerTransform);
		Saver.SaveFloat (playerInfo.gameObject.name, "HP", playerInfo.hp);
		Saver.SaveEquiptInfo (playerInfo.gameObject.name, playerInfo.equiptInfo);
	}
		
}
