using System.Collections;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool atacando = false;
    private bool recuperando = false;

    void Start(){
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        if (IsInScreen()){
            if (Mathf.Abs(playerPosition.x - initialPosition.x) <= 1.8f && !atacando){
                StartCoroutine(Attack("x"));
            }

            if (Mathf.Abs(playerPosition.y - initialPosition.y) <= 1.8f && !atacando){
                StartCoroutine(Attack("y"));
            }
        }

        if (Vector2.Distance(transform.position,initialPosition) <= 0.15f && recuperando){    
            rb.velocity = Vector2.zero;
            transform.position = initialPosition;
            atacando = false;
            recuperando = false;
        }
    }

    private IEnumerator Attack(string dir){
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        atacando = true; 
        if (dir == "x"){
            if (playerPosition.y > initialPosition.y){
                rb.velocity = new Vector2(0,8);
            } else {
                rb.velocity = new Vector2(0,-8);
            }
        }

        if (dir == "y"){
            if (playerPosition.x > initialPosition.x){
                rb.velocity = new Vector2(8,0);
            } else {
                rb.velocity = new Vector2(-8,0);
            }
        }

        yield return new WaitForSeconds(1.5f);

        if (!recuperando){
            GoToInitialPosition();
        }
    }

    private void GoToInitialPosition(){
        recuperando = true;
        rb.velocity = ReverseVelocity();
    }

    private Vector2 ReverseVelocity(){
        Vector2 newVelocity;

        if (rb.velocity.x != 0){
            newVelocity.x = -rb.velocity.x * 0.4f;
        } else {
            newVelocity.x = 0;
        }

        if (rb.velocity.y != 0){
            newVelocity.y = -rb.velocity.y * 0.4f;
        } else {
            newVelocity.y = 0;
        }

        return newVelocity;
    }

    private bool IsInScreen(){
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && 
            viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Paredes"){
            GoToInitialPosition();
        }
    }
}

