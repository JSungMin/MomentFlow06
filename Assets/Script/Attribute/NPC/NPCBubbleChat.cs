using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBubbleChat : NPCAction {
	public string commnicationTitle;
	public List<Chat> chats;
	public int pageNumber = 0;
	public float flipTimer = 0;

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
		flipTimer = 0;
		pageNumber += 1;
	}

	private void PreviousPage()
	{
		flipTimer = 0;
		pageNumber -= 1;
	}

	public override void TryAction (ref int tryCount)
	{
		if (IsSatisfied (ref tryCount))
		{
			DoAction ();
		}
	}

	protected override bool IsSatisfied (ref int tryCount)
	{
		Debug.Log ("Test NPC Satisfied");
		if (actionCirlce >= maxActionCircle && maxActionCircle != 0)
			return false;
		if (tryCount < tryCountForActivate)
			return false;
		
		if (pageNumber < chats.Count)
			return true;
		
		IsActionFinished = true;
		actionCirlce += 1;
		if (actionCirlce < maxActionCircle || maxActionCircle == 0) 
		{
			tryCount = tryCountForActivate;
			pageNumber = 0;
		}
		return false;
	}

	protected override void DoAction ()
	{
		Debug.Log (chats[pageNumber].content);
		NextPage ();
	}

	public override void CancelAction ()
	{
		throw new System.NotImplementedException ();
	}
}
