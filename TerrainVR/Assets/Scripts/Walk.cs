using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
	public Transform camera;
	public float scaler = 10f;
	private CharacterController cc;
	
	// Use this for initialization
	void Start ()
	{
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")  || Input.touchCount > 0)
		{
			Vector3 forward = camera.transform.forward;
			cc.SimpleMove(forward * scaler);
		}
	}
}
