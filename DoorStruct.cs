using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStruct : MonoBehaviour {

    public TextMesh FloorPanelText;
	// Use this for initialization
	void Start () {
        FloorPanelText = GameObject.Find("pCylinder1").transform.Find("Text").GetComponent<TextMesh>();

    }


    public void idleOpendoor()
    {
        this.GetComponent<Animator>().SetBool("OpenDoor",false);
    }

    public void idleClosedoor()
    {
        this.GetComponent<Animator>().SetBool("CloseDoor",false);
    }
    public void OpenAndSetQuiz()
    {
        BrainManager.instance.IsOpenDoor();

    }
	public void LevelUp()   //문 오브젝트 애니메이션에서 이벤트호출 (idleClosedoor)
	{
        if (BrainManager.instance.GameOver == true)
            return;

		SoundManager.instance.LevelUp ();
		BrainManager.instance.Level++;
        Invoke("UpFloor", 1.5f);
		Invoke ("NextLevelFromManager", 3.0f);
	}
    public void UpFloor()
    {
        Debug.Log("층수 올라감");
        int curlevel = BrainManager.instance.Level;
        FloorPanelText.text = curlevel.ToString() + "F";
    }
	public void NextLevelFromManager()
	{
		BrainManager.instance.NextLevel ();
	}
}
