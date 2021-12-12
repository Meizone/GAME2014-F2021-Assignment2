/*
Nathan Nguyen
101268067

12/12/2021

Character Controller


*/

using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {



    [Header("Movement")]
    [SerializeField] public Joystick joystick;

    [Header("Forces")]
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float knockback = 1f;
    [SerializeField] float KBTime = 1f;

    [Header("GroundCheck")]
    [SerializeField] ContactFilter2D groundCheckFilter;
    [SerializeField] private bool isGrounded = false;


    [Header("Combat")]
    [SerializeField] bool HitLagAfterDamage = false;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;

    private Animator animatorController;
    private Rigidbody2D rigidbody;
    private int Direction = 1;
    private Collider2D collider;


    // Use this for initialization
    void Start ()
    {
        animatorController = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update ()
    {

        groundCheck();
        //Get Horizontal Movement from User
        //float inputX = Input.GetAxis("Horizontal");
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;


        // Check if the Player was just Hit by an enemy
        if(!HitLagAfterDamage)
            rigidbody.velocity = new Vector2(inputX * m_speed, rigidbody.velocity.y);


        // Pass Air Velocity to Animator
        animatorController.SetFloat("AirSpeedY", rigidbody.velocity.y);
        // Check if Grounded or not
        animatorController.SetBool("Grounded", isGrounded);



        //Direction Swapping
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Direction = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Direction = -1;
        }

            
        //Jump
        if (inputY >= .5f && isGrounded)
        {
            animatorController.SetTrigger("Jump");
            isGrounded = false;
            animatorController.SetBool("Grounded", isGrounded);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, m_jumpForce);
        }

        //Run
        else if (Mathf.Abs(inputX) > 0.01f)
        {
            animatorController.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            animatorController.SetInteger("AnimState", 0);
        }
    }


    public void groundCheck()
    {
        isGrounded = false; // Assume not grounded until otherwise proven.

        RaycastHit2D[] hits = new RaycastHit2D[1];
        collider.Cast(Vector2.down, groundCheckFilter, hits, 0.1f);

        if (hits[0]) // If there was, in fact, a hit.
        {
            isGrounded = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        // Handling Damage Via Collision
        if(col.gameObject.tag == "Enemy")
        {
            // Disable Input Ability while applying Knockback
            HitLagAfterDamage = true;
            Vector2 direction = col.transform.position - transform.position;
            direction = direction.normalized * knockback;
            direction.y = 0;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(-direction, ForceMode2D.Impulse);
            animatorController.SetTrigger("Hurt");
            StartCoroutine(HitLag());
            FindObjectOfType<GameManager>().DamageTaken();
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Death")
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        if(col.gameObject.tag == "Coin")
        {
            FindObjectOfType<GameManager>().SetGold(100);
            Destroy(col.gameObject);
        }
    }

    // Lag the Player Input
    private IEnumerator HitLag()
    {
        yield return new WaitForSeconds(KBTime);
        HitLagAfterDamage = false;
    }


    public void Attack()
    {

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animatorController.SetTrigger("Attack" + 1);

        // Detect Enemies in Range of Attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage 
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().EnemyDamage();
        }
    }


}
