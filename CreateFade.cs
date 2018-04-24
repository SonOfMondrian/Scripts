using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFade : MonoBehaviour {

    public GameObject FadePrefab;
    public GameObject FadeObject;
    float FadeZposition;
   // FadeZposition = 0.2f;
    void OnEnable()
    {
        FadePrefab = Resources.Load<GameObject>("Prefabs/FadePanel");
        FadeObject = Instantiate(FadePrefab, this.transform);
    }
	// Use this for initialization
	void Start () {

        FadeObject.GetComponentInChildren<Animator>().Play("FadeIn");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
