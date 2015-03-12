using UnityEngine;
using System.Collections;

public class Hostage2 : MonoBehaviour {

	public GameObject idleGO;
	public GameObject releasedGO;
	public GameObject playerGO;

	public float speed;

	private bool isRunning;
	private bool isReleased;

	// Use this for initialization
	void Start () {
	
		idleGO.SetActive (true);
		releasedGO.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		if(isReleased == false){

			float distance = Vector3.Distance(gameObject.transform.position,playerGO.transform.position);
			if(Mathf.Abs(distance) < 0.8f){
				idleGO.SetActive(false);
				releasedGO.SetActive(true);
				isReleased = true;
			}

		}
	
		if(isRunning){
			Vector3 p = gameObject.transform.position;
			p.x-=(speed * Time.deltaTime);
			gameObject.transform.position = p;
		}
	}

	public void Run(){
		isRunning = true;
	}

	public void Stop(){
		isRunning = false;
	}

	public void HostageDestory(){
		Destroy (gameObject);
	}
}
