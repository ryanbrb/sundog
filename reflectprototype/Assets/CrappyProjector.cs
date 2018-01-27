using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrappyProjector : MonoBehaviour {


	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        bool project = Input.GetKeyUp(KeyCode.K);
		if(project)
        {
            GameObject temp = new GameObject();

            temp.transform.position = this.transform.position;
            temp.transform.rotation = this.transform.rotation;

            SoundProjector proj = temp.AddComponent<SoundProjector>();
            proj.Project();
             
        }

    }

}
