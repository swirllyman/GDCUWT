using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

    public GameObject player;
    public GameObject fog;
	// Use this for initialization
	void Start () {
        fog = GameObject.Find("Fog");
        Instantiate(player, transform.position, Quaternion.identity);
        if(fog != null) fog.GetComponent<Fog>().FindPlayer();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
