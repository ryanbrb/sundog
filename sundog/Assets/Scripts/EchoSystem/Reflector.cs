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
                GameObject temp = null;
                switch (input.myType)
                {
                    case SoundProjector.ProjectorType.bark:
                        temp = barkEffect;
                        break;
                    case SoundProjector.ProjectorType.click:
                        temp = clickEffect;
                        break;
                    case SoundProjector.ProjectorType.whistle:
                        temp = whistleEffect;
                        break;
                    case SoundProjector.ProjectorType.stomp:
                        temp = stompEffect;
                        break;
                    case SoundProjector.ProjectorType.monster:
                        temp = monsterEffect;
                        break;
                    default:
                        temp = effect;
                        break;
                }
                if (temp != null)
                {
                    temp = Instantiate(temp);
                    temp.transform.position = this.transform.position;
                    temp.transform.rotation = this.transform.rotation;
                }
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
