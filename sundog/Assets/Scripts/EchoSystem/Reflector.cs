using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {

    public float timeout;

    public List<SoundProjector> collidedSounds;

    public GameObject effect;
    public GameObject barkEffect;
    public GameObject clickEffect;
    public GameObject stompEffect;
    public GameObject whistleEffect;
    public GameObject monsterEffect;

    public float minimumIntensity = 0.3f;

    private void Start()
    {
        collidedSounds = new List<SoundProjector>();
        MeshRenderer visible = GetComponent<MeshRenderer>();

        if (visible != null)
            visible.enabled = false;
        StartCoroutine(Forever());
    }
    public bool ShouldReflect(SoundProjector input)
    {
        if (!collidedSounds.Contains(input))
        {
            collidedSounds.Add(input);
            if (input.GetIntensity() > minimumIntensity)
            {
                SendMessage("Discovered", SendMessageOptions.DontRequireReceiver);
                SendMessage("EchoMessage", input, SendMessageOptions.DontRequireReceiver);
                
                return true;
            }
           
           
        }

        
        return false;

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
