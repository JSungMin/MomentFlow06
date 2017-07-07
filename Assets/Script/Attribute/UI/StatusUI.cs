using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {
	public static StatusUI instance;

	public GameObject statusPane;

	public PlayerDetectState detectState;
	public Image[] detectStateImage; // 순서 detectState Value에 맞출 것

	public Transform hpPool;
	public int hpPoolIndex;

	public Image mpGaugeImage;

	private PlayerInfo playerInfo;
	private EquiptInfo playerEquiptInfo;

	public Transform itemListTransform;
	public int itemMaxRow = 3;

	// Use this for initialization
	void Start () {
		instance = this;
		if (null == statusPane)
			statusPane = gameObject;
		playerInfo = GameObject.FindObjectOfType <PlayerInfo> ();
		playerEquiptInfo = playerInfo.equiptInfo;
	}

	public void Update()
	{
		UpdateDetectState ();
		UpdateMPGauge ();
		ShowPlayerItems ();
	}

	public void SetHpImage ()
	{
		hpPoolIndex = Mathf.RoundToInt (playerInfo.hp / playerInfo.maxHp * 4) ;

		if (hpPoolIndex < 0)
			return;

		for (int i = 0; i < hpPoolIndex; i++)
		{
			hpPool.GetChild (i).gameObject.SetActive (true);
		}

		for (int i = hpPoolIndex; i < 4; i++)
		{
			hpPool.GetChild (i).gameObject.SetActive (false);
		}
	}

	public void UpdateDetectState ()
	{
		var enemiesState = GameObject.FindObjectsOfType<EnemyInfo> ();
		var stateIndex = 0;

		for (int i = 0; i < enemiesState.Length; i++)
		{
			//Set To Warning
			if (enemiesState [i].detectGauge > 0 && 
				enemiesState [i].detectGauge < 1 && 
				!enemiesState [i].isDead &&
				stateIndex < 1)
			{
				stateIndex = 1;
			}
			else if (enemiesState [i].isDetect && 
				!enemiesState [i].isDead)
			{
				stateIndex = 2;
				break;
			}
		}
		if (stateIndex == 0 && detectState != PlayerDetectState.Safe) {
			detectStateImage [0].gameObject.SetActive (true);
			detectStateImage [1].gameObject.SetActive (false);
			detectStateImage [2].gameObject.SetActive (false);
		} 
		else if (stateIndex == 1 && detectState != PlayerDetectState.Warning) {
			detectStateImage [0].gameObject.SetActive (false);
			detectStateImage [1].gameObject.SetActive (true);
			detectStateImage [2].gameObject.SetActive (false);
		} 
		else if (stateIndex == 2 && detectState != PlayerDetectState.Detected){
			detectStateImage [0].gameObject.SetActive (false);
			detectStateImage [1].gameObject.SetActive (false);
			detectStateImage [2].gameObject.SetActive (true);
		}
		detectState = (PlayerDetectState)stateIndex;
	}

	public void UpdateMPGauge()
	{
		mpGaugeImage.fillAmount = playerInfo.mana.ManaPoint / playerInfo.mana.MaxManaPoint;
	}

	List<ItemInfoStruct> showedItemList = new List<ItemInfoStruct>();

	private void ShowPlayerItems ()
	{
		for (int i = 0; i < playerEquiptInfo.itemInfoList.Count; i++)
		{
			if (!showedItemList.Contains (playerEquiptInfo.itemInfoList [i]))
			{
				var newImage = new GameObject ("ItemElement");
				var spriteRenderer = newImage.AddComponent<Image> ();
				spriteRenderer.sprite = ItemFactory.Instance.GetItemInfo<Key> (playerEquiptInfo.itemInfoList [i].itemId).sprite;
				newImage.transform.position = itemListTransform.position;
				newImage.transform.parent = itemListTransform;
				newImage.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
				showedItemList.Add (playerEquiptInfo.itemInfoList [i]);
			}
		}
	}
}
