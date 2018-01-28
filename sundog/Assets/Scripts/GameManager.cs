using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  enum GameState 
  {
    GS_GAMEPLAY,
    GS_DEATH,
  };

	enum GameEvent
	{
		ENTER,
		NOTHING,
		COMING,
		ATTACK
	};

	GameEvent ge = GameEvent.ENTER;
  GameState gs = GameState.GS_GAMEPLAY;

  public PlayerManager Player;

  public List<DogManager> Dogs;
  public List<MonsterManager> Monsters;

  public GameObject MonsterPrefab;
  public Transform[] MonsterSpawnPositions;

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

    SpawnAllMonsters();
  }

  //this will delete all existing monsters
  //and create a new set, This could be used as part
  //of a game reset function.
  void SpawnAllMonsters(bool bDestroyExistingMonsters = false) 
  {
    //if we haven't specified any spawn locations, we shouldn't bother doing anything
    int numberOfSpawnLocations = MonsterSpawnPositions.Length;
    if (numberOfSpawnLocations <= 0)
      return;

    if (Monsters == null)
      Monsters = new List<MonsterManager>();

    if (bDestroyExistingMonsters) {

      //delete all of the existing monsters if there are any
      int numberOfActiveMonsters = Monsters.Count;
      for (int i = numberOfActiveMonsters - 1; i >= 0; --i) {
        Destroy(Monsters[i]);
      }
      Monsters.Clear();
    }

    //spawn monsters at all spawn locations
    for(int i = 0; i < numberOfSpawnLocations; ++i) 
    {
      GameObject monsterGameObject = Instantiate(MonsterPrefab, MonsterSpawnPositions[i].position, MonsterSpawnPositions[i].rotation);
      Monsters.Add(monsterGameObject.GetComponent<MonsterManager>());
    }
  }

  void SwitchGameState(GameState newState) 
  {
    switch(newState) 
    {
      case GameState.GS_GAMEPLAY: 
      {
          //reset gameplay
          SpawnAllMonsters(true);
      }
      break;

      case GameState.GS_DEATH: 
      {
        //here we should spawn some kind of Screen fade effect.
      }
      break;
    }

    gs = newState;
  }

  void UpdateGameState() 
  {
    switch(gs) 
    {
      case GameState.GS_GAMEPLAY: 
      {
          //check to see if the player has died
          bool bPlayerDied = false;
          if (bPlayerDied) 
          {
            SwitchGameState(GameState.GS_DEATH);
          }
      }
      break;

      case GameState.GS_DEATH: 
      {
          //check to see if death sequence is over
          bool bDeathSequenceOver = true;
          if(bDeathSequenceOver) 
          {
            SwitchGameState(GameState.GS_GAMEPLAY);
          }
      }
      break;
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
    UpdateGameState();
  }
}
