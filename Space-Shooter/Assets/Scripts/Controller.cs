using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using Wikitude;
using Plane = Wikitude.Plane;

public class Controller : MonoBehaviour
{

	public InstantTracker tracker;
	public float heightAboveGround = 1f;
	public Button trackingControl;
	public GameObject gameControllerObj;
	public GameObject boltPrefab;

	private GameController _gameController;
	private GameObject[] hazards;
	private GridRenderer grid;
	private bool isTracking = false;
	private bool toggleButton = false;

	public void Awake()
	{
		grid = GetComponent<GridRenderer>();
		_gameController = gameControllerObj.GetComponent<GameController>();
		hazards = _gameController.hazards;
	}

	private void Update()
	{
		if (!isTracking)
		{
			return;
		}

		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			GameObject bolt = Instantiate(boltPrefab, Camera.main.transform.position - new Vector3(0, 0.5f, 0),
				Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up));
			bolt.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 10;
			
			Destroy(bolt, 10f);
		}
		else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			UnityEngine.Plane ground = new UnityEngine.Plane(Vector3.up, Vector3.zero);

			float touchPosition;
			if (ground.Raycast(cameraRay, out touchPosition))
			{
				Vector3 position = cameraRay.GetPoint(touchPosition);

				int randomInt = Random.Range(0, hazards.Length);
				Instantiate(hazards[randomInt], position, Quaternion.identity);
			}
		}
	}

	public void StartTracker()
	{
		toggleButton = !toggleButton;

		if (toggleButton)
		{
			tracker.SetState(InstantTrackingState.Tracking);
		}
		else
		{
			tracker.SetState(InstantTrackingState.Initializing);
		}
	}

	public virtual void OnErrorLoading(Error error)
	{
		Debug.LogError(error);
	}

	public virtual void OnTargetsLoaded()
	{
		Debug.Log("Targets Loaded");
	}

	public void OnEnterFieldOfVision(InstantTarget target)
	{
		Debug.Log("Enter FOV");
		SetScene(true);
	}

	public void OnExitFieldOfVision(InstantTarget target)
	{
		Debug.Log("Exit FOV");
		SetScene(false);
	}

	public void OnStateChanged(InstantTrackingState newState)
	{
		Debug.Log("State to " + newState);

		tracker.DeviceHeightAboveGround = heightAboveGround;
		if (newState == InstantTrackingState.Tracking)
		{
			trackingControl.GetComponent<Image>().color = Color.green;
			isTracking = true;
			toggleButton = true;
		}
		else if (newState == InstantTrackingState.Initializing)
		{
			trackingControl.GetComponent<Image>().color = Color.red;
			isTracking = false;
			toggleButton = false;
		}
	}

	private void SetScene(bool value)
	{
		grid.enabled = value;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			Renderer[] renderers = enemy.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer1 in renderers)
			{
				renderer1.enabled = value;
			}
		}

		GameObject[] bolts = GameObject.FindGameObjectsWithTag("Bolt");
		foreach (GameObject bolt in bolts)
		{
			Renderer[] renderers = bolt.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer1 in renderers)
			{
				renderer1.enabled = value;
			}
		}
	}
}
