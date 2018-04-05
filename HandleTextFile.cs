using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleTextFile : MonoBehaviour
{
    public TextAsset[] QuizDatabase = new TextAsset[5];
    public TextAsset[] SolutionDatabase = new TextAsset[5];

    // public TextAsset QuizDatabase;
    //public TextAsset SolutionDatabase;

   // public string[,] values;
    //public int[,] solutions;


    public string[] values1;
    public string[] values2;
    public string[] values3;
    public string[] values4;
    public string[] values5;

    public int[] solutions1;
    public int[] solutions2;
    public int[] solutions3;
    public int[] solutions4;
    public int[] solutions5;
    public int index;
    public int solindex1;
    public int solindex2;
    public int solindex3;
    public int solindex4;
    public int solindex5;


    int RandomIndex;

    public string RandomQuizString;
    public int solution1;
    public int solution2;

    public GameObject QuizFunction;


    // public int index;

    //static void writestring()
    //{
    //    string Path = "Assets/Resources/Text.txt";
    //    StreamWriter writer = new StreamWriter(Path);

    //    writer.WriteLine("Test");
    //    writer.WriteLine("asdf");
    //    writer.WriteLine("Teqwerst");

    //    writer.WriteLine("Tehhst");
    //    writer.WriteLine("Tem,m,.m,.st");
    //    writer.WriteLine("Tefgfghst");

    //    writer.Close();

    //    //AssetDatabase.ImportAsset(Path);
    //    //TextAsset asset = Resources.Load<TextAsset>("test");

    //    Debug.Log("test");
    //}

    // Use this for initialization
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

        //values = new string[5, 20];
        //solutions = new int[5, 40];

        index = 0;
        solindex1 = 0;
        solindex2 = 0;
        solindex3 = 0;
        solindex4 = 0;
        solindex5 = 0;


        for (int i = 0; i < 5; i++)
        {
            QuizDatabase[i] = Resources.Load<TextAsset>("Text/Quiz" + (i+1).ToString());
            SolutionDatabase[i] = Resources.Load<TextAsset>("Text/Solutions" + (i+1).ToString());
        }
        //QuizDatabase = Resources.Load<TextAsset>("Text/Quiz");
        // SolutionDatabase = Resources.Load<TextAsset>("Text/Solutions");
        readtext();

    }

    void Update()
    {

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



        // StringReader sr = new StringReader(QuizDatabase.text);
        //StringReader sol = new StringReader(SolutionDatabase.text);


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
    }

    public void SetQuiz2()
    {

        switch(BrainManager.instance.Level)
        {
            case 1:
                RandomIndex = Random.Range(0, values1.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.

                RandomQuizString = values1[RandomIndex];

                solution1 = solutions1[RandomIndex * 2];
                solution2 = solutions1[RandomIndex * 2 + 1];
                break;
            case 2:
                RandomIndex = Random.Range(0, values2.Length);   

                RandomQuizString = values2[RandomIndex];

                solution1 = solutions2[RandomIndex * 2];
                solution2 = solutions2[RandomIndex * 2 + 1];
                break;
            case 3:
                RandomIndex = Random.Range(0, values3.Length);   

                RandomQuizString = values3[RandomIndex];

                solution1 = solutions3[RandomIndex * 2];
                solution2 = solutions3[RandomIndex * 2 + 1];
                break;
            case 4:
                RandomIndex = Random.Range(0, values4.Length);   

                RandomQuizString = values4[RandomIndex];

                solution1 = solutions4[RandomIndex * 2];
                solution2 = solutions4[RandomIndex * 2 + 1];
                break;
            case 5:
                RandomIndex = Random.Range(0, values5.Length);   

                RandomQuizString = values5[RandomIndex];

                solution1 = solutions5[RandomIndex * 2];
                solution2 = solutions5[RandomIndex * 2 + 1];
                break;
        }
        if(BrainManager.instance.Level ==1)
        {
            RandomIndex = Random.Range(0, values1.Length);   

            RandomQuizString = values1[RandomIndex];
            
            solution1 = solutions1[RandomIndex * 2];
            solution2 = solutions1[RandomIndex * 2 + 1];

        }
        Debug.Log("랜덤 인덱스 :" + RandomIndex + "문제 :" + RandomQuizString + "답안 :" + solution1 + ", " + solution2);

        QuizFunction.GetComponent<QuizText>().SetQuiz3(RandomQuizString, solution1, solution2);
    }

}
