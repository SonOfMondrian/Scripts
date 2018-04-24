using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raycast:MonoBehaviour
{
    RaycastHit hit;
    public float hittime;
    public float maxhittime;
    public bool start;

    // Use this for initialization
    void Start()
    {
        hittime = 0.0f;
        maxhittime = 1.0f;

        start = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position,transform.forward * 10.0f,Color.yellow);

        if(Physics.Raycast(transform.position,transform.forward,out hit,10.0f))
        {
            if(hit.collider.tag == "LEFT" || hit.collider.tag == "RIGHT" || hit.collider.tag == "GAMESTART"|| hit.collider.tag == "RETRY")
                hittime += Time.deltaTime;
            else
                hittime = 0;
            

            if(hittime >= maxhittime && hit.collider.tag == "GAMESTART" && start == false)     //게임시작
            {
                BrainManager.instance.LookGameStartObject(hit);
                //Debug.Log("gamestart");
                start = true;       //쳐다봤을때 딱 한번 호출하기 위해서(update함수라 계속호출됨)
            }
            if (hittime >= 2.0f && hit.collider.tag == "RETRY")     //재시작
            {
                BrainManager.instance.Retry();
                //Debug.Log("retry");
            }
            
            if (hittime >= maxhittime && hit.collider.tag == "LEFT")
                BrainManager.instance.LookedLeft();

            else if(hittime >= maxhittime && hit.collider.tag == "RIGHT")
                BrainManager.instance.LookedRight();
        }
        else
            hittime = 0;

    }
}
