using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleTextFile:MonoBehaviour
{

    public TextAsset QuizDatabase;
    public TextAsset SolutionDatabase;
    public string[] values;
    public int[] solutions;
    public int index;
    public int solindex;
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
        values = new string[5];
        solutions = new int[10];
       index = 0;
        solindex = 0;
        QuizDatabase = Resources.Load<TextAsset>("Text/Quiz");
        SolutionDatabase = Resources.Load<TextAsset>("Text/Solutions");
        readtext();

    }

    void Update()
    {
    }
    public void readtext()
    {
        StringReader sr = new StringReader(QuizDatabase.text);
        StringReader sol = new StringReader(SolutionDatabase.text);


        while(true)
        {
            string Quizsources = sr.ReadLine(); //한줄 읽는다
            string line = sol.ReadLine();
            
            if(Quizsources == null)
            {
                break;
            }

            values[index] = Quizsources;
            string[] temp = line.Split(' ');
            for(int i = 0 ; i < temp.Length ; i++)
            {
                solutions[solindex] = int.Parse(temp[i]);
                solindex++;
            }
            index++;
        }
    }

    public void SetQuiz2()
    {

        RandomIndex = Random.Range(0,values.Length);   //ex) 0,5 넣으면 0~4까지만 나온다.


        RandomQuizString = values[RandomIndex];


        solution1 = solutions[RandomIndex * 2];
        solution2 = solutions[RandomIndex * 2 + 1];

        Debug.Log("랜덤 인덱스 :" + RandomIndex + "문제 :" + RandomQuizString + "답안 :" + solution1 + ", " + solution2);

       QuizFunction.GetComponent<QuizText>().SetQuiz3(RandomQuizString,solution1,solution2);
    }

}
