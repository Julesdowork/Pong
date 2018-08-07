using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isComputer;

    [SerializeField] float yClamp = 4.3f;
    [SerializeField] string inputAxis;
    [SerializeField] string paddleName;

    float speed = 10f;
    Vector2 startingPosition;
    GameObject ball;
    bool controlsEnabled = true;

    void Awake()
    {
        print(paddleName + " speed: " + speed);
        ball = GameObject.Find("Ball");
    }

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
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
        float diffY = ball.transform.position.y - transform.position.y;
        if (diffY > 0)
        {
            movement.y = speed * Mathf.Min(diffY, 1f);
        }
        if (diffY < 0)
        {
            movement.y = -(speed * Mathf.Min(-diffY, 1f));
        }

        transform.position += movement * Time.deltaTime;
        float clampPosY = Mathf.Clamp(transform.position.y, -yClamp, yClamp);
        transform.position = new Vector3(transform.position.x, clampPosY, 0f);
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

    public void ChangeInputAxis()
    {
        inputAxis = "VerticalRight";
    }
}
