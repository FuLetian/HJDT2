using UnityEngine;
using System.Collections;

public class Hostage1 : MonoBehaviour {

	public GameObject idleGO;
	public GameObject releasedGO;

	public bool isRun;
	public float speed = -0.1f;
	// Use this for initialization
	void Start () {
	
		idleGO.SetActive (true);
		releasedGO.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(isRun){
			Vector3 v = transform.rigidbody.velocity;
			v.x += speed;
			transform.rigidbody.velocity = v;
		}
	}

	public void OnCollisionEnter(Collision collisionInfo){
		
		if(collisionInfo.gameObject.tag == "Player"){
			idleGO.SetActive (false);
			releasedGO.SetActive (true);

			if(collisionInfo.gameObject.transform.position.x < transform.position.x){
				speed *= -1;
				Vector3 ls = transform.localScale;
				ls.x *= -1;
				transform.localScale = ls;
			}
		}
		
	}

	public void RoleRun(){
		isRun = true;
	}

	public void AnimationEnd(){
		Destroy (gameObject);
	}

	public void StopRun(){
		isRun = false;
	}
}
