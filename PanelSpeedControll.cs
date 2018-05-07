using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSpeedControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("QuizPanel").GetComponent<Animator>().speed = 1.0f;

    }
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.Space))
  //      {
  //          Debug.Log("speed UP");
  //          GameObject.Find("QuizPanel").GetComponent<Animator>().speed += 1.0f;

  //      }
	}
}
