using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	public GameObject bullet;
	public Transform bulletSpawner;
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * 3f;
		
		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}

	[Command]
	public void CmdFire()
	{
		GameObject bulletObj = (GameObject) Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
		bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 6f;
		Debug.Log(bulletObj.GetComponent<Rigidbody>().velocity);
		
		NetworkServer.Spawn(bulletObj);
		Debug.Log(bulletObj.GetComponent<Rigidbody>().velocity);
		Destroy(bulletObj, 2f);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
