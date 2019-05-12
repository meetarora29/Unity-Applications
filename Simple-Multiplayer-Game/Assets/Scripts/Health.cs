using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maximumHealth = 100;
	[SyncVar(hook = "OnHealthChange")] public int currentHealth = maximumHealth;
	public bool destroyOnDeath;
	public RectTransform healthBar;

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			if (destroyOnDeath)
			{
				Destroy(gameObject);
			}
			else
			{
				currentHealth = maximumHealth;
				RpcRespawn();
				Debug.Log("Dead");
			}
		}
	}

	void OnHealthChange(int health)
	{
		healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
		Debug.Log(health);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			transform.position = Vector3.zero;
		}
	}
}
