using UnityEngine;
using System.Collections;

public class MainCameraFollowPlayer : MonoBehaviour {

	public GameObject player;

	public bool isConsumerActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//this.transform.position = player.transform.position + fromPosition;
		if(isConsumerActive){
			Vector3 p = transform.position;
			p.x = player.transform.position.x;
			transform.position = p;
		}
	}
}
