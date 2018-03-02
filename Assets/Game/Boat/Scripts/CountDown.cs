using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CountDown : MonoBehaviour 
{
	private int _startCount = 60;
	private int _count;
	private TextMeshProUGUI m_text; 

	// delegated:
	// triggered when countdown finishes
	public delegate void CountdownFinishedAction();			
	public event CountdownFinishedAction OnCountdownFinished;


	void Awake()
	{
		m_text = GetComponent<TextMeshProUGUI> ();

		Reset ();
	}


	void OnDisable ()
	{
		Pause ();
	}


	private void OnDestroy()
	{
		Pause ();
	}


	private IEnumerator DecreaseCounter() 
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

		// trigger delegated 
		if (OnCountdownFinished != null)
			OnCountdownFinished();

		Pause ();
	}


	public void Play()
	{
		StartCoroutine ("DecreaseCounter");
	}


	public void Pause()
	{
		StopCoroutine("DecreaseCounter");
	}


	public void Reset() 
	{
		_count = _startCount;
	}


	public int GetCount()
	{
		return 	_count;
	}


//	public bool IsFinished() {
//		return _count == 0;
//	}


	public int startCount {
		get { return _startCount; }
		set {
			_startCount = value;
			if (_count > _startCount)
				_count = _startCount;
		}
	}
}
