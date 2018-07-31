using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] PlayerPaddle leftPaddle, rightPaddle;

    Vector3 originalPos;
    bool isServed = false;
    Rigidbody2D rigidBody;
    GameController gameController;

	// Use this for initialization
	void Start()
    {
        originalPos = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        //gameController = GameObject.FindGameObjectWithTag("GameController")
        //    .GetComponent<GameController>();
        Invoke("Serve", 3f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Right Collider")
        {
            gameController.AddScore("left");
        }
        else if (other.name == "Left Collider")
        {
            gameController.AddScore("right");
        }
        isServed = false;
        rigidBody.velocity = Vector2.zero;
        ResetPositions();
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
        float randomForceY = Random.Range(-1f, 1f);
        isServed = true;
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
