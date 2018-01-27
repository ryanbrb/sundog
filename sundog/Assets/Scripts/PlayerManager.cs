using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	public enum State
	{
		NO_STATE,
		PANIC,
		ATTACKED,
		DOES_ECHO}

	;

	public enum Action
	{
		NOTHING,
		WALK,

		WRISTLE,
		CLICK,
		STOMP}

	;

	State state = State.NO_STATE;
	Action action = Action.NOTHING;

	AudioSource audio;
	List<AudioClip> audioList;

	// Use this for initialization
	void Start ()
	{
		audio = GetComponent<AudioSource> ();
	}

	public void SetState (State newState)
	{
		switch (newState) {
		case State.NO_STATE:
			break;
		case State.PANIC:
			break;
		case State.ATTACKED:
			break;
		case State.DOES_ECHO:
			break;
		}
		state = newState;
	}

	public void SetAction (Action newAction)
	{
		switch (newAction) {
		case Action.NOTHING:
			break;
		case Action.WALK:
			break;

		case Action.WRISTLE:
			break;
		case Action.CLICK:
			break;
		case Action.STOMP:
			break;
		}
		action = newAction;
	}

	void PlayAudioFromGameManager (int i)
	{
		audio.PlayOneShot (audioList [i]);
		audio.Play ();

	}
	
	// Update is called once per frame
	void Update ()
	{




		//player moving around
		if (Input.GetKeyDown (KeyCode.Z)) {
			this.transform.Translate (Vector3.up);
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			this.transform.Translate (-Vector3.up);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			this.transform.Translate (Vector3.right);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			this.transform.Translate (-Vector3.right);
		}
	}
}
