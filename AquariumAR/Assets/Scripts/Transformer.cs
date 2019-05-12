using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private void OnGUI()
	{
		GUI.Box(new Rect(10, 10, 300, 100), "Occluder Transform");
		GUI.Label(new Rect(20, 40, 300, 20), "Position" + transform.position.ToString("F3"));
		GUI.Label(new Rect(20, 60, 300, 20), "Rotation" + transform.rotation.ToString("F3"));
		GUI.Label(new Rect(20, 80, 300, 20), "Scale" + transform.localScale.ToString("F3"));
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Rotate(-touchDeltaPosition.x * 0.1f, 0, -touchDeltaPosition.y * 0.1f);
		}
		else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.localScale = new Vector3(transform.localScale.x + touchDeltaPosition.y * 0.001f, transform.localScale.y + touchDeltaPosition.y * 0.001f, transform.localScale.z + touchDeltaPosition.y * 0.001f);
		}
		else if (Input.touchCount == 3 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(-touchDeltaPosition.x * 0.001f, 0, -touchDeltaPosition.y * 0.001f);
		}
		else if (Input.touchCount == 4 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(0, -touchDeltaPosition.y * 0.001f, 0);
		}
	}
}
