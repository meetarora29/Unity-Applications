using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	private Rigidbody rigidbody;
	public float speed;
	public Text scoreText;
	public Text winText;
	public GameObject Points;
	private int score;
	public Transform image;
	
	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody>();
		score = 0;
		UpdateScore();
		winText.text = "";
	}

	void Update()
	{
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		rigidbody.AddForce(new Vector3(horizontal, 0f, vertical) * speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Point"))
		{
			other.gameObject.SetActive(false);
			score++;
			UpdateScore();

			if (score >= 8)
			{
				winText.text = "You win!";
			}
		}
	}

	private void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString();
	}

	public void Reset()
	{
		score = 0;
		UpdateScore();
		winText.text = "";
		
//		transform.position = Vector3.zero;
		transform.position = image.transform.position + new Vector3(0f, 0.030232f, 0f);
		
		int childCount = Points.transform.childCount;
		print(childCount);
		for (int i = 0; i < childCount; i++)
		{
			Points.transform.GetChild(i).gameObject.SetActive(true);
		}
		
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
}
