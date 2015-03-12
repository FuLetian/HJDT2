using UnityEngine;
using System.Collections;

public class CarEnter : MonoBehaviour {

	public GameObject player;
	public Transform targetPos;
	public CarWheelMove[] wheels;
	public int smoothing = 2;

	public GameObject board;

	private bool isReach;
	private bool isDriveAway;
	private Vector3 playerFromCarPosition;
	// Use this for initialization
	void Start () {
		playerFromCarPosition = gameObject.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = Vector3.Lerp(transform.position, targetPos.position, smoothing * Time.deltaTime);

		if(isReach == false && Mathf.Abs(targetPos.position.x-transform.position.x) < 0.3f){
			onReach();
		}

		if(isDriveAway){
			Vector3 v = transform.position;
			v.x+=0.1f;
			transform.position = v;
		}

		if(isReach == false){
			player.transform.position = this.transform.position - playerFromCarPosition;
		}
	}

	void onReach(){
		isReach = true;
		foreach(CarWheelMove wheel in wheels){
			wheel.stop();
		}

		OpenBoard ();
	}

	public void OpenBoard(){
		board.transform.Rotate (new Vector3 (0.0f, 0.0f, 135.0f));
		player.GetComponent<Player2InputController> ().jumpOutFromCar ();

		Invoke ("DriveAway", 2.0f);
	}

	void DriveAway(){
		isDriveAway = true;
		foreach(CarWheelMove wheel in wheels){
			wheel.star();
		}

		Invoke ("DestorySelf", 5.0f);
	}

	void DestorySelf(){
		Destroy (gameObject);
	}
}
