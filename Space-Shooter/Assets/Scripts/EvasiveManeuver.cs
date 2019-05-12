using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EvasiveManeuver : MonoBehaviour
{

	public Vector2 startWait, maneuverTime, maneuverWait;
	public float dodge, smoothing, tilt;
	public Boundary boundary;
	public bool usingAR = true;

	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		currentSpeed = rb.velocity.z;
		if (usingAR)
		{
			boundary.xMax = 10000000f;
			boundary.xMin = -10000000f;
			boundary.zMax = 10000000f;
			boundary.zMin = -10000000f;
		}
		StartCoroutine(Evade());
	}
	
	void FixedUpdate ()
	{
		float newManuever = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3(newManuever, 0f, currentSpeed);
		
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
		
		rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

		while (true)
		{
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}
}
