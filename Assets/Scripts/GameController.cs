using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;

	public Vector2 spawnValue;
	public int hazardCount;
	public float hazardSpeed;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public TextMesh scoreText;
	public TextMesh gameOverText;

	private int score;
	public bool gameOver;

	private int livesLeft;

	// Use this for initialization
	void Start() {
		gameOver = false;
		score = 0;
		StartCoroutine(SpawnWaves());
		livesLeft = 3;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true) {
			if (gameOver) {
				gameOverText.text = "Game Over";
				break;
			}
			for (int i = 0; i < hazardCount; i++) {
				Vector2 velocity = new Vector2(hazardSpeed, Random.Range(0, hazardSpeed));
				if (Random.value > 0.5) {
					velocity.x = -hazardSpeed;
				}
				Vector2 spawnPosition = new Vector2(spawnValue.x, Random.Range(-spawnValue.y, spawnValue.y));
				Quaternion spawnRotation = Quaternion.identity;
				int index = Random.Range(0, hazards.Length - 1);
				GameObject hazardInst = Instantiate(hazards[index], spawnPosition, spawnRotation);
				hazardInst.GetComponent<Rigidbody2D>().velocity = velocity;
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	public void AddScore(int scoreToAdd) {
		score += scoreToAdd;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = score.ToString() + "\n";

		for (int i = 1; i <= livesLeft; i++) {
			scoreText.text += "A";
		}
	}

	public void loseLife() {
		livesLeft--;
		UpdateScore();
		if (livesLeft <= 0) {
			GameOver();
		}
	}

	public void GameOver() {
		gameOver = true;
	}
}
