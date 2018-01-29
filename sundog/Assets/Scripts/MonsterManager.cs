using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

	public enum Action
	{
		NOTHING,
		DISCOVERED,
		ATTACK

	};

	Action action = Action.NOTHING;

	AudioSource audio;
	public List<AudioClip> audioList;

    Animator myAnimator;
    Transform child;

	public static bool iAmChasingThePlayer = false;

    // Use this for initialization
    void Start () {
		audio = GetComponent<AudioSource> ();
	
		if(GetComponent<Reflector> () == null)
		{
			gameObject.AddComponent<Reflector> ();
		}

        myAnimator = GetComponentInChildren<Animator>();
        child = transform.GetChild(0);

    }

    public void KillPlayer()
    {

    }

	public void SetAction(Action newAction)
	{
		GameManager GM = GameObject.FindObjectOfType<GameManager> ();
		PlayerManager PM = GameObject.FindObjectOfType<PlayerManager> ();


		switch(newAction)
		{
		case Action.NOTHING:
                if(myAnimator != null)
                {
                    myAnimator.SetBool("IsMoving", false);
                }
			GM.GetComponent<GameManager>().SwitchGameEvent(GameManager.GameEvent.NOTHING);
			break;
		case Action.DISCOVERED:
                if (myAnimator != null)
                {
                    myAnimator.SetBool("IsMoving", true);
                }
               
                audio.PlayOneShot (audioList [0]);
			PM.GetComponent<PlayerManager> ().SetState (PlayerManager.State.PANIC);
			//audio.loop = false;
			break;
		case Action.ATTACK:
                if (myAnimator != null)
                {
                    myAnimator.SetBool("IsMoving", true);
                }
                timerScream = 15.0f;
			newRandomTimeToScream = Random.Range (timerScream, 30.0f);
			GM.GetComponent<GameManager>().SwitchGameEvent(GameManager.GameEvent.ATTACK);
			PM.GetComponent<PlayerManager> ().SetState (PlayerManager.State.ATTACKED);
			//audio.loop = true;
			break;

		} 
		action = newAction;
	}
		

	public void Discovered()
	{
		SetAction(Action.DISCOVERED);
	}


    void Roar()
    {
        //bark
        if(myAnimator != null)
        myAnimator.SetTrigger("Attack");
        audio.PlayOneShot(audioList[0]);
        //light on
        SendSignal(SoundProjector.ProjectorType.monster, 360, Vector2.zero);

        GetComponent<Echo>().SpawnParticleSystem(this.transform.position, 360, 0);

    }

    public void SendSignal(SoundProjector.ProjectorType type, float arc, Vector2 direction)
    {

        float angle = SoundProjector.GetFullAngle(direction);

        GameObject temp = new GameObject();

        temp.transform.position = this.transform.position;
        //temp.transform.rotation = this.transform.rotation;

        SoundProjector proj = temp.AddComponent<SoundProjector>();
        proj.Project(type, angle, arc);


    }

    float timerScream = 0.0f;
	float newRandomTimeToScream = 0.0f;
	// Update is called once per frame
	void Update () {
		if(action == Action.ATTACK)
		{
			timerScream += Time.deltaTime;

			if(timerScream > newRandomTimeToScream)
			{
				timerScream = 0.0f;
   

                newRandomTimeToScream = Random.Range (15.0f, 30.0f);
				audio.PlayOneShot (audioList [1]);
			}
		}
        child.eulerAngles = new Vector3(0, 0, 0);



		iAmChasingThePlayer = GetComponent<AIFollowTargetInRange> ().chasingThePlayer;
    }
}
