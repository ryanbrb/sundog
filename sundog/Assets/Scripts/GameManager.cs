using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	//  enum GameState
	//  {
	//    GS_GAMEPLAY,
	//    GS_DEATH,
	//  };

	enum GameEvent
	{
		NOTHING,
		ATTACK,
		GAME_OVER}

	;

	GameEvent ge = GameEvent.NOTHING;
	//  GameState gs = GameState.GS_GAMEPLAY;

	public GameObject Player;

	public List<DogManager> Dogs;
	public List<MonsterManager> Monsters;

	public GameObject DogPrefab;
	public GameObject DogPrefab2;
	public Transform[] DogSpawnPositions;

	public GameObject MonsterPrefab;
	public GameObject MonsterPrefab2;
	public Transform[] MonsterSpawnPositions;

	AudioSource AmbientAudio;


	List<AudioClip> audioList;

	// Use this for initialization
	void Start ()
	{
		AmbientAudio = GetComponent<AudioSource> ();

//        if (Player == null)
//            Player = FindObjectOfType<PlayerManager>();

		InitGame ();

		if (Dogs == null) {
			Dogs = new List<DogManager> ();
		}

		DogManager[] dogsfound = GameObject.FindObjectsOfType<DogManager> ();

		foreach (DogManager dog in dogsfound) {
			if (!Dogs.Contains (dog))
				Dogs.Add (dog);
		}

		if (Monsters == null) {
			Monsters = new List<MonsterManager> ();
		}
    
		MonsterManager[] monstersfound = GameObject.FindObjectsOfType<MonsterManager> ();
    
		foreach (MonsterManager monster in monstersfound) {
			if (!Monsters.Contains (monster)) {
				Monsters.Add (monster);
			}
		}

    
	}

	void InitGame ()
	{
		SpawnPlayer ();
		SpawnAllDogs ();
		SpawnAllMonsters ();
	}

	void SpawnPlayer ()
	{
		GameObject _player = Instantiate (Player, Vector2.zero, Quaternion.identity);
	}

	//this will delete all existing dogs
	//and create a new set, This could be used as part
	//of a game reset function.
	void SpawnAllDogs (bool bDestroyExistingDogs = false)
	{
		//if we haven't specified any spawn locations, we shouldn't bother doing anything
		int numberOfSpawnLocations = DogSpawnPositions.Length;
		if (numberOfSpawnLocations <= 0)
			return;

		if (Dogs == null)
			Dogs = new List<DogManager> ();

		if (bDestroyExistingDogs) {

			//delete all of the existing monsters if there are any
			int numberOfActiveDogs = Dogs.Count;
			for (int i = numberOfActiveDogs - 1; i >= 0; --i) {
				Destroy (Dogs [i]);
			}
			Dogs.Clear ();
		}

		//spawn monsters at all spawn locations
		for (int i = 0; i < numberOfSpawnLocations; ++i) {
			if(i < 2)
			{
				GameObject dogGameObject = Instantiate (DogPrefab, DogSpawnPositions [i].position, DogSpawnPositions [i].rotation);
				Dogs.Add (dogGameObject.GetComponent<DogManager> ());
			}else
			{
				GameObject dogGameObject = Instantiate (DogPrefab2, DogSpawnPositions [i].position, DogSpawnPositions [i].rotation);
				Dogs.Add (dogGameObject.GetComponent<DogManager> ());
			}
		}
	}

	//this will delete all existing monsters
	//and create a new set, This could be used as part
	//of a game reset function.
	void SpawnAllMonsters (bool bDestroyExistingMonsters = false)
	{
		//if we haven't specified any spawn locations, we shouldn't bother doing anything
		int numberOfSpawnLocations = MonsterSpawnPositions.Length;
		if (numberOfSpawnLocations <= 0)
			return;

		if (Monsters == null)
			Monsters = new List<MonsterManager> ();

		if (bDestroyExistingMonsters) {

			//delete all of the existing monsters if there are any
			int numberOfActiveMonsters = Monsters.Count;
			for (int i = numberOfActiveMonsters - 1; i >= 0; --i) {
				Destroy (Monsters [i]);
			}
			Monsters.Clear ();
		}

		//spawn monsters at all spawn locations
		for (int i = 0; i < numberOfSpawnLocations; ++i) {
			if(i < (numberOfSpawnLocations / 2))
			{
				GameObject monsterGameObject = Instantiate (MonsterPrefab, MonsterSpawnPositions [i].position, MonsterSpawnPositions [i].rotation);
				Monsters.Add (monsterGameObject.GetComponent<MonsterManager> ());
			}
			else{
				GameObject monsterGameObject = Instantiate (MonsterPrefab2, MonsterSpawnPositions [i].position, MonsterSpawnPositions [i].rotation);
				Monsters.Add (monsterGameObject.GetComponent<MonsterManager> ());
			}
		}
	}

	//  void SwitchGameState(GameState newState)
	//  {
	//    switch(newState)
	//    {
	//      case GameState.GS_GAMEPLAY:
	//      {
	//          //reset gameplay
	//          SpawnAllMonsters(true);
	//      }
	//      break;
	//
	//      case GameState.GS_DEATH:
	//      {
	//        //here we should spawn some kind of Screen fade effect.
	//      }
	//      break;
	//    }
	//
	//    gs = newState;
	//  }
	//
	//  void UpdateGameState()
	//  {
	//    switch(gs)
	//    {
	//      case GameState.GS_GAMEPLAY:
	//      {
	//          //check to see if the player has died
	//          bool bPlayerDied = false;
	//          if (bPlayerDied)
	//          {
	//            SwitchGameState(GameState.GS_DEATH);
	//          }
	//      }
	//      break;
	//
	//      case GameState.GS_DEATH:
	//      {
	//          //check to see if death sequence is over
	//          bool bDeathSequenceOver = true;
	//          if(bDeathSequenceOver)
	//          {
	//            SwitchGameState(GameState.GS_GAMEPLAY);
	//          }
	//      }
	//      break;
	//    }
	//  }

	void SwitchGameEvent (GameEvent newGE)
	{
		switch (newGE) {
		case GameEvent.NOTHING:
			//player mode nothing
			AmbientAudio.PlayOneShot (audioList [0]);
			break;

		case GameEvent.ATTACK:
			AmbientAudio.PlayOneShot (audioList [1]);
			break;

		case GameEvent.GAME_OVER:
			SceneManager.LoadScene ("LEVEL1", LoadSceneMode.Single);
			break;
		}
		ge = newGE;
	}


	
	// Update is called once per frame
	void Update ()
	{
		//UpdateGameState();
	}
}
