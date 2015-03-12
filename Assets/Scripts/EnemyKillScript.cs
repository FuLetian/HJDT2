using UnityEngine;
using System.Collections;

public class EnemyKillScript : MonoBehaviour {

	public GameObject enemy;
	private EnemyAnimationController enemyAniationController;

	// Use this for initialization
	void Start () {
	
		enemyAniationController = enemy.GetComponent<EnemyAnimationController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AnimationEnd(){

		enemyAniationController.killAnimationEnd ();
	}
}
