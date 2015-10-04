using UnityEngine;
using System.Collections;

public class RaftCraft : MonoBehaviour {

	public GameObject[] raftPlank;
	public GameObject[] craftPlank;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < raftPlank.Length; i++) {
			if(raftPlank[i] == null) {
				craftPlank[i].gameObject.GetComponent<Renderer>().enabled = true;
			}
		}
	}
}
