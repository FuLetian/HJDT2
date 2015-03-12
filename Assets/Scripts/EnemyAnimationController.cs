using UnityEngine;
using System.Collections;

public enum EnemyAnimation
{
	Idle,
	Walking,
	Kill,
	Die,
	GrenadeDie
}
public class EnemyAnimationController : MonoBehaviour {

	public GameObject enemyIdle;
	public GameObject enemyWalking;
	public GameObject enemyKill;
	public GameObject enemyDie;
	public GameObject enemyGrenadeIdle;

	public GameObject player;
	private int playerMask;
	private Player2InputController playerController;

	public EnemyAnimation currentState = EnemyAnimation.Idle;

	public float animTimeScale = 0.1f;
	public float animChangeCycle = 0;
	public float runSpeed = 1;

	public bool isKillAnimation;
	
	// Use this for initialization
	void Start () {
	
		enemyIdle.SetActive (true);
		enemyWalking.SetActive (false);
		enemyKill.SetActive (false);
		enemyDie.SetActive (false);
		enemyGrenadeIdle.SetActive (false);

		playerController = player.GetComponent<Player2InputController> ();
		playerMask = LayerMask.GetMask("Player");

	}
	
	// Update is called once per frame
	void Update () {

		if(isKillAnimation == true){

		}else if(playerController.getIsDie()){
			AnimationChange(EnemyAnimation.Idle);
		}else if(currentState == EnemyAnimation.Die){

		}else{
			EnemyAnimation newState = currentState;
			if(Physics.Raycast(gameObject.transform.position,-Vector3.right,0.4f,playerMask)){
				newState = EnemyAnimation.Kill;
				isKillAnimation = true;
				playerController.SetDie();
				AnimationChange(newState);
			}else{
				animChangeCycle +=(animTimeScale*Time.deltaTime);
				animChangeCycle %= 360;
				float value = Mathf.Cos (animChangeCycle);
				
				if(value >= 0){
					newState = EnemyAnimation.Idle;
				}else{
					newState = EnemyAnimation.Walking;
				}

				AnimationChange(newState);
				DirectionChangeAndRun();
			}

		}
	}

	void AnimationChange(EnemyAnimation newState){

		if(newState != currentState){
			enemyIdle.SetActive (false);
			enemyWalking.SetActive (false);
			enemyKill.SetActive (false);
			enemyDie.SetActive (false);
			enemyGrenadeIdle.SetActive (false);
			//getGameObjectByState (currentState).SetActive (false);
			getGameObjectByState (newState).SetActive (true);
			
			currentState = newState;
		}
	}

	void DirectionChangeAndRun(){
		if(currentState == EnemyAnimation.Walking){
			Vector3 v = rigidbody.velocity;
			
			if(player.transform.position.x < transform.position.x){
				v.x= 0 - runSpeed;
				transform.localScale = new Vector3(1,1,1);
			}else{
				v.x = runSpeed;
				transform.localScale = new Vector3(-1,1,1);
			}
			rigidbody.velocity = v;
		}
	}

	GameObject getGameObjectByState(EnemyAnimation state){

		GameObject o = null;
		switch (state) {
			case EnemyAnimation.Idle:
				o = enemyIdle;
				break;
			case EnemyAnimation.Walking:
				o = enemyWalking;
				break;
			case EnemyAnimation.Kill:
				o = enemyKill;
				break;
			case EnemyAnimation.Die:
				o = enemyDie;
				break;
			case EnemyAnimation.GrenadeDie:
				o = enemyGrenadeIdle;
				break;
				
			default:
				break;
		}

		return o;
	}

	public void killAnimationEnd(){
		isKillAnimation = false;
	}

	public void OnCollisionEnter(Collision collisionInfo){

		if(collisionInfo.gameObject.tag == "bomb"){
			AnimationChange(EnemyAnimation.Die);
			Destroy(gameObject,1.3f);
		}
	}
	
}
