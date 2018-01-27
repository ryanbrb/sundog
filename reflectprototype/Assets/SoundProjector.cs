using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProjector : MonoBehaviour {

    public enum ProjectorType
    {
        basic
    }

    public enum ProjectorShape
    {
        circle
    }

    //settings
    public float InitialIntensity = 1;
    public float IntensityFallOff = 0.1f; // todo none-linear;
    public float ProjectionRate = 1;
    public float MaxRadius = 5;

    public ProjectorShape myShape;
    public ProjectorType myType;


    //book keeping
    public float Intensity = 1;
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

    public void Project()
    {
        CircleRadius = 0;
        Intensity = InitialIntensity;
        CircleTargetRadius = MaxRadius;
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
