using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	public enum State
	{
		NO_STATE,
		PANIC,
		// heart beat
		ATTACKED,
		// breath
		DOES_ECHO}

	;

	public enum Action
	{
		NOTHING,
		WALK,
		//foot steps if not panic and not close by monster
		DIED,
		WRISTLE,
		CLICK,
		STOMP}

	;

	bool bReachedEndOfLevel = false;

	State state = State.NO_STATE;
	Action action = Action.NOTHING;

	AudioSource audio;
	public List<AudioClip> audioList;

	Rigidbody2D rb;

	Collider2D col;

	public float speed = 3.0f;
	// Use this for initialization
	void Start ()
	{
		audio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();

		if (rb == null) {
			rb = gameObject.AddComponent<Rigidbody2D> ();
			rb.gravityScale = 0;
		}

		//removed temporarily because CircleCollider2D was added to the player prefab
		//this was doubling up on the Colliders, the collider on the prefab was easier
		//to adjust as needed to get trigger collision working (kmartinez)
		//col =  GetComponent<Collider2D>();
		//if(col)
		//{
		//    col = gameObject.AddComponent<CircleCollider2D>();
		//}

	}

	public bool IsDead ()
	{
		return (action == Action.DIED);
	}

	public bool HasReachedEndOfLevel ()
	{
		return bReachedEndOfLevel;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		MonsterManager monster = other.gameObject.GetComponent<MonsterManager> ();
		if (monster != null) {
			//we've collided with a monster, we're dead!
			//TODO: verify or modify death condition
			//for right now, one-hit death is okay
			monster.KillPlayer ();
			SetAction (Action.DIED);
		}
		bool bIsVictoryVolume = other.gameObject.name.Equals ("ExitVictoryVolume");
		if (bIsVictoryVolume) {
			bReachedEndOfLevel = true;
		}
		MonsterManager[] monstersfound = GameObject.FindObjectsOfType<MonsterManager> ();

		if (other.name == "ROOM1") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster1") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = true;
						mm.SetAction (MonsterManager.Action.ATTACK);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM2") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster2") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = true;
						mm.SetAction (MonsterManager.Action.ATTACK);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM3") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster3") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = true;
						mm.SetAction (MonsterManager.Action.ATTACK);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM4") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster4") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = true;
						mm.SetAction (MonsterManager.Action.ATTACK);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM5") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster5") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = true;
						mm.SetAction (MonsterManager.Action.ATTACK);
					}	
					break;
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		MonsterManager[] monstersfound = GameObject.FindObjectsOfType<MonsterManager> ();

		if (other.name == "ROOM1") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster1") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = false;
						mm.SetAction (MonsterManager.Action.NOTHING);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM2") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster2") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = false;
						mm.SetAction (MonsterManager.Action.NOTHING);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM3") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster3") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = false;
						mm.SetAction (MonsterManager.Action.NOTHING);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM4") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster4") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = false;
						mm.SetAction (MonsterManager.Action.NOTHING);
					}	
					break;
				}
			}
		} else if (other.name == "ROOM5") {
			foreach (MonsterManager mm in monstersfound) {
				if (mm.name == "monster5") {
					if(mm.GetComponent<AIFollowTargetOnCommand> ())
					{
						mm.GetComponent<AIFollowTargetOnCommand> ().enabled = false;
						mm.SetAction (MonsterManager.Action.NOTHING);
					}	
					break;
				}
			}
		}
	}

	public void SetState (State newState)
	{
		switch (newState) {
		case State.NO_STATE:
			break;
		case State.PANIC:
			audio.PlayOneShot (audioList [1]);
			break;
		case State.ATTACKED:
			audio.PlayOneShot (audioList [2]);
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
			audio.PlayOneShot (audioList [0]);
			break;
		case Action.DIED:
			audio.PlayOneShot (audioList [3]);
			break;

		case Action.WRISTLE:
			audio.PlayOneShot (audioList [4]);
			SetAction (Action.NOTHING);
			break;
		case Action.CLICK:
			audio.PlayOneShot (audioList [5]);
			SetAction (Action.NOTHING);
			break;
		case Action.STOMP:
			audio.PlayOneShot (audioList [6]);
			SetAction (Action.NOTHING);
			break;
		}
		action = newAction;
	}

	//	void PlayAudioFromGameManager (int i)
	//	{
	//		audio.PlayOneShot (audioList [i]);
	//		audio.Play ();
	//
	//	}
	
	// Update is called once per frame
	void Update ()
	{
		//player moving around
		float Hoz = Input.GetAxis ("Horizontal");

		float Vert = Input.GetAxis ("Vertical");

		Vector2 newPosition = transform.position + (new Vector3 (Hoz, Vert) * Time.deltaTime * speed);

		rb.MovePosition (newPosition);

		Vector3 cameraPosition = this.transform.position;
		cameraPosition.z = -10;
		Camera.main.transform.position = cameraPosition;

	}
}
