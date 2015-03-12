using UnityEngine;
using System.Collections;

public enum ButtleDirection
{
	Left,
	Right,
	Up
}
public class PlayerShootController : MonoBehaviour {

	public Sprite buttleExploriongSprite;

	public ButtleDirection dir = ButtleDirection.Left;
	public float speed = 3;
	private SpriteRenderer spriteRenderer;
	private bool didUpdateDir;

	// Use this for initialization
	void Start () {
		//transform.localScale = new Vector3 (0.3f,0.3f,0.3f);
		Destroy (gameObject, 5.0f);
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p = transform.position;
		switch (dir) {
		case ButtleDirection.Right:
			if(didUpdateDir == false)
				transform.localScale = new Vector3(-1,1,1);
			p.x+=speed*Time.deltaTime;
			break;
		case ButtleDirection.Left:
			if(didUpdateDir == false)
				transform.localScale = new Vector3(1,1,1);
			p.x-=speed*Time.deltaTime;
			break;
		case ButtleDirection.Up:
		{
			if(didUpdateDir == false)
				transform.Rotate(0,0,90);
			p.y += speed*Time.deltaTime;
			break;
		}
				default:
						break;
		}
		didUpdateDir = true;
		transform.position = p;
	}

	public void OnCollisionEnter(Collision collisionInfo){
		if(collisionInfo.gameObject.tag == "enemy"){
			Destroy(collisionInfo.gameObject);
		}
		spriteRenderer.sprite = buttleExploriongSprite;
		Destroy (gameObject,0.1f);

	}
}
