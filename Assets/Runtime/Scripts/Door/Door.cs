using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private bool isOpen;
	[SerializeField] private ButtonObject button;

	Bounds bounds;

	private void Awake()
	{
		button.ButtonActiveEvent += OnButtonActive;
	}    

    private void OnDestroy()
	{
		button.ButtonActiveEvent -= OnButtonActive;
	}

	public void Start()
	{
		// Capture the bounds of the collider while it is closed
		bounds = GetComponent<Collider>().bounds;

		// Initially open the door
		SetState(isOpen);
	}

	private void OnButtonActive(bool isActive)
	{
		SetState(isActive);
	}

	public void SetState(bool isOpen)
	{
		this.isOpen = isOpen;

		// Play door animations
		if (isOpen)
		{
			GetComponent<Animation>().Play("Open");
		}
		else
		{
			GetComponent<Animation>().Play("Close");
		}
		StartCoroutine(OpenDoor());
		//AstarPath.active.UpdateGraphs(bounds);
	}
	private IEnumerator OpenDoor()
	{
		yield return new WaitForSeconds(0.5f);
		AstarPath.active.UpdateGraphs(bounds);
	}
}
