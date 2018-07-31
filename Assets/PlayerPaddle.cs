using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float yClamp = 4.3f;
    [SerializeField] string inputAxis;

    Vector2 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float direction = Input.GetAxisRaw(inputAxis);
        float rawPosY = transform.position.y + (speed * direction * Time.deltaTime);
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    public void ResetPaddlePosition()
    {
        transform.position = startingPosition;
    }
}
