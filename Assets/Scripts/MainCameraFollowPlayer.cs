using UnityEngine;
using System.Collections;

public class MainCameraFollowPlayer : MonoBehaviour {

	public GameObject player;
	public GameObject positionFlag1;

	public bool isConsumerActive;

	public float smoothing;

	public bool catchPostionFlag1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//this.transform.position = player.transform.position + fromPosition;
		if(isConsumerActive){
			Vector3 p = transform.position;
			if(player.transform.position.x > transform.position.x){
				p.x = player.transform.position.x;
				transform.position = p;
			}

			if(catchPostionFlag1 == false && transform.position.x-positionFlag1.transform.position.x > 0){

				Vector3 selfP = transform.position;
				selfP.y += smoothing *Time.deltaTime;
				transform.position = selfP;

				if(transform.position.y - positionFlag1.transform.position.y > 0){
					catchPostionFlag1 = true;
				}
			}
		}
	}
}
