using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallColl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col)
	{		
		if (col.transform.tag == "FLOOR") {
			Debug.Log ("죽음!");
            col.gameObject.GetComponent<AudioSource>().Play();
			//SoundManager.instance.FallBody ();		
		}
	}
}