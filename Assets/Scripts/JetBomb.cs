using UnityEngine;
using System.Collections;

public class JetBomb : MonoBehaviour {

	public GameObject bombGO;
	public GameObject explosionGO;

	// Use this for initialization
	void Start () {
	
		bombGO.SetActive (true);
		explosionGO.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDestory(){
		Destroy (gameObject);
	}

	public void OnCollisionEnter(Collision collisionInfo){

		bombGO.SetActive (false);
		explosionGO.SetActive (true);
	}
}
