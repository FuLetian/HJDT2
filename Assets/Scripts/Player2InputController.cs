using UnityEngine;
using System.Collections;

public enum PlayerBodyAnimation
{
	StandIdle,
	SquatIdle,
	Jump,
	JumpShoot,
	StandWalk,
	SquatWalk,
	StandShoot,
	SquatShoot,
	StandWalkingShoot,
	SquatWalkingShoot,
	IdleThrowBomb,
	AimSkyWalk,
	AimSkyIdle
}

public enum PlayerDir
{
	Left,
	Right
}

public class Player2InputController : MonoBehaviour {

	//up body
	public GameObject upBodyStandIdle;
	public GameObject upBodySquatIdle;
	public GameObject upBodyJump;
	public GameObject upBodyStandWalk;
	public GameObject upBodyShoot;
	public GameObject upBodyIdleThrowBomb;
	public GameObject upBodyAimSky;

	//down body
	public GameObject downBodyStandIdle;
	public GameObject downBodySquatIdle;
	public GameObject downBodySquatWalk;
	public GameObject downBodyJump;
	public GameObject downBodyStandWalk;

	//
	public GameObject buttlePrefab;
	public GameObject bombPrefab;

	//params
	public float standWalkingSpeed = 15;
	public float squatWalingSpeed = 5;
	public float jumpUpSpeed = 5;

	//state
	private PlayerDir playerDir = PlayerDir.Left;
	private PlayerBodyAnimation bodyState = PlayerBodyAnimation.StandIdle;

	public bool shouldSquat;
	public bool shouldWalk;
	public bool shouldJumpUp;
	public bool isOnGround;
	public bool shouldShoot;
	public bool canShoot;
	public bool isThrowBomb;
	public bool shouldThrowBomb;
	public bool isAimSky;

	//store
	private float lastShootTimer;
	private bool isReached = true;
	private bool isDie;
	public int lifeValue = 5;

	// Use this for initialization
	void Start () {
	
		upBodyStandIdle.SetActive (true);
		upBodySquatIdle.SetActive (false);
		upBodyJump.SetActive (false);
		upBodyStandWalk.SetActive (false);
		upBodyShoot.SetActive (false);
		upBodyIdleThrowBomb.SetActive (false);
		downBodyStandIdle.SetActive (true);
		downBodySquatIdle.SetActive (false);
		downBodySquatWalk.SetActive (false);
		downBodyJump.SetActive (false);
		downBodyStandWalk.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		if (isReached == false)
						return;
	
		if (isThrowBomb == true)
						return;

		float h = Input.GetAxis ("Horizontal");

		//player direction
		this.PlayerDirection (h);

		//should walk
		shouldWalk = Mathf.Abs (h) > 0.5;

		//squat
		if(Input.GetKeyDown(KeyCode.S)){
			shouldSquat = true;
		}else if(Input.GetKeyUp(KeyCode.S)){
			shouldSquat = false;
		}

		//aim sky
		if(Input.GetKeyDown(KeyCode.W)){
			isAimSky = true;
		}else if(Input.GetKeyUp(KeyCode.W)){
			isAimSky = false;
		}
		//jump up
		RaycastHit hit;
		isOnGround = Physics.Raycast(transform.position+Vector3.up*0.2f,Vector3.down,out hit,0.5f,LayerMask.GetMask("Ground"));
		if(isOnGround == true){
			if(Input.GetKeyDown(KeyCode.Space)){
				shouldJumpUp = true;
			}else{
				shouldJumpUp = false;
			}
		}else{
			shouldJumpUp = false;
		}

		//shoot
		if(canShoot == false){
			lastShootTimer += Time.deltaTime;
			if(lastShootTimer>0.2f){
				canShoot = true;
				lastShootTimer = 0;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.F)){
			shouldShoot = true;
		}else if(Input.GetKeyUp(KeyCode.F)){
			shouldShoot = false;
		}

		//throw bomb
		shouldThrowBomb = false;
		if (isThrowBomb == false) {
			if(Input.GetKeyDown(KeyCode.G)){
				shouldThrowBomb = true;
				isThrowBomb = true;
			}
		}

		//instantiate buttle
		if(canShoot && shouldShoot){
			canShoot = false;
			lastShootTimer = 0.0f;
			shouldShoot = true;
			Vector3 postion;

			if(isAimSky){
				postion = transform.position + new Vector3(0.0f,1.0f,0.0f);
			}else{
				if(transform.localScale.x > 0.0f){
					postion = transform.position + new Vector3(-0.5f,0.4f,0.0f);
				}else{
					postion = transform.position + new Vector3(0.5f,0.4f,0.0f);
				}
			}
			GameObject o = (GameObject)Instantiate(this.buttlePrefab,postion,transform.rotation);
			if(isAimSky){
				o.GetComponent<PlayerShootController>().dir = ButtleDirection.Up;
			}else{
				o.GetComponent<PlayerShootController>().dir = transform.localScale.x<0 ? ButtleDirection.Right:ButtleDirection.Left
					;
			}
		}

		//instantiate bomb
		if(shouldThrowBomb){
			isThrowBomb = false;
			Vector3 p = this.transform.position;
			p.y +=0.8f;
			if(this.transform.localScale.x == -1){
				p.x +=0.2f;
			}else{

			}
			GameObject bombGO = (GameObject) Instantiate(bombPrefab,p,transform.rotation);
			bombGO.SetActive (true);
			bombGO.rigidbody.velocity = new Vector3(-2.5f*this.transform.localScale.x + this.rigidbody.velocity.x,2.5f,0);
		}

		setPlayerAnimation (countPlayerAnimation());

		//move
		if(shouldJumpUp){
			Vector3 v = rigidbody.velocity;
			rigidbody.velocity = new Vector3(v.x,jumpUpSpeed,v.z);
		}

		if(shouldWalk && isOnGround){
			Vector3 v = rigidbody.velocity;
			rigidbody.velocity = new Vector3(h*(shouldSquat?squatWalingSpeed:standWalkingSpeed),v.y,v.z);
		}
	}

	private PlayerBodyAnimation countPlayerAnimation(){
		PlayerBodyAnimation state = PlayerBodyAnimation.StandIdle;
		if (shouldShoot){
			state = PlayerBodyAnimation.StandShoot;
		}

		if(shouldThrowBomb){
			state = PlayerBodyAnimation.IdleThrowBomb;
		}else if(isOnGround == false){
			if(shouldShoot)
				state = PlayerBodyAnimation.JumpShoot;
			else
				state = PlayerBodyAnimation.Jump;
		}else{

			if(isAimSky && shouldWalk){
				state = PlayerBodyAnimation.AimSkyWalk;
				return state;
			}

			if(isAimSky && !shouldWalk){
				state = PlayerBodyAnimation.AimSkyIdle;
				return state;
			}

			if (shouldWalk && shouldSquat) {
				if(shouldShoot)
					state = PlayerBodyAnimation.SquatWalkingShoot;
				else
					state = PlayerBodyAnimation.SquatWalk;
			}else if(shouldWalk && !shouldSquat){
				if(shouldShoot)
					state = PlayerBodyAnimation.StandWalkingShoot;
				else
					state = PlayerBodyAnimation.StandWalk;

			}else if(!shouldWalk && shouldSquat){
				if(shouldShoot)
					state = PlayerBodyAnimation.SquatShoot;
				else
					state = PlayerBodyAnimation.SquatIdle;
			}
		}

		return state;
	}

	private void PlayerDirection(float h){
		PlayerDir tempPlayerDir = this.playerDir;
		if (h > 0.5)
			tempPlayerDir = PlayerDir.Right;
		else if (h < -0.5)
			tempPlayerDir = PlayerDir.Left;
		if(tempPlayerDir != this.playerDir){
			transform.localScale = new Vector3(tempPlayerDir == PlayerDir.Left?1:-1,1,1);
			this.playerDir = tempPlayerDir;
		}
	}

	private void setPlayerAnimation(PlayerBodyAnimation newState){
		if(this.bodyState != newState){
			this.getUpBodyGameObjectByState (bodyState).SetActive (false);
			this.getDownBodyGameObjectByState(bodyState).SetActive(false);
			
			bodyState = newState;
			
			this.getUpBodyGameObjectByState (newState).SetActive (true);
			this.getDownBodyGameObjectByState(newState).SetActive(true);
		}
	}

	private GameObject getUpBodyGameObjectByState(PlayerBodyAnimation state){
		GameObject o = null;
		switch (state) {
		case PlayerBodyAnimation.StandIdle:
			o = upBodyStandIdle;
			break;
		case PlayerBodyAnimation.Jump:
			o = upBodyJump;
			break;
		case PlayerBodyAnimation.StandShoot:
			o = upBodyShoot;
			break;
		case PlayerBodyAnimation.SquatShoot:
			o = upBodyShoot;
			break;
		case PlayerBodyAnimation.SquatWalk:
			o = upBodySquatIdle;
			break;
		case PlayerBodyAnimation.SquatIdle:
			o = upBodySquatIdle;
			break;
		case PlayerBodyAnimation.StandWalk:
			o = upBodyStandWalk;
			break;
		case PlayerBodyAnimation.JumpShoot:
			o = upBodyShoot;
			break;
		case PlayerBodyAnimation.StandWalkingShoot:
			o = upBodyShoot;
			break;
		case PlayerBodyAnimation.SquatWalkingShoot:
			o = upBodyShoot;
			break;
		case PlayerBodyAnimation.IdleThrowBomb:
			o = upBodyIdleThrowBomb;
			break;
		case PlayerBodyAnimation.AimSkyIdle:
			o = upBodyAimSky;
			break;
		case PlayerBodyAnimation.AimSkyWalk:
			o = upBodyAimSky;
			break;
		default:
			break;
		}

		return o;
	}

	private GameObject getDownBodyGameObjectByState(PlayerBodyAnimation state){
		GameObject o = null;
		switch (state) {
		case PlayerBodyAnimation.StandIdle:
			o = downBodyStandIdle;
			break;
		case PlayerBodyAnimation.Jump:
			o = downBodyJump;
			break;
		case PlayerBodyAnimation.StandShoot:
			o = downBodyStandIdle;
			break;
		case PlayerBodyAnimation.SquatShoot:
			o = downBodySquatIdle;
			break;
		case PlayerBodyAnimation.SquatWalk:
			o = downBodySquatWalk;
			break;
		case PlayerBodyAnimation.SquatIdle:
			o = downBodySquatIdle;
			break;
		case PlayerBodyAnimation.StandWalk:
			o = downBodyStandWalk;
			break;
		case PlayerBodyAnimation.JumpShoot:
			o = downBodyJump;
			break;
		case PlayerBodyAnimation.StandWalkingShoot:
			o = downBodyStandWalk;
			break;
		case PlayerBodyAnimation.SquatWalkingShoot:
			o = downBodySquatIdle;
			break;
		case PlayerBodyAnimation.IdleThrowBomb:
			o = downBodyStandIdle;
			break;
		case PlayerBodyAnimation.AimSkyIdle:
			o = downBodyStandIdle;
			break;
		case PlayerBodyAnimation.AimSkyWalk:
			o = downBodyStandWalk;
			break;
		default:
			break;
		}

		return o;
	}

	public void jumpOutFromCar(){
		isReached = true;
		gameObject.SetActive (true);
		transform.rigidbody.velocity = new Vector3(-2,2,0);
		Invoke ("turnRight", 1.0f);
	}

	public void turnRight(){
		transform.localScale = new Vector3 (-1,1,1);
	}

	public void OnCollisionEnter(Collision collisionInfo){

		if(collisionInfo.gameObject.tag == "enemy" || collisionInfo.gameObject.tag == "bomb"){
			isDie = true;
		}

	}

	public void SetDie(){
		lifeValue--;

	}

	public bool getIsDie(){
		return this.isDie;
	}
}
