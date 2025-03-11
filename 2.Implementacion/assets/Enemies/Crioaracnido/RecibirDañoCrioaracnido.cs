using System.Collections;
using UnityEngine;

public class RecibirDañoCrioaracnido : MonoBehaviour{
    public Animator animator;
    private Rigidbody2D rb;
    private Movimiento movimiento_jugador;
    private Movimiento_crioaracnido movimiento_crioaracnido;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start(){
        movimiento_jugador = GameObject.Find("Player").GetComponent<Movimiento>();
        movimiento_crioaracnido = gameObject.GetComponent<Movimiento_crioaracnido>();
    }
    
    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "MeleeAttack" || other.gameObject.name == "SwordBeam"){
            StartCoroutine(ReceiveDamage(other.gameObject));
        }
        
        if (other.gameObject.name == "Bomb"){
            StartCoroutine(ReceiveDamage(other.gameObject));
        }
    }

    IEnumerator ReceiveDamage(GameObject other){
        animator.SetBool("Damaged",true);
        movimiento_crioaracnido.SetFrozen(true);

        if (other.name == "Bomb"){
            gameObject.GetComponent<Estado_crioaracnido>()
            .ReduceHealth(other.GetComponent<Dañar_bomba>().GetAttackPower());
            Vector2 directionToTarget = gameObject.transform.position - other.transform.position;
            rb.velocity = directionToTarget * 20;
        } else {
            gameObject.GetComponent<Estado_crioaracnido>()
            .ReduceHealth(GameObject.Find("Player").GetComponent<Inventory>().GetAttackPower());
            switch (other.GetComponent<Melee_colision>().GetDirection()){
            case 0:
                rb.velocity = new Vector2(0,-20);
                break;
            case 1:
                rb.velocity = new Vector2(20,0);
                break;
            case 2:
                rb.velocity = new Vector2(0,20);
                break;
            case 3:
                rb.velocity = new Vector2(-20,0);
                break;
            }
        }

        yield return new WaitForSeconds(0.14f);
        rb.velocity = new Vector2(0,0);
        animator.SetBool("Damaged",false);
        movimiento_crioaracnido.SetFrozen(false);
    }
}
