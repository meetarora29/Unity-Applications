using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private void OnCollisionEnter(Collision other)
	{
		GameObject hit = other.gameObject;
		Health health = hit.GetComponent<Health>();

		Debug.Log(hit, health);

		if (health != null)
		{
			health.TakeDamage(10);
			Debug.Log("Took Damage");	
		}
		
		Destroy(gameObject);
		Debug.Log("Destroyed Game object");
	}
}
