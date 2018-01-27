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
        

        float adjustedAngle = (angle != 0)? (angle - 90) + (arc / 2) : 0;

        Vector3 rotator = new Vector3(0, 0, 0 - adjustedAngle);

        GameObject newParticle = Instantiate(particleSystem);

        newParticle.transform.position = position;
        newParticle.transform.rotation = Quaternion.Euler(rotator);

        ParticleSystem[] fxl = newParticle.GetComponentsInChildren<ParticleSystem>();

        

        foreach (ParticleSystem fx in fxl)
        {
            if (fx.shape.enabled)
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
