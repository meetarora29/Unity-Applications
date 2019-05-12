using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TrumpController : MonoBehaviour
{
	public float scaler = 4f;
	private Rigidbody rb;
	private Animation _animation;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		_animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float x = CrossPlatformInputManager.GetAxis("Horizontal");
		float y = CrossPlatformInputManager.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(x, 0, y);

		rb.velocity = movement * scaler;

		if (x!=0 && y!=0)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
			
		}

		if (x!=0 || y!=0)
		{
			_animation.Play("Walk");
		}
		else
		{
			_animation.Play("Idle");
		}
	}
}
