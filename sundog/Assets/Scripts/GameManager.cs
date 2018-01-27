using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	enum GameEvent
	{
		ENTER,
		NOTHING,
		COMING,
		ATTACK
	};

	GameEvent ge = GameEvent.ENTER;

	public GameObject Player;

	AudioSource AmbientAudio;
	public AudioSource PlayerAudio;
	public AudioSource MonsterAudio;
	public AudioSource DogAudio;

	List<AudioClip> audioList;

	// Use this for initialization
	void Start () {
		AmbientAudio = GetComponent<AudioSource> ();
	}

	void SwitchGameEvent(GameEvent newGE)
	{
		switch(newGE)
		{
		case GameEvent.ENTER: //init game
			//ambient sound on
			//AmbientAudio.PlayOneShot (audioList[""]);
			AmbientAudio.Play ();
			//player satisfied

			//player first STOMP

			//

			break;
		case GameEvent.NOTHING:
			//player mode nothing


			break;
		case GameEvent.COMING:
//			Player.GetComponent<PlayerManager> ().SetAction (PlayerManager.State.PANIC);
			break;

		case GameEvent.ATTACK:
//			Player.GetComponent<PlayerManager> ().SetAction (PlayerManager.State.ATTACKED);
			break;
		}
		ge = newGE;
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
