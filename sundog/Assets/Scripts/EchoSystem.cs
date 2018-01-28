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

	public GameObject arrow;

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
        if (timerScrollWheel > 3.0f)
		{
			foreach(GameObject go in echoList)
			{
				go.SetActive (false);
			}
		}



		// 1 : choose your echo : WRISTLE, CLICK, STOMP

        if(Input.GetMouseButtonDown(2))
        {
            timerScrollWheel = 0.0f;
            echoList[i].SetActive(true);
        }

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
        if (echoList[i].activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                posMouseBeforeScale = Vector2.zero;

                if (echoList[i].GetComponent<Echo>().index == SoundProjector.ProjectorType.stomp)
                {

                    return;
                }
                else
                {
                    //orientation
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    orientationEcho = pos - this.transform.position;
                    //do scaling
                    posMouseBeforeScale = pos;
                }
                timerScrollWheel = 0;

            }

            //3 :  move up and down to scale the radius of the echo
            //4 : release the mouse button to throw the echo

            if (Input.GetMouseButtonUp(1))
            {
                if (echoList[i].GetComponent<Echo>().index == SoundProjector.ProjectorType.stomp)
                { // STOMP
                    scaleEcho = 0;
                    echoList[i].GetComponent<Echo>().SpawnParticleSystem(transform.position, 360, 0);
                    SendSignal(SoundProjector.ProjectorType.stomp, 360, Vector2.up);

                    return;
                }
                else
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    scaleEcho = Vector2.Distance(posMouseBeforeScale, pos);

                    float arc = 360 - ((scaleEcho / 6) * 360);
                    arc = Mathf.Max(arc, 15);

                    if (echoList[i].GetComponent<Echo>().index == SoundProjector.ProjectorType.whistle)
                    {

                        echoList[i].GetComponent<Echo>().SpawnParticleSystem(transform.position, arc, SoundProjector.GetFullAngle(orientationEcho));
                        SendSignal(SoundProjector.ProjectorType.whistle, arc, orientationEcho);

                    }
                    else if (echoList[i].GetComponent<Echo>().index == SoundProjector.ProjectorType.click)
                    {
                        echoList[i].GetComponent<Echo>().SpawnParticleSystem(transform.position, arc, SoundProjector.GetFullAngle(orientationEcho));
                        SendSignal(SoundProjector.ProjectorType.click, arc, orientationEcho);
                    }

                }
                timerScrollWheel = 0.0f;
            }
        }
       
        if (!Input.GetMouseButton(1))
        {
            float rawAngle;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
            rawAngle = Mathf.Atan2(Input.mousePosition.y - screenPos.y, Input.mousePosition.x - screenPos.x) * Mathf.Rad2Deg;

            arrow.transform.rotation = Quaternion.Euler(0, 0, rawAngle + 270);
        }
 
	}

    public void SendSignal(SoundProjector.ProjectorType type, float arc, Vector2 direction)
    {
        
        float angle = SoundProjector.GetFullAngle(direction);
       
        GameObject temp = new GameObject();

        temp.transform.position = this.transform.position;
        temp.transform.rotation = this.transform.rotation;

        SoundProjector proj = temp.AddComponent<SoundProjector>();
        proj.Project(type,angle,arc);


    }
}
