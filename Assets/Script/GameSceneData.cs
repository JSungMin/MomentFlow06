using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSceneData
{
    public static GameObject player = GameObject.FindWithTag("Player");
    public static GameObject playerAction = GameObject.Find("PlayerAction");
    public static PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
}