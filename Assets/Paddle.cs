using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isComputer;

    [SerializeField] float speed = 10f;
    [SerializeField] float yClamp = 4.3f;
    [SerializeField] string inputAxis;

    Vector2 startingPosition;
    GameObject ball;

    void Awake()
    {
        ball = GameObject.Find("Ball");
    }

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
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

    void MovePlayer()
    {
        float direction = Input.GetAxisRaw(inputAxis);
        float rawPosY = transform.position.y + (speed * direction * Time.deltaTime);
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    void MoveComputer()
    {
        float rawPosY = Mathf.MoveTowards(transform.position.y, ball.transform.position.y, speed * Time.deltaTime);
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    public void ResetPaddlePosition()
    {
        transform.position = startingPosition;
    }
}
