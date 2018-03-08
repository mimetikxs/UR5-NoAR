using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemCounter : MonoBehaviour 
{
	private int m_count = 0;
	private TextMeshProUGUI m_text;


	private void Awake()
	{
		m_text = GetComponent<TextMeshProUGUI> ();
		m_text.text = "×" + m_count;
	}


	public int count
	{
		get { 
			return m_count; 
		}
		set { 
			m_count = value; 
			m_text.text = "×" + m_count;
		}
	}
}
