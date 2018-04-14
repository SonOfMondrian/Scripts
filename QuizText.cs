using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizText : MonoBehaviour
{
    public string QuizString;
    public int[] QuizSolutions;
    public TextMesh QuizPanelText;
    public TextMesh SolText1;
    public TextMesh SolText2;
    public GameObject RightSol, LeftSol;
    public int[] sol;
    //public GameObject asdf;

    // Use this for initialization
    void Start()
    {
        QuizPanelText = this.transform.Find("Text").GetComponent<TextMesh>();
        SolText1 = GameObject.Find("LeftText").GetComponent<TextMesh>();
        SolText2 = GameObject.Find("RightText").GetComponent<TextMesh>();

        LeftSol = BrainManager.instance.LeftBox;
        RightSol = BrainManager.instance.RightBox;

        QuizSolutions = new int[2];

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            BrainManager.instance.SetQuiz1();
    }

    public void SetQuiz3(string strquiz, int sol1, int sol2)
    {
        BrainManager.instance.CorrectNum = sol1;
        int random = Random.Range(0, 2);
        
        if (strquiz.Length == 3)
            QuizPanelText.fontSize = 158;
        else if (strquiz.Length == 4)
            QuizPanelText.fontSize = 158;
        else if (strquiz.Length == 5)
            QuizPanelText.fontSize = 151;
        else if (strquiz.Length == 6)
            QuizPanelText.fontSize = 145;
        else if (strquiz.Length == 7)
            QuizPanelText.fontSize = 130;
        else if (strquiz.Length == 8)
            QuizPanelText.fontSize = 119;
        else if (strquiz.Length == 9)
            QuizPanelText.fontSize = 106;
        else if (strquiz.Length == 10)
            QuizPanelText.fontSize = 100;
        else if (strquiz.Length == 11)
            QuizPanelText.fontSize = 92;
        else if (strquiz.Length == 12)
            QuizPanelText.fontSize = 88;
        else if (strquiz.Length == 13)
            QuizPanelText.fontSize = 80;


    switch (sol1.ToString().Length)
        {
            case 1:
                SolText1.fontSize = 271;
                break;
            case 2:
                SolText1.fontSize = 253;
                break;
            case 3:
                SolText1.fontSize = 217;
                break;
            case 4:
                SolText1.fontSize = 176;
                break;
            case 5:
                SolText1.fontSize = 151;
                break;
        }

        switch (sol2.ToString().Length)
        {
            case 1:
                SolText2.fontSize = 271;
                break;
            case 2:
                SolText2.fontSize = 253;
                break;
            case 3:
                SolText2.fontSize = 217;
                break;
            case 4:
                SolText2.fontSize = 176;
                break;
            case 5:
                SolText2.fontSize = 151;
                break;
        }



        QuizPanelText.text = strquiz + "?";
        if (random == 0)
        {
            SolText1.text = sol1.ToString();
            SolText2.text = sol2.ToString();
        }
        else if (random == 1)
        {
            SolText1.text = sol2.ToString();
            SolText2.text = sol1.ToString();
        }
    }

    public void Correct()
    {
        QuizPanelText.fontSize = 131;
        QuizPanelText.text = "PERFECT";
    }

    public void Incorrect()
    {
        QuizPanelText.fontSize = 113;
        QuizPanelText.text = "GOOD BYE";
    }
}
