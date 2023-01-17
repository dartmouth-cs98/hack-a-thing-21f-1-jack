using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float move_force = 10f;

    [SerializeField]
    private float jump_force = 11f;

    [SerializeField]
    private float tackle_force = 20f; 

    private float movement_x = 22f;

    private Rigidbody2D body; 

    private SpriteRenderer s_r;

    private Animator anim; 

    private string WALK_ANIMATION = "Walk";

    private string TACKLE_ANIMATION = "Tackle";

    private bool is_tackle = false;

    private bool grounded = true;

    private string GROUND_TAG = "Ground";

    private float timeRem;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        s_r = GetComponent<SpriteRenderer>();

    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAnimate();
        PlayerJump();
    }

    private void FixedUpdate()
    {
    }

    void PlayerMove() 
    {
        movement_x = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movement_x, 0f, 0f) * Time.deltaTime * move_force;
    }

    void PlayerAnimate() 
    {
        print(timeRem);
        print(is_tackle);

        if (Input.GetKey("j") && is_tackle == false)
        {
            is_tackle = true;
            timeRem = 100; 
        }
            
        if (movement_x > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            s_r.flipX = false;
            timeRem -= 1;

            if (is_tackle && timeRem > 0)
            {
                anim.SetBool(TACKLE_ANIMATION, true);
            }
            else 
            {
                is_tackle = false;
                anim.SetBool(TACKLE_ANIMATION, false);
            }
        }
        else if (movement_x < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            s_r.flipX = true;

            timeRem -= 1;

            if (is_tackle && timeRem > 0)
            {
                anim.SetBool(TACKLE_ANIMATION, true);

            }
            else 
            {
                is_tackle = false;
                anim.SetBool(TACKLE_ANIMATION, false);
  
            }
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
            
    }

    void PlayerJump() 
    {
        if(Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            body.AddForce(new Vector2(0f, jump_force), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(GROUND_TAG))
        {
            grounded = true;
        }
    }
}
