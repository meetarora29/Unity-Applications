using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{

	public GameObject bullet;
	public Transform bulletPos;
	private GameObject goober;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Goober") && goober == null)
		{
			goober = other.gameObject;
			InvokeRepeating("Shoot", 0, 1f);
		}
	}

	void Shoot()
	{
		Instantiate(bullet, bulletPos.position, bulletPos.rotation);

		if (goober.GetComponent<Move>().dead)
		{
			goober = null;
			CancelInvoke("Shoot");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (goober == other.gameObject)
		{
			goober = null;
			CancelInvoke("Shoot");
		}
	}

	private void Update()
	{
		if (goober != null)
		{
			transform.LookAt(goober.transform.position);
		}
	}
}
