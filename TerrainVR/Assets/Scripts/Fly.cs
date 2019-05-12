using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
	public float scaler = 10f;
	public Transform camera;
	private Rigidbody rb;
	private float acc;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		acc = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1") || Input.touchCount > 0)
		{
			acc += Time.deltaTime;
			rb.velocity = camera.transform.forward * scaler * acc; 
		}
		else
		{
			acc = 0f;
			rb.velocity = Vector3.zero;
		}
	}
}
