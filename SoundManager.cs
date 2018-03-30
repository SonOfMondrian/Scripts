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

    public void PlayButton()
    {
        AudioPlayer.PlayOneShot(Clips[0]);
    }
	
}
