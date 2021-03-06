using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBubbleChat : NPCAction {
	public Vector3 fromSize;
	public Vector3 toSize;

	private float showDelayTimer = 0;
	public float actionDelay = 0;
	private float actionDelayTimer = 0;
	public float showDelay = 0.25f;
	public string commnicationTitle;
	public List<Chat> chats;
	public int pageNumber = 0;
	public float flipTimer = 0;

	public GameObject bubbleChatObject;

	// Use this for initialization
	void Start () {
		chats = ChatFactory.Instance.GetEqualTitlePages (commnicationTitle);
	}

	public void Update ()
	{
		AutoFlip ();
	}

	private void AutoFlip ()
	{
		if (pageNumber >= chats.Count)
			return;
		if (chats [pageNumber].autoFlipTime != 0) {
			var autoFlipTime = chats [pageNumber].autoFlipTime;
			flipTimer += Time.deltaTime;
			if (flipTimer >= autoFlipTime) {
				NextPage ();
			}
		}
	}

	private void NextPage()
	{
		StartCoroutine (ShowChat());
		flipTimer = 0;
		pageNumber += 1;
	}

	private void PreviousPage()
	{
		flipTimer = 0;
		pageNumber -= 1;
	}

	private IEnumerator ShowChat ()
	{
		bubbleChatObject.transform.localScale = Vector3.zero;
		Debug.Log ("Show");

		while (actionDelayTimer <= actionDelay)
		{
			actionDelayTimer += Time.unscaledDeltaTime;
			yield return null;
		}

		while (showDelayTimer <= showDelay)
		{
			showDelayTimer += Time.unscaledDeltaTime;
			bubbleChatObject.transform.localScale = Vector3.Lerp (bubbleChatObject.transform.localScale, toSize, showDelayTimer / showDelay);
			yield return null;
		}
		bubbleChatObject.transform.localScale = toSize;
		showDelayTimer = 0;
	}

	private IEnumerator HideChat ()
	{
		while (showDelayTimer <= showDelay)
		{
			bubbleChatObject.transform.localScale = Vector3.Lerp (bubbleChatObject.transform.localScale, fromSize, showDelayTimer / showDelay);
			showDelayTimer += Time.unscaledDeltaTime;
			yield return null;
		}
		bubbleChatObject.transform.localScale = fromSize;
		showDelayTimer = 0;
	}

	public override void TryAction (ref int tryCount)
	{
		if (IsSatisfied (ref tryCount))
		{
			if (tryCount == tryCountForActivate) 
			{
				startEvent.Invoke ();
			}
			DoAction ();
		}
		else
		{
			if (IsActionFinished)
			{
				FinishedAction ();
			}
		}
	}

	public override void ForceAction ()
	{
		if (IsActionFinished)
		{
			FinishedAction ();
			return;
		}

		DoAction ();
	}

	protected override bool IsSatisfied (ref int tryCount)
	{
		Debug.Log ("Test NPC Satisfied");
		if (actionCirlce >= maxActionCircle && maxActionCircle != 0) {
			return false;
		}
		if (tryCount < tryCountForActivate)
			return false;
		
		if (pageNumber < chats.Count)
			return true;
	
		actionCirlce += 1;
		if (actionCirlce < maxActionCircle || maxActionCircle == 0) 
		{
			tryCount = tryCountForActivate;
			pageNumber = 0;
		}
		IsActionFinished = true;
		return false;
	}

	protected override void DoAction ()
	{
		Debug.Log (pageNumber);
		bubbleChatObject.GetComponentInChildren<Text> ().text = chats [pageNumber].content;
		NextPage ();
	}

	public override void CancelAction ()
	{
		StopCoroutine (ShowChat ());
		StartCoroutine (HideChat ());
		IsActionFinished = false;
		actionDelayTimer = 0;
		pageNumber = 0;
	}

	protected override void FinishedAction ()
	{
		StartCoroutine (HideChat());
		actionDelayTimer = 0;
		finishEvent.Invoke ();
	}
}
