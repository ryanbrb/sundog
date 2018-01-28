using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour {

	public enum Action
	{
		NOTHING,
		BARK,
		WALK
	};

	Action action = Action.NOTHING;

	AudioSource audio;
	public List<AudioClip> audioList;

  AIFollowTargetOnCommand followBehavior;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		if(GetComponent<Reflector> () == null)
		{
			gameObject.AddComponent<Reflector> ();
		}

    //adding follow behavior if it hasn't been added
    followBehavior = GetComponent<AIFollowTargetOnCommand>();
    if(followBehavior == null) 
    {
      followBehavior = gameObject.AddComponent<AIFollowTargetOnCommand>();
    }

    //immediately disable the follow behavior until we need it.
    if(followBehavior != null) 
    {
      followBehavior.enabled = false;
    }
   
  }

  public void SetAction(Action newAction)
	{
		switch(newAction)
		{
		case Action.NOTHING:
			break;
		case Action.BARK:
			audio.PlayOneShot (audioList[0]);
			break;
		case Action.WALK:
			audio.PlayOneShot (audioList[1]);
			break;

		}
		action = newAction;
	}

	public void EchoMessage(SoundProjector input)
	{
		if(input.myType == SoundProjector.ProjectorType.whistle)
		{
			SetAction(Action.BARK);
			SetAction(Action.WALK);

			//input.transform.position
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
