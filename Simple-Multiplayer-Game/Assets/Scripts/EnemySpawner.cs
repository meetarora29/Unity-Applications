using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

	public GameObject enemy;
	public int numEnemies;

	public override void OnStartServer()
	{
		for (int i = 0; i < numEnemies; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
			Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 180f), 0);

			GameObject enemyObj = (GameObject) Instantiate(enemy, spawnPosition, spawnRotation);
			NetworkServer.Spawn(enemyObj);
		}
	}
}
