using System.Collections;
using UnityEngine;

public class RecibirDañoTopomba : MonoBehaviour{
    public Animator animator;
    private Movimiento_topomba movimiento_topomba;
    private void Awake() {
        
    }
    void Start(){
        movimiento_topomba = gameObject.GetComponent<Movimiento_topomba>();
    }
    void Update(){
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Bomb"){
            StartCoroutine(ReceiveDamage(other.gameObject));
        }
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
        if (other.gameObject.name == "Bomb"){
            gameObject.GetComponent<Estado_topomba>()
            .ReduceHealth(other.GetComponent<Dañar_bomba>().GetAttackPower() / 3);
        } else {
            gameObject.GetComponent<Estado_topomba>()
            .ReduceHealth(GameObject.Find("Player").GetComponent<Inventory>().GetAttackPower());
        }
        
        movimiento_topomba.SetFrozen(true);
        yield return new WaitForSeconds(0.2f);
        movimiento_topomba.SetFrozen(false);
    }
}
