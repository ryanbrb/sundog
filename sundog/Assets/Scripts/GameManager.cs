﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  //General States for the whole game
  enum GameState 
  {
    GS_GAMEPLAY,
    GS_ENTER_DEATH,         //this fades into the deat
    GS_DEATH,               //black screen
    GS_RESPAWN_FROM_DEATH,  //fade back to game
  };

  enum GameEvent
	{
		NOTHING,
		ATTACK,
		GAME_OVER}

	;

	GameEvent ge = GameEvent.NOTHING;
	GameState gs = GameState.GS_GAMEPLAY;

  //UI related
  public Canvas DeathStateCanvas;
  public float fadeToBlackTime = 0.25f;
  public float deathIdleTime = 2.0f;
  public float fadeToRespawnFromDeath = 0.25f;

  float m_fStateTimer;

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

    ClearDeathScreen();
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

  void ClearDeathScreen(bool bEnableRendering = false) 
  {
    if (DeathStateCanvas == null)
      return;

    CanvasGroup canvasGroupComponent = DeathStateCanvas.GetComponent<CanvasGroup>();
    if (canvasGroupComponent != null) 
    {
      canvasGroupComponent.alpha = 0.0f;
    }

    DeathStateCanvas.enabled = bEnableRendering;
  }

  void SetDeathScreenAlphaFade(float newAlpha) 
  {
    if (DeathStateCanvas == null)
      return;

    CanvasGroup canvasGroupComponent = DeathStateCanvas.GetComponent<CanvasGroup>();
    if (canvasGroupComponent != null) 
    {
      canvasGroupComponent.alpha = newAlpha;
    }
  }

  void SwitchGameState(GameState newState) {
    switch (newState) {
      case GameState.GS_GAMEPLAY: 
      {
        //reset gameplay
        ClearDeathScreen(false);
        SpawnAllMonsters(true);          
      }
      break;

      case GameState.GS_ENTER_DEATH: 
      {
        m_fStateTimer = fadeToBlackTime;
        ClearDeathScreen(true);
      }
      break;

      case GameState.GS_DEATH: 
      {
        //here we should spawn some kind of Screen fade effect.
        m_fStateTimer = deathIdleTime;
      }
      break;

      case GameState.GS_RESPAWN_FROM_DEATH: 
      {
        //cleanup should happen here
        m_fStateTimer = fadeToRespawnFromDeath;
        
      }
      break;
    }

    gs = newState;
  }

  void UpdateGameState() 
  {
    switch (gs) 
    {
      case GameState.GS_GAMEPLAY: 
      {
        //check to see if the player has died
        bool bPlayerDied = false;          

        if (bPlayerDied) 
        {
          SwitchGameState(GameState.GS_ENTER_DEATH);
        }
      }
      break;

      case GameState.GS_ENTER_DEATH: 
      {
        m_fStateTimer -= Time.deltaTime;
        if (m_fStateTimer <= 0.0f) 
        {
          SwitchGameState(GameState.GS_DEATH);
        } 
        else 
        {
          float newAlphaValue;
          if (fadeToBlackTime <= 0.0f) 
          {
            newAlphaValue = 0.0f;
          } 
          else 
          {
            newAlphaValue = 1.0f - (m_fStateTimer / fadeToBlackTime);
          }

          SetDeathScreenAlphaFade(newAlphaValue);
        }
      }
      break;

      case GameState.GS_DEATH: 
      {
        //check to see if death sequence is over
        bool bDeathSequenceOver = false;
        m_fStateTimer -= Time.deltaTime;
        if (m_fStateTimer <= 0.0f)
          bDeathSequenceOver = true;

        if (bDeathSequenceOver) 
        {
          SwitchGameState(GameState.GS_RESPAWN_FROM_DEATH);
        }
      }
      break;

      case GameState.GS_RESPAWN_FROM_DEATH: 
      {
        //fade death screen back to game
        m_fStateTimer -= Time.deltaTime;
        if (m_fStateTimer <= 0.0f) 
        {
          SetDeathScreenAlphaFade(0.0f);
          SwitchGameState(GameState.GS_GAMEPLAY);
        } 
        else 
        {
          float newAlphaValue;
          if (fadeToRespawnFromDeath <= 0.0f) 
          {
            newAlphaValue = 0.0f;
          } 
          else 
          {
            newAlphaValue = (m_fStateTimer / fadeToRespawnFromDeath);
          }

          SetDeathScreenAlphaFade(newAlphaValue);
        }
      }
      break;
    }
  }

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
		UpdateGameState();
	}
}
