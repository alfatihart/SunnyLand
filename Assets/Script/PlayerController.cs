using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Start variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D collider;

    //FSM
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;
    private GameObject powerup;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] public int cherries = 0;
    [SerializeField] public int gems = 0;
    [SerializeField] private Text cherryText;
    [SerializeField] private float hurtForce = 10f;

    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource hurt;
    [SerializeField] private int health = 100;
    [SerializeField] private Text healthAmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        powerup = GameObject.Find("Powerups");
        powerup.SetActive(false);
        healthAmount.text = health.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Animator anim = other.GetComponent<Animator>();
        if (other.tag == "Collectable")
        {
            Collectable collectable = other.GetComponent<Collectable>();
            //cherry.Play();
            //anim.SetTrigger("Collected");
            //Destroy(other.gameObject);
            if (collectable.Getting())
            {
                cherries += 1;
            }
            else
            {
                jumpForce = 15f;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                StartCoroutine(ResetPower());
            }
            cherryText.text = cherries.ToString();
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(10);
        jumpForce = 10f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                //Destroy(other.gameObject);
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                HandleHealth();
                hurt.Play();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //move right
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //move left
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
        else if (other.gameObject.tag == "Powerup" && state == State.falling)
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("destroy");
            Jump();
            Debug.Log("tersentuh");
            powerup.SetActive(true);
        }
    }

    private void HandleHealth()
    {
        health -= 10;
        healthAmount.text = health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //Moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        //Jumping
        if (Input.GetButtonDown("Jump") && collider.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (collider.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }
}
