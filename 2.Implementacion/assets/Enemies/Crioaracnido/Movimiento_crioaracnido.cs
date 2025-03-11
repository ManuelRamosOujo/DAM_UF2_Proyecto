using UnityEngine;

public class Movimiento_crioaracnido : MonoBehaviour{
    private Rigidbody2D rb;
    private Animator animator;
    private bool frozen = false;
    private float choiceCooldown = 1;
    private float totalCooldown = 1f;
    public float movementSpeed = 4f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
    }

    private void FixedUpdate() {
        if (!frozen){
            ChooseMovement();
        }
    }

    private void Update(){
        
    }

    private void Move(int choice){
        animator.SetBool("Moving",true);

        switch (choice){
            case 7:
                animator.SetFloat("Direction",1);
                rb.velocity = new Vector2(movementSpeed,0);
                break;
            case 8:
                animator.SetFloat("Direction",2);
                rb.velocity = new Vector2(0,movementSpeed);
                break;
            case 9:
                animator.SetFloat("Direction",3);
                rb.velocity = new Vector2(-1 * movementSpeed,0);
                break;
            case 10:
                animator.SetFloat("Direction",0);
                rb.velocity = new Vector2(0,-1 * movementSpeed);
                break;
        }
    }

    private void MoveToPlayer(){
        animator.SetBool("Moving",true);
        Vector2 playerPosition = GameObject.Find("Player").transform.position;
        Vector2 positionDifference = playerPosition - new Vector2(transform.position.x, transform.position.y);

        if (Mathf.Abs(positionDifference.x) > Mathf.Abs(positionDifference.y)){
            switch (positionDifference.x >= 0){
                case true:
                    animator.SetFloat("Direction",1);
                    rb.velocity = new Vector2(movementSpeed,0);
                    break;
                case false:
                    animator.SetFloat("Direction",3);
                    rb.velocity = new Vector2(-1 * movementSpeed,0);
                    break;
            }
        } else {
            switch (positionDifference.y >= 0){
                case true:
                    animator.SetFloat("Direction",2);
                    rb.velocity = new Vector2(0,movementSpeed);
                    break;
                case false:
                    animator.SetFloat("Direction",0);
                    rb.velocity = new Vector2(0,-1 * movementSpeed);
                    break;
            }
        }
    }

    public void SetFrozen(bool option){
        frozen = option;
        if (frozen){
            animator.SetBool("Moving",false);
            rb.velocity = new Vector2(0,0);
        }
    }

    public void ChooseMovement(){
        int choice = Random.Range(0,11);
        
        choiceCooldown += Time.deltaTime;

        if (choiceCooldown >= totalCooldown){
            if (choice > 6){
                Move(choice);
                choiceCooldown = 0.2f;
            } else if(choice <= 
            (GetComponent<Estado_crioaracnido>().GetAnger() ? 1 : 2)) {
                rb.velocity = new Vector2(0,0);
                animator.SetBool("Moving",false);
                choiceCooldown = 0;
            } else{
                MoveToPlayer();
                choiceCooldown = 0;
            }

            if (GetComponent<Estado_crioaracnido>().GetAnger()){
                totalCooldown = Random.Range(0.3f,0.9f);
                movementSpeed = 6f;
            }else{
                totalCooldown = Random.Range(0.5f,1.2f);
            }
        }
    }
}
