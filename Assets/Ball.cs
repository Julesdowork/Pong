using UnityEngine;

public class Ball : MonoBehaviour
{
    Paddle leftPaddle;
    Paddle rightPaddle;

    float speed;
    Vector3 originalPos;
    Rigidbody2D rigidBody;
    ScoreKeeper scoreKeeper;
    AudioSource audioSource;
    GameManager gameManager;

	// Use this for initialization
	void Start()
    {
        print("Ball speed: " + speed);
        originalPos = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        leftPaddle = gameManager.paddles[0];
        rightPaddle = gameManager.paddles[1];

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioSource = GetComponent<AudioSource>();
        Invoke("Serve", 3f);
	}

    void OnTriggerEnter2D(Collider2D other)
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            audioSource.Play();
            float hitAngleFactor = (transform.position.y - other.transform.position.y) / other.collider.bounds.size.y;
            if (hitAngleFactor == 0)
            {
                hitAngleFactor = Random.Range(-0.5f, 0.5f);
            }
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
        //speed = Random.Range(9f, 15f);
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

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
