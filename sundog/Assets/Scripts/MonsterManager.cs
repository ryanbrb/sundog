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
		switch(newAction)
		{
		case Action.NOTHING:
			break;
		case Action.DISCOVERED:
			audio.PlayOneShot (audioList[0]);
			break;
		case Action.ATTACK:
			audio.PlayOneShot (audioList[1]);
			break;

		} 
		action = newAction;
	}

	void PlayAudioFromGameManager(int i)
	{
		audio.PlayOneShot (audioList[i]);
		audio.Play ();

	}

	public void Discovered()
	{
		SetAction(Action.DISCOVERED);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
