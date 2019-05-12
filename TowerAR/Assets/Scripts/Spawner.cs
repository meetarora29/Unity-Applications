using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject model;

	private void Start()
	{
		InvokeRepeating("Spawn", 0, 0.7f);
	}

	private void Spawn()
	{
		Instantiate(model, transform.position, transform.rotation);
	}
}
