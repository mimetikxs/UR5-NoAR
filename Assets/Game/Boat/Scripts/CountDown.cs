using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CountDown : MonoBehaviour 
{
	public int maxCount = 60;

	private int _count;
	private TextMeshProUGUI m_text; 


	void Awake()
	{
		m_text = GetComponent<TextMeshProUGUI> ();

		Reset ();
		Play ();
	}


	void OnDisable ()
	{
		Pause ();
	}


	private void OnDestroy()
	{
		Pause ();
	}


	IEnumerator DecreaseCounter() 
	{
		while (_count > 0) 
		{
			_count -= 1;

			if (_count > 9)
				m_text.text = _count.ToString ();
			else
				m_text.text = "0" + _count;

			yield return new WaitForSeconds(1f);
		}

		Pause ();
	}


	public void Reset() 
	{
		_count = maxCount;
	}


	public void Play()
	{
		StartCoroutine ("DecreaseCounter");
	}


	public void Pause()
	{
		StopCoroutine("DecreaseCounter");
	}


//	public int maxCount
//	{
//		get { return _maxCount; }
//
//		set {
//			_maxCount = value;
//			if (_maxCount < _count)
//				_count = _maxCount;
//		}
//	}


	public int GetCount()
	{
		return 	_count;
	}
}
