using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opposum : Enemy
{
    float moveSpeed = 3f;
    bool moveRight = true;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x > 110f)
        {
            moveRight = false;
            transform.localScale = new Vector3(1, 1);
        }
        else if (transform.position.x < 103f)
        {
            moveRight = true;
            transform.localScale = new Vector3(-1, 1);
        }

        if (moveRight)
        {   //Move to right
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {   //Move to left
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }

        //Debug.Log(transform.position.x);
    }
}
