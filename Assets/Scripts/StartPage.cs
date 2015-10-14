using UnityEngine;
using System.Collections;

public class StartPage : MonoBehaviour {

	public GUIStyle styler;

	public GUISkin skin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartLevel(string levelName){
		Application.LoadLevel(levelName);
	}

	void OnGUI () {
//		GUI.Box (new Rect(Screen.width / 2 - 100, Screen.height/4, 200, 50), "", styler);
//
//		GUI.skin = skin;
//		if(GUI.Button (new Rect(Screen.height / 2 - 75, Screen.height / 2, 150, 30), "Start")) {
//			Application.LoadLevel ("Practice Zone");
//		}
	}
}
