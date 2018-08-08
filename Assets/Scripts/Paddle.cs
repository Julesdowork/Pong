using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isComputer;

    [SerializeField] string inputAxis;
    [SerializeField] string paddleName;

    float speed = 10f;              // How quickly a paddle moves
    float yClamp = 4.3f;            // The bounds of a paddle's movement
    Vector2 startingPosition;       // Position paddle will be in before a serve
    GameObject ball;
    bool controlsEnabled = true;    // Can the paddle be moved?

    void Awake()
    {
        ball = GameObject.Find("Ball");
    }

    void Start()
    {
        startingPosition = transform.position;
    }
    
    void FixedUpdate()
    {
        if (controlsEnabled)
        {
            if (isComputer)
            {
                MoveComputer();
            }
            else
            {
                MovePlayer();
            }
        }
    }

    void MovePlayer()
    {
        float direction = Input.GetAxisRaw(inputAxis);
        float rawPosY = transform.position.y + (speed * direction * Time.deltaTime);
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    void MoveComputer()
    {
        Vector3 movement = Vector3.zero;

        // vertical distance between the ball and the paddle
        float diffY = ball.transform.position.y - transform.position.y;
        if (diffY > 0)  // move in +y direction
        {
            movement.y = speed * Mathf.Min(diffY, 1f);  // move by at most a factor of 1
        }
        if (diffY < 0)  // move in -y direction
        {
            movement.y = -(speed * Mathf.Min(-diffY, 1f));
        }

        transform.position += movement * Time.deltaTime;
        float clampPosY = Mathf.Clamp(transform.position.y, -yClamp, yClamp);
        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    public void ResetPaddlePosition()
    {
        transform.position = startingPosition;
    }

    public void SetControls(bool areControlsEnabled)
    {
        controlsEnabled = areControlsEnabled;
    }

    public string GetName()
    {
        return paddleName;
    }

    public void SetName(string newName)
    {
        paddleName = newName;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Player 2 should move using the Up and Down Arrow keys
    public void ChangeInputAxis()
    {
        inputAxis = "VerticalRight";
    }
}
