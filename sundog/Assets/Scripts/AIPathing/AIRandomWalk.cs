using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRandomWalk : MonoBehaviour {
  public float m_Range = 25.0f;
  NavMeshAgent m_agent;

  // Use this for initialization
  void Start () {
    m_agent = GetComponent<NavMeshAgent>();
  }


	// Update is called once per frame
	void Update () {
    if (m_agent.pathPending || m_agent.remainingDistance > 0.1f)
      return;

		m_agent.destination = new Vector3(m_Range * Random.insideUnitCircle.x, m_Range * Random.insideUnitCircle.y, 0.0f) ;

  }

  void OnDrawGizmosSelected() 
  {
    Transform myTransform = gameObject.transform;
  
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(myTransform.position, m_Range);    
  }
}
