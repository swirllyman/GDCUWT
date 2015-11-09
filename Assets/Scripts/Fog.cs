using UnityEngine;
using System.Collections;

public class Fog : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
		GetComponent<Renderer>().material.renderQueue=1;
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null) transform.position = player.transform.position;
	}

    public void FindPlayer(){
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
