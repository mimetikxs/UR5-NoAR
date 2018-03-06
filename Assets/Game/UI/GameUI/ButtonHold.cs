using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
	public bool isPressed = false;

	// delegated:
	public delegate void DownAction();			
	public event DownAction OnDown;
	public delegate void UpAction();			
	public event UpAction OnUp;


	public void OnPointerDown(PointerEventData eventData)
	{
		isPressed = true;

		if (OnDown != null)
			OnDown ();
	}


	public void OnPointerUp(PointerEventData eventData)
	{
		isPressed = false;

		if (OnUp != null)
			OnUp ();
	}
}
