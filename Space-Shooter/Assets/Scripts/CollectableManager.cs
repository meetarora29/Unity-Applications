using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

	private GameController _gameController;
	private CountdownTimer timer;
	private Coroutine countdown;

	void Start()
	{
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj)
		{
			_gameController = gameControllerObj.GetComponent<GameController>();
		}

		timer = GetComponent<CountdownTimer>();
	}

	public void Reward(Collider other)
	{
		_gameController.setCollectible(true);
		
		if (other.gameObject.name.Contains("Restore Health"))
		{
			_gameController.RestoreHealth();
			_gameController.setCollectible(false);
		}

		if (other.gameObject.name.Contains("Protection"))
		{
			if (countdown != null)
			{
				StopCoroutine(countdown);
			}

			_gameController.setProtection(true);
			countdown = StartCoroutine(timer.Countdown("Protection"));
		}

		if (other.gameObject.name.Contains("KillAll"))
		{
			if (countdown != null)
			{
				StopCoroutine(countdown);
			}

			_gameController.setKillAll(true);
			countdown = StartCoroutine(timer.Countdown("KillAll"));
		}

		if (other.gameObject.name.Contains("Passable"))
		{
			if (countdown != null)
			{
				StopCoroutine(countdown);
			}

			_gameController.setPlayerCollider(false);
			countdown = StartCoroutine(timer.Countdown("Passable"));
		}
		
		Destroy(other.gameObject);
	}
}
