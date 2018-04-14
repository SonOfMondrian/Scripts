using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelStruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenTheDoor()
    {
        BrainManager.instance.DoorAnimation.SetBool("OpenDoor", true);
        SoundManager.instance.DoorOpen();
    }
}
