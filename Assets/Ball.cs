using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Paddle leftPaddle, rightPaddle;

    Vector3 originalPos;
    Rigidbody2D rigidBody;
    ScoreManager scoreKeeper;

	// Use this for initialization
	void Start()
    {
        originalPos = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        scoreKeeper = FindObjectOfType<ScoreManager>();
        Invoke("Serve", 3f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (scoreKeeper.isGameOver)
        {
            // Go to End Screen
        }
        else
        {
            if (other.name == "Right Collider")
            {
                scoreKeeper.AddScore("left");
            }
            else if (other.name == "Left Collider")
            {
                scoreKeeper.AddScore("right");
            }
            rigidBody.velocity = Vector2.zero;
            ResetPositions();
            Invoke("Serve", 3f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            float hitAngleFactor = (transform.position.y - other.transform.position.y) / other.collider.bounds.size.y;
            CollideWithPaddle(other.gameObject, hitAngleFactor);
        }
    }

    void Serve()
    {
        float randomDirection = Random.Range(0, 2);
        if (randomDirection == 0)
            rigidBody.velocity = Vector2.right * speed;
        else
            rigidBody.velocity = Vector2.left * speed;
    }

    void CollideWithPaddle(GameObject paddle, float factor)
    {
        Vector2 direction;
        float rawNewSpeed = speed + Random.Range(-2f, 2f);
        speed = Mathf.Clamp(rawNewSpeed, 7f, 13f);
        if (paddle == leftPaddle.gameObject)
        {
            direction = new Vector2(1f, factor).normalized;
            rigidBody.velocity = direction * speed;
        }
        else if (paddle == rightPaddle.gameObject)
        {
            direction = new Vector2(-1f, factor).normalized;
            rigidBody.velocity = direction * speed;
        }
    }

    void ResetPositions()
    {
        leftPaddle.ResetPaddlePosition();
        rightPaddle.ResetPaddlePosition();
        transform.position = originalPos;
    }
}
