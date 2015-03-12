using UnityEngine;
using System.Collections;

public class EnemyThrowBoomAnimation : MonoBehaviour {

	public GameObject parent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AnimationEnd(){
		parent.GetComponent<EnemyThrowBoom> ().ThrowEnd ();
	}
}
