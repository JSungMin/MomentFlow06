using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualExplain : MonoBehaviour {

	public Image explainStick;
	public Text explainText;

	public PlayerInfo playerInfo;

	public Transform followTarget;

	public Vector3 offsetVector;

	public float offset = 0.25f;

	void Start()
	{
		playerInfo = GameObject.FindObjectOfType<PlayerInfo> ();
		explainStick = GetComponent<Image> ();
		explainText = transform.GetComponentInChildren<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (null != followTarget) {
			transform.position = followTarget.position + offsetVector;
			if (followTarget.CompareTag ("Enemy")) {
				var notUseable = followTarget.GetComponent<EnemyInfo> ().isDead;
				if (notUseable) {
					var alpha = Mathf.Clamp (explainStick.color.a - Time.unscaledDeltaTime * 2,0,1);

					explainStick.color = new Color (explainStick.color.r, explainStick.color.g, explainStick.color.b, alpha);
					explainText.color = new Color (explainText.color.r, explainText.color.g, explainText.color.b, alpha);
					return;

				}
			}
		}

		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
		Vector3 playerPos = Camera.main.WorldToViewportPoint (playerInfo.transform.position);


		if (Mathf.Abs (pos.x - playerPos.x) <= offset && Mathf.Abs (pos.y - playerPos.y) <= offset) {
			pos.z = 0;

			var alpha = Mathf.Clamp (explainStick.color.a + Time.unscaledDeltaTime * 2,0,1);

			explainStick.color = new Color (explainStick.color.r, explainStick.color.g, explainStick.color.b, alpha);
			explainText.color = new Color (explainText.color.r, explainText.color.g, explainText.color.b, alpha);
		} 
		else {
			var alpha = Mathf.Clamp (explainStick.color.a - Time.unscaledDeltaTime * 2,0,1);

			explainStick.color = new Color (explainStick.color.r, explainStick.color.g, explainStick.color.b, alpha);
			explainText.color = new Color (explainText.color.r, explainText.color.g, explainText.color.b, alpha);
		}
	}
}
