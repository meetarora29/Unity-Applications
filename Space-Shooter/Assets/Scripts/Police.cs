using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{

	public GameObject explosion;
	
	private GameController _gameController;

	private void Start()
	{	
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj)
		{
			_gameController = gameControllerObj.GetComponent<GameController>();
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Police") && !_gameController.getProtection() && !_gameController.getKillAll())
		{
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
			GameObject gameControllerObj = GameObject.FindWithTag("GameController");
			if (gameControllerObj)
			{
				gameControllerObj.GetComponent<GameController>().GameOver();
			}
		}
	}
}
