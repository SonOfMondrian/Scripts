using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
    public Animator FadeAnimator;
    public GameObject Fadeobject;
    // Use this for initialization
    void Start () {
        Fadeobject = this.gameObject;
        FadeAnimator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadGameScene()
    {
        FadeAnimator.SetBool("FadeOut", true);
        Invoke("GameScene",1.0f);
    }

    public void GameScene()
    {

        SceneManager.LoadScene("Game");
    }
}
