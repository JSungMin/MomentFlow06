using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueChat : NPCAction {
	public Vector3 fromSize;
	public Vector3 toSize;

	public int tryCount = 0;

	private float showDelayTimer = 0;
	public float showDelay = 0.25f;
	public string commnicationTitle;
	public List<Chat> chats;
	public int pageNumber = 0;
	public float flipTimer = 0;

	public GameObject dialogueObject;

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
			flipTimer += Time.unscaledDeltaTime;
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
		dialogueObject.transform.localScale = Vector3.zero;
	
		while (showDelayTimer <= showDelay)
		{
			showDelayTimer += Time.unscaledDeltaTime;
			dialogueObject.transform.localScale = Vector3.Lerp (dialogueObject.transform.localScale, toSize, showDelayTimer / showDelay);
			yield return null;
		}
		dialogueObject.transform.localScale = Vector3.one;
		showDelayTimer = 0;
	}

	private IEnumerator HideChat ()
	{
		while (showDelayTimer <= showDelay)
		{
			dialogueObject.transform.localScale = Vector3.Lerp (dialogueObject.transform.localScale, fromSize, showDelayTimer / showDelay);
			showDelayTimer += Time.unscaledDeltaTime;
			yield return null;
		}
		dialogueObject.transform.localScale = Vector3.zero;
		showDelayTimer = 0;
	}

	public override void TryAction (ref int tryCount)
	{
		if (IsSatisfied (ref tryCount))
		{
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

	public void TryAction ()
	{
		if (IsSatisfied ())
		{
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
		if (IsActionFinished) {
			FinishedAction ();
			return;
		}
		if (pageNumber < chats.Count)
		{
			DoAction ();
		}
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

	protected bool IsSatisfied ()
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
		dialogueObject.GetComponentInChildren<Text> ().text = chats [pageNumber].content;
		if (chats[pageNumber].option == "Bold")
		{
			dialogueObject.GetComponentInChildren<Text> ().fontStyle = FontStyle.Bold;
		}
		NextPage ();
	}

	public override void CancelAction ()
	{
		StartCoroutine (HideChat ());
		IsActionFinished = false;
		pageNumber = 0;
	}

	protected override void FinishedAction ()
	{
		Debug.Log ("Finish");
		StartCoroutine (HideChat());
		finishEvent.Invoke ();
	}
}
