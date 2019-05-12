using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

	public GameObject[] particles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{
			var touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast(touchRay, out hitInfo))
			{
				if (hitInfo.collider.gameObject.name == "Bubbles")
				{
					foreach (GameObject particle in particles)
					{
						ParticleSystem bubbles = particle.GetComponent<ParticleSystem>();
						if (bubbles.isPlaying)
						{
							bubbles.Stop();
						}
						else
						{
							bubbles.Play();
						}
					}
				}
			}
		}
	}
}
