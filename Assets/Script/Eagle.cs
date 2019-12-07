using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    float moveSpeed = 2f;
    bool moveUp = true;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y > -2f)
        {
            moveUp = false;
        }
        else if (transform.position.y < -4f)
        {
            moveUp = true;
        }

        if (moveUp)
        {   //Move to up
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {   //Move to down
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }

        //Debug.Log(transform.position.y);
    }
}
