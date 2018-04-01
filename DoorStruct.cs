using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    public void idleOpendoor()
    {
        this.GetComponent<Animator>().SetBool("OpenDoor",false);
    }

    public void idleClosedoor()
    {
        this.GetComponent<Animator>().SetBool("CloseDoor",false);
    }
}
