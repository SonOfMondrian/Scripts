using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrainManager:MonoBehaviour
{

    public static BrainManager instance = null; //싱글턴

    public bool IsLeft, IsRight;        //왼쪽 오른쪽 박스 체크
    public bool IsProgress;
    public bool LookGameStart;
    public bool OpenDoor;
    public bool AfterCheck;             //비활성 되었있는 Player를 찾기 위해 1초후에 찾기 위한 체크변수
    public int Level;                    //현재 스테이지 레벨;

    public float AfterCheckTime;        //Player를찾기위한 시간 변수

    public GameObject Player;           //Player오브젝트
    public GameObject LeftBox, RightBox;
    public GameObject Timer;
    public GameObject Panel;
    public Material[] mats;

    public int CorrectNum;
    public int Score;

    public Animator TimerAnimation;
    public Animator DoorAnimation;
    public Animator GameStartAnimation;

    private AudioSource source;
    private AudioClip clip;

    void Awake()
    {

        instance = this;
        mats = new Material[3];

        mats[0] = Resources.Load<Material>("Materials/Blue");
        mats[1] = Resources.Load<Material>("Materials/Red");
        mats[2] = Resources.Load<Material>("Materials/idleTimer");
    }
    void Start()
    {
        AfterCheckTime = 0;
        Score = 0;
        Level = 1;
        LookGameStart = false;
        OpenDoor = false;
        AfterCheck = false;
        IsLeft = false;
        IsRight = false;
        IsProgress = false;
    }

    void Update()
    {
        if(!AfterCheck && SceneManager.GetActiveScene().name == "Game")
            AfterCheckTime += Time.deltaTime;

        if(!AfterCheck && AfterCheckTime >= 1.0f)
        {
            AfterCheck = true;
            FindPlayer();
        }
    }
    public void OnLoadedScene()     //게임씬으로 이동하면 각종 오브젝트들을 초기화 해주는 함수(Initialize오브젝트에서 호출된다)(OnLevelWasLoaded함수가 Awake함수보다 빨리 호출되어 초기화가 어렵다)
    {
        Panel = GameObject.Find("QuizPanel").transform.Find("Panel").gameObject;
        LeftBox = GameObject.Find("Left");
        RightBox = GameObject.Find("Right");
        Timer = GameObject.Find("Timer");

        TimerAnimation = Timer.GetComponent<Animator>();
        DoorAnimation = GameObject.Find("liftdoor_lambert").GetComponent<Animator>();
        GameStartAnimation = GameObject.Find("MainPanel").GetComponent<Animator>();


        mats[0] = Resources.Load<Material>("Materials/Blue");
        mats[1] = Resources.Load<Material>("Materials/Red");
        mats[2] = Resources.Load<Material>("Materials/idleTimer");
        this.GetComponent<HandleTextFile>().QuizFunction = GameObject.Find("QuizPanel").transform.Find("Panel").gameObject;
        
    }
    void FirstOpenDoor()
    {
        Debug.Log("문 열림!");
        DoorAnimation.SetBool("OpenDoor",true);

    }

    void FindPlayer()       //게임씬으로 이동후 1초후 함수 호출
    {
        Player = GameObject.Find("Player");             //일단 시뮬레이터 오브젝트 찾기
        if(Player == null)                             // 근데 없으면 
            Player = GameObject.Find("CenterEyeAnchor");//오큘러스 오브젝트를 찾기
    }

    public void SetQuiz1()      //문제 출제 최초 출발 함수 (Start함수에서 함수호출하면서 일단 최초로 시작됨)
    {
        if(SceneManager.GetActiveScene().name != "Game") return;       //조기리턴, 버그로 메뉴인 상태에서 함수 호출을 막기위한 미연의 방지

        RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //새로운 문제를 위해 두 상자를 꺼준다
        LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        Timer.transform.Find("body").GetComponent<Renderer>().material = mats[2];   //새로운 문제를 위해 타이머 색을 없앤다
        Timer.transform.Find("Arrow").GetComponent<Renderer>().material = mats[2];

        TimerAnimation.SetBool("SetQuiz",true);    //타이머 애니메이션 시작
        IsProgress = true;

       // if(first == false)     //씬 이동후 최초 호출이 아니면
            Player.GetComponent<Raycast>().hittime = 0.0f;

        IsLeft = false;
        IsRight = false;

        this.GetComponent<HandleTextFile>().SetQuiz2();     //파일입출력을 위해 호출
    }
    public void LookedLeft()    //왼쪽 상자를 보면
    {
        if(IsLeft == false)
        {
            RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            LeftBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Timer.transform.Find("body").GetComponent<Renderer>().material = mats[1];
            Timer.transform.Find("Arrow").GetComponent<Renderer>().material = mats[1];

            SoundManager.instance.PlayButton();

            IsLeft = true;
            IsRight = false;

        }
    }

    public void LookedRight()   //오른쪽 상자를 보면
    {
        if(IsRight == false)
        {
            LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            RightBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Timer.transform.Find("body").GetComponent<Renderer>().material = mats[0];
            Timer.transform.Find("Arrow").GetComponent<Renderer>().material = mats[0];

            SoundManager.instance.PlayButton();
            IsLeft = false;
            IsRight = true;
        }
    }

    public void CheckQuiz()     //선택된 상자를 채점하는 함수 (Timer 오브젝트의 CallCheckQuiz 스크립트에서 호출한다)
    {
        bool IsCorrect = false;
        IsProgress = false;
        TimerAnimation.SetBool("SetQuiz",false);
        if(IsLeft)
        {
            if(LeftBox.transform.Find("LeftText").GetComponent<TextMesh>().text == CorrectNum.ToString())
            {
                Debug.Log("정답입니다");
                IsCorrect = true;
            }
            else
            {
                Debug.Log("오답입니다");
                IsCorrect = false;
            }
        }
        else if(IsRight)
        {
            if(RightBox.transform.Find("RightText").GetComponent<TextMesh>().text == CorrectNum.ToString())
            {
                Debug.Log("정답입니다");
                IsCorrect = true;
            }
            else
            {
                Debug.Log("오답입니다");
                IsCorrect = false;
            }
        }
        else if(!IsLeft && !IsRight)    //둘다 꺼져 있으면 오답처리
        {
            Debug.Log("오답입니다");
            IsCorrect = false;
        }

        if(IsCorrect)
        {
            Panel.GetComponent<QuizText>().Correct();
        }
        else if(!IsCorrect)
        {
            Panel.GetComponent<QuizText>().Incorrect();
            DoorAnimation.SetBool("CloseDoor",true);
        }
    }

    public void LookGameStartObject(RaycastHit hit)    //메인씬의 게임시작 오브젝트 쳐다봤을시 씬 이동
    {
        hit.collider.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        GameStartAnimation.SetBool("GameStart_Fall",true);
        Invoke("IsOpenDoor",1.5f);
        SoundManager.instance.PlayButton();

        
    }
    public void IsOpenDoor()
    {
        DoorAnimation.SetBool("OpenDoor",true);
        Invoke("SetQuiz1",1.5f);
        
    }
}
