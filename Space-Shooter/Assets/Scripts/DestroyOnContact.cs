using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

	public GameObject explosion, playerExplosion;
	public int scoreValue;
	private GameController gameController;

	private void Start()
	{
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj)
		{
			gameController = gameControllerObj.GetComponent<GameController>();
		}

		if (!gameController)
		{
			Debug.Log("No GameController");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Boundary") && !other.CompareTag("Enemy") && !other.CompareTag("Police") && !other.CompareTag("Collectable"))
		{
			
			if (other.CompareTag("Player"))
			{
				if (!gameController.getProtection())
				{
					gameController.Hit(transform, playerExplosion, other);
				}
			}
			else
			{
				gameController.AddScore(scoreValue);
				Destroy(other.gameObject);
			}

			Destroy(gameObject);

			if (explosion)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}
			
		}
	}
}
