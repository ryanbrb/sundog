using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollowTargetOnCommand : MonoBehaviour {

  public GameObject target;
  public float followDistanceThreshold = 0.25f;
  NavMeshAgent m_agent;

	// Use this for initialization
	void Start () 
  {
    m_agent = GetComponent<NavMeshAgent>();

    //if we haven't assigned a target, let's assume it's the player
    if (target == null) 
    {
      PlayerManager[] playerManagers = GameObject.FindObjectsOfType<PlayerManager>();
      if (playerManagers.Length > 0) 
      {
        target = playerManagers[0].gameObject;
      }
    }
  }
  
	
	// Update is called once per frame
	void Update () 
  {
    if (target == null)
      return;

    //as long as this component updates, just go towards the target
    if(m_agent.remainingDistance > followDistanceThreshold)
      m_agent.destination = target.transform.position;
	}
}
