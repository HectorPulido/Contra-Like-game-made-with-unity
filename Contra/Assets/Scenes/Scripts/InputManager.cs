using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
	public static bool JumpButtonPressed
	{
		get
		{
			return Input.GetKeyDown (KeyCode.Space);
		}
	}
	public static bool AttackButtonPressed
	{
		get
		{
			return Input.GetKeyDown (KeyCode.J);
		}
	}
	public static bool JumpButton
	{
		get
		{
			return Input.GetKey (KeyCode.Space);
		}
	}
	public static bool AttackButton
	{
		get
		{
			return Input.GetKey (KeyCode.J);
		}
	}
	public static float HorizontalAxis
	{
		get
		{
			return Input.GetAxis ("Horizontal");
		}
	}
	public static float VerticalAxis
	{
		get
		{
			return Input.GetAxis ("Vertical");
		}
	}
}
