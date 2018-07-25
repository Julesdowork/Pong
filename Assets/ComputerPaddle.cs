using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPaddle : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float yClamp = 4.3f;
    [SerializeField] Ball ball;

    bool serving;
    float timeToWait = 3f;
	
	// Update is called once per frame
	void Update()
    {
        Move();
	}

    void Move()
    {
        float rawPosY = Mathf.MoveTowards(transform.position.y, ball.transform.position.y, speed * Time.deltaTime);
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    IEnumerator Serve()
    {
        yield return new WaitForSeconds(timeToWait);
        ball.Serve();
    }

    public void setIsServing(bool b)
    {
        serving = b;
    }

    bool isServing()
    {
        return serving;
    }
}
