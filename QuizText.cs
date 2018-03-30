using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizText : MonoBehaviour {
    public string QuizString;
    public int[] QuizSolutions;
    public TextMesh QuizPanelText;
    public TextMesh SolText1;
    public TextMesh SolText2;
    public GameObject RightSol, LeftSol;
   public  int[] sol;
    //public GameObject asdf;

    // Use this for initialization
    void Start () {
        QuizPanelText = this.transform.Find("Text").GetComponent<TextMesh>();
        SolText1 = GameObject.Find("LeftText").GetComponent<TextMesh>();
        SolText2 = GameObject.Find("RightText").GetComponent<TextMesh>();
        
        LeftSol = BrainManager.instance.LeftBox;
        RightSol = BrainManager.instance.RightBox;
        
        QuizSolutions = new int[2];

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            BrainManager.instance.SetQuiz1();
    }

    public void SetQuiz3(string strquiz,int sol1,int sol2)
    {
        BrainManager.instance.CorrectNum = sol1;
        //sol = new int[2] { sol1, sol2 };
        int random = Random.Range(0,2);
        Debug.Log(random);

        QuizPanelText.fontSize = 220;
        QuizPanelText.text = strquiz+"?";
        if (random ==0)
        {
            SolText1.text = sol1.ToString();
            SolText2.text = sol2.ToString();
            }
        else if(random==1)
        {

            SolText1.text = sol2.ToString();
            SolText2.text = sol1.ToString();
            }
    }

    public void Correct()
    {
        QuizPanelText.fontSize = 168;
        QuizPanelText.text = "PERFECT";
    }

    public void Incorrect()
    {
        QuizPanelText.fontSize = 150;
        QuizPanelText.text = "GOOD BYE";
    }
}
