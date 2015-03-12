using UnityEngine;
using System.Collections;

public class CarWheelMove : MonoBehaviour {

	public GameObject car;
	public float speed = 20.0f;

	private bool isStop = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(isStop == false){
			float z = speed * Time.deltaTime;
			transform.Rotate (new Vector3(0,0,z));
		}

		//gameObject.transform.pa
	}

	public void stop(){
		isStop = true;
	}
	public void star(){
		isStop = false;
	}
}
