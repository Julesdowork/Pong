using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // radius of ball sprite + half the width of the paddle sprite
    [SerializeField] float xOffset = 0.26f;
    [SerializeField] float speed = 4f;
    [SerializeField] PlayerPaddle leftPaddle, rightPaddle;

    public bool isServed = false;
    Rigidbody2D rigidBody;
    GameController gameController;
    PlayerPaddle server;

	// Use this for initialization
	void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
        AttachBallToServer();
	}
	
	// Update is called once per frame
	void LateUpdate()
    {
        if (!isServed)
        {
            MoveBallWithServer();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isServed)
        {
            Serve();
        }
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
        if (isServed && other.gameObject.GetComponent<Paddle>())
        {
            float hitAngleFactor = (transform.position.y - other.transform.position.y) / other.collider.bounds.size.y;
            CollideWithPaddle(other.gameObject, hitAngleFactor);
        }
    }

    void AttachBallToServer()
    {
        if (server == leftPaddle)
        {
            server = rightPaddle;
        }
        else
        {
            server = leftPaddle;
        }
        float serverPosX = server.transform.position.x;
        float serverPosY = server.transform.position.y;
        transform.position = new Vector2(serverPosX + xOffset, serverPosY);
        xOffset *= -1;
    }

    void MoveBallWithServer()
    {
        float adjustedPosY = server.transform.position.y;
        transform.position = new Vector2(transform.position.x, adjustedPosY);
    }

    public void Serve()
    {
        float randomForceY = Random.Range(-1f, 1f);
        rigidBody.AddRelativeForce(new Vector2(1f, randomForceY) * speed);
        isServed = true;
    }

    void CollideWithPaddle(GameObject paddle, float factor)
    {
        Vector2 direction;
        if (paddle == leftPaddle)
        {
            direction = new Vector2(1f, factor).normalized;
            rigidBody.velocity = direction * speed;
        }
        else if (paddle == rightPaddle)
        {
            direction = new Vector2(-1f, factor).normalized;
            rigidBody.velocity = direction * speed;
        }
    }

    void ResetPositions()
    {
        leftPaddle.ResetPaddlePosition();
        rightPaddle.ResetPaddlePosition();
        AttachBallToServer();
    }
}
