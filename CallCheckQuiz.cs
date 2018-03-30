using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCheckQuiz : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void QuizCheck()     //BrainManager의 CheckQuiz함수를 호출; QuizCheck()함수는  Timer오브젝트의 애니메이션 이벤트에서 호출한다
    {
        BrainManager.instance.CheckQuiz();

    }
}
