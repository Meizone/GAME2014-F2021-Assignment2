/*
Nathan Nguyen
101268067

12/12/2021
Enemy Controllers Developped through Labs


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Player Detection")] 
    public LOS enemyLOS;

    [Header("Movement")] 
    public float runForce;
    public float walkForce;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;

    [Header("Animation")] 
    public Animator animatorController;
    private Rigidbody2D rigidbody;
    


    [Header("Damage Handling")]
    [SerializeField] private GameObject floatingText;
    [SerializeField] private Transform floatingSpawnPoint;
    [SerializeField] private int Health = 2;
    [SerializeField] private bool HitLagAfterDamage = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enemyLOS = GetComponent<LOS>();
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAhead();
        LookInFront();


        if (!HasLOS())
        {
            animatorController.Play("Run");
            MoveEnemy(walkForce);
        }
        else
        {
            MoveEnemy(runForce);
        }
        
    }

    public void EnemyDamage()
    {
        
        Health -= 1;
        Instantiate(floatingText, this.transform.position, Quaternion.identity);

        if(Health == 0)
        {
            FindObjectOfType<GameManager>().SetGold(50);
            FindObjectOfType<GameManager>().EnemyKilled();
            Destroy(gameObject);
        }

        // Disable Input Ability while applying Knockback
        HitLagAfterDamage = true;
        Vector2 direction = FindObjectOfType<CharacterController>().transform.position - transform.position;
        direction = direction.normalized * 2;
        direction.y = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(-direction, ForceMode2D.Impulse);
        StartCoroutine(HitLag());
    }




    private bool HasLOS()
    {
        if (enemyLOS.colliderList.Count > 0)
        {
            // Case 1 enemy polygonCollider2D collides with player and player is at the top of the list
            if ((enemyLOS.collidesWith.gameObject.CompareTag("Player")) &&
                (enemyLOS.colliderList[0].gameObject.CompareTag("Player")))
            {
                return true;
            }
            // Case 2 player is in the Collider List and we can draw ray to the player
            else
            {
                foreach (var collider in enemyLOS.colliderList)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        var hit = Physics2D.Raycast(lookInFrontPoint.position, Vector3.Normalize(collider.transform.position - lookInFrontPoint.position), 5.0f, enemyLOS.contactFilter.layerMask);
                        
                        if((hit) && (hit.collider.gameObject.CompareTag("Player")))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }


    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, groundLayerMask);
        isGroundAhead = (hit) ? true : false;
    }

    private void LookInFront()
    {
        var hit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, wallLayerMask);
        if (hit)
        {
            Flip();
        }
    }

    private void MoveEnemy(float speed)
    {
        if(!HitLagAfterDamage) 
        {
            if (isGroundAhead)
            {
                rigidbody.AddForce(Vector2.left * speed * transform.localScale.x);
                rigidbody.velocity *= 0.90f;
            }
            else
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    // EVENTS

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }




    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(col.transform);
        }
    }


    private IEnumerator HitLag()
    {
        yield return new WaitForSeconds(0.2f);
        HitLagAfterDamage = false;
    }

    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.DrawLine(transform.position, lookInFrontPoint.position);
    }
}
