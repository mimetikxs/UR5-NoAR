
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class Pulsating : MonoBehaviour 
{
	private Image image;

	[Tooltip("How long in seconds it takes to complete a pulse")]
	[SerializeField]
	[Range(0.0f, 60.0f)]
	private float time = 2.0f;

	[Tooltip("Minimum alpha to pulse to")]
	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float minAlpha = 0.0f;

	[Tooltip("Maximum alpha to pulse to")]
	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float maxAlpha = 1.0f;

	private bool alphaIncreasing = false;

	private void Awake()
	{
		image = GetComponent<Image>();
	}

	void Update()
	{
		var color = image.color;
		float alphaChange = Time.deltaTime / time / 2.0f;
		if (alphaIncreasing)
		{
			color.a += alphaChange;
			if (color.a >= maxAlpha)
			{
				color.a = maxAlpha;
				alphaIncreasing = false;
			}
		}
		else
		{
			color.a -= alphaChange;
			if (color.a <= minAlpha)
			{
				color.a = minAlpha;
				alphaIncreasing = true;
			}
		}
		image.color = color;
	}
}
