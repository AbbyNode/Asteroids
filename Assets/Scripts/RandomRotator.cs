using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {
	// Public vars
	public float tumble = 5;

	// Private vars
	private Rigidbody2D rb;

	// Use this for initialization
	void Start() {
		rb = this.GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-0.5f, 0.5f) * tumble;
	}
}
