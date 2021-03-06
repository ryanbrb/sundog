﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProjector : MonoBehaviour {

    public enum ProjectorType
    {
        none,
        basic,
        bark,
        whistle,
        stomp,
        click,
        monster
    }

    //settings
    public float InitialIntensity = 1;
    public float IntensityFallOff = 0.1f; // todo none-linear;
    public float ProjectionRate = 5;
    public float MaxRadius = 10;
    public ProjectorType myType;

    public float myAngle = 0;
    public float myArc;

    //book keeping
    float Intensity = 1;
    float CircleTargetRadius;
    float CircleRadius = 0;
    Light spot;

    public void Start()
    {
        Vector3 setPos = this.transform.position;

        setPos.z = -10;
        this.transform.position = setPos;

        spot = this.gameObject.AddComponent<Light>();

        spot.type = LightType.Spot;
        spot.intensity = 0;
        spot.spotAngle = 0;
        spot.range = 12;
        spot.bounceIntensity = 0;
    }

    public float GetIntensity()
    {
        return Intensity;
    }

    public void ProjectCircle(Vector2 input)
    {
        CircleRadius = Mathf.Lerp(CircleRadius, CircleTargetRadius, Time.deltaTime* ProjectionRate);
        Collider2D[] results = Physics2D.OverlapCircleAll(input, CircleRadius);
        foreach (Collider2D result in results)
        {
            CheckObject(result);
        }

    }

    public void Project(ProjectorType type = ProjectorType.none, float angle = -1, float arc = -1, float intensity = -1, float falloff = -1, float speed = -1, float maxDistance = -1)
    {
        CircleRadius =  0;
        Intensity = (intensity < 0)? InitialIntensity : intensity;
        IntensityFallOff = (falloff < 0) ? IntensityFallOff : falloff;
        ProjectionRate = (speed < 0) ? ProjectionRate : speed;
        CircleTargetRadius = (maxDistance < 0)? MaxRadius : maxDistance;
        myType = (type == ProjectorType.none) ? myType : type;
        myAngle = (angle < 0) ? myAngle : angle;
        myArc = (arc < 0) ? myArc : arc;

       
    }

    void Update()
    {
        if (CircleTargetRadius <= float.Epsilon)
        {
            CircleRadius = 0;
            return;
        }
        if(Intensity <= 0)
        {
            Destroy(gameObject);
            return;
        }
        Intensity -= IntensityFallOff*Time.deltaTime*ProjectionRate; // TODO should not be linear;

        ProjectCircle(transform.position);
        spot.spotAngle = GetSpotAngle();
        spot.intensity = Intensity * 40;
  
    }

    float GetSpotAngle()
    {
        Vector3 compareposition = this.transform.position + (Vector3.up*CircleRadius);
        compareposition.z = 0;

        Debug.DrawLine(transform.position, compareposition,Color.yellow);
        Vector3 compareDirection = (compareposition - this.transform.position);
        float diff =  Vector3.Angle(Vector3.forward, compareDirection);
       

        return diff*1.5f;
    }
    public static float GetFullAngle(Vector2 direction)
    {
       return (direction.x > 0) ? Vector2.Angle(Vector2.up, direction) : 360 - Vector2.Angle(Vector2.up, direction);
    }

    public static bool IsAngleBetweenArc(float angleA, float arc, float angleB)
    {
        float diffAngle = Mathf.Abs(angleA - angleB);

        float halfArc = arc / 2;

        return diffAngle <= halfArc;
    }
    public void CheckObject(Collider2D result)
    {

        Reflector temp = result.gameObject.GetComponent<Reflector>();
        if (temp != null)
        {
        
            Vector3 direction = result.transform.position - this.transform.position;

            if(IsAngleBetweenArc(GetFullAngle(direction), myArc, myAngle))
            {
               
               
                   
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

                    foreach (RaycastHit2D hit in hits)
                    {

                        if (hit.collider == result)
                        {
                            if(temp.ShouldReflect(this))
                                Debug.DrawLine(transform.position, hit.point, Color.cyan, 10);

                            break;
                        }
                        else
                        {
                            PlayerManager player = hit.collider.GetComponent<PlayerManager>();
                            if (player == null)
                            {
                                Debug.DrawLine(transform.position, hit.point, Color.red, 10);
                                break;
                            }
                        }
                    }
                
            }

        }
    }

    void OnDrawGizmos()
    {
        Color linecolor = Color.white;
        linecolor.a = Intensity;
        Gizmos.color = linecolor;
        Gizmos.DrawWireSphere(transform.position, CircleRadius);
    }

}
