using UnityEngine;
using System.Collections;

public class TankFire : MonoBehaviour {

	public float speed;

	public GameObject playerGO;
	private Player2InputController playerScript;

	// Use this for initialization
	void Start () {
	
		playerGO = GameObject.FindGameObjectsWithTag ("Player")[0];
		playerScript = playerGO.GetComponent<Player2InputController> ();
		Invoke ("OnDestory",1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Mathf.Abs(Vector3.Distance(playerGO.transform.position,gameObject.transform.position)) < 1.0f){
			playerScript.SetDie();
			Destroy(gameObject);
		}else{
			Vector3 p = gameObject.transform.position;
			p.x += speed*Time.deltaTime;
			gameObject.transform.position = p;
		}

	}

	void OnDestory(){
		Destroy (gameObject);
	}

	public void OnCollisionEnter(Collision collisionInfo){

		Destroy (gameObject);
	}
}
