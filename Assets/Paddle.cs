using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float yClamp = 4.3f;

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		if (Input.GetKey(KeyCode.W))
        {
            moveUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDown();
        }
	}

    void moveUp()
    {
        float rawPosY = transform.position.y + speed * Time.deltaTime;
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }

    void moveDown()
    {
        float rawPosY = transform.position.y - speed * Time.deltaTime;
        float clampPosY = Mathf.Clamp(rawPosY, -yClamp, yClamp);

        transform.position = new Vector3(transform.position.x, clampPosY);
    }
}
