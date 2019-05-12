using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject[] hazards;
	public Vector3 spawnValue;
	public int hazardCount;
	public float wait, startWait, waveWait;
	public GameObject secondaryCamera;
	public bool usingVR = false, usingAR = true;
	public GameObject restartButton;
	public GameObject arController;

	public Text scoreText, gameOverText, restartText;
	private int score, playerHealth, removeCollectible, numCollectible;
	private bool gameOver, restartGame, protection, hasCollectible, killAll;
	private TakeHit takeHit;
	private GameObject player;
	private bool wave = false;
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		playerHealth = 100;
		gameOver = false;
		restartGame = false;
		protection = false;
		hasCollectible = false;
		killAll = false;
		removeCollectible = 0;
		gameOverText.text = "";
		restartText.text = "";
		UpdateScore();
		takeHit = GetComponent<TakeHit>();

		foreach (GameObject hazard in hazards)
		{
			if (hazard.CompareTag("Collectable"))
			{
				numCollectible++;
			}
		}
		
		player = GameObject.FindWithTag("Player");

		if (!usingAR)
		{
			StartCoroutine(SpawnHazard());
		}
	}

	public void StartWave()
	{
		wave = true;
		StartCoroutine(SpawnHazard());
	}

	public void StopWave()
	{
		wave = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		
		if (usingAR && gameOver)
		{
			restartButton.SetActive(true);
			arController.SetActive(false);
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void AddScore(int value)
	{
		score += value;
		UpdateScore();
	}

	public void setProtection(bool value)
	{
		protection = value;
	}

	public bool getProtection()
	{
		return protection;
	}

	public bool getKillAll()
	{
		return killAll;
	}

	public void setKillAll(bool value)
	{
		killAll = value;
	}

	public void setPlayerCollider(bool value)
	{
		player.GetComponent<Collider>().enabled = value;
	}

	public void setCollectible(bool value)
	{
		hasCollectible = value;

		if (!hasCollectible)
		{
			removeCollectible = 0;
		}
	}

	public void Hit(Transform objectTransform, GameObject playerExplosion, Collider other)
	{
		playerHealth -= 20;
		takeHit.UpdateHealth(playerHealth);

		if (playerHealth <= 0) 
		{
			Instantiate(playerExplosion, objectTransform.position, objectTransform.rotation);
			Destroy(other.gameObject);
			GameOver();
		}
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		if (usingVR)
		{
			secondaryCamera.SetActive(true);
		}
		
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	IEnumerator SpawnHazard()
	{
		yield return new WaitForSeconds(startWait);

		while (true)
		{
			if (usingAR && !wave)
			{
				if (gameOver)
				{
					restartButton.SetActive(true);
				}

				break;
			}
			
			// To show collectibles only once in a wave when the player has no active power up
			if (!hasCollectible)
			{
				removeCollectible = 0;
			}
			
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length - removeCollectible)];
				
				if (hazard.CompareTag("Collectable"))
				{
					removeCollectible = numCollectible;
				}
				
				Vector3 position = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion rotation = Quaternion.identity;
				Instantiate(hazard, position, rotation);

				yield return new WaitForSeconds(wait);
			}
			
			yield return new WaitForSeconds(waveWait);
			
			if (gameOver)
			{
				restartButton.SetActive(true);
				restartText.text = "Press 'r' to Restart";
				restartGame = true;
				break;
			}
		}
	}

	public void RestoreHealth()
	{
		playerHealth = 100;
		takeHit.UpdateHealth(playerHealth);
	}
}
