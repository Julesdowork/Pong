using UnityEngine;

public class Ball : MonoBehaviour
{
    float speed;                // Changes depending on difficulty mode
    Vector3 originalPos;        // Position before being served
    Rigidbody2D rigidBody;
    ScoreKeeper scoreKeeper;
    AudioSource audioSource;
    Paddle leftPaddle;
    Paddle rightPaddle;
    GameManager gameManager;
    Vector2 currentVelocity;
    
	void Start()
    {
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

        // Reset forces on ball to make it stop moving
        rigidBody.velocity = Vector2.zero;
        ResetPositions();
        Invoke("Serve", 3f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Randomize the sound effect of the ball hitting something
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.Play();

        if (other.collider.CompareTag("Player"))
        {
            // Hits the ball at an angle depending on what end of a paddle
            // the ball makes contact with
            float hitAngleFactor =
                (transform.position.y - other.transform.position.y) / 
                other.collider.bounds.size.y;

            // Give it a random angle when the ball hits a paddle dead center
            if (hitAngleFactor == 0)
                hitAngleFactor = Random.Range(-0.5f, 0.5f);

            CollideWithPaddle(other.gameObject, hitAngleFactor);
        }
    }

    // Serve the ball randomly to the left or right paddle.
    void Serve()
    {
        float randomDirection = Random.Range(0, 2);
        if (randomDirection == 0)
            rigidBody.velocity = Vector2.right * speed;
        else
            rigidBody.velocity = Vector2.left * speed;
    }

    // Reverses the direction the ball moves in and adjusts its angle
    // based on where it made contact with a paddle.
    void CollideWithPaddle(GameObject paddle, float factor)
    {
        Vector2 direction;
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

    public void FreezeBall()
    {
        currentVelocity = rigidBody.velocity;
        rigidBody.velocity = Vector2.zero;
    }

    public void UnfreezeBall()
    {
        rigidBody.velocity = currentVelocity;
    }
}
