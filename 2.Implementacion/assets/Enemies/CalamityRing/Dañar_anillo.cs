using UnityEngine;

public class Dañar_anillo : MonoBehaviour{
    private float attackPower = 0.33f;

    void Start(){
    }

    void Update(){
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Player"){
           other.gameObject.GetComponent<HealthUIManager>().LoseHealth(attackPower);
        }
    }

    public float GetAttackPower(){
        return attackPower;
    }
}
