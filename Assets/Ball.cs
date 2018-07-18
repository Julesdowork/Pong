using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // radius of ball sprite + half the width of the paddle sprite
    [SerializeField] float xOffset = 0.26f;
    [SerializeField] float speed = 4f;
    [SerializeField] Paddle[] paddles;

    bool isServed = false;
    Paddle server;
    Rigidbody2D rigidBody;

	// Use this for initialization
	void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        AttachBallToServer();
	}
	
	// Update is called once per frame
	void Update ()
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void AttachBallToServer()
    {
        paddles = FindObjectsOfType<Paddle>();
        if (server == paddles[0])
        {
            server = paddles[1];
        }
        else
        {
            server = paddles[0];
        }
        float serverPosX = server.transform.position.x;
        float serverPosY = server.transform.position.y;
        transform.position = new Vector3(serverPosX + xOffset, serverPosY);
    }

    void MoveBallWithServer()
    {
        float adjustedPosY = server.transform.position.y;
        transform.position = new Vector3(transform.position.x, adjustedPosY);
    }

    void Serve()
    {
        isServed = true;
        float randomForceY = Random.Range(-1f, 1f);
        rigidBody.AddRelativeForce(new Vector2(1f, randomForceY));
    }
}
