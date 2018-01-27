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

	public PlayerManager Player;

    public List<DogManager> Dogs;
    public List<MonsterManager> Monsters;

	AudioSource AmbientAudio;
	public AudioSource PlayerAudio;
	public AudioSource MonsterAudio;
	public AudioSource DogAudio;

	List<AudioClip> audioList;

	// Use this for initialization
	void Start () {
		AmbientAudio = GetComponent<AudioSource> ();

        if (Player == null)
            Player = FindObjectOfType<PlayerManager>();

        if(Dogs == null)
        {
            Dogs = new List<DogManager>();
        }

        DogManager[] dogsfound = GameObject.FindObjectsOfType<DogManager>();

        foreach(DogManager dog in dogsfound)
        {
            if (!Dogs.Contains(dog))
                Dogs.Add(dog);
        }

        if(Monsters == null)
        {
            Monsters = new List<MonsterManager>();
        }

        MonsterManager[] monstersfound = GameObject.FindObjectsOfType<MonsterManager>();

        foreach(MonsterManager monster in monstersfound)
        {
            if(!Monsters.Contains(monster))
            {
                Monsters.Add(monster);
            }
        }
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
