using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null; //싱글턴

    public AudioSource AudioPlayer;
    public AudioClip[] Clips;


    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        AudioPlayer = this.GetComponent<AudioSource>();
        Clips = Resources.LoadAll<AudioClip>("Sound");
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

}
