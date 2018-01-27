using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class EchoSystem : MonoBehaviour
{

	[HideInInspector]
	public float currentAngle = 0f; //Our current angle from the center of the radial menu.

	public List<GameObject> echoList;

	int i = 0;
	Vector2 orientationEcho;
	Vector2 posMouseBeforeScale;
	float scaleEcho;

	float timerScrollWheel = 0.0f;


	// Use this for initialization
	void Start ()
	{
		foreach(GameObject go in echoList)
		{
			go.SetActive (false);
		}
	}
		

	// Update is called once per frame
	void Update ()
	{
		timerScrollWheel += Time.deltaTime;

		if(timerScrollWheel > 3.0f)
		{
			foreach(GameObject go in echoList)
			{
				go.SetActive (false);
			}
		}



		// 1 : choose your echo : WRISTLE, CLICK, STOMP
		if(Input.GetAxis("Mouse ScrollWheel") > 0.0f) //forward
		{
			timerScrollWheel = 0.0f;

			i++;
			echoList [i-1].SetActive (false);
			if(i == echoList.Count)
			{
				
				i = 0;
			}

			echoList [i].SetActive (true);
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0.0f) //backward
		{
			timerScrollWheel = 0.0f;

			i--;

			echoList [i + 1].SetActive (false);
			if(i == -1)
			{
				i = echoList.Count - 1;
			}

			echoList [i].SetActive (true);
		}

		//2 : choose the oriention of the the echo

		if(Input.GetMouseButtonDown(1))
		{
			posMouseBeforeScale = Vector2.zero;

			if (echoList [i].GetComponent<Echo>().index == 2) { // STOMP
				return;
			} else {
				//orientation
				orientationEcho = this.transform.up;
			//do scaling
				posMouseBeforeScale = Input.mousePosition;
			}

		}

		//3 :  move up and down to scale the radius of the echo
		//4 : release the mouse button to throw the echo

		if(Input.GetMouseButtonUp(1))
		{
			if (echoList [i].GetComponent<Echo>().index == 2) { // STOMP
				// send STOMP signals

				return;
			} else {
				scaleEcho = Vector2.Dot(posMouseBeforeScale, Input.mousePosition);
				if (echoList [i].GetComponent<Echo> ().index == 0) { // WRISTLE
					// send STOMP signals
				} else if(echoList [i].GetComponent<Echo>().index == 1){ // CLICK
					// send STOMP signals
				}

			}

		}	


//		Vector3 vectorArrowUp = this.transform.up;
//
//		this.transform.rotation = Quaternion.Slerp(vectorArrowMouse, vectorArrowUp, Time.deltaTime);
//
//		float rawAngle;
//		rawAngle = Mathf.Atan2 (Input.mousePosition.y - this.transform.position.y, Input.mousePosition.x - this.transform.position.x)* Mathf.Rad2Deg;


//		Vector3 vectorArrowMouse = Input.mousePosition - this.transform.position;
//		vectorArrowMouse.z = 0;
//		vectorArrowMouse.Normalize ();
//
//		this.transform.position
//
//		float rawAngle = Vector3.Dot (vectorArrowMouse, Input.mousePosition);
//		this.transform.rotation = Quaternion.Euler(0, 0, vectorArrowMouse.x);

		//		this.transform.rotation = Quaternion.Euler(0, 0, rawAngle + 270);

 
	}
}
