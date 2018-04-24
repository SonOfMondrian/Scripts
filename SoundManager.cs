using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null; //싱글턴
    public AudioSource AudioPlayer;
    public AudioClip[] Clips;
    
    private void Awake()
    {
        instance = this;

        Clips = new AudioClip[9];

        Clips[0] = Resources.Load<AudioClip>("Sound/Correct");
        Clips[1] = Resources.Load<AudioClip>("Sound/DoorClose");
        Clips[2] = Resources.Load<AudioClip>("Sound/DoorOpen");
        Clips[3] = Resources.Load<AudioClip>("Sound/Startup");
        Clips[4] = Resources.Load<AudioClip>("Sound/FloorOpen");
        Clips[5] = Resources.Load<AudioClip>("Sound/InCorrect");
        Clips[6] = Resources.Load<AudioClip>("Sound/Levelup");
        Clips[7] = Resources.Load<AudioClip>("Sound/ObjectSelect");
		Clips [8] = Resources.Load<AudioClip> ("Sound/FallBody");

    }
    void Start () {
        AudioPlayer = this.GetComponent<AudioSource>();
	}

    public void PlayButtonSelect()
    {
        AudioPlayer.PlayOneShot(Clips[7]);
    }

    public void Correct()
    {
        AudioPlayer.PlayOneShot(Clips[0]);
    }

    public void DoorClose()
    {
        AudioPlayer.PlayOneShot(Clips[1]);
    }

    public void DoorOpen()
    {
        AudioPlayer.PlayOneShot(Clips[2]);

    }
    public void StartUp()
    {
        AudioPlayer.PlayOneShot(Clips[3]);
    }
    public void FloorOpen()
    {
        AudioPlayer.PlayOneShot(Clips[4]);
    }
    public void Incorrect()
    {
        AudioPlayer.PlayOneShot(Clips[5]);
    }
    public void LevelUp()
    {
        AudioPlayer.PlayOneShot(Clips[6]);
    }
		public void FallBody()
	{
		AudioPlayer.PlayOneShot (Clips [8]);
	}

}
