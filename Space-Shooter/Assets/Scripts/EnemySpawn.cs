using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

	private GameObject gameControllerObj;
	private GameController _gameController;
	
	// Use this for initialization
	void Start ()
	{
		gameControllerObj = GameObject.FindWithTag("GameController");
		_gameController = gameControllerObj.GetComponent<GameController>();
		_gameController.spawnValue = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_gameController.spawnValue = transform.position;
	}
}
