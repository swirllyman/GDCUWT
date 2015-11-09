using UnityEngine;
using System.Collections;

public enum checkState {open, close, spin, idle}

public class CheckPoint : MonoBehaviour {

	public checkState currentState = checkState.idle;
	Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
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
			currentState = checkState.close;
			StartCoroutine (StopSpin());
		}
	}

	IEnumerator StartSpin()
	{
		yield return new WaitForSeconds(anim["Open"].length);
		currentState = checkState.spin;
	}


	IEnumerator StopSpin()
	{
		yield return new WaitForSeconds(anim["Open"].length);
		currentState = checkState.idle;
	}
}
