using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour {

	public SteamVR_Input_Sources handType;
	public SteamVR_Behaviour_Pose controllerPose;
	public SteamVR_Action_Boolean teleportAction;
	public GameObject laserPrefab;

	public Transform cameraRigTransform;
	public GameObject teleportReticlePrefab;
	public Transform headTransform;
	public Vector3 teleportReticleOffset;
	public LayerMask teleportMask;

	private GameObject reticle;
	private bool shouldTeleport;

	private GameObject laser;
	private Vector3 hitPoint;

	private void Start()
	{
		laser = Instantiate(laserPrefab);
		reticle = Instantiate(teleportReticlePrefab);
	}

	void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true);
		laser.transform.position = Vector3.Lerp(controllerPose.transform.position, hit.point, 0.5f);
		laser.transform.LookAt(hit.point);
		laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);
	}

	private void Update()
	{
		if (teleportAction.GetState(handType))
		{
			RaycastHit hit;

			if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100f, teleportMask))
			{
				hitPoint = hit.point;
				ShowLaser(hit);
				
				reticle.SetActive(true);
				reticle.transform.position = hit.point + teleportReticleOffset;
				shouldTeleport = true;
			}
		}
		else
		{
			laser.SetActive(false);
			reticle.SetActive(false);
		}

		if (teleportAction.GetStateUp(handType) && shouldTeleport)
		{
			Teleport();
		}
	}

	private void Teleport()
	{
		shouldTeleport = false;
		reticle.SetActive(false);

		Vector3 difference = cameraRigTransform.position - headTransform.position;
		difference.y = 0f;
		cameraRigTransform.position = hitPoint + difference;
	}
}
