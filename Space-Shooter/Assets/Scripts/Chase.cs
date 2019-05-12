using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
	private float rotationSpeed = 1f;

	// Update is called once per frame
	void Update ()
	{
		Vector3 direction = Camera.main.transform.position - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
	}
}
