using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour {

	public SoundProjector.ProjectorType index; 
	Sprite Icon;
	public float radius;
	public float height;
	public GameObject particleSystem;


    public void SpawnParticleSystem(Vector3 position, float arc, float angle)
    {
        GameObject newParticle = Instantiate(particleSystem);

        ParticleSystem[] fxl = newParticle.GetComponentsInChildren<ParticleSystem>();

        float adjustedAngle = (angle - 90) + (arc/2);

        Vector3 rotator = new Vector3(0, 0, 0 - adjustedAngle);

        newParticle.transform.rotation = Quaternion.Euler(rotator);
  
        foreach (ParticleSystem fx in fxl)
        {
            if(fx.shape.enabled)
            {
                ParticleSystem.ShapeModule tempShape = fx.shape;
                tempShape.arc = arc;
                
                
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
