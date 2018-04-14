using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEye : MonoBehaviour {
    public Transform Eye;
	// Use this for initialization
	void Start () {
        Eye = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void LateUpdate()
    {
       // transform.position = Eye.position+ new Vector3(0,0,0.1f);
        //transform.LookAt(Eye);
    }
}
