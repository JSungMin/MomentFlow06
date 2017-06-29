using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParsher : MonoBehaviour {
	public static int NumericInputToInteger (string inputString)
	{
		int inputNumeric = 0;
		int.TryParse (inputString, out inputNumeric);
		return inputNumeric;
	}

	public static KeyCode NumericInputToKeyCode (string inputString)
	{
		return (KeyCode)(NumericInputToInteger (inputString) + 48);
	}

	public static int KeyCodeToInteger (KeyCode keyCode)
	{
		return (int)keyCode - 281;
	}
}
