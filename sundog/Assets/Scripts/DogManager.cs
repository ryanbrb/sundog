using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour {

	public enum Action
	{
		NOTHING,
		BARK,
		WALK
	};

	Action action = Action.NOTHING;

	AudioSource audio;
	List<AudioClip> audioList;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	public void SetAction(Action newAction)
	{
		switch(newAction)
		{
		case Action.NOTHING:
			break;
		case Action.BARK:
			break;
		case Action.WALK:
			break;

		}
		action = newAction;
	}

	void PlayAudioFromGameManager(int i)
	{
		audio.PlayOneShot (audioList[i]);
		audio.Play ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
