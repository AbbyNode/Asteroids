using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;
	public float turnSpeed = 5f;

	public Transform lazerSpawn;
	public GameObject lazer;
	public float fireDelta = 0.5f;
	public float lazerSpeed = 4;

	private float nextFire = 0.5f;
	private float myTime = 0.5f;

	private Rigidbody2D rBody;
	private new Collider2D collider;
	GameController gameController;

	void Start() {
		rBody = this.GetComponent<Rigidbody2D>();
		collider = this.GetComponent<Collider2D>();

		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	// http://gamecodeschool.com/unity/building-asteroids-arcade-game-in-unity/

	void Update() {
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		rBody.transform.Rotate(0, 0, -moveHorizontal * turnSpeed);

		rBody.AddForce(rBody.transform.up * speed * moveVertical);


		myTime += Time.deltaTime;

		if (Input.GetButton("Fire1") && myTime > nextFire) {
			nextFire = myTime + fireDelta;

			GameObject lazerInst = Instantiate(lazer, lazerSpawn.position, lazerSpawn.rotation);
			Rigidbody2D lazerRBody = lazerInst.GetComponent<Rigidbody2D>();
			lazerRBody.velocity = lazerRBody.transform.up * lazerSpeed;
			Physics2D.IgnoreCollision(collider, lazerInst.GetComponent<Collider2D>());

			nextFire -= myTime;
			myTime = 0.0f;
		}

		if (gameController.gameOver) {
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Asteroid")) {
			Destroy(collision.gameObject);
			gameController.loseLife();
			rBody.position = new Vector2(0, 0);
			rBody.velocity = new Vector2(0, 0);
			rBody.angularVelocity = 0;
			rBody.rotation = 0;
		}
	}
}
