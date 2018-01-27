using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {

   

    public enum ReflectorType
    {
        basic = 0
    }

    public float timeout;

    List<SoundProjector> collidedSounds;

    public ReflectorType type = ReflectorType.basic;
    public float minimumIntensity = 0.3f;

    private void Start()
    {
        collidedSounds = new List<SoundProjector>();
        StartCoroutine(Forever());
    }
    public bool ShouldReflect(SoundProjector input)
    {
        if (!collidedSounds.Contains(input))
        {
            collidedSounds.Add(input);


            return input.GetIntensity() > minimumIntensity;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Forever()
    {
        while (true)
        {
            List<SoundProjector> newlist = new List<SoundProjector>();
            foreach (var bar in collidedSounds)
            {
                if (bar != null)
                {
                    newlist.Add(bar);
                }
            }

            collidedSounds = newlist;
            yield return new WaitForSeconds(0.1f + Random.Range(0, 0.5f));
        }
    }

}
