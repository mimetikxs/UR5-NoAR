using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bins : MonoBehaviour 
{
	public Color colorBad = new Color (1f, 0f, 0f);
	public Color colorGood = new Color (0f, 1f, 0f);
		
	private Material material;
	private Color initialColor;
	private Color initialEmissiveColor;
	private Color blinkColor;
	private int blinkCount;
	private int totalBlinks;


	private void Awake()
	{
		Renderer rend = GetComponent<Renderer> ();
		material = rend.materials [0];

		initialColor = material.GetColor ("_Color");
		initialEmissiveColor = material.GetColor ("_EmissionColor");
	}


	private void Start() 
	{
	}
	

	void Update() 
	{
	}


	private void OnDisable()
	{
		StopCoroutine("DoBlink");
	}


	private IEnumerator DoBlink()
	{
		Debug.Log (blinkCount);

		while (blinkCount < totalBlinks)
		{
			Color c = (blinkCount % 2 == 0) ? blinkColor : initialEmissiveColor;

			material.SetColor ("_EmissionColor", c);

			blinkCount++;

			yield return new WaitForSeconds(0.1f);
		}

		StopCoroutine("DoBlink");
	} 


	public void Blink(bool isGood, int times)
	{
		blinkColor = isGood ? colorGood : colorBad;

		blinkCount = 0;
		totalBlinks = times * 2;

		StartCoroutine("DoBlink");
	}
}
