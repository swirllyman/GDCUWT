using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum checkState {open, close, spin, idle}

public class CheckPoint : MonoBehaviour {

	checkState currentState = checkState.idle;
	Animation anim;
    Button saveBtn;
    Button loadBtn;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
        if (saveBtn == null)  saveBtn = GameObject.Find("SaveBtn").GetComponent<Button>();
        if (loadBtn == null) loadBtn = GameObject.Find("LoadBtn").GetComponent<Button>();
        saveBtn.GetComponent<Image>().enabled = false;
        loadBtn.GetComponent<Image>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		switch(currentState)
		{
		case checkState.open:
			anim.Play("Open");
			break;
		case checkState.close:
			anim.Play("Close");
			break;
		case checkState.spin:
			anim.Play ("Spin");
			break;
		case checkState.idle:
			anim.Play("Idle");
			break;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
		{
			currentState = checkState.open;
            StartCoroutine(StartSpin ());
		}
	}

	void OnTriggerExit(Collider c)
	{
		if(c.tag == "Player")
		{

            //saveBtn.GetComponentInChildren<Text>().enabled = false;

            saveBtn.GetComponentInChildren<Text>().text = "";
            saveBtn.GetComponent<Image>().enabled = false;
            saveBtn.onClick.RemoveListener(SaveGame);

            loadBtn.GetComponentInChildren<Text>().text = "";
            loadBtn.GetComponent<Image>().enabled = false;
            loadBtn.onClick.RemoveListener(LoadGame);

            currentState = checkState.close;

			StartCoroutine (StopSpin());

		}
	}

	IEnumerator StartSpin()
	{
		yield return new WaitForSeconds(anim["Open"].length);
		currentState = checkState.spin;
        //saveBtn.GetComponentInChildren<Text>().enabled = true;

        saveBtn.GetComponentInChildren<Text>().text = "Save Game";
        saveBtn.GetComponent<Image>().enabled = true;
        saveBtn.onClick.AddListener(SaveGame);

        loadBtn.GetComponentInChildren<Text>().text = "Load Game";
        loadBtn.GetComponent<Image>().enabled = true;
        loadBtn.onClick.AddListener(LoadGame);
    }


	IEnumerator StopSpin()
	{
		yield return new WaitForSeconds(anim["Open"].length);
		currentState = checkState.idle;
    }



    public void SaveGame()
    {
        Debug.Log("Game has been saved");
        PlayerPrefs.SetInt("Level1", 1);
    }

    public void LoadGame()
    {
        Debug.Log("Let's Load the game");
        int level1completed =  PlayerPrefs.GetInt("Level1");
    }
}
