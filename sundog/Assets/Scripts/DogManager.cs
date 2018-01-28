using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour {

	public enum Action
	{
		NOTHING,
		GOTO_PLAYER,
		DIED
	};

	Action action = Action.NOTHING;

	AudioSource audio;
	public List<AudioClip> audioList;

  AIFollowTargetOnCommand followBehavior;

	float timerBark = 0.0f;

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
		case Action.GOTO_PLAYER:
			//foot step sound
			audio.PlayOneShot (audioList[1]);
			//add target to follow player
			this.GetComponent<AIFollowTargetInRange>().enabled = true;
			break;
		case Action.DIED:
			Destroy (this.gameObject);
			break;
		}
		action = newAction;
	}

	public void EchoMessage(SoundProjector input)
	{
		if(input.myType == SoundProjector.ProjectorType.whistle)
		{
			timerBark = 6.0f;
			newRandomTimeToBark = Random.Range (timerBark, 10.0f);
			SetAction(Action.GOTO_PLAYER);
		}
	}

	void BarkAndLight()
	{
		//bark
		audio.PlayOneShot (audioList[0]);
		//light on
		SendSignal(SoundProjector.ProjectorType.bark, 360, Vector2.zero);
		GetComponent<Echo> ().SpawnParticleSystem (this.transform.position, 360, 0);
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

	public void SendSignal(SoundProjector.ProjectorType type, float arc, Vector2 direction)
	{

		float angle = SoundProjector.GetFullAngle(direction);

		GameObject temp = new GameObject();

		temp.transform.position = this.transform.position;
		//temp.transform.rotation = this.transform.rotation;

		SoundProjector proj = temp.AddComponent<SoundProjector>();
		proj.Project(type,angle,arc);


	}

	float newRandomTimeToBark = 0.0f;
	// Update is called once per frame
	void Update () {

		if(action == Action.GOTO_PLAYER)
		{
			timerBark += Time.deltaTime;

			if(timerBark > newRandomTimeToBark)
			{
				timerBark = 0.0f;
				newRandomTimeToBark = Random.Range (6.0f, 10.0f);
				BarkAndLight ();
			}
		}
	}
}
