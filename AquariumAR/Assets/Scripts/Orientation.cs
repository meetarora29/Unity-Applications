using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{

	public Vector3 position;
	public Quaternion rotation;
	public Vector3 localScale;

	// Use this for initialization
	void Start ()
	{
		transform.position = position;
		transform.rotation = rotation;
		transform.localScale = localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
