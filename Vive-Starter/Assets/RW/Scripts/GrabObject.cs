using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabObject : MonoBehaviour
{

	public SteamVR_Input_Sources handType;
	public SteamVR_Behaviour_Pose controllerPose;
	public SteamVR_Action_Boolean grabAction;

	private GameObject collidingObject;
	private GameObject objectInHand;

	private void Update()
	{
		if (grabAction.GetLastStateDown(handType))
		{
			if (collidingObject)
			{
				Grab();
			}
		}

		if (grabAction.GetLastStateUp(handType))
		{
			if (objectInHand)
			{
				Release();
			}
		}
	}

	void SetCollidingObject(Collider other)
	{
		if (collidingObject || !other.GetComponent<Rigidbody>())
		{
			return;
		}

		collidingObject = other.gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	private void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	private void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	void Grab()
	{
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	FixedJoint AddFixedJoint()
	{
		FixedJoint joint = gameObject.AddComponent<FixedJoint>();
		joint.breakForce = 20000f;
		joint.breakTorque = 20000f;
		return joint;
	}

	void Release()
	{
		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
			objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
		}

		objectInHand = null;
	}
}
