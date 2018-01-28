﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	public enum State
	{
		NO_STATE,
		PANIC, // heart beat
		ATTACKED, // breath
		DOES_ECHO
	};

	public enum Action
	{
		NOTHING,
		WALK,  //foot steps if not panic and not close by monster
		DIED,
		WRISTLE,
		CLICK,
		STOMP
	};

	State state = State.NO_STATE;
	Action action = Action.NOTHING;

	AudioSource audio;
	public List<AudioClip> audioList;

    Rigidbody2D rb;

    Collider2D col;

    public float speed = 5.0f;
	// Use this for initialization
	void Start ()
	{
		audio = GetComponent<AudioSource> ();
        rb = GetComponent<Rigidbody2D>();

        if(rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
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

//
//		//Stomp
//		SendSignal(SoundProjector.ProjectorType.stomp, 360, Vector2.up);
//	}
//
//	public void SendSignal(SoundProjector.ProjectorType type, float arc, Vector2 direction)
//	{
//
//		float angle = SoundProjector.GetFullAngle(direction);
//
//		GameObject temp = new GameObject();
//
//		temp.transform.position = this.transform.position;
//		temp.transform.rotation = this.transform.rotation;
//
//		SoundProjector proj = temp.AddComponent<SoundProjector>();
//		proj.Project(type,angle,arc);
//

	}
  
  public bool IsDead() 
  {
    return (action == Action.DIED);
  }

  void OnTriggerEnter2D(Collider2D other) 
  {
    MonsterManager monster = other.gameObject.GetComponent<MonsterManager>();
    if(monster != null) 
    {
      //we've collided with a monster, we're dead!
      //TODO: verify or modify death condition
      //for right now, one-hit death is okay
      SetAction(Action.DIED);
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
			audio.PlayOneShot (audioList [0]);
			break;
		case Action.DIED:
			audio.PlayOneShot (audioList [3]);
			break;

		case Action.WRISTLE:
			audio.PlayOneShot (audioList [4]);
			break;
		case Action.CLICK:
			audio.PlayOneShot (audioList [5]);
			break;
		case Action.STOMP:
			audio.PlayOneShot (audioList [6]);
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
        float Hoz = Input.GetAxis("Horizontal") ;

        float Vert = Input.GetAxis("Vertical") ;

        Vector2 newPosition = transform.position + (new Vector3(Hoz, Vert) * Time.deltaTime* speed);

        rb.MovePosition(newPosition);

        Vector3 cameraPosition = this.transform.position;
        cameraPosition.z = -10;
        Camera.main.transform.position = cameraPosition;

	}
}
