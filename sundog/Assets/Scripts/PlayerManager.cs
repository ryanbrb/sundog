using System.Collections;
using System.Collections.Generic;
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

    public float speed = 3;
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

        col =  GetComponent<Collider2D>();
        if(col)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
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
