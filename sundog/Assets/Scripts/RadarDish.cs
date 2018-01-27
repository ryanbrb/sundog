using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDish : MonoBehaviour {
  public Transform radarTransform;
  private Quaternion radarQuat;

	// Use this for initialization
	void Start () {
    if (radarTransform != null) {
      radarQuat = radarTransform.rotation;
    }

  }

  // Update is called once per frame
  void Update () {
		if (radarTransform != null) {
      radarQuat = radarQuat * Quaternion.Euler(0, 40.0f * Time.deltaTime, 0);
      radarTransform.rotation = radarQuat;
    }
  }
}
