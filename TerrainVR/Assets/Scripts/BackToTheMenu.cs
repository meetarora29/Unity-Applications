using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTheMenu : MonoBehaviour
{

	public Transform camera;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((camera.localEulerAngles.y > 90f && camera.localEulerAngles.y < 120f) ||
		    (camera.localEulerAngles.y < 270f && camera.localEulerAngles.y > 240f))
		{
			SceneManager.LoadScene("Main Menu");
		}
	}
}
