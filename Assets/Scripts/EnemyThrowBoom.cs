using UnityEngine;
using System.Collections;

public class EnemyThrowBoom : MonoBehaviour {

	public GameObject idleGO;
	public GameObject throwGO;
	public GameObject bombBirthGO;
	public GameObject bombPrefab;

	private int count;
	private bool onThrowing;
	// Use this for initialization
	void Start () {
	
		idleGO.SetActive (true);
		throwGO.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
		if(onThrowing){

		}else{
			count ++;
			count %= 300;
			if(count == 0){
				idleGO.SetActive(false);
				throwGO.SetActive(true);
				onThrowing = true;
			}
		}
	}

	public void OnCollisionEnter(Collision collisionInfo){

		if(collisionInfo.gameObject.tag == "bomb"){
			Destroy(gameObject);
		}
	}

	public void ThrowEnd(){
		idleGO.SetActive (true);
		throwGO.SetActive (false);
		onThrowing = false;

		GameObject bombGO = (GameObject)Instantiate (bombPrefab, bombBirthGO.transform.position, bombBirthGO.transform.rotation);

		bombGO.SetActive (true);
		bombGO.rigidbody.velocity = new Vector3(-2.5f,2.5f,0);
	}
}
