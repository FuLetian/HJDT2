using UnityEngine;
using System.Collections;

public class EnemyBomb : MonoBehaviour {

	public GameObject explosionGO;
	public GameObject surfaceGO;
	// Use this for initialization
	void Start () {
	
		surfaceGO.SetActive (true);
		explosionGO.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter(Collision collisionInfo){
		surfaceGO.SetActive(false);
		explosionGO.SetActive(true);
		
		Destroy(gameObject,0.3f);
	}
}
