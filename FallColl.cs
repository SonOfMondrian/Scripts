using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallColl : MonoBehaviour {
    
	void OnCollisionEnter(Collision col)
	{		
		if (col.transform.tag == "FLOOR") {
			//Debug.Log ("죽음!");
            col.gameObject.GetComponent<AudioSource>().Play();
            BrainManager.instance.WindHowlSound.GetComponentInChildren<AudioSource>().Stop();
		}
	}
}