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

    Rigidbody2D rb;

    Collider2D col;

    public float speed;
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
        float Hoz = Input.GetAxis("Horizontal") ;

        float Vert = Input.GetAxis("Vertical") ;

        Vector2 newPosition = transform.position + (new Vector3(Hoz, Vert) * Time.deltaTime);

        rb.MovePosition(newPosition);

        Vector3 cameraPosition = this.transform.position;
        cameraPosition.z = -10;
        Camera.main.transform.position = cameraPosition;
	}
}
