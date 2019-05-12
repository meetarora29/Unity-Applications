using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedGaze : MonoBehaviour {

	public float duration = 1f;

	private bool isGazed = false;
	private float time = 0f;

	private void Update()
	{
		if (isGazed)
		{
			time += Time.deltaTime;

			if (time >= duration)
			{
				time = 0f;
				GetComponent<Button>().onClick.Invoke();
			}
		}
		else
		{
			time = 0f;
		}
	}

	public void setGaze(bool value)
	{
		isGazed = value;
	}
}
