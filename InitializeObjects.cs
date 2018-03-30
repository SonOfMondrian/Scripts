using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BrainManager.instance.OnLoadedScene();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
