using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugRobot : MonoBehaviour {

	public Transform robotTransform;

	private Transform armBase;
	private Transform arm;
	private OscOut oscOut;

	private Image oscCircle;
	Color colorActive = new Color(0f,1f,0f,0.7f);
	Color colorInactive = new Color(1f,0f,0f,0.7f);


	private void Awake()
	{
		armBase = robotTransform.Find ("base_link");
		arm = robotTransform.Find ("shoulder_pan_joint");

		oscOut = robotTransform.GetComponent<OscOut> ();


		oscCircle = this.transform.Find ("OscStatus").GetComponent<Image> ();
	}


	private void Start () 
	{
	}
	

	void Update () 
	{
		Debug.Log(oscOut.remoteStatus == OscRemoteStatus.Connected);

		if (oscOut.remoteStatus == OscRemoteStatus.Connected) 
		{
			oscCircle.color = colorActive;
		} 
		else 
		{
			oscCircle.color = colorInactive;
		}
	}


	public void SetShowVirtualRobot(bool show) 
	{
		armBase.gameObject.SetActive (show);
		arm.gameObject.SetActive (show);
	}
}
