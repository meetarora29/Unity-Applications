using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{

	public GameObject explosion, playerExplosion;
	public int scoreValue = 50;

	private GameController _gameController;
	
	// Use this for initialization
	void Start () {
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj)
		{
			_gameController = gameControllerObj.GetComponent<GameController>();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_gameController.getKillAll() && (other.CompareTag("Bolt") || other.CompareTag("Player")))
		{
			if (other.CompareTag("Player"))
			{
				_gameController.Hit(transform, playerExplosion, other);
			}
			else
			{
				_gameController.AddScore(scoreValue);
			}
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
