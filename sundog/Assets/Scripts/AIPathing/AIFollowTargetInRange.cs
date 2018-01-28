using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollowTargetInRange : MonoBehaviour
{

	//editor accessible variables
	public GameObject targetGameObject = null;
	public float LineOfSightCheckFrequency = 1.5f;
	public float LineOfSightCheckFrequencyDuringFollow = 0.5f;
	public float SightRadius = 15.0f;

	enum FollowTargetState
	{
		FTS_SEARCH,
		FTS_GO_TO_TARGET}

	;

	//member variables
	NavMeshAgent m_agent;
	AIRandomWalk m_randomWalkComponent;
	FollowTargetState m_followTargetState;
	float m_fStateTimer;

	// Use this for initialization
	void Start ()
	{
		m_agent = GetComponent<NavMeshAgent> ();
		m_randomWalkComponent = GetComponent<AIRandomWalk> ();

		//if we haven't assigned a target, let's assume it's the player
		if (targetGameObject == null) {
			PlayerManager[] playerManagers = GameObject.FindObjectsOfType<PlayerManager> ();
			if (playerManagers.Length > 0) {
				targetGameObject = playerManagers [0].gameObject;
			}
		}

		SwitchState (FollowTargetState.FTS_SEARCH);
	}

	void SwitchState (FollowTargetState newState)
	{
		switch (newState) {
		case FollowTargetState.FTS_SEARCH: 
			{
				m_fStateTimer = LineOfSightCheckFrequency;
				//if you're searching, go about your normal behavior
				if (m_randomWalkComponent != null) {
					m_randomWalkComponent.enabled = true;
				}
			}
			break;

		case FollowTargetState.FTS_GO_TO_TARGET: 
			{
				m_fStateTimer = LineOfSightCheckFrequencyDuringFollow;
				//if we've found a target we are no longer
				//interested in our previous behavior
				if (m_randomWalkComponent != null) {
					m_randomWalkComponent.enabled = false;
				}

				m_agent.destination = targetGameObject.transform.position;
			}
			break;
		}
		m_followTargetState = newState;
	}

	bool HasLineOfSightTowardsTarget (ref float outDistance)
	{
		if (targetGameObject == null)
			return false;

		bool bResult = false;

		Vector2 targetDirection = targetGameObject.transform.position - transform.position;
		targetDirection.Normalize ();

		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, targetDirection);
    
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null) {
				if (hit.collider.gameObject == targetGameObject) {
					//we've found the target, let's see if it's within range
					outDistance = hit.distance;
					bResult = true;
					break;
				}
			}
		}
		return bResult;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (targetGameObject == null)
			return;

		switch (m_followTargetState) {
		case FollowTargetState.FTS_SEARCH: 
			{
				m_fStateTimer -= Time.deltaTime;
				if (m_fStateTimer <= 0.0f) {
					//check to see if we have line of sight with our target
					float outDistance = 0.0f;
					if (HasLineOfSightTowardsTarget (ref outDistance)) {
						if (outDistance <= SightRadius) {
							SwitchState (FollowTargetState.FTS_GO_TO_TARGET);
						}
					}
            
					m_fStateTimer = LineOfSightCheckFrequency;
				}
			}
			break;

		case FollowTargetState.FTS_GO_TO_TARGET: 
			{
				bool bUpdateTargetPosition = false;

				//periodically check if we still have line of sight
				m_fStateTimer -= Time.deltaTime;
				if (m_fStateTimer <= 0.0f) {
					//check to see if we have line of sight with our target
					float outDistance = 0.0f;
					if (HasLineOfSightTowardsTarget (ref outDistance)) {
						bUpdateTargetPosition = true;
						m_fStateTimer = LineOfSightCheckFrequencyDuringFollow;
					}
				}

				//if we had line of sight, update target position
				if (bUpdateTargetPosition) {
					m_agent.destination = targetGameObject.transform.position;
				} else {
					if (!m_agent.pathPending || m_agent.remainingDistance < 0.1f) {
						//we've gone to the target location
						//in order to make the AI smarter, we could check for line
						//of sight again here

						//but instead let's just go into search again.
						SwitchState (FollowTargetState.FTS_SEARCH);
					}
				}
			}
			break;
		}
	}
}
