using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeHit : MonoBehaviour {
	
	public Slider healthSlider;
	public GameObject sliderColor;
	public GameObject Flash;
	public Text healthText;

	private bool takenHit = false;

	public void UpdateHealth(int value)
	{
		healthSlider.value = value;

		if (value < 100)
		{
			takenHit = true;
		}
		UpdateHealthText(value);

		if (healthSlider.value < 50)
		{
			sliderColor.GetComponent<Image>().color = Color.red;
		}
		else
		{
			sliderColor.GetComponent<Image>().color = Color.green;
		}
	}

	private void UpdateHealthText(int value)
	{
		healthText.text = "Health: " + value;
	}

	private void Update()
	{
		if (takenHit)
		{
			Flash.SetActive(true);
			takenHit = false;
		}
		else
		{
			Flash.SetActive(false);
		}
	}
}
