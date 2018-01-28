using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

	public enum Action
	{
		NOTHING,
		DISCOVERED,
		ATTACK

	};

	Action action = Action.NOTHING;

	AudioSource audio;
	public List<AudioClip> audioList;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	
		if(GetComponent<Reflector> () == null)
		{
			gameObject.AddComponent<Reflector> ();
		}
			
	}

	public void SetAction(Action newAction)
	{
		GameManager GM = GameObject.FindObjectOfType<GameManager> ();
		PlayerManager PM = GameObject.FindObjectOfType<PlayerManager> ();


		switch(newAction)
		{
		case Action.NOTHING:
			GM.GetComponent<GameManager>().SwitchGameEvent(GameManager.GameEvent.NOTHING);
			break;
		case Action.DISCOVERED:
			audio.PlayOneShot (audioList [0]);
			PM.GetComponent<PlayerManager> ().SetState (PlayerManager.State.PANIC);
			//audio.loop = false;
			break;
		case Action.ATTACK:
			timerScream = 15.0f;
			newRandomTimeToScream = Random.Range (timerScream, 30.0f);
			GM.GetComponent<GameManager>().SwitchGameEvent(GameManager.GameEvent.ATTACK);
			PM.GetComponent<PlayerManager> ().SetState (PlayerManager.State.ATTACKED);
			//audio.loop = true;
			break;

		} 
		action = newAction;
	}
		

	public void Discovered()
	{
		SetAction(Action.DISCOVERED);
	}

	float timerScream = 0.0f;
	float newRandomTimeToScream = 0.0f;
	// Update is called once per frame
	void Update () {
		if(action == Action.ATTACK)
		{
			timerScream += Time.deltaTime;

			if(timerScream > newRandomTimeToScream)
			{
				timerScream = 0.0f;
				newRandomTimeToScream = Random.Range (15.0f, 30.0f);
				audio.PlayOneShot (audioList [1]);
			}
		}
	}
}
