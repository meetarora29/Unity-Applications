using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	Rigidbody rb;
	public float speed;

	private float time = 0f;

	// Use this for initialization
	void Start ()
	{
//		GameObject player = GameObject.FindWithTag("Player");
//		if (player != null && !gameObject.name.Contains("Bolt") &&
//		    (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Police")))
//		{
//			Vector3 direction = player.transform.position - transform.position;
//			transform.rotation = Quaternion.LookRotation(direction);
//		}
		
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;

		if (time >= 15f)
		{
			Destroy(gameObject);
		}
	}
}
