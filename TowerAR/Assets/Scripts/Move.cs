using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	private Animator _animator;
	private float speed = 0.03f;
	private float turnSpeed = 1f;
	public bool dead = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			Hit();
		}
		else if (other.gameObject.CompareTag("Home"))
		{
			Destroy(gameObject, 1);
		}
	}

	public void Hit()
	{
		dead = true;
		_animator.SetTrigger("Dying");
		Destroy(GetComponent<Collider>(), 1);
		Destroy(GetComponent<Rigidbody>(), 1);
		Destroy(gameObject, 4);
	}

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (dead)
		{
			return;
		}
		
		GameObject home = GameObject.FindWithTag("Home");
		if (home != null)
		{
			Vector3 direction = home.transform.position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.smoothDeltaTime);
		}
		
		transform.Translate(0, 0, speed);
	}
}
