//
//  particleDestruct.cs
// script authored by Ryan VanMeter
//

using UnityEngine;
using System.Collections;

// this kills particles so they don't live forever
// not great because you have to make sure that the game object starts disabled otherwise the particle will instantly kill itself
public class ParticleDestruct : MonoBehaviour {

	private ParticleSystem particleComponent;
	private float duration;
	private bool killMe = false;

	void Awake () {
		particleComponent = GetComponent<ParticleSystem>();
		duration = particleComponent.duration;
	}

	void Update() {
		if (killMe == false && gameObject.activeInHierarchy == true) {
			Destroy (this.gameObject, duration);
			killMe = true;
		}
	}
}