using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{

    RaycastHit hit;
    public float hittime;
    public float maxhittime;
    
    public bool  start;
   

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

        Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.yellow);


        if (SceneManager.GetActiveScene().buildIndex == 0)      //빌트씬0 번째 일때 (Main Scene)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
            {
                if (hit.collider.tag == "GAMESTART")
                {
                    hittime += Time.deltaTime;
                }
                else
                    hittime = 0;


                /*if (hittime >= maxhittime && hit.collider.tag == "CORRECTBOX" && left == false)
                {
                    RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    LeftBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    source.PlayOneShot(clip);
                    right = false;
                    left = true;
                }
                else if (hittime >= maxhittime && hit.collider.tag == "INCORRECTBOX" && right == false)
                {
                    LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    RightBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    source.PlayOneShot(clip);
                    right = true;
                    left = false;
                }*/


                if (hittime >= maxhittime && hit.collider.tag == "GAMESTART" && start == false)     //게임시작
                {
                    BrainManager.instance.LookGameStartObject(hit);
                    start = true;       //쳐다봤을때 딱 한번 호출하기 위해서(update함수라 계속호출됨)
                }
            }
            else
                hittime = 0;

        }

        else if (SceneManager.GetActiveScene().buildIndex == 1)      //빌트씬 1 번째 일때 (Game Scene)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
            {
                if(hit.collider.tag == "LEFT" || hit.collider.tag == "RIGHT")
                {
                    hittime += Time.deltaTime;
                }
                else
                    hittime = 0;

                if (hittime >= maxhittime && hit.collider.tag == "LEFT")
                {
                    //Debug.Log(hit.collider.name);
                    BrainManager.instance.LookedLeft();
                    
                }
                else if (hittime >= maxhittime && hit.collider.tag == "RIGHT")
                {
                    //Debug.Log(hit.collider.name);
                    BrainManager.instance.LookedRight();
                   
                }
            }
            else
                hittime = 0;

        }
    }
}
