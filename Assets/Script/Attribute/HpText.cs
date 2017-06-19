using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    private Text textComponent;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
        playerInfo = GameObject.FindObjectOfType<PlayerInfo>();
    }

    private void Update()
    {
        textComponent.text = "hp:" + playerInfo.hp.ToString();
    }
}