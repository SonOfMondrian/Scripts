using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleTextFile : MonoBehaviour
{
    public TextAsset[] QuizDatabase = new TextAsset[5];
    public TextAsset[] SolutionDatabase = new TextAsset[5];

    public TextAsset HighScore;



     string[] values1;
     string[] values2;
     string[] values3;
     string[] values4;
     string[] values5;

     bool[] check1;
     bool[] check2;
     bool[] check3;
     bool[] check4;
     bool[] check5;

     int[] solutions1;
     int[] solutions2;
     int[] solutions3;
     int[] solutions4;
     int[] solutions5;
    public int index;
    public int solindex1;
    public int solindex2;
    public int solindex3;
    public int solindex4;
    public int solindex5;

    public string scoreval;

    int RandomIndex;

    public string RandomQuizString;
    public int solution1;
    public int solution2;

    public GameObject QuizFunction;
    public StringReader sr;
    StreamWriter writer;

    void Awake()
    {
        
    }
    void Start()
    {
        QuizDatabase = new TextAsset[5];
        SolutionDatabase = new TextAsset[5];

        values1 = new string[20];
        values2 = new string[20];
        values3 = new string[20];
        values4 = new string[20];
        values5 = new string[20];

        solutions1 = new int[40];
        solutions2 = new int[40];
        solutions3 = new int[40];
        solutions4 = new int[40];
        solutions5 = new int[40];

        check1 = new bool[20];
        check2 = new bool[20];
        check3 = new bool[20];
        check4 = new bool[20];
        check5 = new bool[20];



        index = 0;
        solindex1 = 0;
        solindex2 = 0;
        solindex3 = 0;
        solindex4 = 0;
        solindex5 = 0;

        HighScore = Resources.Load<TextAsset>("Text/HighScore");

        for (int i = 0; i < 5; i++)
        {
            QuizDatabase[i] = Resources.Load<TextAsset>("Text/Quiz" + (i + 1).ToString());
            SolutionDatabase[i] = Resources.Load<TextAsset>("Text/Solutions" + (i + 1).ToString());
        }
        readtext();
        GetHightScore();
    }

    public void GetHightScore()
    {
        
        sr = new StringReader(HighScore.text);

        string Quizsources1 = sr.ReadLine();

        if (sr == null)
            return;

        scoreval = Quizsources1;
        sr.Close();
        BrainManager.instance.HighScore = scoreval;
        BrainManager.instance.ScoreText.GetComponent<TextMesh>().text = scoreval;
        Debug.Log("텍스트파일에서 가져온 최고점수 : "+scoreval);
    }

    public void SetHighScore(string highscore)
    {
        //string path = "Assets/Resources/Text/HighScore";

        //writer = new StreamWriter(path, true);


        //File.WriteAllText("Assets/Resources/Text/HighScore", "asdf");
        //File.WriteAllText()
        //writer.Write(highscore);
        //writer.Close();
        //---------------------------------------

        //StreamWriter swriter;

        
        //if (!File.Exists("Assets/Resources/Text/HighScore.txt"))
        //    swriter = File.CreateText("Assets/Resources/Text/HighScore.txt");
        //else
        //    swriter = new StreamWriter("Assets/Resources/Text/HighScore.txt");

        File.WriteAllText("Assets/Resources/Text/HighScore.txt", highscore);    //중요!!! 이거 딱 한줄만 쓰면 해당 파일에 덮어씌어줌 심플,간단,꿀팁
        Debug.Log("입력 수행");
        //BrainManager.instance.ScoreText.GetComponent<TextMesh>().text = highscore;
        //swriter.Write(highscore.ToString());
        //swriter.Close();

    }

    public void readtext()
    {
        StringReader[] sr = new StringReader[5];
        StringReader[] sol = new StringReader[5];

        for (int i = 0; i < 5; i++)
        {
            sr[i] = new StringReader(QuizDatabase[i].text);
            sol[i] = new StringReader(SolutionDatabase[i].text);

        }
        while (true)
        {

            string Quizsources1 = sr[0].ReadLine();
            string Quizsources2 = sr[1].ReadLine();
            string Quizsources3 = sr[2].ReadLine();
            string Quizsources4 = sr[3].ReadLine();
            string Quizsources5 = sr[4].ReadLine();

            string line1 = sol[0].ReadLine();
            string line2 = sol[1].ReadLine();
            string line3 = sol[2].ReadLine();
            string line4 = sol[3].ReadLine();
            string line5 = sol[4].ReadLine();


            if (Quizsources5 == null)
                break;

            values1[index] = Quizsources1;
            values2[index] = Quizsources2;
            values3[index] = Quizsources3;
            values4[index] = Quizsources4;
            values5[index] = Quizsources5;

            string[] temp1 = line1.Split(' ');
            string[] temp2 = line2.Split(' ');
            string[] temp3 = line3.Split(' ');
            string[] temp4 = line4.Split(' ');
            string[] temp5 = line5.Split(' ');

            for (int i = 0; i < temp1.Length; i++)
            {
                solutions1[solindex1] = int.Parse(temp1[i]);
                solindex1++;
            }
            for (int i = 0; i < temp2.Length; i++)
            {
                solutions2[solindex2] = int.Parse(temp2[i]);
                solindex2++;
            }
            for (int i = 0; i < temp3.Length; i++)
            {
                solutions3[solindex3] = int.Parse(temp3[i]);
                solindex3++;
            }
            for (int i = 0; i < temp4.Length; i++)
            {
                solutions4[solindex4] = int.Parse(temp4[i]);
                solindex4++;
            }
            for (int i = 0; i < temp5.Length; i++)
            {
                solutions5[solindex5] = int.Parse(temp5[i]);
                solindex5++;
            }
            index++;
        }
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].Close();
            sol[i].Close();
        }

    }


    public void SetQuiz2()
    {
        if(BrainManager.instance.Level == 1)
        {
            RandomIndex = Random.Range(0, values1.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.

            if (check1[RandomIndex] == true)
            {
                SetQuiz2();
                return;
            }
            else if(check1[RandomIndex] == false)
            {
                check1[RandomIndex] = true;
                RandomQuizString = values1[RandomIndex];

                solution1 = solutions1[RandomIndex * 2];
                solution2 = solutions1[RandomIndex * 2 + 1];
            }

        }
        else if (BrainManager.instance.Level == 2)
        {
            RandomIndex = Random.Range(0, values2.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.

            if (check2[RandomIndex] == true)
            {
                SetQuiz2();
                return;
            }
            else if (check2[RandomIndex] == false)
            {
                check2[RandomIndex] = true;
                RandomQuizString = values2[RandomIndex];

                solution1 = solutions2[RandomIndex * 2];
                solution2 = solutions2[RandomIndex * 2 + 1];
            }

        }
        else if (BrainManager.instance.Level == 3)
        {
            RandomIndex = Random.Range(0, values3.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.

            if (check3[RandomIndex] == true)
            {
                SetQuiz2();
                return;
            }
            else if (check3[RandomIndex] == false)
            {
                check3[RandomIndex] = true;
                RandomQuizString = values3[RandomIndex];

                solution1 = solutions3[RandomIndex * 2];
                solution2 = solutions3[RandomIndex * 2 + 1];
            }

        }
        else if (BrainManager.instance.Level == 4)
        {
            RandomIndex = Random.Range(0, values4.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.

            if (check4[RandomIndex] == true)
            {
                SetQuiz2();
                return;
            }
            else if (check4[RandomIndex] == false)
            {
                check4[RandomIndex] = true;
                RandomQuizString = values4[RandomIndex];

                solution1 = solutions4[RandomIndex * 2];
                solution2 = solutions4[RandomIndex * 2 + 1];
            }

        }
        else if (BrainManager.instance.Level == 5)
        {
            RandomIndex = Random.Range(0, values5.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.

            if (check5[RandomIndex] == true)
            {
                SetQuiz2();
                return;
            }
            else if (check5[RandomIndex] == false)
            {
                check5[RandomIndex] = true;
                RandomQuizString = values5[RandomIndex];

                solution1 = solutions5[RandomIndex * 2];
                solution2 = solutions5[RandomIndex * 2 + 1];
            }

        }
        
        Debug.Log("랜덤 인덱스 :" + RandomIndex + "문제 :" + RandomQuizString + "답안 :" + solution1 + ", " + solution2);

        QuizFunction.GetComponent<QuizText>().SetQuiz3(RandomQuizString, solution1, solution2);
    }

}
