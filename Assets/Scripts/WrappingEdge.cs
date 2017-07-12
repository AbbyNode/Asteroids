using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingEdge : MonoBehaviour {
	public bool wrapOnX;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			float xPos = collision.transform.position.x;
			float yPos = collision.transform.position.y;
			if (wrapOnX) {
				xPos = xPos * -0.9f;
			} else {
				yPos = yPos * -0.9f;
			}
			collision.transform.position = new Vector2(xPos, yPos);
		}
	}
}
