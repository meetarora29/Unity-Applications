using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private void OnBecameInvisible()
	{
		DestroyImmediate(gameObject);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, 1f);
	}
}
