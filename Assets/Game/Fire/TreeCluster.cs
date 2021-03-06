﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCluster : MonoBehaviour 
{
	public float temp = 0f;		// 0..1 // use a negative number to delay the heatup
	public float heatingSpeed = 0.01f;  // 0..1 // how much the temp rises
	public float burningThreshold = 0.25f;


	// slider 
	public Canvas tempMeterPrefab;
	public Color lowTempColor = Color.blue;  
	public Color highTempColor = Color.red;
	private Image sliderImage;
	
	private GameObject fine;
	private GameObject burnt;
	private GameObject fires;
	private Slider slider;

	// light intensisty control
	private Light fxLight;
	private float lightIntensity;
	private float initialLightIntensity;

	private enum State {Fine, Burning, Burnt};
	private State currentState;

	// delegated:
	// triggered when waste is collected
	public delegate void BurntAction();			
	public event BurntAction OnBurnt;
	public delegate void BurningStartAction();			
	public event BurningStartAction OnBurningStart;
	public delegate void BurningStopAction();			
	public event BurningStopAction OnBurningStop;


	private void Awake()
	{
		currentState = State.Fine;

		fine = this.transform.Find ("Fine").gameObject;
		burnt = this.transform.Find ("Burnt").gameObject;
		fires = this.transform.Find ("Fires").gameObject;

		Transform container = this.transform.Find ("TempMeter");
		Canvas canvas = Instantiate (tempMeterPrefab, container);
		slider = canvas.transform.Find ("TemperatureSlider").GetComponent<Slider> ();
		sliderImage = slider.transform.Find ("FillArea/Fill").GetComponent<Image> ();

		fxLight = fires.transform.Find ("Light").GetComponent<Light> ();
		initialLightIntensity = fxLight.intensity;
		lightIntensity = 0f;


		slider.value = temp;
	}
		

	private void Update()
	{
		UpdateSlider ();

		if (currentState != State.Burnt)
			CheckTemp ();

		FadeFxLight ();
	}


	private void UpdateSlider()
	{
		slider.value += (temp - slider.value) * 0.1f;

//		if (temp < 0.4f) 
//		{
//			sliderImage.color = lowTempColor;
//		} 
//		else if (temp > 0.6f) 
//		{
//			sliderImage.color = highTempColor;
//		} 
//		else 
//		{
//			float val = (temp - 0.4f) / 0.2f;
//			//val = Mathf.Clamp (val, 0f, 1f);
//			sliderImage.color = Color.Lerp (lowTempColor, highTempColor, val);
//		}

		sliderImage.color = (temp < burningThreshold) ? lowTempColor : highTempColor;
	}


	private void FadeFxLight()
	{
		// dirt-cheap way of fading fire light
		float direction = (currentState == State.Burning) ? 1f : -1f;
		lightIntensity +=  0.05f * direction;	
		lightIntensity = Mathf.Clamp (lightIntensity, 0.1f, initialLightIntensity);
		fxLight.intensity = lightIntensity;
	}
		

	private void CheckTemp()
	{
		if (temp < burningThreshold) 
		{
			if (currentState != State.Fine) 
			{
				currentState = State.Fine;

				fine.SetActive (true);
				burnt.SetActive (false);
				StopFireFX (); 

				// trigger delegated 
				if (OnBurningStop != null)
					OnBurningStop ();
			}
		} 
		else if (temp < 1f) 
		{
			if (currentState != State.Burning) 
			{
				currentState = State.Burning;

				fine.SetActive (true);
				burnt.SetActive (false);
				StartFireFX ();

				// trigger delegated 
				if (OnBurningStart != null)
					OnBurningStart ();
			}
		} 
		else 
		{	
			if (currentState != State.Burnt) 
			{
				currentState = State.Burnt;

				fine.SetActive (false);
				burnt.SetActive (true);
				StopFireFX ();

				Destroy (slider.transform.parent.gameObject);

				// trigger delegated 
				if (OnBurnt != null) 
					OnBurnt();
			}
		}
	}


	private void StartFireFX()
	{
		ParticleSystem[] fxs = fires.transform.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem fx in fxs) 
		{
			var emission = fx.emission;
			emission.enabled = true;
		}

		fires.SetActive (true);
	}


	private void StopFireFX()
	{
		ParticleSystem[] fxs = fires.transform.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem fx in fxs) 
		{
			var emission = fx.emission;
			emission.enabled = false;
		}

		//Invoke("DeactivateFireFX", 5f);
	}


//	private void DeactivateFireFX()
//	{
//		if (currentState == State.Fine || currentState == State.Burnt)
//			fires.SetActive (false);
//	}


	public void Heatup()
	{
		temp += heatingSpeed;
	}


	public void Cooldown(float amount)
	{
		temp -= amount;
		temp = Mathf.Max (temp, 0f);
	}
}
