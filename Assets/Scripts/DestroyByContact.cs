using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	// Public vars
	public int scoreOnDestroy;

	// Private vars
	GameController gameController;

	private void Start() {
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Asteroid")) {
			gameController.AddScore(scoreOnDestroy);
			Destroy(other.gameObject);
		}
		Destroy(this.gameObject);
	}
}
