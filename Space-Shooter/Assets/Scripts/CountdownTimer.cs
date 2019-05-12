using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	private float currentCountdownValue;
	private GameController _gameController;
	private bool timerRunning;

	public Text countdownText;
	public GameObject Flash;

	private void Start()
	{
		countdownText.text = "";
		timerRunning = false;
		
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj)
		{
			_gameController = gameControllerObj.GetComponent<GameController>();
		}
	}

	public IEnumerator Countdown(String name, float time = 20f)
	{
		currentCountdownValue = time;
		countdownText.text = currentCountdownValue + "s";
		timerRunning = true;
		while (currentCountdownValue > 0)
		{
			yield return new WaitForSeconds(1f);
			currentCountdownValue--;
			countdownText.text = currentCountdownValue + "s";
			timerRunning = true;
		}

		countdownText.text = "";
		timerRunning = false;
		_gameController.setCollectible(false);

		if (name.Equals("Protection"))
		{
			_gameController.setProtection(false);
		}

		if (name.Equals("Passable"))
		{
			_gameController.setPlayerCollider(true);
		}

		if (name.Equals("KillAll"))
		{
			_gameController.setKillAll(false);
		}
	}

	private void Update()
	{
		if (timerRunning)
		{
			Flash.SetActive(true);
		}
		else
		{
			Flash.SetActive(false);
		}
	}
}
