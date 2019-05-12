using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

	public float scaleMovement = 1f, tilt, fireRate = 1f;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public GameObject collectableManagerObj;
	public bool useCrossPlatformInput = true;
	
	Rigidbody rb;
	private float nextFire = 0.0f ;
	private CollectableManager _collectableManager;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		_collectableManager = collectableManagerObj.GetComponent<CollectableManager>();
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		if (useCrossPlatformInput)
		{
			moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
			moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
		}

		rb.velocity = new Vector3(moveHorizontal, 0f, moveVertical) * scaleMovement;
		
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
		
		rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
	}

	private void Update()
	{
//		if (Input.GetButton("Fire1") && Time.time > nextFire)
//		{
//			nextFire = Time.time + fireRate;
//			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
//			GetComponent<AudioSource>().Play();
//		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Collectable"))
		{
			_collectableManager.Reward(other);
		}
	}
}
