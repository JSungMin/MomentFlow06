using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Mana View
public class ManaText : MonoBehaviour
{
    private Mana mana;
    private Text textComponent;

    private void Awake()
    {
        mana = GameObject.FindObjectOfType<Mana>();
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        textComponent.text = mana.ManaPoint.ToString();
    }
}