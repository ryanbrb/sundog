using System.Collections;
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
    public float InitialIntensity = 10;
    public float IntensityFallOff = 0.1f; // todo none-linear;
    public float ProjectionRate = 1;
    public float MaxRadius = 10;
    public ProjectorType myType;

    public float myAngle;
    public float myArc;

    //book keeping
    float Intensity = 1;
    float CircleTargetRadius;
    float CircleRadius = 0;

    public float GetIntensity()
    {
        return Intensity;
    }

    public void ProjectCircle(Vector2 input)
    {
        CircleRadius = Mathf.Lerp(CircleRadius, CircleTargetRadius, Time.deltaTime);
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

        Intensity -= IntensityFallOff*Time.deltaTime; // TODO should not be linear;
        ProjectCircle(transform.position);
    }


    public void CheckObject(Collider2D result)
    {

        Reflector temp = result.gameObject.GetComponent<Reflector>();
        if (temp != null)
        {
            Vector3 direction = result.transform.position - this.transform.position;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == result && temp.ShouldReflect(this))
                {
                    Debug.DrawRay(transform.position, direction, Color.cyan, 1);
                    break;
                }
                else
                {

                    Debug.DrawRay(transform.position, direction, Color.red, 1);
                    break;
                }
            }


        }
    }

}
