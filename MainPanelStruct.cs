using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelStruct : MonoBehaviour {

    public void OpenTheDoor()
    {
        BrainManager.instance.DoorAnimation.SetBool("OpenDoor", true);
        SoundManager.instance.DoorOpen();
    }

	public void ElevatorSound()
	{
		SoundManager.instance.LevelUp ();
	}
}
