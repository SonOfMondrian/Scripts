using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrainManager : MonoBehaviour
{

    public static BrainManager instance = null; //싱글턴



    public bool IsLeft, IsRight;        //왼쪽 오른쪽 박스 체크
    public bool IsProgress;
    public bool LookGameStart;
    public bool GameOver;
    public bool RetryCheck;
    public bool OpenDoor;
    public bool AfterCheck;             //비활성 되었있는 Player를 찾기 위해 1초후에 찾기 위한 체크변수
    public int CountOfLevel;
    public int Level;                    //현재 스테이지 레벨;
    public int Score;

    public float AfterCheckTime;        //Player를찾기위한 시간 변수

    public GameObject Player;           //Player오브젝트
    public GameObject LeftBox, RightBox;
    public GameObject Timer;
    public GameObject Panel;
    public GameObject Fade;
    public GameObject GameOver_HighScore;
    public GameObject WindHowlSound;

    public GameObject ScoreText;
    public Material[] mats;

    public int CorrectNum;
    public string HighScore;

    public Animator TimerAnimation;
    public Animator DoorAnimation;
    public Animator FloorAnimation;
    public Animator GameStartAnimation;

    public TextMesh FloorText;

    void Awake()
    {

        instance = this;
        mats = new Material[3];

        mats[0] = Resources.Load<Material>("Materials/Blue");
        mats[1] = Resources.Load<Material>("Materials/Red");
        mats[2] = Resources.Load<Material>("Materials/idleTimer");

        Panel = GameObject.Find("QuizPanel").transform.Find("Panel").gameObject;
        LeftBox = GameObject.Find("Left");
        RightBox = GameObject.Find("Right");
        Timer = GameObject.Find("Timer");
        ScoreText = GameObject.Find("Score").transform.Find("ScoreText").gameObject;
        FloorText = GameObject.Find("pCylinder1").transform.Find("Text").GetComponent<TextMesh>();
        GameOver_HighScore = GameObject.Find("HighScore").transform.Find("score").gameObject;
        WindHowlSound = GameObject.Find("WindSound");

        TimerAnimation = Timer.GetComponent<Animator>();
        DoorAnimation = GameObject.Find("liftdoor_lambert").GetComponent<Animator>();
        FloorAnimation = GameObject.Find("liftdoor_floor").GetComponent<Animator>();
        GameStartAnimation = GameObject.Find("MainPanel").GetComponent<Animator>();


        mats[0] = Resources.Load<Material>("Materials/Blue");
        mats[1] = Resources.Load<Material>("Materials/Red");
        mats[2] = Resources.Load<Material>("Materials/idleTimer");
        this.GetComponent<HandleTextFile>().QuizFunction = GameObject.Find("QuizPanel").transform.Find("Panel").gameObject;

        //this.GetComponent<HandleTextFile>().GetHightScore();




        
    }
    void OnLevelWasLoaded(int level)
    {
       // if(level ==0)
            //this.GetComponent<HandleTextFile>().GetHightScore();
    }
    void Start()
    {
        AfterCheckTime = 0;
        Score = 0;
        Level = 1;

        //HighScore = "0";
        CountOfLevel = 0;
        GameOver = false;
        RetryCheck = false;
        LookGameStart = false;
        OpenDoor = false;
        AfterCheck = false;
        IsLeft = false;
        IsRight = false;
        IsProgress = false;


        ScoreText.GetComponent<TextMesh>().text = HighScore;
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

        if (Input.GetKey(KeyCode.L))
            Cursor.lockState = CursorLockMode.None;


        if (Input.GetKeyDown(KeyCode.Keypad2))
            Time.timeScale = 1.6f;
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Time.timeScale = 1.0f;
        if (Input.GetKeyDown(KeyCode.Keypad5))
            Time.timeScale = 5.0f;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 150, 50), "커서 고정 활성화"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Press this button to confine the Cursor within the screen
        if (GUI.Button(new Rect(160, 0, 150, 50), "비활성화 : 'L'키"))
        {
        }
    }
    //public void OnLoadedScene()     //게임씬으로 이동하면 각종 오브젝트들을 초기화 해주는 함수(Initialize오브젝트에서 호출된다)(OnLevelWasLoaded함수가 Awake함수보다 빨리 호출되어 초기화가 어렵다)
    //{

    //}

    void FirstOpenDoor()
    {
        Debug.Log("문 열림!");
        DoorAnimation.SetBool("OpenDoor", true);
    }

    void FindPlayer()       //게임씬으로 이동후 1초후 함수 호출
    {
        Player = GameObject.Find("Player");             //일단 시뮬레이터 오브젝트 찾기
        if (Player == null)                             // 근데 없으면 
            Player = GameObject.Find("CenterEyeAnchor");//오큘러스 오브젝트를 찾기

        Fade = Player.transform.Find("FadePanel(Clone)").transform.Find("Fade").gameObject;
    }

    public void SetQuiz1()      //문제 출제 최초 출발 함수 (Start함수에서 함수호출하면서 일단 최초로 시작됨)
    {
        if (SceneManager.GetActiveScene().name != "Game") return;       //조기리턴, 버그로 메뉴인 상태에서 함수 호출을 막기위한 미연의 방지

        RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //새로운 문제를 위해 두 상자를 꺼준다
        LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        Timer.transform.Find("polySurface1").GetComponent<Renderer>().material = mats[2];   //새로운 문제를 위해 타이머 색을 없앤다

        TimerAnimation.SetBool("SetQuiz", true);    //타이머 애니메이션 시작
        IsProgress = true;

        // if(first == false)     //씬 이동후 최초 호출이 아니면
        Player.GetComponent<Raycast>().hittime = 0.0f;

        IsLeft = false;
        IsRight = false;

        this.GetComponent<HandleTextFile>().SetQuiz2();     //파일입출력을 위해 호출
    }
    public void LookedLeft()    //왼쪽 상자를 보면
    {
        if (IsLeft == false)
        {
            RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            LeftBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Timer.transform.Find("polySurface1").GetComponent<Renderer>().material = mats[1];

            SoundManager.instance.PlayButtonSelect();

            IsLeft = true;
            IsRight = false;
        }
    }

    public void LookedRight()   //오른쪽 상자를 보면
    {
        if (IsRight == false)
        {
            LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            RightBox.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Timer.transform.Find("polySurface1").GetComponent<Renderer>().material = mats[0];

            SoundManager.instance.PlayButtonSelect();
            IsLeft = false;
            IsRight = true;
        }
    }

    public void CheckQuiz()     //선택된 상자를 채점하는 함수 (Timer 오브젝트의 CallCheckQuiz 스크립트에서 호출한다)
    {
        bool IsCorrect = false;
        IsProgress = false;
        TimerAnimation.SetBool("SetQuiz", false);
        if (IsLeft)
        {
            if (LeftBox.transform.Find("LeftText").GetComponent<TextMesh>().text == CorrectNum.ToString())
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
        else if (IsRight)
        {
            if (RightBox.transform.Find("RightText").GetComponent<TextMesh>().text == CorrectNum.ToString())
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
        else if (!IsLeft && !IsRight)    //둘다 꺼져 있으면 오답처리
        {
            //Debug.Log("오답입니다");
            IsCorrect = false;
        }

        if (IsCorrect)
        {
            SoundManager.instance.Correct();
            Panel.GetComponent<QuizText>().Correct();
            CountOfLevel++;
            Score++;

            if (CountOfLevel < 10)
                Invoke("SetQuiz1", 1.8f);
            else if (CountOfLevel == 10)
            {
                Invoke("CloseDoorForNextLevel", 1.8f);

            }
        }
        else if (!IsCorrect)
        {
            SoundManager.instance.Incorrect();
            //HighScore = Score.ToString();
           
            Panel.GetComponent<QuizText>().Incorrect();
            DoorAnimation.SetBool("CloseDoor", true);
            FloorAnimation.SetBool("OpenFloor", true);
            GameOver_HighScore.GetComponent<TextMesh>().text = Score.ToString();
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();

            if (int.Parse(HighScore) < Score)
                this.GetComponent<HandleTextFile>().SetHighScore(Score.ToString());

            
            WindHowlSound.GetComponentInChildren<AudioSource>().Play();
            SoundManager.instance.DoorClose();
            IsProgress = false;
            GameOver = true;
        }
    }

    public void CloseDoorForNextLevel()
    {
        DoorAnimation.SetBool("CloseDoor", true);
        SoundManager.instance.DoorClose();
        IsProgress = false;

    }

    public void NextLevel()     //DoorStruct.cs의 NextLevelFromManager로 부터 불려짐
    {

        InitialPanel();



        DoorAnimation.SetBool("OpenDoor", true);
        SoundManager.instance.DoorOpen();
    }

    public void InitialPanel()
    {
        CountOfLevel = 0;
        Panel.transform.Find("Text").GetComponent<TextMesh>().text = "Wait";
        LeftBox.transform.Find("LeftText").GetComponent<TextMesh>().text = "";
        RightBox.transform.Find("RightText").GetComponent<TextMesh>().text = "";
        LeftBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        RightBox.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    public void LookGameStartObject(RaycastHit hit)    //메인씬의 게임시작 오브젝트 쳐다봤을시 씬 이동
    {
        hit.collider.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        GameStartAnimation.SetBool("GameStart_Fall", true);
        SoundManager.instance.StartUp();
        Invoke("FirstFloorUp", 5.5f);
    }

    public void FirstFloorUp()// 최초 게임시작시  pCylinder1오브젝트가 B1F에서 1F로 바뀌기 위한 함수
    {
        int curlevel = BrainManager.instance.Level;
        FloorText.text = curlevel.ToString() + "F";

    }
    public void Retry()
    {
        if (!RetryCheck)
        {
            RetryCheck = true;
            //Debug.Log("retry");
            Fade.GetComponent<Animator>().SetBool("FadeOut", true);
            Invoke("GameScene", 1.5f);
        }
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void IsOpenDoor()
    {
        SetQuiz1();
    }
}
