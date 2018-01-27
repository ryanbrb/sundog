using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Travels between an array of transform (nodes)
 */
[RequireComponent(typeof(NavMeshAgent))]
public class AINodePathing : MonoBehaviour {

  public enum PathingMovementTypes { OneWay, Wrap, PingPong};

  //public accessible variables
  public Transform[] PathNodes;
  //public bool WrapAround = false;
  public PathingMovementTypes MovementType = AINodePathing.PathingMovementTypes.OneWay;
  public float RemainingDistanceThreshold = 0.1f;
  
  //local variables declaration
  NavMeshAgent m_Agent;
  int pathNodeIndex = -1;
  bool bPingPongDirectionForward = true;


  // Use this for initialization
  void Start () {
    m_Agent = GetComponent<NavMeshAgent>();
    MakeInitialMove();
	}

  void MakeInitialMove() {
    pathNodeIndex = 0;
    m_Agent.destination = PathNodes[pathNodeIndex].position;
  }
	
	// Update is called once per frame
	void Update () {

    //check if our agent is within the threshold of it's target
    if(m_Agent.remainingDistance < RemainingDistanceThreshold)
    {
      int numberOfPathNodes = PathNodes.Length;
      int previousPathNodeIndex = pathNodeIndex;

      //progress the index
      if (pathNodeIndex < numberOfPathNodes) {
        if (MovementType != PathingMovementTypes.PingPong ||
          bPingPongDirectionForward) {
          pathNodeIndex++;
        } 
        else {
          pathNodeIndex--;
        }
      }

      //make index correction depending on the movement type
      if (pathNodeIndex >= numberOfPathNodes) {
        if (MovementType == PathingMovementTypes.Wrap) {
          pathNodeIndex = 0;
        }
        else if (MovementType == PathingMovementTypes.PingPong) {
          pathNodeIndex = pathNodeIndex - 1;
          bPingPongDirectionForward = false;
        }
        else if (MovementType == PathingMovementTypes.OneWay) {
          pathNodeIndex = numberOfPathNodes - 1;
        }
      }
      //this is likely because we were ping-ponging
      else if(pathNodeIndex < 0) {
        if(MovementType == PathingMovementTypes.PingPong) {
          if(numberOfPathNodes > 1) {
            pathNodeIndex = 1;
            bPingPongDirectionForward = true;
          }
        }
      }

      //set the destination if we're targeting a different node
      if(previousPathNodeIndex != pathNodeIndex && pathNodeIndex >= 0 && pathNodeIndex < numberOfPathNodes) {
        m_Agent.destination = PathNodes[pathNodeIndex].position;
      }
    }
	}
}
