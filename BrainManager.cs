using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrainManager : MonoBehaviour
{

    public static BrainManager instance = null; //싱글턴

    public bool IsLeft, IsRight;        //왼쪽 오른쪽 박스 체크

    public bool AfterCheck;             //비활성 되었있는 Player를 찾기 위해 1초후에 찾기 위한 체크변수
    public float AfterCheckTime;        //Player를찾기위한 시간 변수
    public GameObject Player;           //Player오브젝트

    public GameObject LeftBox, RightBox;
    public GameObject Timer;
    public Material[] mats;

    public int CorrectNum;

    private AudioSource source;
    private AudioClip clip;

    // public GameObject Fadeobject;
    private void OnEnable()
    {
        //if (SceneManager.GetActiveScene().name == "Main")
        //this.GetComponent<PanelSpeedControll>().enabled = false;
    }
    void Awake()
    {
        //DontDestroyOnLoad(this);
        instance = this;
        mats = new Material[3];

        mats[0] = Resources.Load<Material>("Materials/Blue");
        mats[1] = Resources.Load<Material>("Materials/Red");
        mats[2] = Resources.Load<Material>("Materials/idleTimer");
    }
    void Start()
    {
        AfterCheckTime = 0;
        AfterCheck = false;
        IsLeft = false;
        IsRight = false;
    }

    void Update()
    {
        if (!AfterCheck && SceneManager.GetActiveScene().name == "Game")
            AfterCheckTime += Time.deltaTime;

        if (!AfterCheck && AfterCheckTime >= 1.0f)
        {
            AfterCheck = true;
            FindPlayer();
        }
    }
    public void OnLoadedScene()     //게임씬으로 이동하면 각종 오브젝트들을 초기화 해주는 함수(OnLevelWasLoaded함수가 Awake함수보다 빨리 호출되어 초기화가 어렵다)
    {
        LeftBox = GameObject.Find("Left");
        RightBox = GameObject.Find("Right");
        Timer = GameObject.Find("Timer");
        mats[0] = Resources.Load<Material>("Materials/Blue");
        mats[1] = Resources.Load<Material>("Materials/Red");
        mats[2] = Resources.Load<Material>("Materials/idleTimer");
        this.GetComponent<HandleTextFile>().QuizFunction = GameObject.Find("QuizPanel").transform.Find("Panel").gameObject;
        SetQuiz1(true);
    }
    void FindPlayer()       //게임씬으로 이동후 1초후 함수 호출
    {
        Player = GameObject.Find("Player");             //일단 시뮬레이터 오브젝트 초기화
        if (Player == null)                             // 근데 없으면 
            Player = GameObject.Find("CenterEyeAnchor");//오큘러스 오브젝트를 초기화
        
    }

    public void SetQuiz1(bool first = false)      //문제 출제 최초 출발 함수 (Start함수에서 함수호출하면서 일단 최초로 시작됨)
    {

        if (SceneManager.GetActiveScene().name != "Game") return;       //조기리턴, 버그로 메뉴인 상태에서 함수 호출을 막기위한 미연의 방지

        RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        Timer.transform.Find("body").GetComponent<Renderer>().material = mats[2];
        Timer.transform.Find("Arrow").GetComponent<Renderer>().material = mats[2];

        if (first == false)
            Player.GetComponent<Raycast>().hittime = 0.0f;

        IsLeft = false;
        IsRight = false;

        this.GetComponent<HandleTextFile>().SetQuiz2();

    }
    public void LookedLeft()
    {
        if (IsLeft == false)

        {
            RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            LeftBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Timer.transform.Find("body").GetComponent<Renderer>().material = mats[1];
            Timer.transform.Find("Arrow").GetComponent<Renderer>().material = mats[1];

            //source.PlayOneShot(clip);       //사운드 매니저 작업 필요함
            SoundManager.instance.PlayButton();

            IsLeft = true;
            IsRight = false;

            CheckQuiz('L');

        }
    }

    public void CheckQuiz(char direct)
    {
        if (direct == 'L')
        {
            if (LeftBox.transform.Find("LeftText").GetComponent<TextMesh>().text == CorrectNum.ToString())
                Debug.Log("정답입니다");

            else
                Debug.Log("오답입니다");
        }
        else if (direct == 'R')
        {
            if (RightBox.transform.Find("RightText").GetComponent<TextMesh>().text == CorrectNum.ToString())
                Debug.Log("정답입니다");
            else
                Debug.Log("오답입니다");

        }

    }

    public void LookedRight()
    {
        if (IsRight == false)
        {

            //Debug.Log("오른쪽봄");
            LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            RightBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");


            Timer.transform.Find("body").GetComponent<Renderer>().material = mats[0];
            Timer.transform.Find("Arrow").GetComponent<Renderer>().material = mats[0];

            SoundManager.instance.PlayButton();
            IsLeft = false;
            IsRight = true;
            CheckQuiz('R');
        }

    }

    public void LookGameStartObject(RaycastHit hit)
    {
        hit.collider.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        GameObject.Find("Fade").GetComponent<GameStart>().LoadGameScene();

        SoundManager.instance.PlayButton();

    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            //LeftBox = GameObject.Find("Left");
            //RightBox = GameObject.Find("Right");
            //Timer = GameObject.Find("Timer");
            //mats[0] = Resources.Load<Material>("Materials/Blue");
            //mats[1] = Resources.Load<Material>("Materials/Red");
            //mats[2] = Resources.Load<Material>("Materials/idleTimer");
            //this.GetComponent<HandleTextFile>().QuizFunction = GameObject.Find("QuizPanel").transform.Find("Panel").gameObject;

            //SetQuiz1(true);
        }
    }
}
